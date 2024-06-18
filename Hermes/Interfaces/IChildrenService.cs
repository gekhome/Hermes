using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IChildrenService
    {
        void Create(ChildrenViewModel data, int parentsId);
        void Destroy(ChildrenViewModel data);
        IEnumerable<ChildrenViewModel> Read(int parentsId);
        ChildrenViewModel Refresh(int entityId);
        void Update(ChildrenViewModel data, int parentsId);
    }
}