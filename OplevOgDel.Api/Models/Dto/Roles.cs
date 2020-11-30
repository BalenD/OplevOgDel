namespace OplevOgDel.Api.Models.Dto
{
    /// <summary>
    /// Dto class to not hardcode things in authorization processes
    /// </summary>
    public class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string AdminAndUser = "User,Admin";
    }
}
