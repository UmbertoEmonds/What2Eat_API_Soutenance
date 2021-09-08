using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace What2EatAPI
{
    public partial class Categorie
    {
        public Categorie()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategorie { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}