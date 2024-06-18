using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class UserAdminService : IUserAdminService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public UserAdminService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<UserAdminViewModel> Read()
        {
            var data = (from a in entities.USER_ADMINS
                        select new UserAdminViewModel
                        {
                            USER_ID = a.USER_ID,
                            USERNAME = a.USERNAME,
                            PASSWORD = a.PASSWORD,
                            FULLNAME = a.FULLNAME,
                            CREATEDATE = a.CREATEDATE,
                            ISACTIVE = a.ISACTIVE ?? false
                        }).ToList();
            return data;
        }

        public void Create(UserAdminViewModel data)
        {
            USER_ADMINS entity = new USER_ADMINS()
            {
                USERNAME = data.USERNAME,
                PASSWORD = data.PASSWORD,
                FULLNAME = data.FULLNAME,
                ISACTIVE = data.ISACTIVE,
                CREATEDATE = data.CREATEDATE
            };
            entities.USER_ADMINS.Add(entity);
            entities.SaveChanges();

            data.USER_ID = entity.USER_ID;
        }

        public void Update(UserAdminViewModel data)
        {
            USER_ADMINS entity = entities.USER_ADMINS.Where(mod => mod.USER_ID.Equals(data.USER_ID)).FirstOrDefault();

            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.FULLNAME = data.FULLNAME;
            entity.ISACTIVE = data.ISACTIVE;
            entity.CREATEDATE = data.CREATEDATE;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UserAdminViewModel data)
        {
            USER_ADMINS entity = entities.USER_ADMINS.Find(data.USER_ID);
            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.USER_ADMINS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}