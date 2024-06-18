using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IRegistryChildrenService
    {
        IEnumerable<sqlChildrenViewModel> Read(int prosklisiId);
        IEnumerable<sqlChildrenViewModel> Read(int prosklisiId, int stationId);
        ChildrenViewModel Refresh(int entityId);
        void Update(sqlChildrenViewModel data);
    }
}