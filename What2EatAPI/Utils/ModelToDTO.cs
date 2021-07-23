using System;
using System.Collections.Generic;
using What2EatAPI.Models.DTO;

namespace What2EatAPI.Utils
{
    public class ModelToDTO
    {
        private readonly what2eatContext _context;

        public ModelToDTO(what2eatContext context)
        {
            _context = context;
        }

        public UtilisateurDTO UtilisateurToDTO(Utilisateur utilisateur, List<IngredientDTO> userIngredients) =>
            new UtilisateurDTO
            {
                IdUtilisateur = utilisateur.IdUtilisateur,
                Nom = utilisateur.Nom,
                Prenom = utilisateur.Prenom,
                Naissance = utilisateur.Naissance,
                Mail = utilisateur.Mail,
                ImageUrl = _context.Images.FindAsync(utilisateur.ImageIdImage).Result.ImagePath,
                Ingredients = userIngredients
            };

        public IngredientDTO IngredientToDTO(Ingredient ingredient) =>
            new IngredientDTO
            {
                IdIngredient = ingredient.IdIngredient,
                Nom = ingredient.Nom,
                CodeBarre = ingredient.CodeBarre,
                Quantite = ingredient.Quantite,
                Unite = ingredient.Unite,
                ImageUrl = _context.Images.FindAsync(ingredient.ImageIdImage).Result.ImagePath,
                Categorie = _context.Categories.FindAsync(ingredient.CategorieIdCategorie).Result.Nom
            };
    }
}
