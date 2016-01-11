create view view_inventory_dummy as
select
	d.id,
	t.trans_no as transaction_code,
	l.code as location_code,
	d.price_type,
	i.item_code,
	i.item_description,
	d.cases,
	d.pieces,
	d.expiration_date as expiration,
	d.priceperpiece as price_per_piece,
	d.linevalue as line_amount
from inventory_dummy d
	left join transaction_entry t on t.entry_id = d.transaction_link
	left join location l on l.location_id = d.location_link
	left join item_master i on i.item_id = d.item_link
	