namespace MaClassePA.Data
{
    using MaClassePA.Models;

    using System.Linq;

    public interface IClassesContext
    {
        public IQueryable<ClasseModel> Classes { get; }
        public IQueryable<MatiereModel> Matieres { get; }
        public IQueryable<RessourceModel> Ressources { get; }

        public void AjouterClasse(ClasseModel classe);
        public void EditerClasse(ClasseModel classe);
        public void SupprimerClasse(ClasseModel classe);

        public void AjouterMatiere(MatiereModel matiere);
        public void EditerMatiere(MatiereModel matiere);
        public void SupprimerMatiere(MatiereModel matiere);

        public void AjouterRessource(RessourceModel ressource);
        public void EditerRessource(RessourceModel ressource);
        public void SupprimerRessource(RessourceModel ressource);

        public void Sauvegarder();
    }
}
