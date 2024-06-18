using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IAitisiService
    {
        void Create(AitisiViewModel data, int prosklisiId, int parentsId);
        string Delete(int aitisiId);
        void Destroy(AitisiViewModel data);
        IEnumerable<AitisiViewModel> Read(int prosklisiId, int parentsId);
        AitisiViewModel Refresh(int entityId);
        void Update(AitisiViewModel data, int prosklisiId, int parentsId);
    }
}