create view view_transaction_entry as 
select
	t.entry_id as id,
	t.trans_no as transaction_number,
	tt.id as transaction_type,
	t.doc_no as document_number,
	t.trans_date as transaction_date,
	w.code as source_warehouse_code,
	w.name as source_warehouse,
	l.code as source_location_code,
	l.location as source_location,
	w2.code as destination_warehouse_code,
	w2.name as destination_warehouse,
	l2.code as destination_location_code,
	l2.location as destination_location,
	t.price_category,
	t.price_type,
	r.reason_code,
	r.reason_description,
	t.comment,
	t.status
from transaction_entry t
	left join transaction_type tt on tt.id = t.trans_type_link
	left join warehouse w on w.warehouse_id = t.source_wh_link
	left join warehouse w2 on w2.warehouse_id = t.destination_wh_link
	left join location l on l.location_id = t.source_location_link
	left join location l2 on l2.location_id = t.destination_location_link
	left join reason_code r on r.reasoncode_id = t.reason_code_link
	