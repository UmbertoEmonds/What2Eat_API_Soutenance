using System;
using System.Collections.Generic;

#nullable disable

namespace What2EatAPI
{
    public partial class Favori
    {
        public int IdFavori { get; set; }
        public int UtilisateurIdUtilisateur { get; set; }
        public int RecetteIdRecette { get; set; }

        public virtual Recette RecetteIdRecetteNavigation { get; set; }
        public virtual Utilisateur UtilisateurIdUtilisateurNavigation { get; set; }
    }
}
