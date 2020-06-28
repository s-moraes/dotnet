using System;
using System.Collections.Generic;

namespace DbFirst.Models
{
    public partial class CourseSections
    {
        public int SectionId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }

        public virtual Courses Course { get; set; }
    }
}
