using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdImage { get; set; }
        public string Image64 { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Recette> Recettes { get; set; }
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
