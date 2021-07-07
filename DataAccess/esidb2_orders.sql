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
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orders` (
  `Order_id` int(11) NOT NULL AUTO_INCREMENT,
  `Order_number` varchar(45) DEFAULT NULL,
  `Order_date` date DEFAULT NULL,
  `required_ship_date` date NOT NULL,
  `PO_number` varchar(45) DEFAULT NULL,
  `Order_note` varchar(45) DEFAULT NULL,
  `Order_amount` decimal(15,5) DEFAULT NULL,
  `Cases` int(11) DEFAULT NULL,
  `Pieces` int(11) DEFAULT NULL,
  `Served` tinyint(1) DEFAULT NULL,
  `Picked` tinyint(1) DEFAULT NULL,
  `Printed` tinyint(1) DEFAULT NULL,
  `customer_id` int(11) NOT NULL,
  `route_id` int(11) NOT NULL,
  `Term_id` int(11) NOT NULL,
  `price_id` int(11) NOT NULL,
  `district_id` int(11) DEFAULT NULL,
  `warehouse_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`Order_id`),
  KEY `customer_fk_idx` (`customer_id`),
  KEY `route_fk_idx` (`route_id`),
  KEY `term_fk_idx` (`Term_id`),
  KEY `price_fk_idx` (`price_id`),
  KEY `district_fk_idx` (`district_id`),
  CONSTRAINT `customer_fk` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`Customer_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `district_fk` FOREIGN KEY (`district_id`) REFERENCES `districts` (`district_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `route_fk` FOREIGN KEY (`route_id`) REFERENCES `routes` (`route_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `term_fk` FOREIGN KEY (`Term_id`) REFERENCES `terms` (`Term_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (6,'942355','2017-08-29','2017-08-30','','',26729.62000,72,7,0,1,0,3606,4,11,2,5,1),(7,'942356','2017-09-02','2017-09-03','','',435769.92000,1051,12,0,1,0,23,1,10,2,56,1);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
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
