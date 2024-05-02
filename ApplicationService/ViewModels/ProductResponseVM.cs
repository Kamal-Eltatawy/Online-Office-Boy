using Domain.Entities;
using Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ViewModels
{
    public class ProductResponseVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
        public TimeSpan ReadyTime { get; set; }
        public string PictureUrl { get; set; }

        public string CategoryId { get; set; }
        public string CategoryType { get; set; }



    }
}
