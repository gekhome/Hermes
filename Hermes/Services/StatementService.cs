using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.BPM;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class StatementService : IStatementService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public StatementService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public StatementViewModel GetRecord(int parentsId, int prosklisiId)
        {
            var data = (from d in entities.STATEMENTS
                        where d.PROSKLISI_ID == prosklisiId && d.PARENTS_ID == parentsId
                        select new StatementViewModel
                        {
                            STATEMENT_ID = d.STATEMENT_ID,
                            STATEMENT_DATE = d.STATEMENT_DATE,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            STATION_ID = d.STATION_ID,
                            FATHER_JOBSECTOR = d.FATHER_JOBSECTOR,
                            FATHER_AMA = d.FATHER_AMA,
                            MOTHER_JOBSECTOR = d.MOTHER_JOBSECTOR,
                            MOTHER_AMA = d.MOTHER_AMA,
                            FATHER_DELTIOANERGIA = d.FATHER_DELTIOANERGIA,
                            MOTHER_DELTIOANERGIA = d.MOTHER_DELTIOANERGIA,
                            FATHER_EPIDOMA = d.FATHER_EPIDOMA ?? 1,
                            MOTHER_EPIDOMA = d.MOTHER_EPIDOMA ?? 1,
                            INCOME_CATEGORY = d.INCOME_CATEGORY,
                            INCOME_FAMILY = d.INCOME_FAMILY,
                            DIKAIOYXOI_BOTH = d.DIKAIOYXOI_BOTH ?? false,
                            FATHER_DISABILITY = d.FATHER_DISABILITY ?? false,
                            MOTHER_DISABILITY = d.MOTHER_DISABILITY ?? false,
                            CHILD_AMEA = d.CHILD_AMEA ?? false,
                            CHILD_ORPHAN = d.CHILD_ORPHAN ?? false,
                            PARENT_DIVORCED = d.PARENT_DIVORCED ?? false,
                            PARENT_INARMY = d.PARENT_INARMY ?? false,
                            CHILDREN_MINOR = d.CHILDREN_MINOR,
                            SIBLING_IN_BNS = d.SIBLING_IN_BNS ?? false
                        }).FirstOrDefault();
            return data;
        }

        public StatementViewModel GetRecord(int statementId)
        {
            var data = (from d in entities.STATEMENTS
                        where d.STATEMENT_ID == statementId
                        select new StatementViewModel
                        {
                            STATEMENT_ID = d.STATEMENT_ID,
                            STATEMENT_DATE = d.STATEMENT_DATE,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            STATION_ID = d.STATION_ID,
                            FATHER_JOBSECTOR = d.FATHER_JOBSECTOR,
                            FATHER_AMA = d.FATHER_AMA,
                            MOTHER_JOBSECTOR = d.MOTHER_JOBSECTOR,
                            MOTHER_AMA = d.MOTHER_AMA,
                            FATHER_DELTIOANERGIA = d.FATHER_DELTIOANERGIA,
                            MOTHER_DELTIOANERGIA = d.MOTHER_DELTIOANERGIA,
                            FATHER_EPIDOMA = d.FATHER_EPIDOMA ?? 1,
                            MOTHER_EPIDOMA = d.MOTHER_EPIDOMA ?? 1,
                            INCOME_CATEGORY = d.INCOME_CATEGORY,
                            INCOME_FAMILY = d.INCOME_FAMILY,
                            DIKAIOYXOI_BOTH = d.DIKAIOYXOI_BOTH ?? false,
                            FATHER_DISABILITY = d.FATHER_DISABILITY ?? false,
                            MOTHER_DISABILITY = d.MOTHER_DISABILITY ?? false,
                            CHILD_AMEA = d.CHILD_AMEA ?? false,
                            CHILD_ORPHAN = d.CHILD_ORPHAN ?? false,
                            PARENT_DIVORCED = d.PARENT_DIVORCED ?? false,
                            PARENT_INARMY = d.PARENT_INARMY ?? false,
                            CHILDREN_MINOR = d.CHILDREN_MINOR,
                            SIBLING_IN_BNS = d.SIBLING_IN_BNS ?? false
                        }).FirstOrDefault();
            return data;
        }

        public void CreateRecord(StatementViewModel data, int parentsId, int prosklisiId)
        {
            STATEMENTS entity = new STATEMENTS()
            {
                STATEMENT_DATE = DateTime.Now.Date,
                PROSKLISI_ID = prosklisiId,
                PARENTS_ID = parentsId,
                STATION_ID = Common.GetStationIdFromParent(prosklisiId, parentsId),
                FATHER_AMA = data.FATHER_AMA,
                FATHER_JOBSECTOR = data.FATHER_JOBSECTOR,
                FATHER_DELTIOANERGIA = data.FATHER_DELTIOANERGIA,
                FATHER_DISABILITY = data.FATHER_DISABILITY,
                FATHER_EPIDOMA = data.FATHER_EPIDOMA,
                MOTHER_AMA = data.MOTHER_AMA,
                MOTHER_JOBSECTOR = data.MOTHER_JOBSECTOR,
                MOTHER_DELTIOANERGIA = data.MOTHER_DELTIOANERGIA,
                MOTHER_DISABILITY = data.MOTHER_DISABILITY,
                MOTHER_EPIDOMA = data.MOTHER_EPIDOMA,
                INCOME_FAMILY = data.INCOME_FAMILY,
                INCOME_CATEGORY = Common.ComputeIncomeCategory(data.INCOME_FAMILY),
                CHILD_AMEA = data.CHILD_AMEA,
                CHILD_ORPHAN = data.CHILD_ORPHAN,
                CHILDREN_MINOR = data.CHILDREN_MINOR,
                DIKAIOYXOI_BOTH = data.DIKAIOYXOI_BOTH,
                PARENT_DIVORCED = data.PARENT_DIVORCED,
                PARENT_INARMY = data.PARENT_INARMY,
                SIBLING_IN_BNS = data.SIBLING_IN_BNS
            };
            entities.STATEMENTS.Add(entity);
            entities.SaveChanges();
        }

        public void UpdateRecord(StatementViewModel data, int statementId, int parentsId, int prosklisiId)
        {
            STATEMENTS entity = entities.STATEMENTS.Find(statementId);

            entity.STATEMENT_DATE = DateTime.Now.Date;
            entity.PROSKLISI_ID = prosklisiId;
            entity.PARENTS_ID = parentsId;
            entity.STATION_ID = Common.GetStationIdFromParent(prosklisiId, parentsId);
            entity.FATHER_AMA = data.FATHER_AMA;
            entity.FATHER_JOBSECTOR = data.FATHER_JOBSECTOR;
            entity.FATHER_DELTIOANERGIA = data.FATHER_DELTIOANERGIA;
            entity.FATHER_DISABILITY = data.FATHER_DISABILITY;
            entity.FATHER_EPIDOMA = data.FATHER_EPIDOMA;
            entity.MOTHER_AMA = data.MOTHER_AMA;
            entity.MOTHER_JOBSECTOR = data.MOTHER_JOBSECTOR;
            entity.MOTHER_DELTIOANERGIA = data.MOTHER_DELTIOANERGIA;
            entity.MOTHER_DISABILITY = data.MOTHER_DISABILITY;
            entity.MOTHER_EPIDOMA = data.MOTHER_EPIDOMA;
            entity.INCOME_FAMILY = data.INCOME_FAMILY;
            entity.INCOME_CATEGORY = Common.ComputeIncomeCategory(data.INCOME_FAMILY);
            entity.CHILD_AMEA = data.CHILD_AMEA;
            entity.CHILD_ORPHAN = data.CHILD_ORPHAN;
            entity.CHILDREN_MINOR = data.CHILDREN_MINOR;
            entity.DIKAIOYXOI_BOTH = data.DIKAIOYXOI_BOTH;
            entity.PARENT_DIVORCED = data.PARENT_DIVORCED;
            entity.PARENT_INARMY = data.PARENT_INARMY;
            entity.SIBLING_IN_BNS = data.SIBLING_IN_BNS;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}