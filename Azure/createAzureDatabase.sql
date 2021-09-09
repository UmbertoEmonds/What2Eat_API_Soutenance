-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema What2Eat
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema What2Eat
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS What2Eat DEFAULT CHARACTER SET utf8 ;
USE What2Eat ;

-- -----------------------------------------------------
-- Table What2Eat.Image
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS What2Eat.Image (
  idImage INT NOT NULL AUTO_INCREMENT,
  image_64 TEXT NULL DEFAULT NULL,
  image_path VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (idImage))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table What2Eat.Recette
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS What2Eat.Recette (
  idRecette INT NOT NULL AUTO_INCREMENT,
  nom VARCHAR(255) NULL DEFAULT NULL,
  Image_idImage INT NULL,
  temps INT NULL DEFAULT NULL,
  personne INT NULL DEFAULT NULL,
  PRIMARY KEY (idRecette),
  INDEX fk_Recette_Image1_idx (Image_idImage ASC) ,
  CONSTRAINT fk_Recette_Image1
    FOREIGN KEY (Image_idImage)
    REFERENCES What2Eat.Image (idImage))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table What2Eat.Etape
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS What2Eat.Etape (
  idEtape INT NOT NULL AUTO_INCREMENT,
  description VARCHAR(255) NULL DEFAULT NULL,
  position INT NULL DEFAULT NULL,
  Recette_idRecette INT NULL,
  PRIMARY KEY (idEtape),
  INDEX fk_Etape_Recette1_idx (Recette_idRecette ASC) ,
  CONSTRAINT fk_Etape_Recette1
    FOREIGN KEY (Recette_idRecette)
    REFERENCES What2Eat.Recette (idRecette))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table What2Eat.Utilisateur
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS What2Eat.Utilisateur (
  idUtilisateur INT NOT NULL AUTO_INCREMENT,
  nom VARCHAR(255) NULL DEFAULT NULL,
  prenom VARCHAR(255) NULL DEFAULT NULL,
  naissance DATE NULL,
  mail VARCHAR(255) NULL DEFAULT NULL,
  mot_de_passe VARCHAR(255) NULL DEFAULT NULL,
  Image_idImage INT NULL,
  Frigo_idFrigo INT NULL,
  PRIMARY KEY (idUtilisateur),
  INDEX fk_Utilisateur_Image1_idx (Image_idImage ASC) ,
  CONSTRAINT fk_Utilisateur_Image1
    FOREIGN KEY (Image_idImage)
    REFERENCES What2Eat.Image (idImage))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table What2Eat.Favori
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS What2Eat.Favori (
  idFavori INT NOT NULL AUTO_INCREMENT,
  Utilisateur_idUtilisateur INT NOT NULL,
  Recette_idRecette INT NOT NULL,
  PRIMARY KEY (idFavori, Utilisateur_idUtilisateur, Recette_idRecette),
  INDEX fk_Utilisateur_has_Recette_Recette1_idx (Recette_idRecette ASC) ,
  INDEX fk_Utilisateur_has_Recette_Utilisateur1_idx (Utilisateur_idUtilisateur ASC) ,
  CONSTRAINT fk_Utilisateur_has_Recette_Recette1
    FOREIGN KEY (Recette_idRecette)
    REFERENCES What2Eat.Recette (idRecette),
  CONSTRAINT fk_Utilisateur_has_Recette_Utilisateur1
    FOREIGN KEY (Utilisateur_idUtilisateur)
    REFERENCES What2Eat.Utilisateur (idUtilisateur))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table What2Eat.Categorie
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS What2Eat.Categorie (
  idCategorie INT NOT NULL AUTO_INCREMENT,
  nom VARCHAR(255) NULL,
  PRIMARY KEY (idCategorie))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table What2Eat.Ingredient
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS What2Eat.Ingredient (
  idIngredient INT NOT NULL AUTO_INCREMENT,
  nom VARCHAR(255) NULL DEFAULT NULL,
  codeBarre VARCHAR(20) NULL DEFAULT NULL,
  quantite INT NULL DEFAULT NULL,
  unite VARCHAR(255) NULL,
  Image_idImage INT NULL,
  Categorie_idCategorie INT NULL,
  PRIMARY KEY (idIngredient),
  INDEX fk_Ingredient_Image1_idx (Image_idImage ASC) ,
  INDEX fk_Ingredient_Categorie1_idx (Categorie_idCategorie ASC) ,
  CONSTRAINT fk_Ingredient_Image1
    FOREIGN KEY (Image_idImage)
    REFERENCES What2Eat.Image (idImage),
  CONSTRAINT fk_Ingredient_Categorie1
    FOREIGN KEY (Categorie_idCategorie)
    REFERENCES What2Eat.Categorie (idCategorie)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table What2Eat.Ingredient_has_Recette
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS What2Eat.Ingredient_has_Recette (
  idIngredient_has_Recettecol INT NOT NULL AUTO_INCREMENT,
  Ingredient_idIngredient INT NOT NULL,
  Recette_idRecette INT NOT NULL,
  PRIMARY KEY (idIngredient_has_Recettecol, Ingredient_idIngredient, Recette_idRecette),
  INDEX fk_Ingredient_has_Recette_Recette1_idx (Recette_idRecette ASC) ,
  INDEX fk_Ingredient_has_Recette_Ingredient1_idx (Ingredient_idIngredient ASC) ,
  CONSTRAINT fk_Ingredient_has_Recette_Ingredient1
    FOREIGN KEY (Ingredient_idIngredient)
    REFERENCES What2Eat.Ingredient (idIngredient),
  CONSTRAINT fk_Ingredient_has_Recette_Recette1
    FOREIGN KEY (Recette_idRecette)
    REFERENCES What2Eat.Recette (idRecette))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table What2Eat.ListCourse
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS What2Eat.ListCourse (
  idListCourse INT NOT NULL AUTO_INCREMENT,
  Utilisateur_idUtilisateur INT NOT NULL,
  Ingredient_idIngredient INT NOT NULL,
  PRIMARY KEY (idListCourse, Utilisateur_idUtilisateur, Ingredient_idIngredient),
  INDEX fk_Utilisateur_has_Ingredient_Ingredient1_idx (Ingredient_idIngredient ASC) ,
  INDEX fk_Utilisateur_has_Ingredient_Utilisateur1_idx (Utilisateur_idUtilisateur ASC) ,
  CONSTRAINT fk_Utilisateur_has_Ingredient_Ingredient1
    FOREIGN KEY (Ingredient_idIngredient)
    REFERENCES What2Eat.Ingredient (idIngredient),
  CONSTRAINT fk_Utilisateur_has_Ingredient_Utilisateur1
    FOREIGN KEY (Utilisateur_idUtilisateur)
    REFERENCES What2Eat.Utilisateur (idUtilisateur))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table What2Eat.Frigo
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS What2Eat.Frigo (
  Utilisateur_idUtilisateur INT NOT NULL,
  Ingredient_idIngredient INT NOT NULL,
  PRIMARY KEY (Utilisateur_idUtilisateur, Ingredient_idIngredient),
  INDEX fk_Utilisateur_has_Ingredient_Ingredient2_idx (Ingredient_idIngredient ASC) ,
  INDEX fk_Utilisateur_has_Ingredient_Utilisateur2_idx (Utilisateur_idUtilisateur ASC) ,
  CONSTRAINT fk_Utilisateur_has_Ingredient_Utilisateur2
    FOREIGN KEY (Utilisateur_idUtilisateur)
    REFERENCES What2Eat.Utilisateur (idUtilisateur)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_Utilisateur_has_Ingredient_Ingredient2
    FOREIGN KEY (Ingredient_idIngredient)
    REFERENCES What2Eat.Ingredient (idIngredient)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;



