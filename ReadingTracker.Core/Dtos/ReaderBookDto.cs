using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Dtos
{
    public class ReaderBookDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; }         

        public int ReaderId { get; set; }
        public string ReaderName { get; set; }

        public double Rating { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }

}
