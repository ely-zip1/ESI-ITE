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
-- Table structure for table `programs`
--

DROP TABLE IF EXISTS `programs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `programs` (
  `program_id` int(11) NOT NULL AUTO_INCREMENT,
  `program_name` varchar(45) DEFAULT NULL,
  `program_category` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`program_id`)
) ENGINE=InnoDB AUTO_INCREMENT=85 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `programs`
--

LOCK TABLES `programs` WRITE;
/*!40000 ALTER TABLE `programs` DISABLE KEYS */;
INSERT INTO `programs` VALUES (2,'Sales Order Entry','Orders CN DN'),(3,'Sales Order Printing','Orders CN DN'),(4,'Order Selection for Picking','Orders CN DN'),(5,'Orders Allocation','Orders CN DN'),(6,'Picklist Printing','Orders CN DN'),(7,'Allocation Maintenance','Orders CN DN'),(8,'Invoice Assignment','Orders CN DN'),(9,'Gatepass Printing','Orders CN DN'),(10,'Delivery Receipt Printing','Orders CN DN'),(11,'Invoice Printing','Orders CN DN'),(12,'Despatches Creation','Orders CN DN'),(13,'Invoice Register Printing','Orders CN DN'),(14,'CN-DN Entry','Orders CN DN'),(15,'CN-DN Printing','Orders CN DN'),(16,'CN-DN Posting','Orders CN DN'),(17,'Day-end Processing','Orders CN DN'),(18,'Inventory Transaction Entry','Inventory Transaction'),(19,'Inventory Transaction Posting','Inventory Transaction'),(20,'Receipt of Purchase Orders Entry','Inventory Transaction'),(21,'Receipt of Purchase Orders Posting','Inventory Transaction'),(22,'Post-shipment Entry','Inventory Transaction'),(23,'Post-shipment Posting','Inventory Transaction'),(24,'Inventoy Balance Inquiry','Inventory Transaction'),(25,'Month-end Close Processing','Inventory Transaction'),(26,'Year-end Processing','Inventory Transaction'),(27,'Physical Count Worksheet','Physical Count'),(28,'Freeze Stock Balance','Physical Count'),(29,'Physical Count Maintenance','Physical Count'),(30,'Physical Count Variance Report','Physical Count'),(31,'Physical Count Update of Stock Balance','Physical Count'),(32,'Order Plan Preparation','Order Planning'),(33,'Purchase Order Generation','Order Planning'),(34,'Pended Orders Monitoring','Order Planning'),(35,'Purging of Pended Orders','Order Planning'),(36,'Sales Reports','Reports'),(37,'Inventory Reports','Reports'),(38,'Purchase Reports','Reports'),(39,'Acounts Receivable Reports','Reports'),(40,'Sales Per Town by Supplier','Reports'),(41,'Sales Per Province by Supplier','Reports'),(42,'Sales Summary Per Province by Supplier','Reports'),(43,'District Sales by Tradeclass','Reports'),(44,'Supplier Sales Per District by Tradeclass','Reports'),(45,'Unpaid Bills','Reports'),(46,'Monthly VAT Report by Supplier','Reports'),(47,'Transactions History','Reports'),(48,'Item Transaction History','Reports'),(49,'Database Maintenance','System'),(50,'System Parameters and Information','System'),(51,'Backup Files','System'),(52,'Restore Files','System'),(53,'User Access Maintenance','System'),(54,'Task Scheduling','System'),(55,'Item Structure Maintenance','File Maintenance'),(56,'Customer Master Maintenance','File Maintenance'),(57,'Codes Master','File Maintenance'),(58,'Term Code Maintenance','File Maintenance'),(59,'Calendar File Maintenance','File Maintenance'),(60,'Reason Code Maintenance','File Maintenance'),(61,'Sales Target Maintenance','File Maintenance'),(62,'Archiving of History Files','House Keeping'),(63,'Dearchiving of History Files','House Keeping'),(64,'Purging of System Files','House Keeping'),(65,'Deleting of Temporary Files','House Keeping'),(66,'Upload New Item File','Maintenance'),(67,'Upload Satellite Transaction Files','Maintenance'),(68,'Extract Transaction Files','Maintenance'),(69,'File Location','System'),(70,'Apply Payment','AR'),(71,'Apply Credit Notes','AR'),(72,'Apply Debit Notes','AR'),(73,'Change / Void Credit Notes','AR'),(74,'Change / Void Debit Notes','AR'),(75,'Adjustment','AR'),(76,'A/R Archiving of History Files','AR'),(77,'A/R Dearchiving of History Files','AR'),(78,'A/R Purging of Archived Files','AR'),(79,'Apply Payment to DN','AR'),(80,'Create Credit Note','AR'),(81,'Create Debit Note','AR'),(82,'Zip Codes File','File Maintenance'),(83,'Provinces File','File Maintenance'),(84,'Print Listing File...','Reports');
/*!40000 ALTER TABLE `programs` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-11-09 16:58:20
