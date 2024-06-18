using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class StationDataService : IStationDataService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public StationDataService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StationsGridViewModel> Read()
        {
            var data = (from d in entities.SYS_STATIONS
                        orderby d.ΕΠΩΝΥΜΙΑ
                        select new StationsGridViewModel
                        {
                            ΣΤΑΘΜΟΣ_ΚΩΔ = d.ΣΤΑΘΜΟΣ_ΚΩΔ,
                            ΕΠΩΝΥΜΙΑ = d.ΕΠΩΝΥΜΙΑ,
                            EMAIL = d.EMAIL,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ ?? 0,
                            ΠΕΡΙΦΕΡΕΙΑ = d.ΠΕΡΙΦΕΡΕΙΑ ?? 0,
                            ΣΥΜΜΕΤΟΧΗ = d.ΣΥΜΜΕΤΟΧΗ ?? false
                        }).ToList();
            return data;
        }

        public void Create(StationsGridViewModel data)
        {
            SYS_STATIONS entity = new SYS_STATIONS()
            {
                ΕΠΩΝΥΜΙΑ = data.ΕΠΩΝΥΜΙΑ,
                EMAIL = data.EMAIL,
                ΠΕΡΙΦΕΡΕΙΑΚΗ = data.ΠΕΡΙΦΕΡΕΙΑΚΗ,
                ΠΕΡΙΦΕΡΕΙΑ = data.ΠΕΡΙΦΕΡΕΙΑ,
                ΣΥΜΜΕΤΟΧΗ = data.ΣΥΜΜΕΤΟΧΗ
            };
            entities.SYS_STATIONS.Add(entity);
            entities.SaveChanges();

            data.ΣΤΑΘΜΟΣ_ΚΩΔ = entity.ΣΤΑΘΜΟΣ_ΚΩΔ;
        }

        public void Update(StationsGridViewModel data)
        {
            SYS_STATIONS entity = entities.SYS_STATIONS.Find(data.ΣΤΑΘΜΟΣ_ΚΩΔ);

            entity.ΕΠΩΝΥΜΙΑ = data.ΕΠΩΝΥΜΙΑ;
            entity.EMAIL = data.EMAIL;
            entity.ΠΕΡΙΦΕΡΕΙΑΚΗ = data.ΠΕΡΙΦΕΡΕΙΑΚΗ;
            entity.ΠΕΡΙΦΕΡΕΙΑ = data.ΠΕΡΙΦΕΡΕΙΑ;
            entity.ΣΥΜΜΕΤΟΧΗ = data.ΣΥΜΜΕΤΟΧΗ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StationsGridViewModel data)
        {
            SYS_STATIONS entity = entities.SYS_STATIONS.Find(data.ΣΤΑΘΜΟΣ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.SYS_STATIONS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}