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
-- Table structure for table `allocated_stocks`
--

DROP TABLE IF EXISTS `allocated_stocks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `allocated_stocks` (
  `allocated_stocks_id` int(11) NOT NULL AUTO_INCREMENT,
  `pickhead_id` int(11) DEFAULT NULL,
  `inventory_dummy_id` int(11) DEFAULT NULL,
  `cases` int(11) DEFAULT NULL,
  `pieces` int(11) DEFAULT NULL,
  `inventory_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`allocated_stocks_id`),
  KEY `dummy_link_fk_idx` (`inventory_dummy_id`),
  KEY `pickhead_link_fk_idx` (`pickhead_id`),
  KEY `inventory_link_fk_idx` (`inventory_id`),
  CONSTRAINT `alloc_pickhead_link_fk` FOREIGN KEY (`pickhead_id`) REFERENCES `pickhead` (`pickhead_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `dummy_link_fk` FOREIGN KEY (`inventory_dummy_id`) REFERENCES `inventory_dummy_2` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `inventory_link_fk` FOREIGN KEY (`inventory_id`) REFERENCES `inventory_master2` (`inventory2_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=96 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `allocated_stocks`
--

LOCK TABLES `allocated_stocks` WRITE;
/*!40000 ALTER TABLE `allocated_stocks` DISABLE KEYS */;
INSERT INTO `allocated_stocks` VALUES (80,4,5,1,0,1147),(81,4,6,16,0,1702),(82,4,6,16,0,1702),(83,4,6,16,0,1702),(84,4,6,16,0,1702),(85,4,6,16,0,1702),(86,4,6,16,0,1702),(87,4,6,16,0,1702),(88,4,6,16,0,1702),(89,4,7,9,81,1195),(90,4,7,20,0,1236),(91,4,7,20,0,1252),(92,4,7,0,31,1263),(93,4,4,19,69,1263),(94,4,4,0,38,1265),(95,4,2,2,0,1702);
/*!40000 ALTER TABLE `allocated_stocks` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-11-09 16:58:21
