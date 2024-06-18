using Hermes.Models;

namespace Hermes.Services
{
    public interface IParentService
    {
        void CreateRecord(ParentsViewModel data, int userId);
        ParentsViewModel GetRecord(int userId);
        void UpdateRecord(ParentsViewModel data, int parentsId, int userId = 0);
    }
}