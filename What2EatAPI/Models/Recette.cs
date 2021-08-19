using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace What2EatAPI
{
    public partial class Recette
    {
        public Recette()
        {
            Etapes = new HashSet<Etape>();
            Favoris = new HashSet<Favori>();
            IngredientHasRecettes = new HashSet<IngredientHasRecette>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRecette { get; set; }
        public string Nom { get; set; }
        public int? ImageIdImage { get; set; }
        public int? Temps { get; set; }
        public int? Personne { get; set; }

        public virtual Image ImageIdImageNavigation { get; set; }
        public virtual ICollection<Etape> Etapes { get; set; }
        public virtual ICollection<Favori> Favoris { get; set; }
        public virtual ICollection<IngredientHasRecette> IngredientHasRecettes { get; set; }
    }
}
