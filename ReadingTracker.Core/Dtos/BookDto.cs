using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Dtos
{
    public class BookDto
    {
        //new
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }
}
