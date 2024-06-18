using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IStationDataService
    {
        void Create(StationsGridViewModel data);
        void Destroy(StationsGridViewModel data);
        IEnumerable<StationsGridViewModel> Read();
        void Update(StationsGridViewModel data);
    }
}