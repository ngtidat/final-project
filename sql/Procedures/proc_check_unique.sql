DELIMITER $$

CREATE PROCEDURE `proc_check_unique`(
    IN p_table_name VARCHAR(64),
    IN p_column_name VARCHAR(64),
    IN p_column_value VARCHAR(255),
    IN p_primary_key_name VARCHAR(64),
    IN p_primary_key_value VARCHAR(14)
)
BEGIN
    SET @sql = CONCAT(
        'SELECT COUNT(*) AS CountResult
         FROM ', p_table_name,
        ' WHERE ', p_column_name, ' = ? ',
        ' AND (', p_primary_key_name, ' <> ? OR ? IS NULL)'
    );

    PREPARE stmt FROM @sql;
    SET @val = p_column_value;
    SET @id = p_primary_key_value;

    EXECUTE stmt USING @val, @id, @id;
    DEALLOCATE PREPARE stmt;
END
$$

DELIMITER ;