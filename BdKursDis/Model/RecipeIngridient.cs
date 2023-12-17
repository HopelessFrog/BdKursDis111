using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdKursDis.Model
{
    public class RecipeIngridient
    {
        public int Id { get; set; }

        public Ingredient Ingredient { get; set; }

        public Product Product { get; set; }

        public int Count { get; set; }


    }
}
