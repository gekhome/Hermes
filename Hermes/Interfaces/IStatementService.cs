using Hermes.Models;

namespace Hermes.Services
{
    public interface IStatementService
    {
        void CreateRecord(StatementViewModel data, int parentsId, int prosklisiId);
        StatementViewModel GetRecord(int statementId);
        StatementViewModel GetRecord(int parentsId, int prosklisiId);
        void UpdateRecord(StatementViewModel data, int statementId, int parentsId, int prosklisiId);
    }
}