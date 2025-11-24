DELIMITER $$

CREATE PROCEDURE `proc_paginate_customers`(
    IN p_page_index INT,
    IN p_page_size INT,
    IN p_search VARCHAR(255),
    IN p_sort_column VARCHAR(50),
    IN p_sort_direction TINYINT
)
BEGIN
    DECLARE v_offset INT DEFAULT (p_page_index - 1) * p_page_size;
    DECLARE v_sort_dir VARCHAR(4);
    DECLARE v_like_value TEXT;
    DECLARE v_where_clause TEXT DEFAULT '';
    DECLARE v_sql TEXT;

    IF p_sort_column IS NULL OR p_sort_column = '' THEN
        SET p_sort_column = 'c.created_at';
    END IF;

    SET v_sort_dir = IF(p_sort_direction = 1, 'ASC', 'DESC');

    IF p_search IS NULL THEN
        SET p_search = '';
    END IF;
    SET v_like_value = CONCAT('%', p_search, '%');

    SET v_where_clause = ' WHERE c.is_deleted = 0';

    IF p_search <> '' THEN
        SET v_where_clause = CONCAT(
            v_where_clause,
            ' AND (c.customer_email LIKE "', v_like_value,
            '" OR c.customer_phone LIKE "', v_like_value,
            '" OR c.customer_name LIKE "', v_like_value, '")'
        );
    END IF;

    SET @v_sql = CONCAT(
        'SELECT 
            c.customer_id AS CustomerId,
            c.customer_name AS CustomerName,
            c.customer_tax_code AS CustomerTaxCode,
            c.shipping_address AS ShippingAddress,
            c.customer_phone AS CustomerPhone,
            c.last_purchase_date AS LastPurchaseDate,
            c.purchase_items AS PurchaseItems,
            c.purchase_item_name AS PurchaseItemName,
            c.gender AS Gender,
            c.customer_email AS CustomerEmail,
            c.customer_avatar AS CustomerAvatar,
            c.customer_type_id AS CustomerTypeId,
            ct.customer_type_name AS CustomerTypeName
        FROM customer c
        LEFT JOIN customer_type ct ON c.customer_type_id = ct.customer_type_id',
        v_where_clause,
        ' ORDER BY ', p_sort_column, ' ', v_sort_dir,
        ' LIMIT ', p_page_size,
        ' OFFSET ', v_offset
    );

    PREPARE stmt FROM @v_sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;

    SET @count_sql = CONCAT(
        'SELECT COUNT(*) AS TotalCount FROM customer c LEFT JOIN customer_type ct ON c.customer_type_id = ct.customer_type_id',
        v_where_clause
    );
    PREPARE stmt_count FROM @count_sql;
    EXECUTE stmt_count;
    DEALLOCATE PREPARE stmt_count;
END
$$

DELIMITER ;