using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.BPM;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class AitisiService : IAitisiService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public AitisiService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<AitisiViewModel> Read(int prosklisiId, int parentsId)
        {
            var data = (from d in entities.AITISIS
                        where d.PROSKLISI_ID == prosklisiId && d.PARENTS_ID == parentsId
                        orderby d.AITISI_PROTOCOL
                        select new AitisiViewModel
                        {
                            AITISI_ID = d.AITISI_ID,
                            AITISI_DATE = d.AITISI_DATE,
                            CHILD_ID = d.CHILD_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            AITISI_PROTOCOL = d.AITISI_PROTOCOL,
                            STATION_ID = d.STATION_ID,
                            TIMESTAMP = d.TIMESTAMP,
                            AGE = d.AGE,
                            AGE_CATEGORY = d.AGE_CATEGORY,
                            RE_REGISTRATION = d.RE_REGISTRATION ?? false
                        }).ToList();
            return data;
        }

        public void Create(AitisiViewModel data, int prosklisiId, int parentsId)
        {
            AITISIS entity = new AITISIS()
            {
                AITISI_PROTOCOL = Common.GenerateProtocol(),
                AITISI_DATE = DateTime.Now.Date,
                PROSKLISI_ID = prosklisiId,
                PARENTS_ID = parentsId,
                CHILD_ID = data.CHILD_ID,
                STATION_ID = data.STATION_ID,
                RE_REGISTRATION = data.RE_REGISTRATION,
                AGE = Common.ComputeChildAge((int)data.CHILD_ID),
                AGE_CATEGORY = Common.ComputeAgeCategory((int)data.CHILD_ID),
                TIMESTAMP = DateTime.Now
            };
            entities.AITISIS.Add(entity);
            entities.SaveChanges();

            data.AITISI_ID = entity.AITISI_ID;
        }

        public void Update(AitisiViewModel data, int prosklisiId, int parentsId)
        {
            AITISIS entity = entities.AITISIS.Find(data.AITISI_ID);

            entity.AITISI_PROTOCOL = data.AITISI_PROTOCOL;
            entity.PROSKLISI_ID = prosklisiId;
            entity.AITISI_DATE = DateTime.Now.Date;
            entity.PARENTS_ID = parentsId;
            entity.CHILD_ID = data.CHILD_ID;
            entity.STATION_ID = data.STATION_ID;
            entity.RE_REGISTRATION = data.RE_REGISTRATION;
            entity.AGE = Common.ComputeChildAge((int)data.CHILD_ID);
            entity.AGE_CATEGORY = Common.ComputeAgeCategory((int)data.CHILD_ID);
            entity.TIMESTAMP = DateTime.Now;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(AitisiViewModel data)
        {
            AITISIS entity = entities.AITISIS.Find(data.AITISI_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.AITISIS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public string Delete(int aitisiId)
        {
            string msg = "";

            AITISIS entity = entities.AITISIS.Find(aitisiId);
            if (entity != null)
            {
                if (Kerberos.CanDeleteAitisi(entity.AITISI_ID))
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.AITISIS.Remove(entity);
                    entities.SaveChanges();
                }
                else
                {
                    msg = "Δεν μπορεί να διαγραφεί η αίτηση διότι έχει συνημμένα αρχεία.<br>";
                    msg += "Διαγράψτε πρώτα τα συνημμένα και ύστερα την αίτηση.";
                }
            }
            return msg;
        }

        public AitisiViewModel Refresh(int entityId)
        {
            return entities.AITISIS.Select(d => new AitisiViewModel
            {
                AITISI_ID = d.AITISI_ID,
                AITISI_DATE = d.AITISI_DATE,
                CHILD_ID = d.CHILD_ID,
                PARENTS_ID = d.PARENTS_ID,
                PROSKLISI_ID = d.PROSKLISI_ID,
                AITISI_PROTOCOL = d.AITISI_PROTOCOL,
                STATION_ID = d.STATION_ID,
                TIMESTAMP = d.TIMESTAMP,
                AGE = d.AGE,
                AGE_CATEGORY = d.AGE_CATEGORY,
                RE_REGISTRATION = d.RE_REGISTRATION ?? false
            }).Where(d => d.AITISI_ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}