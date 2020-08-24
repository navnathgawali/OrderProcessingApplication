using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrderProcessingApplication.Models.ViewModels
{
    public class ShipmentItemDetailsViewModel
    {
       
        public int ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        public string PrecausionsDesc { get; set; }
        [Required]
        public string FromLocation { get; set; }
        [Required]
        public string ToLocation { get; set; }
        public int OrderId { get; set; }

    }
}