using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParadiseExplorer.Domains;
using ParadiseExplorer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParadiseExplorer.Controllers
{
    [Route("api/[controller]")]
    public class NodeController : Controller
    {
        private readonly ParadiseService _service;
        private readonly IMapper _mapper;
        // GET: api/<controller>
        public NodeController(ParadiseService service)
        {

            _service = service;
        }
        [HttpGet("get-entity")]
        public PagedResult<EdgeNodeDto> GetEntity(int page, int pageSize)
        {
            return _service.GetEntities(page, pageSize);
        }

        [HttpGet("get-officer")]
        public PagedResult<EdgeNodeDto> GetOfficer(int page, int pageSize)
        {
            return _service.GetOfficer(page, pageSize);
        }

        [HttpGet("expand-node")]
        public List<EdgeNodeDto> ExpandNode(int nodeId)
        {
            return _service.ExpandNode(nodeId);
        }

    }
}
