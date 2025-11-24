DELIMITER $$

CREATE FUNCTION `func_cu_gen_customer_id`()
  RETURNS CHAR(14) CHARSET utf8mb4 COLLATE utf8mb4_0900_as_ci
  READS SQL DATA
BEGIN
    DECLARE prefix CHAR(8); 
    DECLARE seq INT;
    DECLARE code CHAR(14);

    SET prefix = CONCAT('KH', DATE_FORMAT(NOW(), '%Y%m'));

    SELECT IFNULL(MAX(CAST(SUBSTRING(customer_id, 9, 6) AS UNSIGNED)), 0) + 1
    INTO seq
    FROM customer;

    SET code = CONCAT(prefix, LPAD(seq, 6, '0'));

    RETURN code;
END
$$

DELIMITER ;