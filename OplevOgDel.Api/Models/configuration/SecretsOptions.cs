using System;

namespace OplevOgDel.Api.Models.Configuration
{
    /// <summary>
    /// A DTO to retrieve the secrets options in appsettings.json
    /// </summary>
    public class SecretOptions
    {
        /// <summary>
        /// a constant so we don't hardcode strings in startup.cs
        /// </summary>
        public const string Secret = "SecretOptions";
        private string issuer;
        public string Issuer 
        {
            get { return issuer; } 
            set
            {
                // if the value does not exist
                if (value == null && value == string.Empty)
                {
                    // get it from environment variable
                    issuer = Environment.GetEnvironmentVariable("Issuer");
                }
                else
                {
                    // otherwise assign value
                    issuer = value;
                }
            } 
        }


        private string audience;
        public string Audience 
        {
            get { return audience; } 
            set
            {
                // if the value does not exist
                if (value == null && value == string.Empty)
                {
                    // get it from environment variable
                    audience = Environment.GetEnvironmentVariable("Audience");
                }
                else
                {
                    // otherwise assign value
                    audience = value;
                }
            } 
        }

        private string signature;

        public string Signature
        {
            get { return signature; }
            set 
            {
                // if the value does not exist
                if (value == null && value == string.Empty)
                {
                    // get it from environment variable
                    signature = Environment.GetEnvironmentVariable("Signature");
                }
                {
                    // otherwise assign value
                    signature = value;
                }
                
            }
        }

    }
}
