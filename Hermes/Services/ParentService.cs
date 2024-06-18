using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.BPM;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class ParentService : IParentService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public ParentService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public ParentsViewModel GetRecord(int userId)
        {
            var data = (from d in entities.PARENTS
                        where d.PARENT_USERID == userId
                        select new ParentsViewModel
                        {
                            PARENT_USERID = d.PARENT_USERID,
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

        public void CreateRecord(ParentsViewModel data, int userId)
        {
            PARENTS entity = new PARENTS()
            {
                PARENT_USERID = userId,
                FATHER_ADT = data.FATHER_ADT,
                FATHER_AFM = data.FATHER_AFM,
                FATHER_AMKA = data.FATHER_AMKA,
                FATHER_PASSPORT = data.FATHER_PASSPORT,
                FATHER_FIRSTNAME = data.FATHER_FIRSTNAME,
                FATHER_LASTNAME = data.FATHER_LASTNAME,
                FATHER_ADDRESS = data.FATHER_ADDRESS,
                FATHER_PHONEHOME = data.FATHER_PHONEHOME,
                FATHER_PHONEWORK = data.FATHER_PHONEWORK,
                FATHER_PHONEMOBILE = data.FATHER_PHONEMOBILE,
                FATHER_EMAIL = data.FATHER_EMAIL,
                FATHER_PERMIT = data.FATHER_PERMIT,
                MOTHER_ADT = data.MOTHER_ADT,
                MOTHER_AFM = data.MOTHER_AFM,
                MOTHER_AMKA = data.MOTHER_AMKA,
                MOTHER_PASSPORT = data.MOTHER_PASSPORT,
                MOTHER_FIRSTNAME = data.MOTHER_FIRSTNAME,
                MOTHER_LASTNAME = data.MOTHER_LASTNAME,
                MOTHER_ADDRESS = data.MOTHER_ADDRESS,
                MOTHER_PHONEHOME = data.MOTHER_PHONEHOME,
                MOTHER_PHONEWORK = data.MOTHER_PHONEWORK,
                MOTHER_PHONEMOBILE = data.MOTHER_PHONEMOBILE,
                MOTHER_EMAIL = data.MOTHER_EMAIL,
                MOTHER_PERMIT = data.MOTHER_PERMIT,
                FAMILY_STATUS = data.FAMILY_STATUS,
                CHILD_EPIMELEIA = data.CHILD_EPIMELEIA
            };
            entities.PARENTS.Add(entity);
            entities.SaveChanges();
        }

        public void UpdateRecord(ParentsViewModel data, int parentsId, int userId = 0)
        {
            PARENTS entity = entities.PARENTS.Find(parentsId);

            if (userId > 0)
            {
                entity.PARENT_USERID = userId;
            }
            entity.FATHER_ADT = data.FATHER_ADT;
            entity.FATHER_AFM = data.FATHER_AFM;
            entity.FATHER_AMKA = data.FATHER_AMKA;
            entity.FATHER_PASSPORT = data.FATHER_PASSPORT;
            entity.FATHER_FIRSTNAME = data.FATHER_FIRSTNAME;
            entity.FATHER_LASTNAME = data.FATHER_LASTNAME;
            entity.FATHER_ADDRESS = data.FATHER_ADDRESS;
            entity.FATHER_PHONEHOME = data.FATHER_PHONEHOME;
            entity.FATHER_PHONEWORK = data.FATHER_PHONEWORK;
            entity.FATHER_PHONEMOBILE = data.FATHER_PHONEMOBILE;
            entity.FATHER_EMAIL = data.FATHER_EMAIL;
            entity.FATHER_PERMIT = data.FATHER_PERMIT;
            entity.MOTHER_ADT = data.MOTHER_ADT;
            entity.MOTHER_AFM = data.MOTHER_AFM;
            entity.MOTHER_AMKA = data.MOTHER_AMKA;
            entity.MOTHER_PASSPORT = data.MOTHER_PASSPORT;
            entity.MOTHER_FIRSTNAME = data.MOTHER_FIRSTNAME;
            entity.MOTHER_LASTNAME = data.MOTHER_LASTNAME;
            entity.MOTHER_ADDRESS = data.MOTHER_ADDRESS;
            entity.MOTHER_PHONEHOME = data.MOTHER_PHONEHOME;
            entity.MOTHER_PHONEWORK = data.MOTHER_PHONEWORK;
            entity.MOTHER_PHONEMOBILE = data.MOTHER_PHONEMOBILE;
            entity.MOTHER_EMAIL = data.MOTHER_EMAIL;
            entity.MOTHER_PERMIT = data.MOTHER_PERMIT;
            entity.FAMILY_STATUS = data.FAMILY_STATUS;
            entity.CHILD_EPIMELEIA = data.CHILD_EPIMELEIA;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}