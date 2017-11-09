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
-- Table structure for table `salesman`
--

DROP TABLE IF EXISTS `salesman`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `salesman` (
  `salesman_id` int(11) NOT NULL AUTO_INCREMENT,
  `Salesman_number` varchar(45) DEFAULT NULL,
  `Salesman_name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`salesman_id`)
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `salesman`
--

LOCK TABLES `salesman` WRITE;
/*!40000 ALTER TABLE `salesman` DISABLE KEYS */;
INSERT INTO `salesman` VALUES (1,'700725','TRIFON N. GADOR'),(2,'700730','H/A - CDO'),(3,'700731','VACANT'),(4,'700732','REGILOU M. ATCHAME'),(5,'700733','BOBBY N. TULANG'),(6,'700741','ROLANDO B. MATA'),(7,'700742','ROMMEL A. VALLENTE'),(8,'700743','ACHED M. AMBROSE'),(9,'700744','JOSE NILJEAN G. TALIBONG'),(10,'700761','VACANT'),(11,'700700','ALLAN M. MACKNO'),(12,'700734','NELSON T. CUAJAO'),(13,'700751','SEGUNDO A. MASALTA'),(14,'700752','VACANT'),(15,'700762','ROMEO R. LEONIDA'),(16,'700711','BENGIE S. ERANA'),(17,'700712','RUEL QUIDET'),(18,'700735','ALLAN DAVE TABAN'),(19,'700726','JOCEVER VILLANUEVA'),(20,'700753','VACANT'),(21,'700771','RAYMOND CADIAO'),(22,'700772','PROCULO G. PASTOR'),(23,'700773','VACANT'),(24,'700775','VACANT'),(25,'700781','VACANT'),(26,'700782','VACANT'),(27,'700774','VINCENT SOGOCCIO'),(28,'700783','JULIUS S. MABALA'),(29,'700776','VACANT'),(30,'700784','MARKLEO C. LAGROSAS'),(31,'700786','ENGR. ARCIMELAN B. ANDRADA'),(32,'700785','VACANT'),(33,'700722','JOSE VICTOR LOPEZ'),(34,'700723','VACANT'),(35,'700727','JOSE VICTOR LOPEZ'),(36,'700728','MAURICE JIMENO'),(37,'700713','ENGELBERG JUN B. MENESES'),(38,'700714','JONATHAN L. CLAM'),(39,'700715','JULIUS G. JIMENO'),(40,'700716','ANDY S. CENIZA'),(41,'700724','JOCEVER VILLANUEVA'),(42,'700729','MAURICE JIMENO'),(43,'700717','WELTON A. VACALARES'),(44,'700721','SEGUNDO A. MASALTA'),(45,'700720','BOBBY N. TULANG'),(46,'700718','MARK TITO G. TAN'),(47,'700710','WALTER CATAM-ISAN'),(48,'700709','LIOPOLDO M. WAPIRE'),(49,'700719','RICARDO ARAT'),(50,'700708','BALTAZAR S. ERANA'),(51,'700707','ARJI RONO'),(52,'700790','ROBERTO N. TORRES JR.'),(53,'700791','EMMANUEL MARTIN JAUDIAN'),(54,'700792','YANCHE E. RAGANOT'),(55,'700793','MARK RUNIEL LLAGAS'),(56,'700794','RUEL PACUBAS'),(57,'700795','BRYAN PARAMI'),(58,'700796','BENHOVEN A. OBSID');
/*!40000 ALTER TABLE `salesman` ENABLE KEYS */;
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