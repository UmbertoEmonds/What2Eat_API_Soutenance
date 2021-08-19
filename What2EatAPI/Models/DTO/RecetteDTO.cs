using System;
namespace What2EatAPI.Models.DTO
{
    public class RecetteDTO
    {
        public string Nom { get; set; }
        public string ImageUrl { get; set; }
        public int? Temps { get; set; }
        public int? Personne { get; set; }
    }
}
