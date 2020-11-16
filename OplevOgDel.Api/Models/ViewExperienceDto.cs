﻿using OplevOgDel.Api.Data.Models;
using System;

namespace OplevOgDel.Api.Models
{
    public class ViewExperienceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Guid ExpCategoryId { get; set; }
        public Category Category { get; set; }
    }
}
