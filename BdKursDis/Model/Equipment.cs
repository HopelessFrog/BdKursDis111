using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdKursDis.Model
{
    public  class Equipment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Capacity { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
