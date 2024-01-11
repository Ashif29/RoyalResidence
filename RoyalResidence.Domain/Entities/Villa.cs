using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalResidence.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [DisplayName("Price Per Night")]
        [Range(100, 100000)]
        public int Price { get; set; }
        public int Sqft { get; set; }
        [Range(0, 10)]
        public int Occupancy { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [DisplayName("Image")]
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
