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

        [ApiExplorerSettings(IgnoreApi = true)]
        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUsers()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        // GET: api/Utilisateur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateurDTO>> GetUtilisateur(int id, string token)
        {
            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, id);

            if (isValidToken)
            {
                var utilisateur = await _context.Utilisateurs.FindAsync(id);

                List<IngredientDTO> ingredients = await GetIngredients(id);

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
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur, string token)
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
            var users = await _context.Utilisateurs.ToListAsync();

            foreach (Utilisateur user in users)
            {
                if (user.Mail.Equals(email) && user.MotDePasse.Equals(pass))
                {
                    // génération d'un nouveau token, sauvegarde en base et retour de l'user
                    user.Token = TokenUtils.GenerateJWT(_config);
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    List<IngredientDTO> ingredients = await GetIngredients(user.IdUtilisateur);

                    return await DTOUtils.UtilisateurToDTO(user, ingredients);
                }
            }
            return NotFound();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("ingredientsUser")]
        public async Task<List<IngredientDTO>> GetIngredients(int userId)
        {
            var frigos = await _context.Frigos.ToListAsync();

            List<IngredientDTO> ingredients = new List<IngredientDTO>();

            foreach (var frigo in frigos)
            {
                if (userId.Equals(frigo.UtilisateurIdUtilisateur))
                {
                    var ingredient = await _context.Ingredients.FindAsync(frigo.IngredientIdIngredient);
                    var ingredientDTO = await DTOUtils.IngredientToDTOAsync(ingredient);
                    ingredients.Add(ingredientDTO);
                }
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
                List<IngredientDTO> ingredients = await GetIngredients(userId);

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