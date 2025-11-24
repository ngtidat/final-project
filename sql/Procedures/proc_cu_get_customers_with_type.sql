DELIMITER $$

CREATE PROCEDURE `proc_cu_get_customers_with_type`()
BEGIN
  SELECT 
        c.customer_id AS CustomerId,
        c.customer_name AS CustomerName,
        c.customer_tax_code AS CustomerTaxCode,
        c.customer_type_id AS CustomerTypeId,
        c.shipping_address AS ShippingAddress,
        c.customer_phone AS CustomerPhone,
        c.last_purchase_date AS LastPurchaseDate,
        c.purchase_items AS PurchaseItems,
        c.purchase_item_name AS PurchaseItemName,
        c.gender AS Gender,
        c.customer_email AS CustomerEmail,
        ct.customer_type_id AS CustomerTypeId,
        ct.customer_type_name AS CustomerTypeName
    FROM customer c
    LEFT JOIN customer_type ct 
        ON c.customer_type_id = ct.customer_type_id;
END
$$

DELIMITER ;