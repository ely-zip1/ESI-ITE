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
-- Table structure for table `item_master`
--

DROP TABLE IF EXISTS `item_master`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `item_master` (
  `Item_master2_id` int(11) NOT NULL AUTO_INCREMENT,
  `item_code` varchar(10) NOT NULL,
  `description` varchar(100) NOT NULL,
  `location_id` int(11) NOT NULL,
  `warehouse_id` int(11) NOT NULL,
  `supplier_id` int(11) NOT NULL,
  `packsize` int(11) NOT NULL,
  `pack_size_bo` int(11) DEFAULT NULL,
  `unit_measure` varchar(45) NOT NULL,
  `smallest_unit` varchar(45) NOT NULL,
  `smallest_unit_symbol` varchar(2) NOT NULL,
  `unit_weight` decimal(20,0) NOT NULL,
  `tax_rate` decimal(5,0) NOT NULL,
  `OPG_active` tinyint(1) DEFAULT NULL,
  `active_item` tinyint(1) DEFAULT NULL,
  `show_item` tinyint(1) DEFAULT NULL,
  `category` varchar(45) DEFAULT NULL,
  `brand` varchar(45) DEFAULT NULL,
  `category_code` varchar(45) DEFAULT NULL,
  `brand_code` varchar(45) DEFAULT NULL,
  `selected` tinyint(1) DEFAULT NULL,
  `weeks_cover` int(11) DEFAULT NULL,
  `lot_controlled` tinyint(1) DEFAULT NULL,
  `loc_description` varchar(45) DEFAULT NULL,
  `current_price` varchar(45) DEFAULT NULL,
  `updated` tinyint(1) DEFAULT NULL,
  `source` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Item_master2_id`),
  KEY `item_master2_location_fk_idx` (`location_id`),
  KEY `item_master2_warehouse_fk_idx` (`warehouse_id`),
  KEY `item_master2_supplier_fk_idx` (`supplier_id`),
  CONSTRAINT `item_master2_location_fk` FOREIGN KEY (`location_id`) REFERENCES `location` (`location_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `item_master2_supplier_fk` FOREIGN KEY (`supplier_id`) REFERENCES `supplier` (`supplier_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `item_master2_warehouse_fk` FOREIGN KEY (`warehouse_id`) REFERENCES `warehouse` (`warehouse_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item_master`
--

LOCK TABLES `item_master` WRITE;
/*!40000 ALTER TABLE `item_master` DISABLE KEYS */;
INSERT INTO `item_master` VALUES (1,'128101','JTI-WINSTON FULL FLAVORED SP 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',1,''),(2,'128102','JTI-WINSTON LIGHTS SP 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',10000,10,1,1,1,'','','','10',0,3,1,'','0',1,''),(3,'128103','JTI-WINSTON LIGHTS GREEN SP 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(4,'128201','JTI-WINSTON FULL FLAVORED CPB 20S 50BXSX10PKSX20S',1,3,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(5,'128303','JTI-WINSTON LIGHTS GREEN CPB 10S 50BXSX20PKSX10S',1,1,1,20,20,'CS','Packs','PK',350,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(6,'128401','JTI-WINSTON XPRESSION RCB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(7,'128501','JTI-WINSTON EXTREME MINT CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(8,'128502','JTI-WINSTON EXTREME MINT SP 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',1,''),(9,'128601','JTI-CAMEL FULL FLAVORED SP 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',10000,10,1,1,1,'','','','10',0,3,1,'','0',1,''),(10,'128701','JTI-MEVIUS ORIGINAL CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(11,'128702','JTI-MEVIUS LIGHTS CPB 20S 50BXSX10PKSX20S',1,3,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(12,'128703','JTI-MEVIUS MENTHOL CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(13,'128705','JTI-MEVIUS OPTION RCB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(14,'128706','JTI-MEVIUS ORIGINAL SP 20S 50BXSX10PKSX20S',1,2,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(15,'128707','JTI-MEVIUS LIGHTS SP 20S 50BXSX10PKSX20S',1,2,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(16,'128801','JTI-LD KS RED SFP 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',10000,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(17,'128802','JTI-LD 100 MENTHOL CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',10000,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(18,'128901','JTI-MILD SEVEN LIGHTS MENTHOL 20S 50BXSX10PKSX20G',1,1,1,10,10,'CS','Packs','PK',300,10,0,1,1,'','','','10',0,3,1,'','0',0,''),(19,'128902','JTI-MILD SEVEN OPTION CPB 20S 50BXSX10PKSX20G',1,1,1,10,10,'CS','Packs','PK',300,10,0,1,1,'','','','10',0,3,1,'','0',0,''),(20,'128903','JTI-LOTTE PACK 1PCKX4PCSX20G',1,1,1,1,1,'CS','Packs','PK',80,10,0,1,1,'','','','10',0,3,1,'','0',0,''),(21,'128301','JTI-WINSTON FULL FLAVORED CPB 10S 50BXSX20PKSX10S',1,1,1,20,20,'CS','Packs','PK',350,10,1,1,1,'','','','10',0,3,1,'','0',1,''),(22,'128302','JTI-WINSTON LIGHTS CPB 10S 50BXSX20PKSX10S',1,1,1,20,20,'CS','Packs','PK',10000,10,1,1,1,'','','','10',0,3,1,'','0',1,''),(23,'128704','JTI-MEVIUS SUPERLIGHTS CPB 20S 50BXSX10PCKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(24,'128202','JTI-WINSTON LIGHTS CPB 20S 50XSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',1,''),(25,'128904','JTI-AKI CHEESE',1,1,1,1,1,'CS','Packs','PK',1000,10,0,1,1,'','','','10',0,3,1,'','0',0,''),(26,'128905','JTI-AKI CHILI',1,1,1,1,1,'CS','Packs','PK',1000,10,0,1,1,'','','','10',0,3,1,'','0',0,''),(27,'128906','JTI-FIGHTER WINE',1,1,1,24,24,'CS','Pieces','PC',8400,10,0,1,1,'','','','10',0,3,1,'','0',0,''),(28,'128907','JTI-MENTOS FRUIT LINKS (FREE GOODS)',1,1,1,120,120,'CS','Packs','PK',68,10,0,1,1,'','','','10',0,3,1,'','0',0,''),(29,'128503','JTI-WINSTON EXTREME MINT CPB 20S GWP 50BXX10PKX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(30,'128708','JTI-MEVIUS ORIGINAL CPB 20S GWP 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(31,'128709','JTI-MEVIUS LIGHTS CPB 20S GWP TWIN PK 50X5X2X20S',1,1,1,5,5,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(32,'128504','JTI-WINSTON EXTREME MINT SP 20S GWP 25BXSX10PKSX20',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(33,'128908','JTI-MENTOS MINT LINKS (FREEGOODS)',1,1,1,120,120,'CS','Packs','PK',68,10,0,1,1,'','','','10',0,3,1,'','0',0,''),(34,'128402','JTI-WINSTON XPRESSION GWP 25BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(35,'128909','JTI-MILD SEVEN ORIGINAL 20S 50X10X20G',1,1,1,10,10,'CS','Packs','PK',300,10,0,1,1,'','','','10',0,3,1,'','0',0,''),(36,'128910','JTI-MILD SEVEN LIGHTS 20S 50X10X20G',1,1,1,10,10,'CS','Packs','PK',300,10,0,1,1,'','','','10',0,3,1,'','0',0,''),(37,'128911','JTI-CAMEL FULL FLAVORED CPB10S 50X20X10G',1,1,1,20,20,'CS','Packs','PK',10000,10,0,1,1,'','','','10',0,3,1,'','0',0,''),(38,'128602','JTI-CAMEL LIGHTS CPB 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',10000,10,1,1,1,'','','','10',0,3,1,'','0',1,''),(39,'128211','JTI-WINSTON FULL FLAVOR CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Pieces','PC',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(40,'128212','JTI-WINSTON LIGHTS CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Pieces','PC',300,10,1,1,1,'','','','10',0,3,1,'','0',1,''),(41,'128511','JTI-WINSTON EXTREME MINT CPB 20S X50BXSX10PKSX20S',1,1,1,10,10,'CS','Pieces','PC',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(42,'128603','JTI-CAMEL FILTER CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',10000,10,1,1,1,'','','','10',0,3,1,'','0',1,''),(43,'128604','JTI-CAMEL MAX MINT CPB 20S  50BXS X 10PCKS X 20S',1,1,1,10,10,'CS','Packs','PK',1000,10,1,1,1,'','','','10',0,3,1,'','0',1,''),(44,'128605','JTI-WINSTON FULL FLAVORED CPB LTD ED  50X10X20S',1,1,1,10,10,'CS','Packs','PK',1000,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(45,'128606','JTI-WINSTON LIGHTS CPB LTD ED 50X10X20S',1,1,1,10,10,'CS','Packs','PK',1000,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(46,'128710','JTI-MEVIUS OPTION DUO 50 X 10 X 20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(47,'128505','JTI-WINSTON EXTREME MINT CPB 20S PROMO',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(48,'128711','JTI-MEVIUS OPTION DUO GWP 50 X 10 X 20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(49,'128104','WINSTON RED SP 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(50,'128105','WINSTON BLUE SP 20S 50BXSX10PKSX20S',1,1,1,10,100,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(51,'128203','WINSTON RED CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(52,'128204','WINSTON BLUE CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(53,'128607','CAMEL ORIGINAL SP 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',10000,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(54,'128608','CAMEL WHITE CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',10000,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(55,'128712','MEVIUS SKY BLUE CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(56,'128713','MEVIUS WIND BLUE CPB 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(57,'128714','MEVIUS OPTION DUO 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,''),(58,'128715','MEVIUS SKY BLUE SP 20S 50BXSX10PKSX20S',1,1,1,10,10,'CS','Packs','PK',300,10,1,1,1,'','','','10',0,3,1,'','0',0,'');
/*!40000 ALTER TABLE `item_master` ENABLE KEYS */;
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
