SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

DROP SCHEMA IF EXISTS `emprestae_db` ;
CREATE SCHEMA IF NOT EXISTS `emprestae_db` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `emprestae_db` ;

-- -----------------------------------------------------
-- Table `emprestae_db`.`objLivro`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `emprestae_db`.`objLivro` ;

CREATE  TABLE IF NOT EXISTS `emprestae_db`.`objLivro` (
  `idLivro` INT NOT NULL AUTO_INCREMENT ,
  `titulo` VARCHAR(255) NOT NULL ,
  `autor` VARCHAR(45) NULL ,
  `edicao` INT NULL ,
  `editora` VARCHAR(255) NULL ,
  PRIMARY KEY (`idLivro`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `emprestae_db`.`usuario`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `emprestae_db`.`usuario` ;

CREATE  TABLE IF NOT EXISTS `emprestae_db`.`usuario` (
  `idusuario` INT NOT NULL AUTO_INCREMENT ,
  `email` VARCHAR(255) NOT NULL ,
  `nome` VARCHAR(255) NULL ,
  `senha` VARCHAR(255) NOT NULL ,
  `addressLat` INT NULL ,
  `addressLong` INT NULL ,
  PRIMARY KEY (`idusuario`) ,
  UNIQUE INDEX `email_UNIQUE` (`email` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `emprestae_db`.`amizade`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `emprestae_db`.`amizade` ;

CREATE  TABLE IF NOT EXISTS `emprestae_db`.`amizade` (
  `idamizade` INT NOT NULL AUTO_INCREMENT ,
  `idusuario_a` INT NOT NULL ,
  `idusuario_b` INT NOT NULL ,
  PRIMARY KEY (`idamizade`) ,
  INDEX `idusuario_a` (`idusuario_a` ASC) ,
  INDEX `idusuario_b` (`idusuario_b` ASC) ,
  CONSTRAINT `idusuario_a`
    FOREIGN KEY (`idusuario_a` )
    REFERENCES `emprestae_db`.`usuario` (`idusuario` )
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `idusuario_b`
    FOREIGN KEY (`idusuario_b` )
    REFERENCES `emprestae_db`.`usuario` (`idusuario` )
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `emprestae_db`.`objJogo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `emprestae_db`.`objJogo` ;

CREATE  TABLE IF NOT EXISTS `emprestae_db`.`objJogo` (
  `idJogo` INT NOT NULL AUTO_INCREMENT ,
  `titulo` VARCHAR(45) NOT NULL ,
  `plataforma` VARCHAR(45) NULL ,
  `produtora` VARCHAR(45) NULL ,
  PRIMARY KEY (`idJogo`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `emprestae_db`.`objFilme`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `emprestae_db`.`objFilme` ;

CREATE  TABLE IF NOT EXISTS `emprestae_db`.`objFilme` (
  `idFilme` INT NOT NULL AUTO_INCREMENT ,
  `titulo` VARCHAR(45) NOT NULL ,
  `distribuidora` VARCHAR(45) NULL ,
  `diretor` VARCHAR(45) NULL ,
  PRIMARY KEY (`idFilme`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `emprestae_db`.`emprestimo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `emprestae_db`.`emprestimo` ;

CREATE  TABLE IF NOT EXISTS `emprestae_db`.`emprestimo` (
  `idemprestimo` INT NOT NULL AUTO_INCREMENT ,
  `idamigos` INT NOT NULL ,
  `tipoObjeto` VARCHAR(15) NOT NULL ,
  `idobjeto` INT NOT NULL ,
  `dtemprestimo` DATETIME NULL ,
  `dtdevolucao` DATETIME NULL ,
  `devolvido` TINYINT(1) NULL ,
  PRIMARY KEY (`idemprestimo`) ,
  INDEX `idamigos` (`idamigos` ASC) ,
  CONSTRAINT `idamigos`
    FOREIGN KEY (`idamigos` )
    REFERENCES `emprestae_db`.`amizade` (`idamizade` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `emprestae_db`.`possuiLivro`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `emprestae_db`.`possuiLivro` ;

CREATE  TABLE IF NOT EXISTS `emprestae_db`.`possuiLivro` (
  `fk_idUser` INT NOT NULL ,
  `fk_idLivro` INT NOT NULL ,
  INDEX `fk_idUser` (`fk_idUser` ASC) ,
  INDEX `fk_idLivro` (`fk_idLivro` ASC) ,
  CONSTRAINT `possuiLivro_fk_idUser`
    FOREIGN KEY (`fk_idUser` )
    REFERENCES `emprestae_db`.`usuario` (`idusuario` )
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `idlivro`
    FOREIGN KEY (`fk_idLivro` )
    REFERENCES `emprestae_db`.`objLivro` (`idLivro` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `emprestae_db`.`possuiJogo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `emprestae_db`.`possuiJogo` ;

CREATE  TABLE IF NOT EXISTS `emprestae_db`.`possuiJogo` (
  `fk_idUser` INT NOT NULL ,
  `fk_idJogo` INT NOT NULL ,
  INDEX `fk_idUser` (`fk_idUser` ASC) ,
  INDEX `fk_idJogo` (`fk_idJogo` ASC) ,
  CONSTRAINT `possuiJogo_fk_idUser`
    FOREIGN KEY (`fk_idUser` )
    REFERENCES `emprestae_db`.`usuario` (`idusuario` )
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `idjogo`
    FOREIGN KEY (`fk_idJogo` )
    REFERENCES `emprestae_db`.`objJogo` (`idJogo` )
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `emprestae_db`.`possuiFilme`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `emprestae_db`.`possuiFilme` ;

CREATE  TABLE IF NOT EXISTS `emprestae_db`.`possuiFilme` (
  `fk_idUser` INT NOT NULL ,
  `fk_idFilme` INT NOT NULL ,
  INDEX `fk_idUser` (`fk_idUser` ASC) ,
  INDEX `fk_idFilme` (`fk_idFilme` ASC) ,
  CONSTRAINT `possuiFilme_fk_idUser`
    FOREIGN KEY (`fk_idUser` )
    REFERENCES `emprestae_db`.`usuario` (`idusuario` )
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `idfilme`
    FOREIGN KEY (`fk_idFilme` )
    REFERENCES `emprestae_db`.`objFilme` (`idFilme` )
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
