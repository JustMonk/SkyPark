-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               5.5.45 - MySQL Community Server (GPL)
-- ОС Сервера:                   Win32
-- HeidiSQL Версия:              9.3.0.4984
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Дамп структуры базы данных skypark
CREATE DATABASE IF NOT EXISTS `skypark` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `skypark`;


-- Дамп структуры для таблица skypark.accounts
CREATE TABLE IF NOT EXISTS `accounts` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `account type` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `username` (`username`),
  KEY `FK_accounts_account_types` (`account type`),
  CONSTRAINT `FK_accounts_account_types` FOREIGN KEY (`account type`) REFERENCES `account_types` (`type`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы skypark.accounts: ~2 rows (приблизительно)
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
INSERT INTO `accounts` (`ID`, `username`, `password`, `account type`) VALUES
	(1, 'User1', '555', 'Оператор'),
	(2, 'User2', '777', 'Менеджер');
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;


-- Дамп структуры для таблица skypark.account_types
CREATE TABLE IF NOT EXISTS `account_types` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `type` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `type` (`type`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы skypark.account_types: ~2 rows (приблизительно)
/*!40000 ALTER TABLE `account_types` DISABLE KEYS */;
INSERT INTO `account_types` (`ID`, `type`) VALUES
	(2, 'Менеджер'),
	(1, 'Оператор');
/*!40000 ALTER TABLE `account_types` ENABLE KEYS */;


-- Дамп структуры для таблица skypark.active
CREATE TABLE IF NOT EXISTS `active` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` text,
  `type` varchar(100) DEFAULT NULL,
  `gosnumber_auto` varchar(50) DEFAULT NULL,
  `entry` datetime DEFAULT NULL,
  `IsPaid` float DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `type` (`type`),
  CONSTRAINT `FK_active_tarif` FOREIGN KEY (`type`) REFERENCES `tarif` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы skypark.active: ~2 rows (приблизительно)
/*!40000 ALTER TABLE `active` DISABLE KEYS */;
INSERT INTO `active` (`id`, `code`, `type`, `gosnumber_auto`, `entry`, `IsPaid`) VALUES
	(1, 'V4I2-LKKE-6315-ZDXA', 'Легковое ТС', 'о515хр', '2016-12-20 10:20:15', NULL),
	(2, 'R5N2-B8B1-2U65-MYM4', 'Мото', 'р873кн', '2016-12-20 11:07:22', NULL);
/*!40000 ALTER TABLE `active` ENABLE KEYS */;


-- Дамп структуры для таблица skypark.logs
CREATE TABLE IF NOT EXISTS `logs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` text,
  `type` varchar(100) DEFAULT NULL,
  `gosnumber_auto` varchar(50) DEFAULT NULL,
  `entry` datetime DEFAULT NULL,
  `departure` datetime DEFAULT NULL,
  `price` float DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `type` (`type`),
  CONSTRAINT `FK_logs_tarif` FOREIGN KEY (`type`) REFERENCES `tarif` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы skypark.logs: ~2 rows (приблизительно)
/*!40000 ALTER TABLE `logs` DISABLE KEYS */;
INSERT INTO `logs` (`id`, `code`, `type`, `gosnumber_auto`, `entry`, `departure`, `price`) VALUES
	(1, 'J5C8-Z104-OZP3-6F3F', 'Легковое ТС', 'е555кх', '2016-10-18 13:10:00', '2016-11-18 13:11:08', 145),
	(2, 'N2J7-FYDQ-N3AX-1V54', 'Легковое ТС', 'к659сс', '2016-12-18 13:10:44', '2016-12-18 13:11:15', 320);
/*!40000 ALTER TABLE `logs` ENABLE KEYS */;


-- Дамп структуры для таблица skypark.settings
CREATE TABLE IF NOT EXISTS `settings` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` text,
  `value` text,
  `options` text,
  `active` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы skypark.settings: ~4 rows (приблизительно)
/*!40000 ALTER TABLE `settings` DISABLE KEYS */;
INSERT INTO `settings` (`id`, `name`, `value`, `options`, `active`) VALUES
	(1, 'sitename', 'SkyPark', NULL, b'1'),
	(2, 'email', 'skyboxpark@skybox.com', NULL, b'1'),
	(3, 'phone', '+7 (771) 772-72-73', NULL, b'1'),
	(4, 'address', 'Сочи, ул. Академика-17А', NULL, b'1');
/*!40000 ALTER TABLE `settings` ENABLE KEYS */;


-- Дамп структуры для таблица skypark.tarif
CREATE TABLE IF NOT EXISTS `tarif` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` text,
  `name` varchar(100) NOT NULL,
  `price_temp` decimal(10,0) DEFAULT NULL,
  `price_time` decimal(10,0) DEFAULT NULL,
  `options` text,
  `active` int(11) DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы skypark.tarif: ~11 rows (приблизительно)
/*!40000 ALTER TABLE `tarif` DISABLE KEYS */;
INSERT INTO `tarif` (`id`, `type`, `name`, `price_temp`, `price_time`, `options`, `active`) VALUES
	(1, 'Программный', 'Почасовая', 10, 2, NULL, 1),
	(2, 'Программный', 'Посуточная', NULL, 80, NULL, 1),
	(3, 'Программный', 'Месяц', NULL, 2600, NULL, 1),
	(4, 'Стандарт', 'Легковое ТС', 100, 100, NULL, 1),
	(5, 'Стандарт', 'Грузовое ТС', 130, 125, NULL, 1),
	(6, 'Стандарт', 'Прицеп ТС', 130, 125, NULL, 1),
	(7, 'Стандарт', 'Тягач с прицепом', 150, 140, NULL, 1),
	(8, 'Стандарт', 'Мото', 70, 90, NULL, 1),
	(9, 'Услуга', 'Тёплый бокс', 20, 18, NULL, 1),
	(10, 'Промокод', 'START15', 15, 15, NULL, 1),
	(12, 'Услуга', 'Печеньки', 100, 90, NULL, 0);
/*!40000 ALTER TABLE `tarif` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
