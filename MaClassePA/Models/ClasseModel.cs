namespace MaClassePA.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ClasseModel : Base
    {
        [Required(ErrorMessage = "Le nom de la classe est requis.")]
        public string Nom { get; set; } = "Nom par défaut";

        public virtual List<MatiereModel> Matieres { get; set; }

        [Display(Name = "Nom")]
        public string NomComplet => Nom + " (" + Id + ")";
    }
}
