using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ParadiseExplorer.Domains;
using ParadiseExplorer.Models;

namespace ParadiseExplorer
{
    public class ParadiseService
    {
        private readonly ParadiseContext _context;
        private readonly IMapper _mapper;

        private enum Direction
        {
            FromCaller,
            ToCaller
        }

        public ParadiseService(ParadiseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PagedResult<EdgeNodeDto> GetEntities(int page, int pageSize)
        {
            pageSize = Math.Min(pageSize, 100);
            var total = _context.Entity.Count();
            var skipCount = Math.Max(0, (page - 1) * pageSize);

            var entQuery = _context.Entity.Where(e => !string.IsNullOrEmpty(e.Name)).OrderBy(e => e.Name).Skip(skipCount).Take(pageSize);

            var entDtos = entQuery.ToList().Select(e => _mapper.Map<EntityDto>(e));
            var edgeNodes = entDtos.Select(e => new EdgeNodeDto()
            {
                Node = new NodeDto(e)
            }).ToList();

            return new PagedResult<EdgeNodeDto>()
            {
                Items = edgeNodes,
                Page = page,
                TotalCount = total
            };
            //pageSize = Math.Min(pageSize, 100);
            //var total = _context.Entity.Count();
            //var skipCount = Math.Max(0, (page - 1) * pageSize);

            //var entQuery = _context.Entity.Where(e => !string.IsNullOrEmpty(e.Name)).OrderBy(e => e.Name).Skip(skipCount).Take(pageSize).Select(e => e.NodeId);
            //var edgeNodes = new List<EdgeNodeDto>();
            //foreach (var entNodeId in entQuery)
            //{
            //    edgeNodes.AddRange(ExpandNode(entNodeId));
            //}

            //return new PagedResult<EdgeNodeDto>()
            //{
            //    Items = edgeNodes,
            //    Page = page,
            //    TotalCount = total
            //};
        }

        public PagedResult<EdgeNodeDto> GetOfficer(int page, int pageSize)
        {
            pageSize = Math.Min(pageSize, 100);
            var total = _context.Entity.Count();
            var skipCount = Math.Max(0, (page - 1) * pageSize);

            var officerQuery = _context.Officer.Where(e => !string.IsNullOrEmpty(e.Name)).OrderBy(e => e.Name).Skip(skipCount).Take(pageSize).Select(e => e.NodeId);
            var edgeNodes = new List<EdgeNodeDto>();
            foreach (var officerNodeId in officerQuery)
            {
                edgeNodes.AddRange(ExpandNode(officerNodeId));
            }

            return new PagedResult<EdgeNodeDto>()
            {
                Items = edgeNodes,
                Page = page,
                TotalCount = total
            };
        }

        public List<EdgeNodeDto> ExpandNode(int nodeId)
        {
            //outgoing nodes
            var results = new List<EdgeNodeDto>();
            var outgoingEdges = _context.Edges.Where(e => e.StartId == nodeId);
            var incommingEdges = _context.Edges.Where(e => e.EndId == nodeId);
            results.AddRange(GetConnectedNodes(nodeId, outgoingEdges, Direction.FromCaller));
            results.AddRange(GetConnectedNodes(nodeId, incommingEdges, Direction.ToCaller));

            //var incomingEdges = _context.Edges.Where(e => e.EndId == nodeId);

            return results;
        }

        private List<EdgeNodeDto> GetConnectedNodes(int nodeId, IEnumerable<Edges> edges, Direction dir)
        {
            var results = new List<EdgeNodeDto>();
            foreach (var pair in edges.GroupBy(e => e.EdgeType, l => l))
            {
                var nodes = pair.ToList();
                switch (pair.Key)
                {

                    case EdgeType.registered_address:
                        results.AddRange(GetAddresses(nodeId, nodes, dir));
                        break;
                    case EdgeType.same_name_as:
                        results.AddRange(GetSameNameAs(nodeId, nodes, dir));
                        break;
                    case EdgeType.officer_of:
                        results.AddRange(GetOfficer(nodeId, nodes, dir));
                        break;
                }
            }

            return results;
        }

        private List<EdgeNodeDto> GetOfficer(int nodeId, List<Edges> edges, Direction dir)
        {
            var query = from E in edges
                        join Off in _context.Officer
                            on (dir == Direction.FromCaller ? E.EndId : E.StartId) equals Off.NodeId
                        select new
                        {
                            Offcier = Off,
                            Edge = E
                        };



            return query.ToList().Select(q => new EdgeNodeDto()
            {
                Edge = _mapper.Map<EdgeDto>(q.Edge),
                Node = new NodeDto(_mapper.Map<OfficerDto>(q.Offcier))
            }).ToList();
        }

        private List<EdgeNodeDto> GetAddresses(int nodeId, List<Edges> edges, Direction dir)
        {
            var query = from E in edges
                        join A in _context.Address
                            on (dir == Direction.FromCaller ? E.EndId : E.StartId) equals A.NodeId
                        select new
                        {
                            Addr = A,
                            Edge = E
                        };
            return query.ToList().Select(q => new EdgeNodeDto()
            {
                Edge = _mapper.Map<EdgeDto>(q.Edge),
                Node = new NodeDto(_mapper.Map<AddressDto>(q.Addr))
            }).ToList();

        }

        private List<EdgeNodeDto> GetSameNameAs(int nodeId, List<Edges> edges, Direction dir)
        {
            var query = from E in edges
                        join ent in _context.Entity
                            on (dir == Direction.FromCaller ? E.EndId : E.StartId) equals ent.NodeId
                        select new
                        {
                            Entity = ent,
                            Edge = E
                        };

            return query.ToList().Select(q => new EdgeNodeDto()
            {
                Edge = _mapper.Map<EdgeDto>(q.Edge),
                Node = new NodeDto(_mapper.Map<EntityDto>(q.Entity))
            }).ToList();
        }
    }
}
