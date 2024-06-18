using Hermes.DAL;
using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IAitisisMoriaService
    {
        AITISIS AuditAitisi(AitisiCheckViewModel model, int aitisiId);
        ParentsInfoViewModel Detail(int entityId);
        AitisiCheckViewModel GetAitisi(int aitisiId);
        ChildInfoViewModel GetChildDetail(int aitisiId);
        IEnumerable<AitiseisListViewModel> Read(int prosklisiId);
        IEnumerable<AitiseisListViewModel> Read(int prosklisiId, int stationId);
        IEnumerable<sqlUploadedFilesViewModel> ReadFiles(int entityId);
    }
}