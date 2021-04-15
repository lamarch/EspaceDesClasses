using System;
using System.ComponentModel.DataAnnotations;

namespace MaClassePA.Models
{
    public class RessourceModel : Base, ITimeTracker
    {
        public string Nom { get; set; }

        public string Contenu { get; set; }

        public virtual MatiereModel Matiere { get; set; }
        public int MatiereId { get; set; }

        [Display(Name = "Nom")]
        public string NomComplet => $"{Nom} ({Id})";

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
