using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace What2EatAPI
{
    public partial class what2eatContext : DbContext
    {

        public what2eatContext()
        {
        }

        public what2eatContext(DbContextOptions<what2eatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorie> Categories { get; set; }
        public virtual DbSet<Etape> Etapes { get; set; }
        public virtual DbSet<Favori> Favoris { get; set; }
        public virtual DbSet<Frigo> Frigos { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<IngredientHasRecette> IngredientHasRecettes { get; set; }
        public virtual DbSet<ListCourse> ListCourses { get; set; }
        public virtual DbSet<Recette> Recettes { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost;Port=8889;Database=what2eat;Uid=root;Pwd=root;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.HasKey(e => e.IdCategorie)
                    .HasName("PRIMARY");

                entity.ToTable("Categorie");

                entity.Property(e => e.IdCategorie)
                    .HasColumnType("int(11)")
                    .HasColumnName("idCategorie");

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<Etape>(entity =>
            {
                entity.HasKey(e => e.IdEtape)
                    .HasName("PRIMARY");

                entity.ToTable("Etape");

                entity.HasIndex(e => e.RecetteIdRecette, "fk_Etape_Recette1_idx");

                entity.Property(e => e.IdEtape)
                    .HasColumnType("int(11)")
                    .HasColumnName("idEtape");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Position)
                    .HasColumnType("int(11)")
                    .HasColumnName("position");

                entity.Property(e => e.RecetteIdRecette)
                    .HasColumnType("int(11)")
                    .HasColumnName("Recette_idRecette");

                entity.HasOne(d => d.RecetteIdRecetteNavigation)
                    .WithMany(p => p.Etapes)
                    .HasForeignKey(d => d.RecetteIdRecette)
                    .HasConstraintName("fk_Etape_Recette1");
            });

            modelBuilder.Entity<Favori>(entity =>
            {
                entity.HasKey(e => new { e.IdFavori, e.UtilisateurIdUtilisateur, e.RecetteIdRecette })
                    .HasName("PRIMARY");

                entity.ToTable("Favori");

                entity.HasIndex(e => e.RecetteIdRecette, "fk_Utilisateur_has_Recette_Recette1_idx");

                entity.HasIndex(e => e.UtilisateurIdUtilisateur, "fk_Utilisateur_has_Recette_Utilisateur1_idx");

                entity.Property(e => e.IdFavori)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idFavori");

                entity.Property(e => e.UtilisateurIdUtilisateur)
                    .HasColumnType("int(11)")
                    .HasColumnName("Utilisateur_idUtilisateur");

                entity.Property(e => e.RecetteIdRecette)
                    .HasColumnType("int(11)")
                    .HasColumnName("Recette_idRecette");

                entity.HasOne(d => d.RecetteIdRecetteNavigation)
                    .WithMany(p => p.Favoris)
                    .HasForeignKey(d => d.RecetteIdRecette)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Utilisateur_has_Recette_Recette1");

                entity.HasOne(d => d.UtilisateurIdUtilisateurNavigation)
                    .WithMany(p => p.Favoris)
                    .HasForeignKey(d => d.UtilisateurIdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Utilisateur_has_Recette_Utilisateur1");
            });

            modelBuilder.Entity<Frigo>(entity =>
            {
                entity.HasKey(e => new { e.UtilisateurIdUtilisateur, e.IngredientIdIngredient })
                    .HasName("PRIMARY");

                entity.ToTable("Frigo");

                entity.HasIndex(e => e.IngredientIdIngredient, "fk_Utilisateur_has_Ingredient_Ingredient2_idx");

                entity.HasIndex(e => e.UtilisateurIdUtilisateur, "fk_Utilisateur_has_Ingredient_Utilisateur2_idx");

                entity.Property(e => e.UtilisateurIdUtilisateur)
                    .HasColumnType("int(11)")
                    .HasColumnName("Utilisateur_idUtilisateur");

                entity.Property(e => e.IngredientIdIngredient)
                    .HasColumnType("int(11)")
                    .HasColumnName("Ingredient_idIngredient");

                entity.HasOne(d => d.IngredientIdIngredientNavigation)
                    .WithMany(p => p.Frigos)
                    .HasForeignKey(d => d.IngredientIdIngredient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Utilisateur_has_Ingredient_Ingredient2");

                entity.HasOne(d => d.UtilisateurIdUtilisateurNavigation)
                    .WithMany(p => p.Frigos)
                    .HasForeignKey(d => d.UtilisateurIdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Utilisateur_has_Ingredient_Utilisateur2");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.IdImage)
                    .HasName("PRIMARY");

                entity.ToTable("Image");

                entity.Property(e => e.IdImage)
                    .HasColumnType("int(11)")
                    .HasColumnName("idImage");

                entity.Property(e => e.Image64).HasColumnName("image_64");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(255)
                    .HasColumnName("image_path");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(e => e.IdIngredient)
                    .HasName("PRIMARY");

                entity.ToTable("Ingredient");

                entity.HasIndex(e => e.CategorieIdCategorie, "fk_Ingredient_Categorie1_idx");

                entity.HasIndex(e => e.ImageIdImage, "fk_Ingredient_Image1_idx");

                entity.Property(e => e.IdIngredient)
                    .HasColumnType("int(11)")
                    .HasColumnName("idIngredient");

                entity.Property(e => e.CategorieIdCategorie)
                    .HasColumnType("int(11)")
                    .HasColumnName("Categorie_idCategorie");

                entity.Property(e => e.CodeBarre)
                    .HasMaxLength(20)
                    .HasColumnName("codeBarre");

                entity.Property(e => e.ImageIdImage)
                    .HasColumnType("int(11)")
                    .HasColumnName("Image_idImage");

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .HasColumnName("nom");

                entity.Property(e => e.Quantite)
                    .HasColumnType("int(11)")
                    .HasColumnName("quantite");

                entity.Property(e => e.Unite)
                    .HasMaxLength(255)
                    .HasColumnName("unite");

                entity.HasOne(d => d.CategorieIdCategorieNavigation)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.CategorieIdCategorie)
                    .HasConstraintName("fk_Ingredient_Categorie1");

                entity.HasOne(d => d.ImageIdImageNavigation)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.ImageIdImage)
                    .HasConstraintName("fk_Ingredient_Image1");
            });

            modelBuilder.Entity<IngredientHasRecette>(entity =>
            {
                entity.HasKey(e => new { e.IdIngredientHasRecettecol, e.IngredientIdIngredient, e.RecetteIdRecette })
                    .HasName("PRIMARY");

                entity.ToTable("Ingredient_has_Recette");

                entity.HasIndex(e => e.IngredientIdIngredient, "fk_Ingredient_has_Recette_Ingredient1_idx");

                entity.HasIndex(e => e.RecetteIdRecette, "fk_Ingredient_has_Recette_Recette1_idx");

                entity.Property(e => e.IdIngredientHasRecettecol)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idIngredient_has_Recettecol");

                entity.Property(e => e.IngredientIdIngredient)
                    .HasColumnType("int(11)")
                    .HasColumnName("Ingredient_idIngredient");

                entity.Property(e => e.RecetteIdRecette)
                    .HasColumnType("int(11)")
                    .HasColumnName("Recette_idRecette");

                entity.HasOne(d => d.IngredientIdIngredientNavigation)
                    .WithMany(p => p.IngredientHasRecettes)
                    .HasForeignKey(d => d.IngredientIdIngredient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ingredient_has_Recette_Ingredient1");

                entity.HasOne(d => d.RecetteIdRecetteNavigation)
                    .WithMany(p => p.IngredientHasRecettes)
                    .HasForeignKey(d => d.RecetteIdRecette)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ingredient_has_Recette_Recette1");
            });

            modelBuilder.Entity<ListCourse>(entity =>
            {
                entity.HasKey(e => new { e.IdListCourse, e.UtilisateurIdUtilisateur, e.IngredientIdIngredient })
                    .HasName("PRIMARY");

                entity.ToTable("ListCourse");

                entity.HasIndex(e => e.IngredientIdIngredient, "fk_Utilisateur_has_Ingredient_Ingredient1_idx");

                entity.HasIndex(e => e.UtilisateurIdUtilisateur, "fk_Utilisateur_has_Ingredient_Utilisateur1_idx");

                entity.Property(e => e.IdListCourse)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idListCourse");

                entity.Property(e => e.UtilisateurIdUtilisateur)
                    .HasColumnType("int(11)")
                    .HasColumnName("Utilisateur_idUtilisateur");

                entity.Property(e => e.IngredientIdIngredient)
                    .HasColumnType("int(11)")
                    .HasColumnName("Ingredient_idIngredient");

                entity.HasOne(d => d.IngredientIdIngredientNavigation)
                    .WithMany(p => p.ListCourses)
                    .HasForeignKey(d => d.IngredientIdIngredient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Utilisateur_has_Ingredient_Ingredient1");

                entity.HasOne(d => d.UtilisateurIdUtilisateurNavigation)
                    .WithMany(p => p.ListCourses)
                    .HasForeignKey(d => d.UtilisateurIdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Utilisateur_has_Ingredient_Utilisateur1");
            });

            modelBuilder.Entity<Recette>(entity =>
            {
                entity.HasKey(e => e.IdRecette)
                    .HasName("PRIMARY");

                entity.ToTable("Recette");

                entity.HasIndex(e => e.ImageIdImage, "fk_Recette_Image1_idx");

                entity.Property(e => e.IdRecette)
                    .HasColumnType("int(11)")
                    .HasColumnName("idRecette");

                entity.Property(e => e.ImageIdImage)
                    .HasColumnType("int(11)")
                    .HasColumnName("Image_idImage");

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .HasColumnName("nom");

                entity.Property(e => e.Personne)
                    .HasColumnType("int(11)")
                    .HasColumnName("personne");

                entity.Property(e => e.Temps)
                    .HasColumnType("int(11)")
                    .HasColumnName("temps");

                entity.HasOne(d => d.ImageIdImageNavigation)
                    .WithMany(p => p.Recettes)
                    .HasForeignKey(d => d.ImageIdImage)
                    .HasConstraintName("fk_Recette_Image1");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.IdUtilisateur)
                    .HasName("PRIMARY");

                entity.ToTable("Utilisateur");

                entity.HasIndex(e => e.ImageIdImage, "fk_Utilisateur_Image1_idx");

                entity.Property(e => e.IdUtilisateur)
                    .HasColumnType("int(11)")
                    .HasColumnName("idUtilisateur");

                entity.Property(e => e.ImageIdImage)
                    .HasColumnType("int(11)")
                    .HasColumnName("Image_idImage");

                entity.Property(e => e.Mail)
                    .HasMaxLength(255)
                    .HasColumnName("mail");

                entity.Property(e => e.MotDePasse)
                    .HasMaxLength(255)
                    .HasColumnName("mot_de_passe");

                entity.Property(e => e.Naissance)
                    .HasColumnType("date")
                    .HasColumnName("naissance");

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .HasColumnName("nom");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(255)
                    .HasColumnName("prenom");

                entity.HasOne(d => d.ImageIdImageNavigation)
                    .WithMany(p => p.Utilisateurs)
                    .HasForeignKey(d => d.ImageIdImage)
                    .HasConstraintName("fk_Utilisateur_Image1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
