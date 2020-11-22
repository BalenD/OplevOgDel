namespace OplevOgDel.Api.Helpers
{
    public class ErrorObject
    {
        public string Method { get; set; }
        public string At { get; set; }
        public int StatusCode { get; set; }
        public string Error { get; set; }
    }
}
