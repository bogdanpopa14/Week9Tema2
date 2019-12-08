using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Entities
{
    public  class NumberOfBooksPerPublisher
    {
        public Publisher publisher { get; set; }
        public int nrBooks { get; set; }
    }
}
