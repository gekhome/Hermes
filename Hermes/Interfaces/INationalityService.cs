using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface INationalityService
    {
        void Create(NationalityViewModel data);
        void Destroy(NationalityViewModel data);
        IEnumerable<NationalityViewModel> Read();
        NationalityViewModel Refresh(int entityId);
        void Update(NationalityViewModel data);
    }
}