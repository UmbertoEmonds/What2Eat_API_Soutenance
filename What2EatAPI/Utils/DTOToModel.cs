using System;
using System.Collections.Generic;
using What2EatAPI.Models.DTO;

namespace What2EatAPI.Utils
{
    public class DTOToModel
    {
        private readonly what2eatContext _context;

        public DTOToModel(what2eatContext context)
        {
            _context = context;
        }

        public Ingredient DTOToIngredient(IngredientDTO ingredientDTO)
        {
            // création d'une image en bdd à partir de l'image url
            var image = new Image
            {
                ImagePath = ingredientDTO.ImageUrl
            };

            _context.Images.Add(image);
            _context.SaveChanges();

            var ingredient = new Ingredient
            {
                Nom = ingredientDTO.Nom,
                CodeBarre = ingredientDTO.CodeBarre,
                Quantite = ingredientDTO.Quantite,
                Unite = ingredientDTO.Unite,

                ImageIdImage = image.IdImage, //on récupère ensuite l'id de l'image créee (clé etrangère)
                CategorieIdCategorie = ingredientDTO.Categorie
            };

            return ingredient;
        }

        public Recette DTOToRecette(RecetteDTO recetteDTO)
        {
            var image = new Image
            {
                ImagePath = recetteDTO.ImageUrl
            };

            _context.Images.Add(image);
            _context.SaveChanges();

            var recette = new Recette
            {
                Nom = recetteDTO.Nom,
                ImageIdImage = image.IdImage,
                Temps = recetteDTO.Temps,
                Personne = recetteDTO.Personne
            };

            return recette;
        }
    }
}