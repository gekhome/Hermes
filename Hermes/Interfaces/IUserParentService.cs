using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IUserParentService
    {
        void Create(UserParentEditViewModel data);
        void Destroy(UserParentEditViewModel data);
        IEnumerable<ParentAccountInfoViewModel> Detail(int entityId);
        IEnumerable<UserParentEditViewModel> Read();
        UserParentEditViewModel Refresh(int entityId);
        void Update(UserParentEditViewModel data);
    }
}