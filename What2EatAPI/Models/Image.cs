using System;
using System.Collections.Generic;

#nullable disable

namespace What2EatAPI
{
    public partial class Image
    {
        public Image()
        {
            Ingredients = new HashSet<Ingredient>();
            Recettes = new HashSet<Recette>();
            Utilisateurs = new HashSet<Utilisateur>();
        }

        public int IdImage { get; set; }
        public string Image64 { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Recette> Recettes { get; set; }
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
