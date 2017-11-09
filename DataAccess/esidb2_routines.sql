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
-- Temporary view structure for view `view_inventory_dummy`
--

DROP TABLE IF EXISTS `view_inventory_dummy`;
/*!50001 DROP VIEW IF EXISTS `view_inventory_dummy`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_inventory_dummy` AS SELECT 
 1 AS `id`,
 1 AS `transaction_code`,
 1 AS `location_code`,
 1 AS `price_type`,
 1 AS `item_code`,
 1 AS `item_description`,
 1 AS `cases`,
 1 AS `pieces`,
 1 AS `expiration`,
 1 AS `price_per_piece`,
 1 AS `line_amount`,
 1 AS `lot_number`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_invoices`
--

DROP TABLE IF EXISTS `view_invoices`;
/*!50001 DROP VIEW IF EXISTS `view_invoices`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_invoices` AS SELECT 
 1 AS `invoice_id`,
 1 AS `invoice_number`,
 1 AS `username`,
 1 AS `invoice_date`,
 1 AS `customer_number`,
 1 AS `customer_name`,
 1 AS `salesman_name`,
 1 AS `district_number`,
 1 AS `warehouse_code`,
 1 AS `order_number`,
 1 AS `order_date`,
 1 AS `term_code`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_transaction_entry`
--

DROP TABLE IF EXISTS `view_transaction_entry`;
/*!50001 DROP VIEW IF EXISTS `view_transaction_entry`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_transaction_entry` AS SELECT 
 1 AS `id`,
 1 AS `transaction_number`,
 1 AS `transaction_code`,
 1 AS `transaction_type`,
 1 AS `document_number`,
 1 AS `transaction_date`,
 1 AS `source_warehouse_code`,
 1 AS `source_warehouse`,
 1 AS `source_location_code`,
 1 AS `source_location`,
 1 AS `destination_warehouse_code`,
 1 AS `destination_warehouse`,
 1 AS `destination_location_code`,
 1 AS `destination_location`,
 1 AS `price_category`,
 1 AS `price_type`,
 1 AS `reason_code`,
 1 AS `reason_description`,
 1 AS `comment`,
 1 AS `status`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_allocated_stocks`
--

DROP TABLE IF EXISTS `view_allocated_stocks`;
/*!50001 DROP VIEW IF EXISTS `view_allocated_stocks`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_allocated_stocks` AS SELECT 
 1 AS `item_code`,
 1 AS `description`,
 1 AS `cases`,
 1 AS `pieces`,
 1 AS `order_number`,
 1 AS `customer_number`,
 1 AS `customer_name`,
 1 AS `price_per_case`,
 1 AS `price_per_piece`,
 1 AS `item_amount`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_inventory_dummy_2`
--

DROP TABLE IF EXISTS `view_inventory_dummy_2`;
/*!50001 DROP VIEW IF EXISTS `view_inventory_dummy_2`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_inventory_dummy_2` AS SELECT 
 1 AS `id`,
 1 AS `order_number`,
 1 AS `price_type`,
 1 AS `location`,
 1 AS `item_code`,
 1 AS `description`,
 1 AS `cases`,
 1 AS `pieces`,
 1 AS `price_per_piece`,
 1 AS `line_amount`,
 1 AS `lot_number`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_pickline`
--

DROP TABLE IF EXISTS `view_pickline`;
/*!50001 DROP VIEW IF EXISTS `view_pickline`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_pickline` AS SELECT 
 1 AS `item_code`,
 1 AS `description`,
 1 AS `cases`,
 1 AS `pieces`,
 1 AS `order_number`,
 1 AS `customer_number`,
 1 AS `customer_name`,
 1 AS `price_per_case`,
 1 AS `price_per_piece`,
 1 AS `item_amount`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_pickhead`
--

DROP TABLE IF EXISTS `view_pickhead`;
/*!50001 DROP VIEW IF EXISTS `view_pickhead`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_pickhead` AS SELECT 
 1 AS `pickhead_id`,
 1 AS `pick_number`,
 1 AS `username`,
 1 AS `pick_date`,
 1 AS `is_successful`,
 1 AS `is_assigned`,
 1 AS `is_gatepass_printed`,
 1 AS `gatepass_id`,
 1 AS `gatepass_number`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_user_permissions`
--

DROP TABLE IF EXISTS `view_user_permissions`;
/*!50001 DROP VIEW IF EXISTS `view_user_permissions`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_user_permissions` AS SELECT 
 1 AS `permission_id`,
 1 AS `user_id`,
 1 AS `username`,
 1 AS `program_id`,
 1 AS `program_name`,
 1 AS `program_category`*/;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `view_inventory_dummy`
--

/*!50001 DROP VIEW IF EXISTS `view_inventory_dummy`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_inventory_dummy` AS select `d`.`id` AS `id`,`t`.`trans_no` AS `transaction_code`,`l`.`Code` AS `location_code`,`d`.`price_type` AS `price_type`,`i`.`item_code` AS `item_code`,`i`.`description` AS `item_description`,`d`.`cases` AS `cases`,`d`.`pieces` AS `pieces`,`d`.`expiration_date` AS `expiration`,`d`.`pricePerPiece` AS `price_per_piece`,`d`.`lineValue` AS `line_amount`,`d`.`lot_number` AS `lot_number` from (((`inventory_dummy` `d` left join `transaction_entry` `t` on((`t`.`entry_id` = `d`.`transaction_link`))) left join `location` `l` on((`l`.`location_id` = `d`.`location_link`))) left join `item_master` `i` on((`i`.`Item_master2_id` = `d`.`item_link`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_invoices`
--

/*!50001 DROP VIEW IF EXISTS `view_invoices`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_invoices` AS select `inv`.`id` AS `invoice_id`,`inv`.`invoice_number` AS `invoice_number`,`usr`.`username` AS `username`,`inv`.`date` AS `invoice_date`,`cust`.`Customer_number` AS `customer_number`,`cust`.`Customer_name` AS `customer_name`,`sman`.`Salesman_name` AS `salesman_name`,`dist`.`district_number` AS `district_number`,`wh`.`Code` AS `warehouse_code`,`ordr`.`Order_number` AS `order_number`,`ordr`.`Order_date` AS `order_date`,`term`.`Term_code` AS `term_code` from (((((((`invoices` `inv` join `users` `usr` on((`usr`.`id` = `inv`.`user_id`))) join `orders` `ordr` on((`ordr`.`Order_id` = `inv`.`order_id`))) join `customers` `cust` on((`cust`.`Customer_id` = `ordr`.`customer_id`))) join `districts` `dist` on((`dist`.`district_id` = `ordr`.`district_id`))) join `salesman` `sman` on((`sman`.`salesman_id` = `dist`.`salesman`))) join `warehouse` `wh` on((`wh`.`warehouse_id` = `ordr`.`warehouse_id`))) join `terms` `term` on((`term`.`Term_id` = `ordr`.`Term_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_transaction_entry`
--

/*!50001 DROP VIEW IF EXISTS `view_transaction_entry`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_transaction_entry` AS select `t`.`entry_id` AS `id`,`t`.`trans_no` AS `transaction_number`,`tt`.`transaction_code` AS `transaction_code`,`tt`.`transaction_type` AS `transaction_type`,`t`.`doc_no` AS `document_number`,`t`.`trans_date` AS `transaction_date`,`w`.`Code` AS `source_warehouse_code`,`w`.`Name` AS `source_warehouse`,`l`.`Code` AS `source_location_code`,`l`.`Location` AS `source_location`,`w2`.`Code` AS `destination_warehouse_code`,`w2`.`Name` AS `destination_warehouse`,`l2`.`Code` AS `destination_location_code`,`l2`.`Location` AS `destination_location`,`t`.`price_category` AS `price_category`,`t`.`price_type` AS `price_type`,`r`.`Reason_Code` AS `reason_code`,`r`.`Reason_Description` AS `reason_description`,`t`.`comment` AS `comment`,`t`.`status` AS `status` from ((((((`transaction_entry` `t` left join `transaction_type` `tt` on((`tt`.`id` = `t`.`trans_type_link`))) left join `warehouse` `w` on((`w`.`warehouse_id` = `t`.`source_WH_link`))) left join `warehouse` `w2` on((`w2`.`warehouse_id` = `t`.`destination_WH_link`))) left join `location` `l` on((`l`.`location_id` = `t`.`source_location_link`))) left join `location` `l2` on((`l2`.`location_id` = `t`.`destination_location_link`))) left join `reason_code` `r` on((`r`.`reasoncode_id` = `t`.`reason_code_link`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_allocated_stocks`
--

/*!50001 DROP VIEW IF EXISTS `view_allocated_stocks`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_allocated_stocks` AS select `item`.`item_code` AS `item_code`,`item`.`description` AS `description`,`alloc`.`cases` AS `cases`,`alloc`.`pieces` AS `pieces`,`ord`.`Order_number` AS `order_number`,`cust`.`Customer_number` AS `customer_number`,`cust`.`Customer_name` AS `customer_name`,`price`.`selling_price` AS `price_per_case`,((`price`.`selling_price` / `item`.`packsize`) / `item`.`pack_size_bo`) AS `price_per_piece`,((`price`.`selling_price` * `alloc`.`cases`) + (`dummy`.`price_per_piece` * `alloc`.`pieces`)) AS `item_amount` from (((((`allocated_stocks` `alloc` join `inventory_dummy_2` `dummy` on((`alloc`.`inventory_dummy_id` = `dummy`.`id`))) join `item_master` `item` on((`dummy`.`item` = `item`.`Item_master2_id`))) join `price_selling` `price` on((`item`.`Item_master2_id` = `price`.`item_id`))) join `orders` `ord` on((`dummy`.`order_id` = `ord`.`Order_id`))) join `customers` `cust` on((`ord`.`customer_id` = `cust`.`Customer_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_inventory_dummy_2`
--

/*!50001 DROP VIEW IF EXISTS `view_inventory_dummy_2`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_inventory_dummy_2` AS select `d`.`id` AS `id`,`o`.`Order_number` AS `order_number`,`p`.`code` AS `price_type`,`l`.`Code` AS `location`,`i`.`item_code` AS `item_code`,`i`.`description` AS `description`,`d`.`cases` AS `cases`,`d`.`pieces` AS `pieces`,`d`.`price_per_piece` AS `price_per_piece`,`d`.`line_amount` AS `line_amount`,`d`.`lot_number` AS `lot_number` from ((((`inventory_dummy_2` `d` left join `orders` `o` on((`o`.`Order_id` = `d`.`order_id`))) left join `pricetype` `p` on((`p`.`pricetype_id` = `d`.`price_type`))) left join `location` `l` on((`l`.`location_id` = `d`.`location`))) left join `item_master` `i` on((`i`.`Item_master2_id` = `d`.`item`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_pickline`
--

/*!50001 DROP VIEW IF EXISTS `view_pickline`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_pickline` AS select `item`.`item_code` AS `item_code`,`item`.`description` AS `description`,`pick`.`allocated_cases` AS `cases`,`pick`.`allocated_pieces` AS `pieces`,`ord`.`Order_number` AS `order_number`,`cust`.`Customer_number` AS `customer_number`,`cust`.`Customer_name` AS `customer_name`,`price`.`selling_price` AS `price_per_case`,((`price`.`selling_price` / `item`.`packsize`) / `item`.`pack_size_bo`) AS `price_per_piece`,((`price`.`selling_price` * `pick`.`allocated_cases`) + (`dummy`.`price_per_piece` * `pick`.`allocated_pieces`)) AS `item_amount` from (((((`pickline` `pick` join `inventory_dummy_2` `dummy` on((`pick`.`inventory_dummy_id` = `dummy`.`id`))) join `item_master` `item` on((`dummy`.`item` = `item`.`Item_master2_id`))) join `price_selling` `price` on((`item`.`Item_master2_id` = `price`.`item_id`))) join `orders` `ord` on((`dummy`.`order_id` = `ord`.`Order_id`))) join `customers` `cust` on((`ord`.`customer_id` = `cust`.`Customer_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_pickhead`
--

/*!50001 DROP VIEW IF EXISTS `view_pickhead`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_pickhead` AS select `h`.`pickhead_id` AS `pickhead_id`,`h`.`pick_number` AS `pick_number`,`u`.`username` AS `username`,`h`.`pick_date` AS `pick_date`,`h`.`is_successful` AS `is_successful`,`h`.`is_assigned` AS `is_assigned`,`h`.`is_gatepass_printed` AS `is_gatepass_printed`,`g`.`gatepass_id` AS `gatepass_id`,`g`.`gatepass_number` AS `gatepass_number` from ((`pickhead` `h` join `users` `u` on((`h`.`user_id` = `u`.`id`))) join `gatepass` `g` on((`h`.`gatepass_id` = `g`.`gatepass_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_user_permissions`
--

/*!50001 DROP VIEW IF EXISTS `view_user_permissions`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `view_user_permissions` AS select `up`.`id` AS `permission_id`,`u`.`id` AS `user_id`,`u`.`username` AS `username`,`p`.`program_id` AS `program_id`,`p`.`program_name` AS `program_name`,`p`.`program_category` AS `program_category` from ((`user_permissions` `up` join `users` `u` on((`up`.`user_id` = `u`.`id`))) join `programs` `p` on((`up`.`program_id` = `p`.`program_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-11-09 16:58:22
