namespace OplevOgDel.Api.Models.Configuration
{
    public class SwaggerOptions
    {
        public const string Swagger = "SwaggerOptions";
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TermsOfService { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactUrl { get; set; }
        public string LicenseName { get; set; }
        public string LicenseUrl { get; set; }
        public string Endpoint { get; set; }
        public string Name { get; set; }
    }
}
