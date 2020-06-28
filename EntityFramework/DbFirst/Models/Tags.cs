using System;
using System.Collections.Generic;

namespace DbFirst.Models
{
    public partial class Tags
    {
        public Tags()
        {
            CourseTags = new HashSet<CourseTags>();
        }

        public int TagId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CourseTags> CourseTags { get; set; }
    }
}
