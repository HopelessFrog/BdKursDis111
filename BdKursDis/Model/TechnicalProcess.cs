using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdKursDis.Model
{
    public class TechnicalProcess
    {
        public int Id { get; set; }
        public Product Product { get; set; }

        public string Description { get; set; }
    }
}
