using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.BPM;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class RegistryParentService : IRegistryParentService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public RegistryParentService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<sqlParentGridViewModel> ReadParents(int prosklisiId)
        {
            var data = (from d in entities.gridPARENT_DATA
                        where d.PROSKLISI_ID == prosklisiId
                        orderby d.FATHER_FULLNAME, d.MOTHER_FULLNAME
                        select new sqlParentGridViewModel
                        {
                            PARENTS_ID = d.PARENTS_ID,
                            STATION_ID = d.STATION_ID,
                            FATHER_FULLNAME = d.FATHER_FULLNAME,
                            MOTHER_FULLNAME = d.MOTHER_FULLNAME,
                            FATHER_PHONEHOME = d.FATHER_PHONEHOME,
                            FATHER_PHONEMOBILE = d.FATHER_PHONEMOBILE,
                            FATHER_PHONEWORK = d.FATHER_PHONEWORK,
                            MOTHER_PHONEHOME = d.MOTHER_PHONEHOME,
                            MOTHER_PHONEMOBILE = d.MOTHER_PHONEMOBILE,
                            MOTHER_PHONEWORK = d.MOTHER_PHONEWORK
                        }).ToList();
            return data;
        }

        public IEnumerable<sqlParentGridViewModel> ReadParents(int prosklisiId, int stationId)
        {
            var data = (from d in entities.gridPARENT_DATA
                        where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId
                        orderby d.FATHER_FULLNAME, d.MOTHER_FULLNAME
                        select new sqlParentGridViewModel
                        {
                            PARENTS_ID = d.PARENTS_ID,
                            STATION_ID = d.STATION_ID,
                            FATHER_FULLNAME = d.FATHER_FULLNAME,
                            MOTHER_FULLNAME = d.MOTHER_FULLNAME,
                            FATHER_PHONEHOME = d.FATHER_PHONEHOME,
                            FATHER_PHONEMOBILE = d.FATHER_PHONEMOBILE,
                            FATHER_PHONEWORK = d.FATHER_PHONEWORK,
                            MOTHER_PHONEHOME = d.MOTHER_PHONEHOME,
                            MOTHER_PHONEMOBILE = d.MOTHER_PHONEMOBILE,
                            MOTHER_PHONEWORK = d.MOTHER_PHONEWORK
                        }).ToList();
            return data;
        }

        public IEnumerable<sqlAitisiGridViewModel> ReadAitisis(int parentsId)
        {
            var data = (from d in entities.gridAITISEIS_DATA
                        where d.PARENTS_ID == parentsId
                        orderby d.AITISI_PROTOCOL
                        select new sqlAitisiGridViewModel
                        {
                            AITISI_ID = d.AITISI_ID,
                            AITISI_DATE = d.AITISI_DATE,
                            AITISI_PROTOCOL = d.AITISI_PROTOCOL,
                            PROTOCOL = d.PROTOCOL,
                            FULLNAME = d.FULLNAME,
                            BIRTHDATE = d.BIRTHDATE,
                            AGE = d.AGE,
                            AGE_CATEGORY = d.AGE_CATEGORY,
                            GENDER = d.GENDER,
                            SY_TEXT = d.SY_TEXT,
                            SCHOOL_YEAR = d.SCHOOL_YEAR
                        }).ToList();
            return data;
        }

        public ParentsViewModel GetRecord(int entityId)
        {
            var data = (from d in entities.PARENTS
                        where d.PARENTS_ID == entityId
                        select new ParentsViewModel
                        {
                            PARENTS_ID = d.PARENTS_ID,
                            FATHER_AFM = d.FATHER_AFM,
                            FATHER_AMKA = d.FATHER_AMKA,
                            FATHER_ADT = d.FATHER_ADT,
                            FATHER_PASSPORT = d.FATHER_PASSPORT,
                            FATHER_FIRSTNAME = d.FATHER_FIRSTNAME,
                            FATHER_LASTNAME = d.FATHER_LASTNAME,
                            FATHER_ADDRESS = d.FATHER_ADDRESS,
                            FATHER_PHONEHOME = d.FATHER_PHONEHOME,
                            FATHER_PHONEWORK = d.FATHER_PHONEWORK,
                            FATHER_PHONEMOBILE = d.FATHER_PHONEMOBILE,
                            FATHER_EMAIL = d.FATHER_EMAIL,

                            MOTHER_AFM = d.MOTHER_AFM,
                            MOTHER_AMKA = d.MOTHER_AMKA,
                            MOTHER_ADT = d.MOTHER_ADT,
                            MOTHER_PASSPORT = d.MOTHER_PASSPORT,
                            MOTHER_FIRSTNAME = d.MOTHER_FIRSTNAME,
                            MOTHER_LASTNAME = d.MOTHER_LASTNAME,
                            MOTHER_ADDRESS = d.MOTHER_ADDRESS,
                            MOTHER_PHONEHOME = d.MOTHER_PHONEHOME,
                            MOTHER_PHONEWORK = d.MOTHER_PHONEWORK,
                            MOTHER_PHONEMOBILE = d.MOTHER_PHONEMOBILE,
                            MOTHER_EMAIL = d.MOTHER_EMAIL,

                            FATHER_PERMIT = d.FATHER_PERMIT,
                            MOTHER_PERMIT = d.MOTHER_PERMIT,
                            FAMILY_STATUS = d.FAMILY_STATUS,
                            CHILD_EPIMELEIA = d.CHILD_EPIMELEIA
                        }).FirstOrDefault();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}