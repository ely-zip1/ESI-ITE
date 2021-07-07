CREATE DEFINER=`root`@`localhost` PROCEDURE `procedure_inventory_master`()
begin

declare totalRows int;
declare x int;
declare id int;

set totalRows = (select count(*) from inventory_master2);

set x = 0;

while x < totalRows
do

set id = (select inventory2_id from inventory_master2 limit 1 offset x);

/*update inventory_master2 set I_Cases = 20 where inventory2_id = id;*/

update inventory_master2 set expiration_date = date_add(curdate(), interval 5 year) where inventory2_id = id;

set x = x+1;

end while;

end