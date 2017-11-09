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
-- Table structure for table `terms`
--

DROP TABLE IF EXISTS `terms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `terms` (
  `Term_id` int(11) NOT NULL AUTO_INCREMENT,
  `Term_code` varchar(45) DEFAULT NULL,
  `Term_description` varchar(45) DEFAULT NULL,
  `discount_1` decimal(10,0) DEFAULT NULL,
  `discount_2` decimal(10,0) DEFAULT NULL,
  `discount_3` decimal(10,0) NOT NULL,
  `days` int(11) DEFAULT NULL,
  PRIMARY KEY (`Term_id`)
) ENGINE=InnoDB AUTO_INCREMENT=123 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `terms`
--

LOCK TABLES `terms` WRITE;
/*!40000 ALTER TABLE `terms` DISABLE KEYS */;
INSERT INTO `terms` VALUES (1,'CODA','CASH ON DELIVERY - 10%,10%',10,10,0,0),(2,'CR07','CREDIT 7 DAYS',0,0,0,7),(3,'CR14','CREDIT 14 DAYS',0,0,0,14),(4,'CODB','CASH ON DELIVERY - 10%,5%,5%',10,5,5,0),(5,'CR30','CREDIT 30 DAYS',0,0,0,30),(6,'CR45','CREDIT 45 DAYS',0,0,0,45),(7,'CR60','CREDIT 60 DAYS',0,0,0,60),(8,'CR-A','CREDIT 7 DAYS - LESS 2%',2,0,0,0),(9,'CR-B','CREDIT 14 DAYS - LESS 2%',2,0,0,0),(10,'CR15','CREDIT 15 DAYS',0,0,0,15),(11,'COD0','CASH ON DELIVERY',0,0,0,0),(12,'COD2','CASH ON DELIVERY - 2%',2,0,0,0),(13,'COD5','CASH ON DELIVERY - 5%',5,0,0,0),(14,'CR-C','CREDIT 15 DAYS  - 10%',10,0,0,0),(15,'C011','CASH ON DELIVERY - 10%,4%,2%',10,4,2,0),(16,'CR-D','CREDIT 30 DAYS - LESS 10%,3%',10,3,0,0),(17,'CR-E','CREDIT 30 DAYS - LESS 2%,3%',2,3,0,0),(18,'CR-F','CREDIT 30 DAYS - LESS 10%',10,0,0,0),(19,'CR-G','CREDIT 30 DAYS - LESS 2%',2,0,0,0),(20,'COD3','CASH ON DELIVERY - 3%',3,0,0,0),(21,'CR-H','CREDIT 30 DAYS - LESS 3%',3,0,0,0),(22,'CR-J','CREDIT 15 DAYS - LESS 4%',4,0,0,0),(23,'CR-I','CREDIT 30 DAYS - LESS 5%',5,0,0,0),(24,'CR-K','CREDIT 15 DAYS - LESS 5%',5,0,0,0),(25,'CR-L','CREDIT 15 DAYS - LESS 6%',6,0,0,0),(26,'CR-M','CREDIT 15 DAYS - LESS 3%',3,0,0,0),(27,'CR-N','CREDIT 21 DAYS',0,0,0,0),(28,'CR-O','CREDIT 30 DAYS - LESS 10%,5%',10,5,0,0),(29,'CR-P','CASH ON DELIVERY - LESS 10%,2%',10,2,0,0),(30,'CR-Q','CREDIT 15 DAYS - LESS 2%',2,0,0,0),(31,'CR-R','CASH ON DELIVERY - LESS 4%',4,0,0,0),(32,'CR-S','CASH ON DELIVERY - LESS 15%,2%',15,2,0,0),(33,'CO12','CASH ON DELIVERY - LESS 12%',12,0,0,0),(34,'CR-U','CREDIT 15 DAYS - LESS 30%',30,0,0,0),(35,'CR-V','CREDIT 30 DAYS - LESS 2%,2%',2,2,0,0),(36,'CR-W','CREDIT 15 DAYS - LESS 13%',13,0,0,0),(37,'CR-X','CASH ON DELIVERY - LESS 8%',8,0,0,0),(38,'CR-Y','CREDIT 30 - LESS 4%',4,0,0,0),(39,'CR-Z','CASH ON DELIVERY - LESS 4%',4,0,0,0),(40,'C15','CREDIT 30 DAYS - LESS 15%',15,0,0,0),(41,'C03','CASH ON DELIVERY - LESS 3%,2%',3,2,0,0),(42,'C01','CREDIT 15 DAYS - LESS 15%,2%',15,2,0,0),(43,'CO10','CASH ON DELIVERY - LESS 10%',10,0,0,0),(44,'CO30','CASH ON DELIVERY - LESS 30%',30,0,0,0),(45,'CODC','CASH ON DELIVERY - 10%,3%,3%',10,3,3,0),(46,'CO15','CASH ON DELIVERY - 15%',15,0,0,0),(47,'C30','CREDIT 30 DAYS - LESS 20%',20,0,0,0),(48,'CR16','CREDIT 15 DAYS - LESS 16%',16,0,0,0),(49,'CO50','CASH ON DELIVERY - 50%',50,0,0,0),(50,'CR50','CREDIT 15 DAYS - LESS 50%',50,0,0,0),(51,'CRAA','CREDIT 30 DAYS - LESS 6%',6,0,0,0),(52,'CO7','CASH ON DELIVERY - LESS 7%',7,0,0,0),(53,'CWO5','CASH WITH ORDER - LESS 5%',5,0,0,0),(54,'CRAB','CR 15 DAYS - LESS 10%,5%,2%',10,5,2,0),(55,'CRAC','CR 30 DAYS - LESS 10%,2%',10,2,0,0),(56,'CO-5','CASH ON DELIVERY - LESS 5%,2%',5,2,0,0),(57,'CWO4','CASH WITH ORDER - LESS 4%',4,0,0,0),(58,'C-12','CASH ON DELIVERY - LESS 12%,2%',12,2,0,0),(59,'C5-3','CASH ON DELIVERY - LESS 5%,3%',5,3,0,0),(60,'CRAD','CREDIT 30 DAYS - LESS 3%,2%',3,2,0,0),(61,'CO A','CASH ON DELIVERY - LESS 3%,3%',3,3,0,0),(62,'CR17','CREDIT  15 DAYS - LESS 4%,2%',4,2,0,0),(63,'CR18','CASH ON DELIVERY - LESS 17%',17,0,0,0),(64,'C060','CREDIT 60 DAYS - LESS 60%',60,0,0,0),(65,'CO B','CASH ON DELIVERY - LESS 2.25%',2,0,0,0),(66,'AA1','CASH ON DELIVERY-LESS 2.5%,2%',3,2,0,0),(67,'CRAE','CREDIT 30 DAYS - LESS 12%',12,0,0,0),(68,'CRAF','CREDIT 15 DAYS-LESS 2.5%,.5%',3,1,0,0),(69,'CRAJ','COD - LESS 2.5%,3%',3,3,0,0),(70,'CRAH','CASH ON DELIVERY - LESS 7%,2%',7,2,0,0),(71,'CRAG','CASH ON DELIVERY - LESS 40%',40,0,0,0),(72,'CRAK','CREDIT 15 DAYS - LESS 2.5%',3,0,0,0),(73,'CRAL','CASH ON DELIVERY - LESS 7%,3%',7,3,0,0),(74,'CRAM','CASH ON DELIVERY - LESS 7%,5%',7,5,0,0),(75,'CRAN','CASH ON DELIVERY - LESS 6%,4%',0,6,4,0),(76,'CRA0','CASH ON DELIVERY-LESS 2.25%,5%',2,5,0,0),(77,'CRAP','CASH ON DELIVERY - LESS 6%,2%',6,2,0,0),(78,'CO C','CASH ON DELIVERY - LESS 2%,2%',2,2,0,0),(79,'CRAQ','COD - LESS 8%,2%',8,2,0,0),(80,'CRAR','CREDIT 30 DAYS - LESS 2%,3%,2%',2,3,2,0),(81,'CRAS','CREDIT 30 DAYS -LESS 10%,3%,2%',10,3,2,0),(82,'CRAT','CASH ON DELIVERY-LESS 5%,2%,3%',5,2,3,0),(83,'CRAU','CREDIT 15 DAYS - LESS 5%,5%',5,5,0,0),(84,'CRAV','CASHONDLIVRY-LESS 15%,5%,2%',15,5,2,0),(85,'CRAX','CREDIT 30 DAYS - LESS 50%',50,0,0,0),(86,'CRAW','CREDIT 30 DAYS - LESS 30%',30,0,0,0),(87,'CRAY','CREDIT 15 DAYS-LESS 5%,10%,25%',5,10,25,0),(88,'CRBA','CREDIT 30 DAYS - LESS 7%',7,0,0,0),(89,'CRBB','CREDIT 45 DAYS - LESS 7%',7,0,0,0),(90,'CRBC','CREDIT 60 DAYS - LESS 7%',7,0,0,0),(91,'CRBD','CREDIT 15 DAYS - LESS 7%',7,0,0,0),(92,'CRBE','CREDIT 15 DAYS - LESS 9%',9,0,0,0),(93,'CRBF','CREDIT 30 DAYS - LESS 9%',9,0,0,0),(94,'CRBG','CREDIT 45 DAYS - LESS 9%',9,0,0,0),(95,'CRBH','CREDIT 7 DAYS - LESS 9%',9,0,0,0),(96,'CRBI','CREDIT 30 DAYS - LESS 17%',17,0,0,0),(97,'CO-D','CASH ON DELIVERY - LESS 50%,2%',50,2,0,0),(98,'COD1','CASH ON DELIVERY - LESS 1%',1,0,0,0),(99,'CRBJ','CREDIT 15 DAYS - LESS 1%',1,0,0,0),(100,'CRBK','CREDIT 15 DAYS - LESS 5%, 2%',5,2,0,0),(101,'C0D3','CREDIT 15 DAYS - LESS 3%',3,0,0,0),(102,'IS60','I.S. 60 DAYS',0,0,0,60),(103,'CR20','CREDIT 20 DAYS',0,0,0,20),(104,'CO27','CASH ON DELIVERY - LESS 2%,7%',2,7,0,0),(105,'CR33','CREDIT 7 DAYS - LESS 3%',3,0,0,0),(106,'CO-M','COD - 5%,3%, 2%',5,3,2,0),(107,'CR24','CREDIT 15 DAYS 2%,4%',2,4,0,0),(108,'CW06','COD - LESS 5%, 5%, 2%',5,5,2,0),(109,'CO25','COD -2%,5%',2,5,0,0),(110,'CR-T','CREDIT 15 DAYS-6%,2%',6,2,0,0),(111,'CODE','PDC UPON DELIVERY - 30 DAYS',0,0,0,30),(112,'CODS','CASH - DELIVERY TO COLLECT',0,0,0,0),(113,'CO31','CASH ON DELIVERY LESS 3%,1%',3,1,0,0),(114,'CO51','CASH ON DELIVERY- LESS 5%, 1%',5,1,0,0),(115,'CO23','CASH ON DELIVERY - LESS 2%,3%',2,3,0,0),(116,'CO32','CASH ON DELIVERY - LESS 3%,2%',3,2,0,0),(117,'C072','CASH ON DELIVERY-LESS 7%,2%',7,2,0,0),(118,'COX1','CASH ON DELIVERY - LESS 1%, 2%',1,2,0,0),(119,'COX2','CREDIT 15 DAYS - LESS 2%, 1%',2,1,0,0),(120,'COX3','CASH ON DELIVERY-LESS 1%,1%,1%',1,1,1,0),(121,'CR05','CREDIT 30 DAYS-LESS 3.5%-1.5%',4,2,0,0),(122,'CR08','CREDIT 8%',8,0,0,0);
/*!40000 ALTER TABLE `terms` ENABLE KEYS */;
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
