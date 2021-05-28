namespace MaClassePA.Models.Compte
{
    using System.ComponentModel.DataAnnotations;

    public class EnregistrerModel
    {
        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Le {0} doit faire au moins entre {2} et {1} caractères de long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe : confirmation")]
        [Compare("MotDePasse", ErrorMessage = "Les 2 entrées de mots de passe ne condornent pas.")]
        public string ConfirmeMDP { get; set; }

        public string UrlRetour { get; set; }
    }
}
