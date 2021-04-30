namespace MaClassePA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MatiereModel : Base
    {
        [Required(ErrorMessage = "Le nom de la classe est requis.")]
        public string Nom { get; set; } = "Nom par défaut";

        public virtual List<RessourceModel> Ressources { get; set; } = new();

        public virtual ClasseModel Classe { get; set; }
        public int ClasseId { get; set; }

        [Display(Name = "Nom")]
        public string NomComplet => $"{Nom} ({Id})";
    }
}
