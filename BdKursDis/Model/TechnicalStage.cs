using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdKursDis.Model
{
    public class TechnicalStage
    {

        public int Id { get; set; }

        public Product Product { get; set; }

        public Stage Stage { get; set; }

        public int Number { get; set; }
        public string Time { get; set; }
    }
}
