namespace MaClassePA.Data
{
    using MaClassePA.Models;

    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;

    public class ClassesDbContext : DbContext, IClassesContext
    {
        public ClassesDbContext(DbContextOptions<ClassesDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<ClasseModel> Classes { get; set; }
        public DbSet<MatiereModel> Matieres { get; set; }
        public DbSet<RessourceModel> Ressources { get; set; }

        IQueryable<ClasseModel> IClassesContext.Classes => this.Classes;
        IQueryable<MatiereModel> IClassesContext.Matieres => this.Matieres;
        IQueryable<RessourceModel> IClassesContext.Ressources => this.Ressources;

        public void AjouterClasse(ClasseModel classe) => Classes.Add(classe);
        public void EditerClasse(ClasseModel classe) => Classes.Update(classe).State = EntityState.Modified;
        public void SupprimerClasse(ClasseModel classe) => Classes.Remove(classe);

        public void AjouterMatiere(MatiereModel matiere) => Matieres.Add(matiere);
        public void EditerMatiere(MatiereModel matiere) => Matieres.Update(matiere).State = EntityState.Modified;
        public void SupprimerMatiere(MatiereModel matiere) => Matieres.Remove(matiere);

        public void AjouterRessource(RessourceModel ressource) => Ressources.Add(ressource);
        public void EditerRessource(RessourceModel ressource) => Ressources.Update(ressource);
        public void SupprimerRessource(RessourceModel ressource) => Ressources.Remove(ressource);

        public void Sauvegarder() => this.SaveChanges();

        public override int SaveChanges()
        {
            foreach(var entityEntry in ChangeTracker.Entries())
            {
                if(entityEntry.Entity is ITimeTracker tracker)
                {
                    if(entityEntry.State == EntityState.Added)
                    {
                        tracker.Modified = tracker.Created = DateTime.Now;
                    }
                    else if(entityEntry.State == EntityState.Modified)
                    {
                        tracker.Modified = DateTime.Now;
                    }
                }
            }

            return base.SaveChanges();
        }

    }
}
