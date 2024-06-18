using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IRegistryParentService
    {
        ParentsViewModel GetRecord(int entityId);
        IEnumerable<sqlAitisiGridViewModel> ReadAitisis(int parentsId);
        IEnumerable<sqlParentGridViewModel> ReadParents(int prosklisiId);
        IEnumerable<sqlParentGridViewModel> ReadParents(int prosklisiId, int stationId);
    }
}