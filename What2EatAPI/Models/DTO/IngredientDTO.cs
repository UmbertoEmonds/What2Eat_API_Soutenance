using System;
namespace What2EatAPI.Models.DTO
{
    public class IngredientDTO
    {
        public int IdIngredient { get; set; }
        public string Nom { get; set; }
        public string CodeBarre { get; set; }
        public int? Quantite { get; set; }
        public String contenant { get; set; }
        public string Unite { get; set; }
        public string ImageUrl { get; set; }
        public string Categorie { get; set; }
    }
}