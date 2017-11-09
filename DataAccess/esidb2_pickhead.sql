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
-- Table structure for table `pickhead`
--

DROP TABLE IF EXISTS `pickhead`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pickhead` (
  `pickhead_id` int(11) NOT NULL AUTO_INCREMENT,
  `pick_number` varchar(45) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  `pick_date` datetime DEFAULT NULL,
  `is_successful` tinyint(1) DEFAULT NULL,
  `is_assigned` tinyint(1) DEFAULT NULL,
  `is_gatepass_printed` tinyint(1) DEFAULT NULL,
  `gatepass_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`pickhead_id`),
  UNIQUE KEY `pickhead_id_UNIQUE` (`pickhead_id`),
  UNIQUE KEY `pick_number_UNIQUE` (`pick_number`),
  KEY `user_link_fk_idx` (`user_id`),
  KEY `gatepass_link_fk_idx` (`gatepass_id`),
  CONSTRAINT `user_link_fk` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pickhead`
--

LOCK TABLES `pickhead` WRITE;
/*!40000 ALTER TABLE `pickhead` DISABLE KEYS */;
INSERT INTO `pickhead` VALUES (4,'000054',6,'2017-10-25 14:55:15',1,0,0,NULL);
/*!40000 ALTER TABLE `pickhead` ENABLE KEYS */;
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
