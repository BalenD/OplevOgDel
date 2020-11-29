using System;
using System.Collections.Generic;

namespace OplevOgDel.Api.Models.Configuration
{
    /// <summary>
    /// A DTO to retrieve the fileupload options in appsettings.json
    /// </summary>
    public class FileUploadOptions
    {
        /// <summary>
        /// a constant so we don't hardcode strings in startup.cs
        /// </summary>
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
