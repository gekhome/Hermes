using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class AitisisMoriaService : IAitisisMoriaService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public AitisisMoriaService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<AitiseisListViewModel> Read(int prosklisiId)
        {
            var data = (from d in entities.sqlAITISEIS_LIST
                        where d.PROSKLISI_ID == prosklisiId
                        orderby d.CHILD_FULLNAME, d.AITISI_PROTOCOL
                        select new AitiseisListViewModel
                        {
                            AITISI_ID = d.AITISI_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            STATION_ID = d.STATION_ID,
                            AITISI_DATE = d.AITISI_DATE,
                            AITISI_PROTOCOL = d.AITISI_PROTOCOL,
                            CHILD_FULLNAME = d.CHILD_FULLNAME,
                            CHILD_ID = d.CHILD_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            STATION_NAME = d.STATION_NAME,
                            AGE = d.AGE,
                            AGE_CATEGORY = d.AGE_CATEGORY,
                            BIRTHDATE = d.BIRTHDATE,
                            GENDER = d.GENDER,
                            MORIA_TOTAL = d.MORIA_TOTAL,
                            RANKING_TEXT = d.RANKING_TEXT
                        }).ToList();
            return data;
        }

        public IEnumerable<AitiseisListViewModel> Read(int prosklisiId, int stationId)
        {
            var data = (from d in entities.sqlAITISEIS_LIST
                        where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId
                        orderby d.CHILD_FULLNAME, d.AITISI_PROTOCOL
                        select new AitiseisListViewModel
                        {
                            AITISI_ID = d.AITISI_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            STATION_ID = d.STATION_ID,
                            AITISI_DATE = d.AITISI_DATE,
                            AITISI_PROTOCOL = d.AITISI_PROTOCOL,
                            CHILD_FULLNAME = d.CHILD_FULLNAME,
                            CHILD_ID = d.CHILD_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            STATION_NAME = d.STATION_NAME,
                            AGE = d.AGE,
                            AGE_CATEGORY = d.AGE_CATEGORY,
                            BIRTHDATE = d.BIRTHDATE,
                            GENDER = d.GENDER,
                            MORIA_TOTAL = d.MORIA_TOTAL,
                            RANKING_TEXT = d.RANKING_TEXT
                        }).ToList();
            return data;
        }

        public ParentsInfoViewModel Detail(int entityId)
        {
            var data = (from d in entities.sqlPARENT_INFO
                        where d.PARENTS_ID == entityId
                        select new ParentsInfoViewModel
                        {
                            PARENTS_ID = d.PARENTS_ID,
                            FATHER_AFM = d.FATHER_AFM,
                            FATHER_AMKA = d.FATHER_AMKA,
                            FATHER_FULLNAME = d.FATHER_FULLNAME,
                            FATHER_PHONEHOME = d.FATHER_PHONEHOME,
                            FATHER_PHONEMOBILE = d.FATHER_PHONEMOBILE,
                            FATHER_PHONEWORK = d.FATHER_PHONEWORK,
                            FATHER_ADDRESS = d.FATHER_ADDRESS,
                            FATHER_EMAIL = d.FATHER_EMAIL,
                            MOTHER_AFM = d.MOTHER_AFM,
                            MOTHER_AMKA = d.MOTHER_AMKA,
                            MOTHER_FULLNAME = d.MOTHER_FULLNAME,
                            MOTHER_PHONEHOME = d.MOTHER_PHONEHOME,
                            MOTHER_PHONEMOBILE = d.MOTHER_PHONEMOBILE,
                            MOTHER_PHONEWORK = d.MOTHER_PHONEWORK,
                            MOTHER_ADDRESS = d.MOTHER_ADDRESS,
                            MOTHER_EMAIL = d.MOTHER_EMAIL,
                            FSTATUS_TEXT = d.FSTATUS_TEXT,
                            PARENT_TYPETEXT = d.PARENT_TYPETEXT
                        }).FirstOrDefault();
            return data;
        }

        public IEnumerable<sqlUploadedFilesViewModel> ReadFiles(int entityId)
        {
            var data = (from d in entities.sqlUPLOADED_FILES
                        where d.AITISI_ID == entityId
                        select new sqlUploadedFilesViewModel
                        {
                            FILE_ID = d.FILE_ID,
                            AITISI_ID = d.AITISI_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            STATION_ID = d.STATION_ID,
                            FILENAME = d.FILENAME,
                            EXTENSION = d.EXTENSION,
                            UPLOAD_NAME = d.UPLOAD_NAME,
                            UPLOAD_SUMMARY = d.UPLOAD_SUMMARY,
                            STATION_USER = d.STATION_USER,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT
                        }).ToList();
            return data;
        }

        public ChildInfoViewModel GetChildDetail(int aitisiId)
        {
            var data = (from d in entities.mor_CHILD_INFO
                        where d.AITISI_ID == aitisiId
                        select new ChildInfoViewModel
                        {
                            CHILD_ID = d.CHILD_ID,
                            AITISI_ID = d.AITISI_ID,
                            AMKA = d.AMKA,
                            FULLNAME = d.FULLNAME
                        }).FirstOrDefault();
            return data;
        }

        public AitisiCheckViewModel GetAitisi(int aitisiId)
        {
            var data = (from d in entities.AITISIS
                        where d.AITISI_ID == aitisiId
                        select new AitisiCheckViewModel
                        {
                            AITISI_ID = d.AITISI_ID,
                            AITISI_DATE = d.AITISI_DATE,
                            AITISI_PROTOCOL = d.AITISI_PROTOCOL,
                            STATION_ID = d.STATION_ID,
                            CHILD_ID = d.CHILD_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            TIMESTAMP = d.TIMESTAMP,
                            AGE = d.AGE,
                            AGE_CATEGORY = d.AGE_CATEGORY,
                            MORIA_FAMILY = d.MORIA_FAMILY,
                            MORIA_INCOME = d.MORIA_INCOME,
                            MORIA_OAED = d.MORIA_OAED,
                            MORIA_REREGISTER = d.MORIA_REREGISTER,
                            MORIA_SIBLING = d.MORIA_SIBLING,
                            MORIA_SOCIAL = d.MORIA_SOCIAL,
                            MORIA_TOTAL = d.MORIA_TOTAL,
                            RANKING = d.RANKING,
                            APOKLEISMOS_AITIA = d.APOKLEISMOS_AITIA,
                            ENSTASI = d.ENSTASI ?? false,
                            EPITROPI1_TEXT = d.EPITROPI1_TEXT,
                            EPITROPI2_TEXT = d.EPITROPI2_TEXT
                        }).FirstOrDefault();
            return data;
        }

        public AITISIS AuditAitisi(AitisiCheckViewModel model, int aitisiId)
        {
            AITISIS entity = entities.AITISIS.Find(aitisiId);

            entity.AGE_CATEGORY = model.AGE_CATEGORY;
            entity.RANKING = model.RANKING;
            entity.ENSTASI = model.ENSTASI;
            entity.APOKLEISMOS_AITIA = model.APOKLEISMOS_AITIA;
            entity.EPITROPI1_TEXT = model.EPITROPI1_TEXT;
            entity.EPITROPI2_TEXT = model.EPITROPI2_TEXT;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();

            return entity;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}