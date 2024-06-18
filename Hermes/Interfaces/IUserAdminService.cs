using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IUserAdminService
    {
        void Create(UserAdminViewModel data);
        void Destroy(UserAdminViewModel data);
        IEnumerable<UserAdminViewModel> Read();
        void Update(UserAdminViewModel data);
    }
}