using System;

namespace OplevOgDel.Api.Models.Configuration
{
    public class SecretOptions
    {
        public const string Secret = "SecretOptions";
        private string issuer;
        public string Issuer 
        {
            get { return issuer; } 
            set
            {
                if (value == null && value == string.Empty)
                {
                    issuer = Environment.GetEnvironmentVariable("Issuer");
                }
                else
                {
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
                if (value == null && value == string.Empty)
                {
                    audience = Environment.GetEnvironmentVariable("Audience");
                }
                else
                {
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
                if (value == null && value == string.Empty)
                {
                    signature = Environment.GetEnvironmentVariable("Signature");
                }
                {
                    signature = value;
                }
                
            }
        }

    }
}
