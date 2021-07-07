-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: esidb2
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `supplier`
--

DROP TABLE IF EXISTS `supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `supplier` (
  `supplier_id` int(11) NOT NULL AUTO_INCREMENT,
  `supplier_code` varchar(45) NOT NULL,
  `supplier_name` varchar(100) DEFAULT NULL,
  `address` varchar(45) DEFAULT NULL,
  `contact_person` varchar(45) DEFAULT NULL,
  `contact_number` varchar(45) DEFAULT NULL,
  `term_id` int(11) NOT NULL,
  PRIMARY KEY (`supplier_id`),
  KEY `term_fk_idx` (`term_id`),
  KEY `supplier_term_fk_idx` (`term_id`),
  CONSTRAINT `supplier_term_fk` FOREIGN KEY (`term_id`) REFERENCES `terms` (`Term_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplier`
--

LOCK TABLES `supplier` WRITE;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
INSERT INTO `supplier` VALUES (1,'0128','JAPAN TOBACCO INTERNATIONAL','Metro Manila','Cris Soberano','',11),(25,'0129','PHILCHEM MARKETING','','','',1),(26,'0130','MARYLAND DISTRIBUTORS, INC.','','','',1),(27,'0131','SANOFI-SYNTHELABO','','','',1),(28,'0132','ECO FOODS CORP.','','','',1),(29,'0133','PERFETTI VAN MELLE','','','',1),(30,'0134','MATAHARI TRADING, INC.','','','',1),(31,'0135','SANOFI-WINTHROP','','','',1),(32,'0136','ABBOTT NUTRITION INTERNATIONAL','','','',1),(33,'0137','DENTAL B','','','',1),(34,'0138','NUTRI ASIA GROUP - SAFI','','','',1),(35,'0139','GLOBAL FOODS SOLUTION, INC.','','','',1),(36,'0140','MEGA FISHING CORPORATION','','','',1),(37,'0141','INTEGRATED MARKETING DISTRIBUTION SERVIC','','','',1),(38,'0142','NUTRI ASIA GROUP - UFC','','','',1),(39,'0143','UNITED HARVEST CORPORATION (UHC)','','','',1),(40,'0144','NUTRIASIA FOOD SERVICE - SAFI','','','',1),(41,'0145','NUTRIASIA FOOD SERVICE - UFC','','','',1),(42,'0146','ONE VENDOR-SAFI','','','',1),(43,'0147','ONE VENDOR-UFC','','','',1),(44,'0148','MERCK SHARP & DOHME I.A. CORP. (MSD)','','','',1),(45,'0149','BAYER HEALTHCARE','','','',1),(46,'0150','HENKEL PRODUCTS','','','',1);
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-11-09 16:58:18
