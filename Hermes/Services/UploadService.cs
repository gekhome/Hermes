using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.BPM;
using Hermes.DAL;
using Hermes.Models;
using System.Data.Entity;

namespace Hermes.Services
{
    public class UploadService : IUploadService, IDisposable
    {
        private readonly HermesDBEntities entities;

        public UploadService(HermesDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<UploadsViewModel> Read(int prosklisiId, int parentsId)
        {
            var data = (from d in entities.UPLOADS
                        where d.PROSKLISI_ID == prosklisiId && d.PARENT_ID == parentsId
                        orderby d.UPLOAD_DATE descending
                        select new UploadsViewModel
                        {
                            UPLOAD_ID = d.UPLOAD_ID,
                            AITISI_ID = d.AITISI_ID,
                            PARENT_ID = d.PARENT_ID,
                            STATION_ID = d.STATION_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            UPLOAD_DATE = d.UPLOAD_DATE,
                            UPLOAD_NAME = d.UPLOAD_NAME,
                            UPLOAD_SUMMARY = d.UPLOAD_SUMMARY
                        }).ToList();
            return data;
        }

        public void Create(UploadsViewModel data, int prosklisiId, USER_PARENTS loggedParent)
        {
            UPLOADS entity = new UPLOADS()
            {
                PARENT_ID = Common.GetParentIdFromUserId(loggedParent.USER_ID),
                PROSKLISI_ID = prosklisiId,
                STATION_ID = Common.GetStationIdFromAitisi((int)data.AITISI_ID),
                AITISI_ID = data.AITISI_ID,
                UPLOAD_DATE = data.UPLOAD_DATE,
                UPLOAD_NAME = Common.GetParentNameFromUser(loggedParent),
                UPLOAD_SUMMARY = data.UPLOAD_SUMMARY
            };
            entities.UPLOADS.Add(entity);
            entities.SaveChanges();

            data.UPLOAD_ID = entity.UPLOAD_ID;
        }

        public void Update(UploadsViewModel data, int prosklisiId, USER_PARENTS loggedParent)
        {
            UPLOADS entity = entities.UPLOADS.Find(data.UPLOAD_ID);

            entity.PARENT_ID = Common.GetParentIdFromUserId(loggedParent.USER_ID);
            entity.PROSKLISI_ID = prosklisiId;
            entity.AITISI_ID = data.AITISI_ID;
            entity.STATION_ID = Common.GetStationIdFromAitisi((int)data.AITISI_ID);
            entity.UPLOAD_DATE = data.UPLOAD_DATE;
            entity.UPLOAD_NAME = Common.GetParentNameFromUser(loggedParent);
            entity.UPLOAD_SUMMARY = data.UPLOAD_SUMMARY;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UploadsViewModel data)
        {
            UPLOADS entity = entities.UPLOADS.Find(data.UPLOAD_ID);

            entities.Entry(entity).State = EntityState.Deleted;
            entities.UPLOADS.Remove(entity);
            entities.SaveChanges();
        }

        public string Delete(int uploadId)
        {
            string msg = "";

            UPLOADS entity = entities.UPLOADS.Find(uploadId);
            if (entity != null)
            {
                if (Kerberos.CanDeleteUpload(entity.UPLOAD_ID))
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.UPLOADS.Remove(entity);
                    entities.SaveChanges();
                }
                else
                {
                    msg = "Για να γίνει η διαγραφή πρέπει πρώτα να διαγραφούν τα σχετικά μεταφορτωμένα αρχεία.";
                }
            }
            return msg;
        }

        public UploadsViewModel Refresh(int entityId)
        {
            return entities.UPLOADS.Select(d => new UploadsViewModel
            {
                UPLOAD_ID = d.UPLOAD_ID,
                AITISI_ID = d.AITISI_ID,
                PARENT_ID = d.PARENT_ID,
                STATION_ID = d.STATION_ID,
                PROSKLISI_ID = d.PROSKLISI_ID,
                UPLOAD_DATE = d.UPLOAD_DATE,
                UPLOAD_NAME = d.UPLOAD_NAME,
                UPLOAD_SUMMARY = d.UPLOAD_SUMMARY
            }).Where(d => d.UPLOAD_ID == entityId).FirstOrDefault();
        }

        public IEnumerable<UploadsFilesViewModel> ReadFiles(int uploadId)
        {
            var data = (from d in entities.UPLOADS_FILES
                        where d.UPLOAD_ID == uploadId
                        orderby d.STATION_USER, d.SCHOOLYEAR_TEXT, d.FILENAME
                        select new UploadsFilesViewModel
                        {
                            ID = d.ID,
                            UPLOAD_ID = d.UPLOAD_ID,
                            STATION_USER = d.STATION_USER,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            FILENAME = d.FILENAME,
                            EXTENSION = d.EXTENSION
                        }).ToList();
            return data;
        }

        public void DestroyFile(UploadsFilesViewModel data)
        {
            UPLOADS_FILES entity = entities.UPLOADS_FILES.Find(data.ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.UPLOADS_FILES.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}