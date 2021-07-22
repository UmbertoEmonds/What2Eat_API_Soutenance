using System;
using System.Collections.Generic;

#nullable disable

namespace What2EatAPI
{
    public partial class Etape
    {
        public int IdEtape { get; set; }
        public string Description { get; set; }
        public int? Position { get; set; }
        public int? RecetteIdRecette { get; set; }

        public virtual Recette RecetteIdRecetteNavigation { get; set; }
    }
}
