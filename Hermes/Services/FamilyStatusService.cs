using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class FamilyStatusService : IFamilyStatusService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public FamilyStatusService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<FamilyStatusViewModel> Read()
        {
            var data = (from d in entities.FAMILY_STATUS
                        orderby d.FSTATUS_TEXT
                        select new FamilyStatusViewModel
                        {
                            FSTATUS_ID = d.FSTATUS_ID,
                            FSTATUS_TEXT = d.FSTATUS_TEXT
                        }).ToList();
            return data;
        }

        public void Create(FamilyStatusViewModel data)
        {
            FAMILY_STATUS entity = new FAMILY_STATUS()
            {
                FSTATUS_TEXT = data.FSTATUS_TEXT
            };
            entities.FAMILY_STATUS.Add(entity);
            entities.SaveChanges();

            data.FSTATUS_ID = entity.FSTATUS_ID;
        }

        public void Update(FamilyStatusViewModel data)
        {
            FAMILY_STATUS entity = entities.FAMILY_STATUS.Find(data.FSTATUS_ID);

            entity.FSTATUS_TEXT = data.FSTATUS_TEXT;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(FamilyStatusViewModel data)
        {
            FAMILY_STATUS entity = entities.FAMILY_STATUS.Find(data.FSTATUS_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.FAMILY_STATUS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public FamilyStatusViewModel Refresh(int entityId)
        {
            return entities.FAMILY_STATUS.Select(d => new FamilyStatusViewModel
            {
                FSTATUS_ID = d.FSTATUS_ID,
                FSTATUS_TEXT = d.FSTATUS_TEXT
            }).Where(d => d.FSTATUS_ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}