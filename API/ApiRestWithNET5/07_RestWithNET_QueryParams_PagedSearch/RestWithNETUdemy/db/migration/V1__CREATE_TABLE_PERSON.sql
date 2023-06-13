CREATE TABLE `person` (
  `id` bigint(20) AUTO_INCREMENT PRIMARY KEY,
  `first_name` varchar(80) NOT NULL,
  `last_name` varchar(80) NOT NULL,
  `address` varchar(100) NOT NULL,
  `gender` varchar(6) NOT NULL
)