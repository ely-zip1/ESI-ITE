CREATE VIEW view_sales_order AS
    SELECT DISTINCT
        o.order_id,
        o.order_number,
        o.order_date,
        o.required_ship_date,
        o.order_note,
        o.order_amount,
        o.cases,
        o.pieces,
        o.served,
        o.picked,
        o.printed,
        c.customer_id,
        c.customer_number,
        c.customer_name,
        c.address_main,
        c.address_city,
        c.address_province,
        c.address_zipcode,
        c.telephone,
        c.taxrate,
        c.credit_limit,
        c.net_sales,
        c.tin_number,
        c.entry_date,
        c.is_bad,
        t.trade_class_id,
        t.code AS trade_code,
        t.description AS trade_description,
        s.salesman_id,
        s.salesman_number,
        s.salesman_name,
        r.route_id,
        r.code AS route_code,
        r.description AS route_description,
        trm.term_id,
        trm.term_code,
        trm.term_description,
        trm.discount_1,
        trm.discount_2,
        trm.discount_3,
        trm.days AS term_days,
        pt.pricetype_id,
        pt.code AS price_code,
        pt.description AS price_description,
        pt.modify
    FROM
        orders o
            LEFT JOIN
        customers c ON o.customer_id = c.customer_id
            LEFT JOIN
        salesman s ON o.salesman_id = s.salesman_id
            LEFT JOIN
        routes r ON o.route_id = r.route_id
            LEFT JOIN
        terms trm ON o.term_id = trm.term_id
            LEFT JOIN
        trade_class t ON c.trade_class_id = t.trade_class_id
            LEFT JOIN
        so_pricetype pt ON o.price_id = pt.pricetype_id
