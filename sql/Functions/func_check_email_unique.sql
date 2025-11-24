DELIMITER $$

CREATE FUNCTION `func_check_email_unique`(p_email VARCHAR(128))
  RETURNS TINYINT(4)
  READS SQL DATA
BEGIN
  DECLARE countEmail INT;

    SELECT COUNT(*) INTO countEmail
    FROM customer
    WHERE customer_email = p_email;

    RETURN countEmail > 0;
END
$$

DELIMITER ;