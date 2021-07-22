using System;
using System.Collections.Generic;

#nullable disable

namespace What2EatAPI
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            Frigos = new HashSet<Frigo>();
            IngredientHasRecettes = new HashSet<IngredientHasRecette>();
            ListCourses = new HashSet<ListCourse>();
        }

        public int IdIngredient { get; set; }
        public string Nom { get; set; }
        public string CodeBarre { get; set; }
        public int? Quantite { get; set; }
        public string Unite { get; set; }
        public int? ImageIdImage { get; set; }
        public int? CategorieIdCategorie { get; set; }

        public virtual Categorie CategorieIdCategorieNavigation { get; set; }
        public virtual Image ImageIdImageNavigation { get; set; }
        public virtual ICollection<Frigo> Frigos { get; set; }
        public virtual ICollection<IngredientHasRecette> IngredientHasRecettes { get; set; }
        public virtual ICollection<ListCourse> ListCourses { get; set; }
    }
}
