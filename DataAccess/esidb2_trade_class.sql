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
-- Table structure for table `trade_class`
--

DROP TABLE IF EXISTS `trade_class`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `trade_class` (
  `Trade_class_id` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(45) DEFAULT NULL,
  `Description` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Trade_class_id`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trade_class`
--

LOCK TABLES `trade_class` WRITE;
/*!40000 ALTER TABLE `trade_class` DISABLE KEYS */;
INSERT INTO `trade_class` VALUES (1,'BS','BAKESHOP'),(2,'BZ','BAZAAR'),(3,'CS','CONVENIENCE STORE'),(4,'DE','DEPARTMENT STORE / SHOPPING MALL'),(5,'DS','DRUGSTORE'),(6,'EM','EMPLOYEE'),(7,'GR','GROCERY'),(8,'HT','HOTEL'),(9,'IS','INSTITUTION'),(10,'MS','MARKET STALLS'),(11,'OT','OTHERS'),(12,'RT','RESTAURANT/FASTFOOD'),(13,'SSS','SARI-SARI STORE'),(14,'SM','SUPERMARKET'),(15,'VS','VAN SALESMAN'),(16,'DP','DP'),(17,'WS','WHOLESALER'),(18,'AG','AGRIVET'),(19,'SD','SD'),(20,'TPA','TPA'),(21,'GS','GASOLINE STATION'),(22,'SC','SCHOOL CANTEEN'),(23,'SS','SUNDRY'),(24,'BK','BAKERY'),(25,'CT','CATERER'),(26,'CP','COOPERATIVE'),(27,'FM','FOOD MANUFACTURER'),(28,'HL','HAULER'),(29,'IN','INDUSTRIAL'),(30,'NC','NEW CHANNELS'),(31,'HH','THIRD PARTY AGENCY'),(32,'WN','WALK-IN');
/*!40000 ALTER TABLE `trade_class` ENABLE KEYS */;
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
