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
-- Table structure for table `inventory_dummy`
--

DROP TABLE IF EXISTS `inventory_dummy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `inventory_dummy` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `transaction_link` int(11) NOT NULL,
  `location_link` int(11) NOT NULL,
  `price_type` varchar(45) NOT NULL,
  `item_link` int(11) NOT NULL,
  `cases` int(11) NOT NULL,
  `pieces` int(11) NOT NULL,
  `expiration_date` text NOT NULL,
  `pricePerPiece` decimal(15,5) NOT NULL,
  `lineValue` decimal(15,5) NOT NULL,
  `lot_number` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `transaction_link` (`transaction_link`),
  KEY `dummy_location_fk_idx` (`location_link`),
  CONSTRAINT `dummy_location_fk` FOREIGN KEY (`location_link`) REFERENCES `location` (`location_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `dummy_trans_fk` FOREIGN KEY (`transaction_link`) REFERENCES `transaction_entry` (`entry_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=78 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventory_dummy`
--

LOCK TABLES `inventory_dummy` WRITE;
/*!40000 ALTER TABLE `inventory_dummy` DISABLE KEYS */;
INSERT INTO `inventory_dummy` VALUES (76,11,1,'PL1',6,5,2,'12/15/2017',4.94000,256.88000,''),(77,11,1,'PL2',8,-5,0,'12/15/2081',4.17000,-208.50000,'');
/*!40000 ALTER TABLE `inventory_dummy` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-11-09 16:58:19
