using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.BPM;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class AitisisEditService : IAitisisEditService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public AitisisEditService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<AitisiViewModel> Read(int prosklisiId)
        {
            var data = (from d in entities.AITISIS
                        where d.PROSKLISI_ID == prosklisiId
                        orderby d.CHILDREN.LASTNAME, d.CHILDREN.FIRSTNAME
                        select new AitisiViewModel
                        {
                            AITISI_ID = d.AITISI_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            STATION_ID = d.STATION_ID,
                            AITISI_DATE = d.AITISI_DATE,
                            AITISI_PROTOCOL = d.AITISI_PROTOCOL,
                            CHILD_ID = d.CHILD_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            AGE = d.AGE,
                            AGE_CATEGORY = d.AGE_CATEGORY,
                            RE_REGISTRATION = d.RE_REGISTRATION ?? false
                        }).ToList();
            return data;
        }

        public IEnumerable<AitisiViewModel> Read(int prosklisiId, int stationId)
        {
            var data = (from d in entities.AITISIS
                        where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId
                        orderby d.CHILDREN.LASTNAME, d.CHILDREN.FIRSTNAME
                        select new AitisiViewModel
                        {
                            AITISI_ID = d.AITISI_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            STATION_ID = d.STATION_ID,
                            AITISI_DATE = d.AITISI_DATE,
                            AITISI_PROTOCOL = d.AITISI_PROTOCOL,
                            CHILD_ID = d.CHILD_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            AGE = d.AGE,
                            AGE_CATEGORY = d.AGE_CATEGORY,
                            RE_REGISTRATION = d.RE_REGISTRATION ?? false
                        }).ToList();
            return data;
        }

        public void Update(AitisiViewModel data)
        {
            AITISIS entity = entities.AITISIS.Find(data.AITISI_ID);

            entity.RE_REGISTRATION = data.RE_REGISTRATION;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
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
                    msg += "Διαγράψτε πρώτα τις μεταφορτώσεις και ύστερα την αίτηση.";
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