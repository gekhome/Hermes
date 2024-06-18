using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class JobSectorService : IJobSectorService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public JobSectorService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<JobSectorViewModel> Read()
        {
            var data = (from d in entities.JOB_SECTORS
                        orderby d.JOBSECTOR_TEXT
                        select new JobSectorViewModel
                        {
                            JOBSECTOR_ID = d.JOBSECTOR_ID,
                            JOBSECTOR_TEXT = d.JOBSECTOR_TEXT
                        }).ToList();
            return data;
        }

        public void Create(JobSectorViewModel data)
        {
            JOB_SECTORS entity = new JOB_SECTORS()
            {
                JOBSECTOR_TEXT = data.JOBSECTOR_TEXT
            };
            entities.JOB_SECTORS.Add(entity);
            entities.SaveChanges();

            data.JOBSECTOR_ID = entity.JOBSECTOR_ID;
        }

        public void Update(JobSectorViewModel data)
        {
            JOB_SECTORS entity = entities.JOB_SECTORS.Find(data.JOBSECTOR_ID);

            entity.JOBSECTOR_TEXT = data.JOBSECTOR_TEXT;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(JobSectorViewModel data)
        {
            JOB_SECTORS entity = entities.JOB_SECTORS.Find(data.JOBSECTOR_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.JOB_SECTORS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public JobSectorViewModel Refresh(int entityId)
        {
            return entities.JOB_SECTORS.Select(d => new JobSectorViewModel
            {
                JOBSECTOR_ID = d.JOBSECTOR_ID,
                JOBSECTOR_TEXT = d.JOBSECTOR_TEXT
            }).Where(d => d.JOBSECTOR_ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}