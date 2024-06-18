using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IProsklisiService
    {
        void Create(ProsklisisViewModel data);
        void Destroy(ProsklisisViewModel data);
        IEnumerable<ProsklisisViewModel> Read();
        void Update(ProsklisisViewModel data);
    }
}