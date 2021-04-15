namespace MaClassePA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MatiereModel : Base
    {
        public string Nom { get; set; }

        public virtual List<RessourceModel> Ressources { get; set; } = new();

        public virtual ClasseModel Classe { get; set; }
        public int ClasseId { get; set; }

        [Display(Name = "Nom")]
        public string NomComplet => $"{Nom} ({Id})";
    }
}
