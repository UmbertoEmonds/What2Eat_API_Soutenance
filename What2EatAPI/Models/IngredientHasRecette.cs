using System;
using System.Collections.Generic;

#nullable disable

namespace What2EatAPI
{
    public partial class IngredientHasRecette
    {
        public int IdIngredientHasRecettecol { get; set; }
        public int IngredientIdIngredient { get; set; }
        public int RecetteIdRecette { get; set; }

        public virtual Ingredient IngredientIdIngredientNavigation { get; set; }
        public virtual Recette RecetteIdRecetteNavigation { get; set; }
    }
}
