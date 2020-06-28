using System;
using System.Collections.Generic;

namespace DbFirst.Models
{
    public partial class Authors
    {
        public Authors()
        {
            Courses = new HashSet<Courses>();
        }

        public int AuthorId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Courses> Courses { get; set; }
    }
}
