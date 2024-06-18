using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.BPM;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class ChildrenService : IChildrenService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public ChildrenService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ChildrenViewModel> Read(int parentsId)
        {
            var data = (from d in entities.CHILDREN
                        where d.PARENTS_ID == parentsId
                        orderby d.LASTNAME, d.FIRSTNAME
                        select new ChildrenViewModel
                        {
                            CHILD_ID = d.CHILD_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            AMKA = d.AMKA,
                            FIRSTNAME = d.FIRSTNAME,
                            LASTNAME = d.LASTNAME,
                            BIRTHDATE = d.BIRTHDATE,
                            GENDER = d.GENDER,
                            NATIONALITY = d.NATIONALITY
                        }).ToList();
            return data;
        }

        public void Create(ChildrenViewModel data, int parentsId)
        {
            CHILDREN entity = new CHILDREN()
            {
                AMKA = data.AMKA,
                FIRSTNAME = data.FIRSTNAME,
                LASTNAME = data.LASTNAME,
                BIRTHDATE = data.BIRTHDATE,
                GENDER = data.GENDER,
                NATIONALITY = data.NATIONALITY,
                PARENTS_ID = parentsId
            };
            entities.CHILDREN.Add(entity);
            entities.SaveChanges();

            data.CHILD_ID = entity.CHILD_ID;
        }

        public void Update(ChildrenViewModel data, int parentsId)
        {
            CHILDREN entity = entities.CHILDREN.Find(data.CHILD_ID);

            entity.AMKA = data.AMKA;
            entity.FIRSTNAME = data.FIRSTNAME;
            entity.LASTNAME = data.LASTNAME;
            entity.BIRTHDATE = data.BIRTHDATE;
            entity.GENDER = data.GENDER;
            entity.NATIONALITY = data.NATIONALITY;
            entity.PARENTS_ID = parentsId;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ChildrenViewModel data)
        {
            CHILDREN entity = entities.CHILDREN.Find(data.CHILD_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.CHILDREN.Remove(entity);
                entities.SaveChanges();
            }
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