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
-- Table structure for table `inventory_dummy_2`
--

DROP TABLE IF EXISTS `inventory_dummy_2`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `inventory_dummy_2` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `order_id` int(11) NOT NULL,
  `price_type` int(11) NOT NULL,
  `location` int(11) NOT NULL,
  `item` int(11) NOT NULL,
  `cases` int(11) NOT NULL,
  `pieces` int(11) NOT NULL,
  `price_per_piece` decimal(15,5) NOT NULL,
  `line_amount` decimal(15,5) NOT NULL,
  `lot_number` text,
  PRIMARY KEY (`id`),
  KEY `inventorydummy2_order_fk_idx` (`order_id`),
  KEY `inventorydummy2_pricetype_fk_idx` (`price_type`),
  KEY `inventorydummy2_location_fk_idx` (`location`),
  KEY `inventorydummy2_item_fk_idx` (`item`),
  CONSTRAINT `inventorydummy2_item_fk` FOREIGN KEY (`item`) REFERENCES `item_master` (`Item_master2_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `inventorydummy2_location_fk` FOREIGN KEY (`location`) REFERENCES `location` (`location_id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `inventorydummy2_order_fk` FOREIGN KEY (`order_id`) REFERENCES `orders` (`Order_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `inventorydummy2_pricetype_fk` FOREIGN KEY (`price_type`) REFERENCES `pricetype` (`pricetype_id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventory_dummy_2`
--

LOCK TABLES `inventory_dummy_2` WRITE;
/*!40000 ALTER TABLE `inventory_dummy_2` DISABLE KEYS */;
INSERT INTO `inventory_dummy_2` VALUES (2,6,2,2,8,2,0,4.17000,834.00000,''),(3,6,2,1,54,50,0,3.71000,18550.00000,''),(4,6,2,1,1,20,7,3.66000,7345.62000,''),(5,7,2,1,7,1,0,4.26000,426.00000,''),(6,7,2,1,8,1000,0,4.17000,417000.00000,''),(7,7,2,1,1,50,12,3.66000,18343.92000,'');
/*!40000 ALTER TABLE `inventory_dummy_2` ENABLE KEYS */;
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
