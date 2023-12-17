using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace BdKursDis.Model
{
    public  class Stage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Equipment Equipment { get; set; }

        public string Description { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
