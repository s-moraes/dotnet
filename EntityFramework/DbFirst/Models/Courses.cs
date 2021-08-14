using System;
using System.Collections.Generic;

namespace DbFirst.Models
{
    public partial class Courses
    {
        public Courses()
        {
            CourseSections = new HashSet<CourseSections>();
            CourseTags = new HashSet<CourseTags>();
        }

        public int CourseId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public short Price { get; set; }
        public string LevelString { get; set; }
        public byte Level { get; set; }

        public virtual Authors Author { get; set; }
        public virtual ICollection<CourseSections> CourseSections { get; set; }
        public virtual ICollection<CourseTags> CourseTags { get; set; }
    }
}
