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
  `imagePath` VARCHAR(255) NOT NULL ,
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
  `addressLat` DOUBLE NULL ,
  `addressLong` DOUBLE NULL ,
  `imagePath` VARCHAR(255) NULL ,
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
  `imagePath` VARCHAR(255) NOT NULL ,
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
  `imagePath` VARCHAR(255) NOT NULL ,
  PRIMARY KEY (`idFilme`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `emprestae_db`.`emprestimo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `emprestae_db`.`emprestimo` ;

CREATE  TABLE IF NOT EXISTS `emprestae_db`.`emprestimo` (
  `idemprestimo` INT NOT NULL AUTO_INCREMENT ,
  `fk_idUser1` INT NOT NULL ,
  `fk_idUser2` INT NOT NULL ,
  `tipoObjeto` VARCHAR(15) NOT NULL ,
  `idObj` INT NOT NULL ,
  `dtEmprestimo` DATETIME NULL ,
  `dtDevolucao` DATETIME NULL ,
  `status` TINYINT(1)  NULL ,
  PRIMARY KEY (`idemprestimo`) ,
  INDEX `fk_idUsuario1` (`fk_idUser1` ASC) ,
  INDEX `fk_idUsuario2` (`fk_idUser2` ASC) ,
  CONSTRAINT `fk_idUsuario1`
    FOREIGN KEY (`fk_idUser1` )
    REFERENCES `emprestae_db`.`usuario` (`idusuario` )
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
  CONSTRAINT `fk_idUsuario2`
    FOREIGN KEY (`fk_idUser2` )
    REFERENCES `emprestae_db`.`usuario` (`idusuario` )
    ON DELETE CASCADE
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

-- -----------------------------------------------------
-- Data for table `emprestae_db`.`objLivro`
-- -----------------------------------------------------
START TRANSACTION;
USE `emprestae_db`;
INSERT INTO `emprestae_db`.`objLivro` (`idLivro`, `titulo`, `autor`, `edicao`, `editora`, `imagePath`) VALUES (1, 'l1', 'a1', 1, 'edi1', 'http://localhost/sd/emprestei/system/WebService/services/images/objects/defaultObjectImage.jpg');
INSERT INTO `emprestae_db`.`objLivro` (`idLivro`, `titulo`, `autor`, `edicao`, `editora`, `imagePath`) VALUES (2, 'l2', 'a2', 2, 'edi2', 'http://localhost/sd/emprestei/system/WebService/services/images/objects/defaultObjectImage.jpg');
INSERT INTO `emprestae_db`.`objLivro` (`idLivro`, `titulo`, `autor`, `edicao`, `editora`, `imagePath`) VALUES (3, 'l3', 'a3', 3, 'edi3', 'http://localhost/sd/emprestei/system/WebService/services/images/objects/defaultObjectImage.jpg');

COMMIT;

-- -----------------------------------------------------
-- Data for table `emprestae_db`.`usuario`
-- -----------------------------------------------------
START TRANSACTION;
USE `emprestae_db`;
INSERT INTO `emprestae_db`.`usuario` (`idusuario`, `email`, `nome`, `senha`, `addressLat`, `addressLong`, `imagePath`) VALUES (1, 'm1', 'n1', 'p1', NULL, NULL, 'http://localhost/sd/emprestei/system/WebService/services/images/users/defaultUserImage.png');
INSERT INTO `emprestae_db`.`usuario` (`idusuario`, `email`, `nome`, `senha`, `addressLat`, `addressLong`, `imagePath`) VALUES (2, 'm2', 'n2', 'p2', NULL, NULL, 'http://localhost/sd/emprestei/system/WebService/services/images/users/defaultUserImage.png');
INSERT INTO `emprestae_db`.`usuario` (`idusuario`, `email`, `nome`, `senha`, `addressLat`, `addressLong`, `imagePath`) VALUES (3, 'm3', 'n3', 'p3', NULL, NULL, 'http://localhost/sd/emprestei/system/WebService/services/images/users/defaultUserImage.png');
INSERT INTO `emprestae_db`.`usuario` (`idusuario`, `email`, `nome`, `senha`, `addressLat`, `addressLong`, `imagePath`) VALUES (4, 'm4', 'n4', 'p4', NULL, NULL, 'http://localhost/sd/emprestei/system/WebService/services/images/users/defaultUserImage.png');
INSERT INTO `emprestae_db`.`usuario` (`idusuario`, `email`, `nome`, `senha`, `addressLat`, `addressLong`, `imagePath`) VALUES (5, 'm5', 'n5', 'p5', NULL, NULL, 'http://localhost/sd/emprestei/system/WebService/services/images/users/defaultUserImage.png');

COMMIT;

-- -----------------------------------------------------
-- Data for table `emprestae_db`.`amizade`
-- -----------------------------------------------------
START TRANSACTION;
USE `emprestae_db`;
INSERT INTO `emprestae_db`.`amizade` (`idamizade`, `idusuario_a`, `idusuario_b`) VALUES (1, 1, 2);
INSERT INTO `emprestae_db`.`amizade` (`idamizade`, `idusuario_a`, `idusuario_b`) VALUES (2, 3, 2);
INSERT INTO `emprestae_db`.`amizade` (`idamizade`, `idusuario_a`, `idusuario_b`) VALUES (3, 3, 1);

COMMIT;

-- -----------------------------------------------------
-- Data for table `emprestae_db`.`objJogo`
-- -----------------------------------------------------
START TRANSACTION;
USE `emprestae_db`;
INSERT INTO `emprestae_db`.`objJogo` (`idJogo`, `titulo`, `plataforma`, `produtora`, `imagePath`) VALUES (1, 'j1', 'p1', 'pr1', 'http://localhost/sd/emprestei/system/WebService/services/images/objects/defaultObjectImage.jpg');
INSERT INTO `emprestae_db`.`objJogo` (`idJogo`, `titulo`, `plataforma`, `produtora`, `imagePath`) VALUES (2, 'j2', 'p2', 'pr2', 'http://localhost/sd/emprestei/system/WebService/services/images/objects/defaultObjectImage.jpg');
INSERT INTO `emprestae_db`.`objJogo` (`idJogo`, `titulo`, `plataforma`, `produtora`, `imagePath`) VALUES (3, 'j3', 'p3', 'pr3', 'http://localhost/sd/emprestei/system/WebService/services/images/objects/defaultObjectImage.jpg');

COMMIT;

-- -----------------------------------------------------
-- Data for table `emprestae_db`.`objFilme`
-- -----------------------------------------------------
START TRANSACTION;
USE `emprestae_db`;
INSERT INTO `emprestae_db`.`objFilme` (`idFilme`, `titulo`, `distribuidora`, `diretor`, `imagePath`) VALUES (1, 'f1', 'dis1', 'd1', 'http://localhost/sd/emprestei/system/WebService/services/images/objects/defaultObjectImage.jpg');
INSERT INTO `emprestae_db`.`objFilme` (`idFilme`, `titulo`, `distribuidora`, `diretor`, `imagePath`) VALUES (2, 'f2', 'dis2', 'd2', 'http://localhost/sd/emprestei/system/WebService/services/images/objects/defaultObjectImage.jpg');
INSERT INTO `emprestae_db`.`objFilme` (`idFilme`, `titulo`, `distribuidora`, `diretor`, `imagePath`) VALUES (3, 'f3', 'dis3', 'd3', 'http://localhost/sd/emprestei/system/WebService/services/images/objects/defaultObjectImage.jpg');

COMMIT;

-- -----------------------------------------------------
-- Data for table `emprestae_db`.`emprestimo`
-- -----------------------------------------------------
START TRANSACTION;
USE `emprestae_db`;
INSERT INTO `emprestae_db`.`emprestimo` (`idemprestimo`, `fk_idUser1`, `fk_idUser2`, `tipoObjeto`, `idObj`, `dtEmprestimo`, `dtDevolucao`, `status`) VALUES (1, 1, 2, 'b', 2, '2012-12-10', '2012-12-12', 0);
INSERT INTO `emprestae_db`.`emprestimo` (`idemprestimo`, `fk_idUser1`, `fk_idUser2`, `tipoObjeto`, `idObj`, `dtEmprestimo`, `dtDevolucao`, `status`) VALUES (2, 2, 3, 'c', 1, '2011-9-12', '2011-10-3', 0);
INSERT INTO `emprestae_db`.`emprestimo` (`idemprestimo`, `fk_idUser1`, `fk_idUser2`, `tipoObjeto`, `idObj`, `dtEmprestimo`, `dtDevolucao`, `status`) VALUES (3, 3, 1, 'a', 3, '2012-1-2', '2012-3-4', 0);

COMMIT;

-- -----------------------------------------------------
-- Data for table `emprestae_db`.`possuiLivro`
-- -----------------------------------------------------
START TRANSACTION;
USE `emprestae_db`;
INSERT INTO `emprestae_db`.`possuiLivro` (`fk_idUser`, `fk_idLivro`) VALUES (1, 2);
INSERT INTO `emprestae_db`.`possuiLivro` (`fk_idUser`, `fk_idLivro`) VALUES (3, 3);
INSERT INTO `emprestae_db`.`possuiLivro` (`fk_idUser`, `fk_idLivro`) VALUES (2, 2);
INSERT INTO `emprestae_db`.`possuiLivro` (`fk_idUser`, `fk_idLivro`) VALUES (1, 1);

COMMIT;

-- -----------------------------------------------------
-- Data for table `emprestae_db`.`possuiJogo`
-- -----------------------------------------------------
START TRANSACTION;
USE `emprestae_db`;
INSERT INTO `emprestae_db`.`possuiJogo` (`fk_idUser`, `fk_idJogo`) VALUES (2, 3);
INSERT INTO `emprestae_db`.`possuiJogo` (`fk_idUser`, `fk_idJogo`) VALUES (1, 3);
INSERT INTO `emprestae_db`.`possuiJogo` (`fk_idUser`, `fk_idJogo`) VALUES (3, 2);

COMMIT;

-- -----------------------------------------------------
-- Data for table `emprestae_db`.`possuiFilme`
-- -----------------------------------------------------
START TRANSACTION;
USE `emprestae_db`;
INSERT INTO `emprestae_db`.`possuiFilme` (`fk_idUser`, `fk_idFilme`) VALUES (1, 2);
INSERT INTO `emprestae_db`.`possuiFilme` (`fk_idUser`, `fk_idFilme`) VALUES (2, 1);
INSERT INTO `emprestae_db`.`possuiFilme` (`fk_idUser`, `fk_idFilme`) VALUES (3, 1);

COMMIT;
