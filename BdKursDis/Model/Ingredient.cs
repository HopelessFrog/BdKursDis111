using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdKursDis.Model
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Unit Unit { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
