using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<UtilisateurDTO> UtilisateurToDTO(Utilisateur utilisateur, List<IngredientDTO> userIngredients)
        {
            var image = await _context.Images.FindAsync(utilisateur.ImageIdImage);

            return new UtilisateurDTO
            {
                IdUtilisateur = utilisateur.IdUtilisateur,
                Nom = utilisateur.Nom,
                Prenom = utilisateur.Prenom,
                Naissance = utilisateur.Naissance,
                Mail = utilisateur.Mail,
                ImageUrl = image.ImagePath,
                Token = utilisateur.Token,
                Ingredients = userIngredients
            };
        }   

        public async Task<IngredientDTO> IngredientToDTOAsync(Ingredient ingredient)
        {

            var image = await _context.Images.FindAsync(ingredient.ImageIdImage);
            var categorie = await _context.Categories.FindAsync(ingredient.CategorieIdCategorie);

            return new IngredientDTO
            {
                IdIngredient = ingredient.IdIngredient,
                Nom = ingredient.Nom,
                CodeBarre = ingredient.CodeBarre,
                Quantite = ingredient.Quantite,
                Unite = ingredient.Unite,
                ImageUrl = image.ImagePath,
                Categorie = categorie.Nom
            };
        }
    }
}