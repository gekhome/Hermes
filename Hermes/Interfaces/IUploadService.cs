using Hermes.DAL;
using Hermes.Models;
using System.Collections.Generic;

namespace Hermes.Services
{
    public interface IUploadService
    {
        void Create(UploadsViewModel data, int prosklisiId, USER_PARENTS loggedParent);
        string Delete(int uploadId);
        void Destroy(UploadsViewModel data);
        void DestroyFile(UploadsFilesViewModel data);
        IEnumerable<UploadsViewModel> Read(int prosklisiId, int parentsId);
        IEnumerable<UploadsFilesViewModel> ReadFiles(int uploadId);
        UploadsViewModel Refresh(int entityId);
        void Update(UploadsViewModel data, int prosklisiId, USER_PARENTS loggedParent);
    }
}