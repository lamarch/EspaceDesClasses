namespace MaClassePA.Models.Compte
{
    using System.ComponentModel.DataAnnotations;

    public class ConnecterModel
    {
        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Le {0} doit faire au moins entre {2} et {1} caractères de long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }

        public string UrlRetour { get; set; }
        [Display(Name = "Se souvenir de moi")]
        public bool SeSouvenir { get; set; }
    }
}
