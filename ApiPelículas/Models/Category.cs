﻿using System.ComponentModel.DataAnnotations;

namespace ApiPelículas.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

    }
}
