DELIMITER $$

CREATE FUNCTION `func_check_phone_unique`(p_phone VARCHAR(11))
  RETURNS TINYINT(4)
  READS SQL DATA
BEGIN
  DECLARE countPhone INT;

    SELECT COUNT(*) INTO countPhone
    FROM customer
    WHERE customer_phone = p_phone;

    RETURN countPhone > 0;
END
$$

DELIMITER ;