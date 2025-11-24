DELIMITER $$

CREATE PROCEDURE `proc_add_customer`(
    IN p_customer_name VARCHAR(128),
    IN p_customer_address VARCHAR(255),
    IN p_customer_phone VARCHAR(11),
    IN p_customer_email VARCHAR(100),
    IN p_customer_type_id CHAR(36),
    IN p_customer_tax_code VARCHAR(20),
    IN p_customer_industry VARCHAR(255),
    IN p_gender TINYINT,
    IN p_other_phone_number VARCHAR(11),
    IN p_last_purchase_date DATETIME,
    IN p_purchase_items VARCHAR(255),
    IN p_purchase_item_name VARCHAR(255),
    IN p_shipping_address VARCHAR(255),
    IN p_customer_avatar TEXT,
    IN p_created_at DATETIME
)
BEGIN
    DECLARE v_customer_id CHAR(14);

    SET v_customer_id = func_cu_gen_customer_id();

    INSERT INTO customer (
        customer_id,
        customer_name,
        customer_type_id,
        customer_address,
        customer_phone,
        customer_email,
        customer_tax_code,
        customer_industry,
        gender,
        customer_avatar,
        other_phone_number,
        last_purchase_date,
        purchase_items,
        purchase_item_name,
        shipping_address,
        created_at
    )
    VALUES (
        v_customer_id,
        p_customer_name,
        p_customer_type_id,
        p_customer_address,
        p_customer_phone,
        p_customer_email,
        p_customer_tax_code,
        p_customer_industry,
        p_gender,
        p_customer_avatar,
        p_other_phone_number,
        p_last_purchase_date,
        p_purchase_items,
        p_purchase_item_name,
        p_shipping_address,
        p_created_at
    );
END
$$

DELIMITER ;