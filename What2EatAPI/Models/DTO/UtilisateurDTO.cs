using System;
using System.Collections.Generic;

namespace What2EatAPI.Models.DTO
{
    public class UtilisateurDTO
    {
        public int IdUtilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime? Naissance { get; set; }
        public string Mail { get; set; }
        public string ImageUrl { get; set; }
        public List<IngredientDTO> Ingredients { get; set; }
    }
}