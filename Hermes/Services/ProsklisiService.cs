using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class ProsklisiService : IProsklisiService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public ProsklisiService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ProsklisisViewModel> Read()
        {
            var data = (from d in entities.PROSKLISIS
                        orderby d.SCHOOL_YEAR descending, d.DATE_START descending
                        select new ProsklisisViewModel
                        {
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            SCHOOL_YEAR = d.SCHOOL_YEAR,
                            PROTOCOL = d.PROTOCOL,
                            DIOIKITIS = d.DIOIKITIS,
                            DATE_START = d.DATE_START,
                            DATE_END = d.DATE_END,
                            HOUR_START = d.HOUR_START,
                            HOUR_END = d.HOUR_END,
                            STATUS = d.STATUS,
                            ACTIVE = d.ACTIVE ?? false,
                            USERVIEW = d.USERVIEW ?? false
                        }).ToList();
            return data;
        }

        public void Create(ProsklisisViewModel data)
        {
            PROSKLISIS entity = new PROSKLISIS()
            {
                SCHOOL_YEAR = data.SCHOOL_YEAR,
                PROTOCOL = data.PROTOCOL,
                DATE_START = data.DATE_START,
                DATE_END = data.DATE_END,
                HOUR_START = data.HOUR_START,
                HOUR_END = data.HOUR_END,
                STATUS = data.STATUS,
                ACTIVE = data.ACTIVE,
                USERVIEW = data.USERVIEW
            };
            entities.PROSKLISIS.Add(entity);
            entities.SaveChanges();

            data.PROSKLISI_ID = entity.PROSKLISI_ID;
        }

        public void Update(ProsklisisViewModel data)
        {
            PROSKLISIS entity = entities.PROSKLISIS.Find(data.PROSKLISI_ID);

            entity.PROSKLISI_ID = data.PROSKLISI_ID;
            entity.SCHOOL_YEAR = data.SCHOOL_YEAR;
            entity.PROTOCOL = data.PROTOCOL;
            entity.DATE_START = data.DATE_START;
            entity.DATE_END = data.DATE_END;
            entity.HOUR_START = data.HOUR_START;
            entity.HOUR_END = data.HOUR_END;
            entity.STATUS = data.STATUS;
            entity.ACTIVE = data.ACTIVE;
            entity.USERVIEW = data.USERVIEW;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ProsklisisViewModel data)
        {
            PROSKLISIS entity = entities.PROSKLISIS.Find(data.PROSKLISI_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.PROSKLISIS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}