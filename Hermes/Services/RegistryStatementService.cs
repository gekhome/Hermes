using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.BPM;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class RegistryStatementService : IRegistryStatementService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public RegistryStatementService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<sqlStatementGridViewModel> Read(int prosklisiId)
        {
            var data = (from d in entities.gridSTATEMENT_DATA
                        where d.PROSKLISI_ID == prosklisiId
                        orderby d.MOTHER_FULLNAME, d.FATHER_FULLNAME
                        select new sqlStatementGridViewModel
                        {
                            STATEMENT_ID = d.STATEMENT_ID,
                            STATEMENT_DATE = d.STATEMENT_DATE,
                            PARENTS_ID = d.PARENTS_ID,
                            STATION_ID = d.STATION_ID,
                            FATHER_FULLNAME = d.FATHER_FULLNAME,
                            MOTHER_FULLNAME = d.MOTHER_FULLNAME,
                            FATHER_JOBSECTOR = d.FATHER_JOBSECTOR,
                            MOTHER_JOBSECTOR = d.MOTHER_JOBSECTOR,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            PROTOCOL = d.PROTOCOL,
                            STATION_NAME = d.STATION_NAME,
                            SY_TEXT = d.SY_TEXT
                        }).ToList();
            return data;
        }

        public IEnumerable<sqlStatementGridViewModel> Read(int prosklisiId, int stationId)
        {
            var data = (from d in entities.gridSTATEMENT_DATA
                        where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId
                        orderby d.MOTHER_FULLNAME, d.FATHER_FULLNAME
                        select new sqlStatementGridViewModel
                        {
                            STATEMENT_ID = d.STATEMENT_ID,
                            STATEMENT_DATE = d.STATEMENT_DATE,
                            PARENTS_ID = d.PARENTS_ID,
                            STATION_ID = d.STATION_ID,
                            FATHER_FULLNAME = d.FATHER_FULLNAME,
                            MOTHER_FULLNAME = d.MOTHER_FULLNAME,
                            FATHER_JOBSECTOR = d.FATHER_JOBSECTOR,
                            MOTHER_JOBSECTOR = d.MOTHER_JOBSECTOR,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            PROTOCOL = d.PROTOCOL,
                            STATION_NAME = d.STATION_NAME,
                            SY_TEXT = d.SY_TEXT
                        }).ToList();
            return data;
        }

        public StatementViewModel GetRecord(int entityId)
        {
            var data = (from d in entities.STATEMENTS
                        where d.STATEMENT_ID == entityId
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
                            INCOME_FAMILY = d.INCOME_FAMILY,
                            INCOME_CATEGORY = d.INCOME_CATEGORY,
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

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}