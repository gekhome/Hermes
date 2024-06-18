using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class UserParentService : IUserParentService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public UserParentService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<UserParentEditViewModel> Read()
        {
            var data = (from d in entities.USER_PARENTS
                        select new UserParentEditViewModel
                        {
                            USER_ID = d.USER_ID,
                            USERNAME = d.USERNAME,
                            PASSWORD = d.PASSWORD,
                            AFM = d.USER_AFM,
                            CREATEDATE = d.CREATEDATE,
                            PARENT_TYPE = d.PARENT_TYPE
                        }).ToList();
            return data;
        }

        public void Create(UserParentEditViewModel data)
        {
            USER_PARENTS entity = new USER_PARENTS()
            {
                USERNAME = data.USERNAME,
                PASSWORD = data.PASSWORD,
                USER_AFM = data.AFM,
                PARENT_TYPE = data.PARENT_TYPE,
                CREATEDATE = data.CREATEDATE
            };
            entities.USER_PARENTS.Add(entity);
            entities.SaveChanges();

            data.USER_ID = entity.USER_ID;
        }

        public void Update(UserParentEditViewModel data)
        {
            USER_PARENTS entity = entities.USER_PARENTS.Where(d => d.USER_ID.Equals(data.USER_ID)).FirstOrDefault();

            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.USER_AFM = data.AFM;
            entity.PARENT_TYPE = data.PARENT_TYPE;
            entity.CREATEDATE = data.CREATEDATE;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UserParentEditViewModel data)
        {
            USER_PARENTS entity = entities.USER_PARENTS.Find(data.USER_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.USER_PARENTS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public UserParentEditViewModel Refresh(int entityId)
        {
            return entities.USER_PARENTS.Select(d => new UserParentEditViewModel
            {
                USER_ID = d.USER_ID,
                USERNAME = d.USERNAME,
                PASSWORD = d.PASSWORD,
                AFM = d.USER_AFM,
                CREATEDATE = d.CREATEDATE,
                PARENT_TYPE = d.PARENT_TYPE
            }).Where(d => d.USER_ID == entityId).FirstOrDefault();
        }

        public IEnumerable<ParentAccountInfoViewModel> Detail(int entityId)
        {
            var data = (from d in entities.sqlAPPLICANT_INFO
                        where d.USER_ID == entityId
                        select new ParentAccountInfoViewModel
                        {
                            USER_ID = d.USER_ID,
                            USERNAME = d.USERNAME,
                            PARENT_AFM = d.PARENT_AFM,
                            PARENT_AMKA = d.PARENT_AMKA,
                            PARENT_FULLNAME = d.PARENT_FULLNAME,
                            PARENT_PHONEHOME = d.PARENT_PHONEHOME,
                            PARENT_PHONEMOBILE = d.PARENT_PHONEMOBILE,
                            PARENT_PHONEWORK = d.PARENT_PHONEWORK
                        }).ToList();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}