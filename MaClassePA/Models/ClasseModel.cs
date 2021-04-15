namespace MaClassePA.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ClasseModel : Base
    {
        public string Nom { get; set; }

        public virtual List<MatiereModel> Matieres { get; set; }

        [Display(Name = "Nom")]
        public string NomComplet => Nom + " (" + Id + ")";
    }
}
