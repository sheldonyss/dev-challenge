using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParadiseExplorer.Models
{
    public class NodeDto
    {
        public int Id { get; set; }
        public NodeType NodeType { get; set; }
        public string NodeTypeStr => NodeType.ToString();
        public string Label { get; set; }
        public string Prop1 { get; set; }

        public NodeDto()
        {
        }

        public NodeDto(AddressDto addr)
        {
            Id = addr.NodeId;
            NodeType = NodeType.Address;
            Label = addr.Name;
            Prop1 = addr.Address1;
        }

        public NodeDto(EntityDto ent)
        {
            Id = ent.NodeId;
            NodeType = NodeType.Entity;
            Label = ent.Name;
            Prop1 = ent.JurisdictionDescription;
        }

        public NodeDto(OfficerDto ent)
        {
            Id = ent.NodeId;
            NodeType = NodeType.Officer;
            Label = ent.Name;
            Prop1 = ent.Countries;
        }


    }
}
