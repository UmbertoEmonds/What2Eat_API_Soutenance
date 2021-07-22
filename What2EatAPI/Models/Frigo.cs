using System;
using System.Collections.Generic;

#nullable disable

namespace What2EatAPI
{
    public partial class Frigo
    {
        public int UtilisateurIdUtilisateur { get; set; }
        public int IngredientIdIngredient { get; set; }

        public virtual Ingredient IngredientIdIngredientNavigation { get; set; }
        public virtual Utilisateur UtilisateurIdUtilisateurNavigation { get; set; }
    }
}
