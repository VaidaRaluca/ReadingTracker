using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Entities
{
    public class Book : BaseEntity
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public virtual ICollection<ReaderBook> ReaderBooks { get; set; }
        public double AverageRating => ReaderBooks?.Any() == true
            ? ReaderBooks.Average(rb => rb.Rating)
            : 0;
    }
}
