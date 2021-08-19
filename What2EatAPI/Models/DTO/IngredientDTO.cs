﻿using System;
namespace What2EatAPI.Models.DTO
{
    public class IngredientDTO
    {
        public string Nom { get; set; }
        public string CodeBarre { get; set; }
        public int? Quantite { get; set; }
        public string Unite { get; set; }
        public string ImageUrl { get; set; }
        public int Categorie { get; set; }
    }
}