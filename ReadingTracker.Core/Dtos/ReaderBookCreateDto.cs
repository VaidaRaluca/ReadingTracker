using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Dtos
{
    public class ReaderBookCreateDto
    {
        public int BookId { get; set; }
        public int ReaderId { get; set; }

        public double Rating { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }

}
