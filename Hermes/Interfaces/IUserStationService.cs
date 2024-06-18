using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IUserStationService
    {
        void Create(UserStationViewModel data);
        void Destroy(UserStationViewModel data);
        IEnumerable<UserStationViewModel> Read();
        void Update(UserStationViewModel data);
    }
}