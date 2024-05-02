using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Office
    {
        public int ID { get; set; }

        public string Name { get; set; }
        
        public bool IsKitchen { get; set; }

        public string Location { get; set; }


    }
}
