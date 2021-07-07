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
-- Table structure for table `transaction_entry`
--

DROP TABLE IF EXISTS `transaction_entry`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `transaction_entry` (
  `entry_id` int(11) NOT NULL AUTO_INCREMENT,
  `trans_no` text NOT NULL,
  `trans_type_link` int(11) NOT NULL,
  `doc_no` text NOT NULL,
  `trans_date` date NOT NULL,
  `source_WH_link` int(11) DEFAULT NULL,
  `source_location_link` int(11) DEFAULT NULL,
  `source_salesman_link` int(11) DEFAULT NULL,
  `destination_WH_link` int(11) DEFAULT NULL,
  `destination_location_link` int(11) DEFAULT NULL,
  `destination_salesman_link` int(11) DEFAULT NULL,
  `price_category` varchar(50) NOT NULL,
  `price_type` varchar(50) NOT NULL,
  `reason_code_link` int(11) DEFAULT NULL,
  `comment` text,
  `status` int(11) NOT NULL,
  PRIMARY KEY (`entry_id`),
  KEY `trans_code_link` (`trans_type_link`),
  KEY `source_WH_link` (`source_WH_link`),
  KEY `source_location_link` (`source_location_link`),
  KEY `source_salesman_link` (`source_salesman_link`),
  KEY `destination_WH_link` (`destination_WH_link`),
  KEY `destination_location_link` (`destination_location_link`),
  KEY `destination_salesman_link` (`destination_salesman_link`),
  KEY `reason_code_link` (`reason_code_link`),
  CONSTRAINT `d_loc_fk` FOREIGN KEY (`destination_location_link`) REFERENCES `location` (`location_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `d_wh_fk` FOREIGN KEY (`destination_WH_link`) REFERENCES `warehouse` (`warehouse_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `reason_fk` FOREIGN KEY (`reason_code_link`) REFERENCES `reason_code` (`reasoncode_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `s_loc_fk` FOREIGN KEY (`source_location_link`) REFERENCES `location` (`location_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `s_wh_fk` FOREIGN KEY (`source_WH_link`) REFERENCES `warehouse` (`warehouse_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `trans_type_fk` FOREIGN KEY (`trans_type_link`) REFERENCES `transaction_type` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transaction_entry`
--

LOCK TABLES `transaction_entry` WRITE;
/*!40000 ALTER TABLE `transaction_entry` DISABLE KEYS */;
INSERT INTO `transaction_entry` VALUES (11,'000004',6,'123456','2016-11-24',1,1,NULL,NULL,NULL,NULL,'Selling Price','Current',30,'',0),(12,'000005',6,'765487263675','2017-07-21',1,1,NULL,NULL,NULL,NULL,'Purchase Price','3 Months Ago',31,'',0);
/*!40000 ALTER TABLE `transaction_entry` ENABLE KEYS */;
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
