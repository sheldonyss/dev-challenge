using System.Collections.Generic;
using ParadiseExplorer.Models;

namespace ParadiseExplorer.Services
{
    public interface IParadiseService
    {
        PagedResult<EdgeNodeDto> GetEntities(int page, int pageSize);
        PagedResult<EdgeNodeDto> GetOfficer(int page, int pageSize);
        List<EdgeNodeDto> ExpandNode(int nodeId);
    }
}
