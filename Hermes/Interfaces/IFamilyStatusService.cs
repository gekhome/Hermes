using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IFamilyStatusService
    {
        void Create(FamilyStatusViewModel data);
        void Destroy(FamilyStatusViewModel data);
        IEnumerable<FamilyStatusViewModel> Read();
        FamilyStatusViewModel Refresh(int entityId);
        void Update(FamilyStatusViewModel data);
    }
}