using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class UserStationService : IUserStationService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public UserStationService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<UserStationViewModel> Read()
        {
            var data = (from a in entities.USER_STATIONS
                        orderby a.USERNAME
                        select new UserStationViewModel
                        {
                            USER_ID = a.USER_ID,
                            USERNAME = a.USERNAME,
                            PASSWORD = a.PASSWORD,
                            STATION_ID = a.STATION_ID ?? 0,
                            ISACTIVE = a.ISACTIVE ?? false
                        }).ToList();
            return data;
        }

        public void Create(UserStationViewModel data)
        {
            USER_STATIONS entity = new USER_STATIONS()
            {
                USERNAME = data.USERNAME,
                PASSWORD = data.PASSWORD,
                STATION_ID = data.STATION_ID,
                ISACTIVE = data.ISACTIVE,
            };

            entities.USER_STATIONS.Add(entity);
            entities.SaveChanges();

            data.USER_ID = entity.USER_ID;
        }

        public void Update(UserStationViewModel data)
        {
            USER_STATIONS entity = entities.USER_STATIONS.Where(d => d.USER_ID.Equals(data.USER_ID)).FirstOrDefault();

            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.STATION_ID = data.STATION_ID;
            entity.ISACTIVE = data.ISACTIVE;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UserStationViewModel data)
        {
            USER_STATIONS entity = entities.USER_STATIONS.Find(data.USER_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.USER_STATIONS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}