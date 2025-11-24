DELIMITER $$

CREATE PROCEDURE `proc_get_customer_by_id`(
  IN p_customer_id VARCHAR(14))
BEGIN
SELECT 
        c.customer_id AS CustomerId,
        c.customer_name AS CustomerName,
        c.customer_tax_code AS CustomerTaxCode,
        c.shipping_address AS ShippingAddress,
        c.customer_email AS CustomerEmail,
        c.customer_industry AS CustomerIndustry,
        c.customer_address AS CustomerAddress,
        c.customer_phone AS CustomerPhone,
        c.other_phone_number AS OtherPhoneNumber,
        c.last_purchase_date AS LastPurchaseDate,
        c.purchase_items AS PurchaseItems,
        c.purchase_item_name AS PurchaseItemName,
        c.gender AS Gender,
        c.customer_avatar AS CustomerAvatar,
        c.customer_type_id AS CustomerTypeId,
        ct.customer_type_name AS CustomerTypeName
    FROM customer c
    LEFT JOIN customer_type ct 
        ON c.customer_type_id = ct.customer_type_id
    WHERE c.customer_id = p_customer_id
      AND c.is_deleted = 0;
END
$$

DELIMITER ;