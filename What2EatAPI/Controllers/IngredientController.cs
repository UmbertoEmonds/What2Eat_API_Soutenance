using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using What2EatAPI.Models.DTO;
using What2EatAPI.Utils;

namespace What2EatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly what2eatContext _context;
        private DTOToModel DTOUtils;
        private ModelToDTO ModelUtils;

        public IngredientController(what2eatContext context)
        {
            _context = context;
            DTOUtils = new DTOToModel(context);
            ModelUtils = new ModelToDTO(context);
        }

        /*
        // GET: api/Ingredient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients(int userId, string token)
        {
            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, userId);

            if (isValidToken)
            {
                return await _context.Ingredients.ToListAsync();
            }
            return Unauthorized();
        }
        */

        [HttpGet]
        public async Task<ActionResult<IngredientDTO>> GetIngredient(string barcode, string token, int userId)
        {
            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, userId);
            if (isValidToken)
            {
                List<Ingredient> ingredients = await _context.Ingredients.ToListAsync();

                foreach(var ingredient in ingredients)
                {
                    if (ingredient.CodeBarre.Equals(barcode))
                    {
                        return await ModelUtils.IngredientToDTOAsync(ingredient);
                    }
                }
            }else
            {
                return Unauthorized();
            }
            return NotFound();
        }         

        // GET: api/Ingredient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id, int userId, string token)
        {
            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, userId);

            if (isValidToken)
            {
                var ingredient = await _context.Ingredients.FindAsync(id);

                if (ingredient == null)
                {
                    return NotFound();
                }

                return ingredient;
            }

            return Unauthorized();
        }

        // PUT: api/Ingredient/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, Ingredient ingredient, int userId, string token)
        {
            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, userId);

            if (isValidToken)
            {
                if (id != ingredient.IdIngredient)
                {
                    return BadRequest();
                }

                _context.Entry(ingredient).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(id))
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

        // POST: api/Ingredient
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ingredient>> PostIngredient(IngredientDTO ingredientDTO, string token, int idUser)
        {
            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, idUser);

            if (isValidToken)
            {
                var ingredient = DTOUtils.DTOToIngredient(ingredientDTO);
                var ingredients = await _context.Ingredients.ToListAsync();
                Ingredient ingredientExists = null;

                foreach (Ingredient i in ingredients)
                {
                    // ingredient déjà existant
                    if (ingredientDTO.CodeBarre.Equals(i.CodeBarre))
                    {
                        ingredientExists = i;
                    }
                }

                if (ingredientExists != null)
                {
                    ingredientExists.Quantite = ingredientExists.Quantite += 1;

                    _context.Entry(ingredientExists).State = EntityState.Modified;
                }
                else
                {
                    _context.Ingredients.Add(ingredient);
                    await _context.SaveChangesAsync();

                    var frigo = new Frigo
                    {
                        UtilisateurIdUtilisateur = idUser,
                        IngredientIdIngredient = ingredient.IdIngredient
                    };

                    _context.Frigos.Add(frigo);
                }

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetIngredient", new { id = ingredient.IdIngredient }, ingredientDTO);
            }

            return Unauthorized();
        }

        // DELETE: api/Ingredient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id, int userId, string token)
        {
            Boolean isValidToken = await TokenUtils.VerifyJWT(token, _context, userId);

            if (isValidToken)
            {
                var ingredient = await _context.Ingredients.FindAsync(id);
                if (ingredient == null)
                {
                    return NotFound();
                }

                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.IdIngredient == id);
        }
    }
}