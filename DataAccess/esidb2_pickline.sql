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
-- Table structure for table `pickline`
--

DROP TABLE IF EXISTS `pickline`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pickline` (
  `pickline_id` int(11) NOT NULL AUTO_INCREMENT,
  `pickhead_id` int(11) DEFAULT NULL,
  `order_id` int(11) DEFAULT NULL,
  `inventory_dummy_id` int(11) DEFAULT NULL,
  `allocated_cases` int(11) DEFAULT NULL,
  `allocated_pieces` int(11) DEFAULT NULL,
  `is_critical` int(1) DEFAULT NULL,
  PRIMARY KEY (`pickline_id`),
  UNIQUE KEY `pickline_id_UNIQUE` (`pickline_id`),
  KEY `dummy_link_fk_idx` (`inventory_dummy_id`),
  KEY `dummy_picklinelink_fk_idx` (`inventory_dummy_id`),
  KEY `pickhead_link_fk_idx` (`pickhead_id`),
  KEY `order_link_fk_idx` (`order_id`),
  CONSTRAINT `dummy_pickline_link_fk` FOREIGN KEY (`inventory_dummy_id`) REFERENCES `inventory_dummy_2` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `order_link_fk` FOREIGN KEY (`order_id`) REFERENCES `orders` (`Order_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `pickhead_link_fk` FOREIGN KEY (`pickhead_id`) REFERENCES `pickhead` (`pickhead_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pickline`
--

LOCK TABLES `pickline` WRITE;
/*!40000 ALTER TABLE `pickline` DISABLE KEYS */;
INSERT INTO `pickline` VALUES (19,4,7,5,1,0,0),(20,4,7,6,156,0,1),(21,4,7,7,50,12,0),(22,4,6,2,2,0,1),(23,4,6,3,0,0,1),(24,4,6,4,20,7,0);
/*!40000 ALTER TABLE `pickline` ENABLE KEYS */;
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
