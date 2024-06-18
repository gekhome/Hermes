using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IAitisisEditService
    {
        string Delete(int aitisiId);
        IEnumerable<AitisiViewModel> Read(int prosklisiId);
        IEnumerable<AitisiViewModel> Read(int prosklisiId, int stationId);
        AitisiViewModel Refresh(int entityId);
        void Update(AitisiViewModel data);
    }
}