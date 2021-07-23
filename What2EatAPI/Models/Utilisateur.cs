using System;
using System.Collections.Generic;

#nullable disable

namespace What2EatAPI
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Favoris = new HashSet<Favori>();
            Frigos = new HashSet<Frigo>();
            ListCourses = new HashSet<ListCourse>();
        }

        public int IdUtilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime? Naissance { get; set; }
        public string Mail { get; set; }
        public string MotDePasse { get; set; }
        public string Token { get; set; }
        public int? ImageIdImage { get; set; }

        public virtual Image ImageIdImageNavigation { get; set; }
        public virtual ICollection<Favori> Favoris { get; set; }
        public virtual ICollection<Frigo> Frigos { get; set; }
        public virtual ICollection<ListCourse> ListCourses { get; set; }
    }
}
