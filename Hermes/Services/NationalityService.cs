using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class NationalityService : INationalityService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public NationalityService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<NationalityViewModel> Read()
        {
            var data = (from d in entities.SYS_NATIONALITIES
                        orderby d.NATIONALITY_ID
                        select new NationalityViewModel
                        {
                            NATIONALITY_ID = d.NATIONALITY_ID,
                            NATIONALITY_TEXT = d.NATIONALITY_TEXT
                        }).ToList();
            return data;
        }

        public void Create(NationalityViewModel data)
        {
            SYS_NATIONALITIES entity = new SYS_NATIONALITIES()
            {
                NATIONALITY_TEXT = data.NATIONALITY_TEXT
            };
            entities.SYS_NATIONALITIES.Add(entity);
            entities.SaveChanges();

            data.NATIONALITY_ID = entity.NATIONALITY_ID;
        }

        public void Update(NationalityViewModel data)
        {
            SYS_NATIONALITIES entity = entities.SYS_NATIONALITIES.Find(data.NATIONALITY_ID);

            entity.NATIONALITY_TEXT = data.NATIONALITY_TEXT;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(NationalityViewModel data)
        {
            SYS_NATIONALITIES entity = entities.SYS_NATIONALITIES.Find(data.NATIONALITY_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.SYS_NATIONALITIES.Remove(entity);
                entities.SaveChanges();
            }
        }

        public NationalityViewModel Refresh(int entityId)
        {
            return entities.SYS_NATIONALITIES.Select(d => new NationalityViewModel
            {
                NATIONALITY_ID = d.NATIONALITY_ID,
                NATIONALITY_TEXT = d.NATIONALITY_TEXT
            }).Where(d => d.NATIONALITY_ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}