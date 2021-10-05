using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using What2EatAPI.Models.DTO;
using What2EatAPI.Utils;

namespace What2EatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly what2eatContext _context;
        private readonly IConfiguration _config;
        private readonly ModelToDTO DTOUtils;

        public UtilisateurController(what2eatContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            DTOUtils = new ModelToDTO(_context);
        }


        /*
        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUsers()
        {
            return await _context.Utilisateurs.ToListAsync();
        }
        */

        // GET: api/Utilisateur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateurDTO>> GetUtilisateur(int id, string token)
        {
            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, id);

            if (isValidToken)
            {
                var utilisateur = await _context.Utilisateurs.FindAsync(id);

                List<IngredientDTO> ingredients = GetIngredients(id);

                if (utilisateur == null)
                {
                    return NotFound();
                }

                return await DTOUtils.UtilisateurToDTO(utilisateur, ingredients);
            }

            return Unauthorized();
        }

        // PUT: api/Utilisateur/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, UtilisateurDTO utilisateur, string token)
        {

            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, id);

            if (isValidToken)
            {
                if (id != utilisateur.IdUtilisateur)
                {
                    return BadRequest();
                }

                _context.Entry(utilisateur).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilisateurExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            return Unauthorized();
        }

        

        // POST: api/Utilisateur
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.IdUtilisateur }, utilisateur);
        }

        // DELETE: api/Utilisateur/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id, string token)
        {
            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, id);

            if (isValidToken)
            {
                var utilisateur = await _context.Utilisateurs.FindAsync(id);
                if (utilisateur == null)
                {
                    return NotFound();
                }

                _context.Utilisateurs.Remove(utilisateur);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();
        }

        private bool UtilisateurExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.IdUtilisateur == id);
        }

        //api/Utilisateur/login
        [HttpPost("login")]
        public async Task<ActionResult<UtilisateurDTO>> Login(string email, string pass)
        {
            var userQuery = _context.Utilisateurs.Where(user => user.Mail == email && user.MotDePasse == pass);

            if(userQuery.Any())
            {
                var userLogged = userQuery.First();

                if (userLogged.Token == null)
                {
                    // génération d'un nouveau token, sauvegarde en base et retour de l'user
                    userLogged.Token = TokenUtils.GenerateJWT(_config);
                    _context.Entry(userLogged).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                List<IngredientDTO> ingredients = GetIngredients(userLogged.IdUtilisateur);

                return await DTOUtils.UtilisateurToDTO(userLogged, ingredients);
            }else
            {
                return NotFound();
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("ingredientsUser")]
        public List<IngredientDTO> GetIngredients(int userId)
        {

            List<IngredientDTO> ingredients = new List<IngredientDTO>();

            var ingredientsData = _context.Utilisateurs.Join(

                _context.Frigos,
                user => user.IdUtilisateur,
                frigo => frigo.UtilisateurIdUtilisateur,
                (user, frigo) => new
                {
                    user.IdUtilisateur,
                    frigo.IngredientIdIngredient

                }).Join(

                _context.Ingredients,
                frigo => frigo.IngredientIdIngredient,
                ingredient => ingredient.IdIngredient,
                (frigo, ingredient) => new
                {
                    frigo.IdUtilisateur,
                    ingredient.IdIngredient,
                    ingredient.Nom,
                    ingredient.CodeBarre,
                    ingredient.Quantite,
                    ingredient.Contenant,
                    ingredient.Unite,
                    ingredient.ImageIdImage,
                    ingredient.CategorieIdCategorie

                }).Where(result => result.IdUtilisateur == userId).ToList();

            foreach (var ingredientData in ingredientsData)
            {
                var ingredientDTO = new IngredientDTO
                {
                    IdIngredient = ingredientData.IdIngredient,
                    Nom = ingredientData.Nom,
                    CodeBarre = ingredientData.CodeBarre,
                    Quantite = ingredientData.Quantite,
                    contenant = ingredientData.Contenant,
                    Unite = ingredientData.Unite
                };

                ingredients.Add(ingredientDTO);
            }

            return ingredients;
        }

        //api/Utilisateur/ingredients
        [HttpGet("frigo")]
        public async Task<ActionResult<List<IngredientDTO>>> GetIngredientsFromUserAsync(int userId, string token)
        {
            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, userId);

            if (isValidToken)
            {
                List<IngredientDTO> ingredients = GetIngredients(userId);

                return Ok(ingredients);

            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("verifyToken")]
        public async Task<Boolean> VerifyToken(string token, int userId)
        {
            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, userId);

            return isValidToken;
        }

    }
}