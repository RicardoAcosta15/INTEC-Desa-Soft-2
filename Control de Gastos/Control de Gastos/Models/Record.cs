using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_de_Gastos.Models
{
    public class Record
    {
        public int Id { get; set; }
        public object Concept { get; set; }
        public object Category { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        
    }
}
