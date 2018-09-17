using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ParadiseExplorer.Domains;
using ParadiseExplorer.Models;

namespace ParadiseExplorer.Services
{
    public class MongoParadiseService : IParadiseService
    {
        private readonly IMongoDatabase _database;

        public MongoParadiseService(IMongoClient client)
        {
            _database = client.GetDatabase("paradise");
        }

        public PagedResult<EdgeNodeDto> GetEntities(int page, int pageSize)
        {
            var skipCount = Math.Max(0, (page - 1) * pageSize);
            var query = _database.GetCollection<EntityDto>("entity").AsQueryable().OrderBy(b => b.Name);
            var total = query.Count();
            var entityDto = query.Skip(skipCount).Take(pageSize).ToList();
            return new PagedResult<EdgeNodeDto>()
            {
                Items = entityDto.Select(e => new EdgeNodeDto()
                {
                    Edge = new EdgeDto(),
                    Node = new NodeDto(e)
                }).ToList(),
                TotalCount = total,
                Page = page
            };
        }

        public PagedResult<EdgeNodeDto> GetOfficer(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<EdgeNodeDto> ExpandNode(int nodeId)
        {
            var results = new List<EdgeNodeDto>();
            var outgoingEdges = _database.GetCollection<EdgeDto>("edges").AsQueryable().Where(e => e.From == nodeId)
                .ToList();
            var incommingEdges = _database.GetCollection<EdgeDto>("edges").AsQueryable().Where(e => e.To == nodeId)
                .ToList();
            results.AddRange(GetConnectedNodes(nodeId, outgoingEdges, EdgeDirection.FromCaller));
            results.AddRange(GetConnectedNodes(nodeId, incommingEdges, EdgeDirection.ToCaller));
            return results;
        }

        private List<EdgeNodeDto> GetConnectedNodes(int nodeId, List<EdgeDto> edges, EdgeDirection dir)
        {
            var results = new List<EdgeNodeDto>();
            foreach (var pair in edges.GroupBy(e => Enum.Parse<EdgeType>(e.Type.Trim('"')), l => l))
            {
                var nodes = pair.ToList();
                switch (pair.Key)
                {

                    case EdgeType.registered_address:
                        results.AddRange(GetAddresses(nodes, dir));
                        break;
                    case EdgeType.same_name_as:
                        results.AddRange(GetSameNameAs(nodes, dir));
                        break;
                    case EdgeType.officer_of:
                        results.AddRange(GetOfficer(nodes, dir));
                        break;
                }
            }

            return results;
        }

        private List<EdgeNodeDto> GetOfficer(List<EdgeDto> edges, EdgeDirection dir)
        {
            if (dir == EdgeDirection.FromCaller)
            {
                var query = from E in edges
                            join off in _database.GetCollection<OfficerDto>("officer").AsQueryable()
                                on E.To equals off.NodeId
                            select new
                            {
                                OfficerId = off.NodeId,
                                OfficerName = off.Name,
                                EdgeFrom = E.From,
                                EdgeTo = E.To,
                                EdgeType = E.Type,
                                EdgeLink = E.Link
                            };
                var results = query.ToList();
                return results.Select(q => new EdgeNodeDto()
                {
                    Edge = new EdgeDto() { From = q.EdgeFrom, To = q.EdgeTo, Link = q.EdgeLink },
                    Node = new NodeDto() { Id = q.OfficerId, Label = q.OfficerName, NodeType = NodeType.Officer }
                }).ToList();
            }
            else
            {
                var query = from E in edges
                            join off in _database.GetCollection<OfficerDto>("officer").AsQueryable()
                                on E.From equals off.NodeId
                            select new
                            {
                                OfficerId = off.NodeId,
                                OfficerName = off.Name,
                                EdgeFrom = E.From,
                                EdgeTo = E.To,
                                EdgeType = E.Type,
                                EdgeLink = E.Link
                            };
                var results = query.ToList();
                return results.Select(q => new EdgeNodeDto()
                {
                    Edge = new EdgeDto() { From = q.EdgeFrom, To = q.EdgeTo, Link = q.EdgeLink },
                    Node = new NodeDto() { Id = q.OfficerId, Label = q.OfficerName, NodeType = NodeType.Officer }
                }).ToList();
            }


        }



        private List<EdgeNodeDto> GetAddresses(List<EdgeDto> edges, EdgeDirection dir)
        {
            var query = from E in _database.GetCollection<EdgeDto>("edges").AsQueryable()
                        join A in _database.GetCollection<AddressDto>("address").AsQueryable()
                            on (dir == EdgeDirection.FromCaller ? E.To : E.From) equals A.NodeId
                        select new
                        {
                            Addr = A,
                            Edge = E
                        };
            return query.ToList().Select(q => new EdgeNodeDto()
            {
                Edge = (q.Edge),
                Node = new NodeDto((q.Addr))
            }).ToList();

        }

        private List<EdgeNodeDto> GetSameNameAs(List<EdgeDto> edges, EdgeDirection dir)
        {
            var query = from E in _database.GetCollection<EdgeDto>("edges").AsQueryable()
                        join ent in _database.GetCollection<EntityDto>("entity").AsQueryable()
                    on (dir == EdgeDirection.FromCaller ? E.To : E.From) equals ent.NodeId
                        select new
                        {
                            Entity = ent,
                            Edge = E
                        };

            return query.ToList().Select(q => new EdgeNodeDto()
            {
                Edge = (q.Edge),
                Node = new NodeDto((q.Entity))
            }).ToList();
        }
    }
}
