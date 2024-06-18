using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface ISchoolYearService
    {
        void Create(SchoolYearsViewModel data);
        void Destroy(SchoolYearsViewModel data);
        IEnumerable<SchoolYearsViewModel> Read();
        SchoolYearsViewModel Refresh(int entityId);
        void Update(SchoolYearsViewModel data);
    }
}