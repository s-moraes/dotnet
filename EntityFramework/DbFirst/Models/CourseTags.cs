using System;
using System.Collections.Generic;

namespace DbFirst.Models
{
    public partial class CourseTags
    {
        public int CourseId { get; set; }
        public int TagId { get; set; }

        public virtual Courses Course { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
