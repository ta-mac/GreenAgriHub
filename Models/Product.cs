
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using GreenAgriHub.Models;


namespace GreenAgriHub.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string ProductCategory { get; set; }
        public double ProductPrice { get; set; }
        public DateTime ProductDate { get; set; }

        public int FarmerId { get; set; }

        [BindNever]
        public Farmer Farmer { get; set; }
    }
}
