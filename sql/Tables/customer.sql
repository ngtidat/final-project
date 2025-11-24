CREATE TABLE `customer` (
  `customer_id` CHAR(14) NOT NULL,
  `customer_name` VARCHAR(128) NOT NULL,
  `customer_address` VARCHAR(255) DEFAULT NULL,
  `customer_phone` VARCHAR(11) DEFAULT NULL,
  `customer_email` VARCHAR(100) DEFAULT NULL,
  `customer_tax_code` VARCHAR(20) DEFAULT NULL,
  `customer_type_id` CHAR(36) DEFAULT NULL COMMENT 'Nhom khach hang: NBH01/LKHA/VIP',
  `customer_industry` VARCHAR(255) DEFAULT NULL,
  `gender` TINYINT(1) DEFAULT NULL COMMENT '0=Nam,1=Nu,2=Khac',
  `other_phone_number` VARCHAR(11) DEFAULT NULL,
  `created_at` DATETIME DEFAULT NULL,
  `updated_at` DATETIME DEFAULT NULL,
  `deleted_at` DATETIME DEFAULT NULL,
  `is_deleted` TINYINT DEFAULT 0,
  `last_purchase_date` DATETIME DEFAULT NULL,
  `purchase_items` VARCHAR(255) DEFAULT NULL,
  `purchase_item_name` VARCHAR(255) DEFAULT NULL,
  `shipping_address` VARCHAR(255) DEFAULT NULL,
  `customer_avatar` TEXT DEFAULT NULL,
  PRIMARY KEY (customer_id)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 4096,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_as_ci,
ROW_FORMAT = DYNAMIC;

ALTER TABLE `customer` 
  ADD UNIQUE INDEX customer_email(customer_email);

ALTER TABLE `customer` 
  ADD UNIQUE INDEX customer_phone(customer_phone);

ALTER TABLE `customer` 
  ADD CONSTRAINT `FK_customer_customer_type_id` FOREIGN KEY (customer_type_id)
    REFERENCES customer_type(customer_type_id) ON DELETE CASCADE;