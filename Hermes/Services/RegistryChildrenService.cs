using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.BPM;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class RegistryChildrenService : IRegistryChildrenService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public RegistryChildrenService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<sqlChildrenViewModel> Read(int prosklisiId)
        {
            var data = (from d in entities.sqlCHILDREN_DATA
                        where d.PROSKLISI_ID == prosklisiId
                        orderby d.PARENT_FULLNAME, d.LASTNAME, d.FIRSTNAME
                        select new sqlChildrenViewModel
                        {
                            CHILD_ID = d.CHILD_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            PARENT_FULLNAME = d.PARENT_FULLNAME,
                            STATION_ID = d.STATION_ID,
                            AMKA = d.AMKA,
                            FIRSTNAME = d.FIRSTNAME,
                            LASTNAME = d.LASTNAME,
                            BIRTHDATE = d.BIRTHDATE,
                            GENDER = d.GENDER,
                            NATIONALITY = d.NATIONALITY
                        }).ToList();
            return data;
        }

        public IEnumerable<sqlChildrenViewModel> Read(int prosklisiId, int stationId)
        {
            var data = (from d in entities.sqlCHILDREN_DATA
                        where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId
                        orderby d.PARENT_FULLNAME, d.LASTNAME, d.FIRSTNAME
                        select new sqlChildrenViewModel
                        {
                            CHILD_ID = d.CHILD_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            PARENT_FULLNAME = d.PARENT_FULLNAME,
                            STATION_ID = d.STATION_ID,
                            AMKA = d.AMKA,
                            FIRSTNAME = d.FIRSTNAME,
                            LASTNAME = d.LASTNAME,
                            BIRTHDATE = d.BIRTHDATE,
                            GENDER = d.GENDER,
                            NATIONALITY = d.NATIONALITY
                        }).ToList();
            return data;
        }

        public void Update(sqlChildrenViewModel data)
        {
            CHILDREN entity = entities.CHILDREN.Find(data.CHILD_ID);

            entity.AMKA = data.AMKA;
            entity.FIRSTNAME = data.FIRSTNAME;
            entity.LASTNAME = data.LASTNAME;
            entity.BIRTHDATE = data.BIRTHDATE;
            entity.GENDER = data.GENDER;
            entity.NATIONALITY = data.NATIONALITY;
            entity.PARENTS_ID = data.PARENTS_ID;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public ChildrenViewModel Refresh(int entityId)
        {
            return entities.CHILDREN.Select(d => new ChildrenViewModel
            {
                CHILD_ID = d.CHILD_ID,
                PARENTS_ID = d.PARENTS_ID,
                AMKA = d.AMKA,
                FIRSTNAME = d.FIRSTNAME,
                LASTNAME = d.LASTNAME,
                BIRTHDATE = d.BIRTHDATE,
                GENDER = d.GENDER,
                NATIONALITY = d.NATIONALITY
            }).Where(d => d.CHILD_ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}