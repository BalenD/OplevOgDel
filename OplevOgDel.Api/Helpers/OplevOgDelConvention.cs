using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;

namespace OplevOgDel.Api.Helpers
{
    public static class OplevOgDelConvention
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void GetOne([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] Guid id)
        {

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void GetAll()
        {

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void CreateOne([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)] object created)
        {

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void UpdateOne([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] Guid id, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)] object updated)
        {

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void DeleteOne([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] Guid id)
        {

        }

    }
}
