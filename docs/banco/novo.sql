SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

CREATE SCHEMA IF NOT EXISTS `emprestei` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `emprestei` ;

-- -----------------------------------------------------
-- Table `emprestei`.`livro`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `emprestei`.`livro` (
  `idlivro` INT NOT NULL AUTO_INCREMENT ,
  `titulo` VARCHAR(255) NULL ,
  `editora` VARCHAR(255) NULL ,
  `edicao` INT NULL ,
  PRIMARY KEY (`idlivro`) ,
  UNIQUE INDEX `idlivro_UNIQUE` (`idlivro` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `emprestei`.`acervo`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `emprestei`.`acervo` (
  `idacervo` INT NOT NULL AUTO_INCREMENT ,
  `idlivro` INT NOT NULL ,
  PRIMARY KEY (`idacervo`) ,
  UNIQUE INDEX `idacervo_UNIQUE` (`idacervo` ASC) ,
  UNIQUE INDEX `idlivro_UNIQUE` (`idlivro` ASC) ,
  INDEX `idlivro` (`idlivro` ASC) ,
  CONSTRAINT `idlivro`
    FOREIGN KEY (`idlivro` )
    REFERENCES `emprestei`.`livro` (`idlivro` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `emprestei`.`usuario`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `emprestei`.`usuario` (
  `idusuario` INT NOT NULL AUTO_INCREMENT ,
  `email` VARCHAR(255) NULL ,
  `nome` VARCHAR(255) NULL ,
  `senha` VARCHAR(255) NULL ,
  `idacervo` INT NOT NULL ,
  PRIMARY KEY (`idusuario`) ,
  UNIQUE INDEX `idusuario_UNIQUE` (`idusuario` ASC) ,
  UNIQUE INDEX `idacervo_UNIQUE` (`idacervo` ASC) ,
  INDEX `idacervo` (`idacervo` ASC) ,
  CONSTRAINT `idacervo`
    FOREIGN KEY (`idacervo` )
    REFERENCES `emprestei`.`acervo` (`idacervo` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
