﻿using System.ComponentModel.DataAnnotations;

namespace RenginiaiWebApp.Models
{
    public class Address
    {
        [Key]

        public int Id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

    }
}
