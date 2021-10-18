﻿using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;

namespace Backups.StorageAlgorithms
{
    public class SplitStorage : IStorageAlgorithm
    {
        public List<Storage> Store(List<JobObject> jobObjects, DirectoryInfo directoryInfo)
        {
            var storages = new List<Storage>();

            foreach (JobObject jobObject in jobObjects)
            {
                if (!File.Exists(jobObject.FilePath)) continue;

                string filename = Path.GetFileNameWithoutExtension(jobObject.FilePath);
                string archivePath = Path.Join(directoryInfo.FullName, $@"\{filename}.zip");

                using ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Create);

                string pathFileToAdd = $"{jobObject.FilePath}";

                string nameFileToAdd = Path.GetFileName(jobObject.FilePath);

                zipArchive.CreateEntryFromFile(pathFileToAdd, nameFileToAdd);

                var storage = new Storage(new FileInfo(archivePath));
                storages.Add(storage);
            }

            return storages;
        }
    }
}