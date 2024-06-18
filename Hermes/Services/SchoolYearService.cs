using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class SchoolYearService : ISchoolYearService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public SchoolYearService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SchoolYearsViewModel> Read()
        {
            var data = (from d in entities.SYS_SCHOOLYEARS
                        orderby d.SY_TEXT
                        select new SchoolYearsViewModel
                        {
                            SY_ID = d.SY_ID,
                            SY_TEXT = d.SY_TEXT,
                            SY_DATESTART = d.SY_DATESTART,
                            SY_DATEEND = d.SY_DATEEND
                        }).ToList();
            return data;
        }

        public void Create(SchoolYearsViewModel data)
        {
            SYS_SCHOOLYEARS entity = new SYS_SCHOOLYEARS()
            {
                SY_TEXT = data.SY_TEXT,
                SY_DATESTART = data.SY_DATESTART,
                SY_DATEEND = data.SY_DATEEND
            };
            entities.SYS_SCHOOLYEARS.Add(entity);
            entities.SaveChanges();

            data.SY_ID = entity.SY_ID;
        }

        public void Update(SchoolYearsViewModel data)
        {
            SYS_SCHOOLYEARS entity = entities.SYS_SCHOOLYEARS.Find(data.SY_ID);

            entity.SY_TEXT = data.SY_TEXT;
            entity.SY_DATESTART = data.SY_DATESTART;
            entity.SY_DATEEND = data.SY_DATEEND;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SchoolYearsViewModel data)
        {
            SYS_SCHOOLYEARS entity = entities.SYS_SCHOOLYEARS.Find(data.SY_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.SYS_SCHOOLYEARS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SchoolYearsViewModel Refresh(int entityId)
        {
            return entities.SYS_SCHOOLYEARS.Select(d => new SchoolYearsViewModel
            {
                SY_ID = d.SY_ID,
                SY_TEXT = d.SY_TEXT,
                SY_DATESTART = d.SY_DATESTART,
                SY_DATEEND = d.SY_DATEEND
            }).Where(d => d.SY_ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}