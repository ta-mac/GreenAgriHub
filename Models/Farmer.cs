
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Text.Json.Serialization;
using GreenAgriHub.Models;


namespace GreenAgriHub.Models
{
    public class Farmer
    {
        public int FarmerId { get; set; }
        public string FarmerName { get; set; }
        public string FarmerSurname { get; set; }
        public string FarmerPhone { get; set; }
        public string FarmerEmail { get; set; }
        public string FarmerPassword { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
