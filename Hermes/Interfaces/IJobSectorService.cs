using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IJobSectorService
    {
        void Create(JobSectorViewModel data);
        void Destroy(JobSectorViewModel data);
        IEnumerable<JobSectorViewModel> Read();
        JobSectorViewModel Refresh(int entityId);
        void Update(JobSectorViewModel data);
    }
}