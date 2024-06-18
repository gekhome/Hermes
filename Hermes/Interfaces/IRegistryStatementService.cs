using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IRegistryStatementService
    {
        StatementViewModel GetRecord(int entityId);
        IEnumerable<sqlStatementGridViewModel> Read(int prosklisiId);
        IEnumerable<sqlStatementGridViewModel> Read(int prosklisiId, int stationId);
    }
}