namespace OplevOgDel.Api.Helpers
{
    /// <summary>
    /// Error object response for controllers
    /// </summary>
    public class ErrorObject
    {
        /// <summary>
        /// The http method where the error occured
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// The URL where the error occured
        /// </summary>
        public string At { get; set; }
        /// <summary>
        /// The status code returned by the error
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// The error messsage returned
        /// </summary>
        public string Error { get; set; }
    }
}
