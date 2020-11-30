namespace OplevOgDel.Api.Models.Dto.RequestDto
{
    public class UserRequestParametersDto : BaseRequestParametersDto
    {
        /// <summary>
        /// Filter the users by role
        /// </summary>
        /// <example>User</example>
        public string FilterByRole { get; set; }
        /// <summary>
        /// Search by email
        /// </summary>
        /// <example>JohnSmith4</example>
        public string SearchByEmail { get; set; }
        /// <summary>
        /// Search by username
        /// </summary>
        /// <example>John</example>
        public string SearchByUsername { get; set; }
    }
}
