using System;
using System.Collections.Generic;

namespace OplevOgDel.Api.Models.Configuration
{
    public class FileUploadOptions
    {
        public const string FileUpload = "FileUploadOptions";
        
        public IList<string> AllowedExtensions { get; set; }
        public bool UseRepositoryPath { get; set; }

        private string path;
        public string Path
        {
            get { return path; }
            set
            {
                if (UseRepositoryPath)
                {
                    path = Environment.CurrentDirectory + value;
                }
                else
                {
                    path = value;
                }
            }
        }
    }
}
