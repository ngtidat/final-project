CREATE TABLE `customer_type` (
  `customer_type_id` CHAR(36) NOT NULL,
  `customer_type_name` VARCHAR(100) NOT NULL,
  `created_at` DATETIME DEFAULT NULL,
  `created_by_id` CHAR(36) DEFAULT NULL,
  `updated_at` DATETIME DEFAULT NULL,
  `updated_by_id` CHAR(36) DEFAULT NULL,
  `deleted_at` DATETIME DEFAULT NULL,
  `deleted_by_id` CHAR(36) DEFAULT NULL,
  `is_deleted` TINYINT DEFAULT NULL,
  PRIMARY KEY (customer_type_id)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_as_ci,
ROW_FORMAT = DYNAMIC;

ALTER TABLE `customer_type` 
  ADD UNIQUE INDEX UK_customer_type_created_by_id(created_by_id);