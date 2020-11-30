using System;

namespace OplevOgDel.Api.Helpers
{
    /// <summary>
    /// Class that has extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Calculates the age based by date of birth
        /// </summary>
        /// <param name="dateOfBirth">Date of birth to calculate the age from</param>
        /// <returns></returns>
        public static int GetAge(this DateTime dateOfBirth)
        {

            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }
    }
}
