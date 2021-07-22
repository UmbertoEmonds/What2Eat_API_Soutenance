using System;
using System.Collections.Generic;

#nullable disable

namespace What2EatAPI
{
    public partial class Categorie
    {
        public Categorie()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        public int IdCategorie { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
