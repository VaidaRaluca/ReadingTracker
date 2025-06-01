using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Entities
{
    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public virtual ICollection<ReaderBook> ReaderBooks { get; set; }
    }

}
