-- phpMyAdmin SQL Dump
-- version 3.5.5
-- http://www.phpmyadmin.net
--
-- Host: sql7.freemysqlhosting.net
-- Generation Time: May 22, 2016 at 10:34 PM
-- Server version: 5.5.49-0ubuntu0.14.04.1
-- PHP Version: 5.3.28

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `sql7112557`
--
CREATE DATABASE `sql7112557` DEFAULT CHARACTER SET cp1251 COLLATE cp1251_general_ci;
USE `sql7112557`;

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`sql7112557`@`%` PROCEDURE `add_driverowner`(IN `driver_owner_id` BIGINT(10), IN `first_name` VARCHAR(20), IN `second_name` VARCHAR(20), IN `last_name` VARCHAR(20), IN `sex` ENUM('М','Ж'), IN `nationality` VARCHAR(20), IN `birth_date` DATE, IN `birth_place` VARCHAR(30), IN `residence` VARCHAR(50), IN `tel_num` VARCHAR(20), IN `email` VARCHAR(30), IN `remaining_pts` TINYINT(2), IN `licence_issue_date` DATE, IN `licence_expiry_date` DATE, IN `licence_issued_by` VARCHAR(30), IN `a1_acquiry_date` DATE, IN `a1_expiry_date` DATE, IN `a1_restrictions` VARCHAR(30), IN `a_acquiry_date` DATE, IN `a_expiry_date` DATE, IN `a_restrictions` VARCHAR(30), IN `b1_acquiry_date` DATE, IN `b1_expiry_date` DATE, IN `b1_restrictions` VARCHAR(30), IN `b_acquiry_date` DATE, IN `b_expiry_date` DATE, IN `b_restrictions` VARCHAR(30), IN `c1_acquiry_date` DATE, IN `c1_expiry_date` DATE, IN `c1_restrictions` VARCHAR(30), IN `c_acquiry_date` DATE, IN `c_expiry_date` DATE, IN `c_restrictions` VARCHAR(30), IN `d1_acquiry_date` DATE, IN `d1_expiry_date` DATE, IN `d1_restrictions` VARCHAR(30), IN `d_acquiry_date` DATE, IN `d_expiry_date` DATE, IN `d_restrictions` VARCHAR(30), IN `be_acquiry_date` DATE, IN `be_expiry_date` DATE, IN `be_restrictions` VARCHAR(30), IN `c1e_acquiry_date` DATE, IN `c1e_expiry_date` DATE, IN `c1e_restrictions` VARCHAR(30), IN `ce_acquiry_date` DATE, IN `ce_expiry_date` DATE, IN `ce_restrictions` VARCHAR(30), IN `d1e_acquiry_date` DATE, IN `d1e_expiry_date` DATE, IN `d1e_restrictions` VARCHAR(30), IN `de_acquiry_date` DATE, IN `de_expiry_date` DATE, IN `de_restrictions` VARCHAR(30), IN `ttb_acquiry_date` DATE, IN `ttb_expiry_date` DATE, IN `ttb_restrictions` VARCHAR(30), IN `ttm_acquiry_date` DATE, IN `ttm_expiry_date` DATE, IN `ttm_restrictions` VARCHAR(30), IN `tkt_acquiry_date` DATE, IN `tkt_expiry_date` DATE, IN `tkt_restrictions` VARCHAR(30), IN `m_acquiry_date` DATE, IN `m_expiry_date` DATE, IN `m_restrictions` VARCHAR(30))
    NO SQL
BEGIN
	INSERT INTO driver_owners (driver_owner_id,first_name,second_name,last_name,
                                             sex,nationality,birth_date,birth_place,residence,tel_num,email,remaining_pts,licence_issue_date,
                                             licence_expiry_date,licence_issued_by,
                                             a1_acquiry_date,a1_expiry_date,a1_restrictions,
                                             a_acquiry_date,a_expiry_date,a_restrictions,
                                             b1_acquiry_date,b1_expiry_date,b1_restrictions,
                                             b_acquiry_date,b_expiry_date,b_restrictions,
                                             c1_acquiry_date,c1_expiry_date,c1_restrictions,
                                             c_acquiry_date,c_expiry_date,c_restrictions,
                                             d1_acquiry_date,d1_expiry_date,d1_restrictions,
                                             d_acquiry_date,d_expiry_date,d_restrictions,
                                             be_acquiry_date,be_expiry_date,be_restrictions,
                                             c1e_acquiry_date,c1e_expiry_date,c1e_restrictions,
                                             ce_acquiry_date,ce_expiry_date,ce_restrictions,
                                             d1e_acquiry_date,d1e_expiry_date,d1e_restrictions,
                                             de_acquiry_date,de_expiry_date,de_restrictions,
                                             ttb_acquiry_date,ttb_expiry_date,ttb_restrictions,
                                             ttm_acquiry_date,ttm_expiry_date,ttm_restrictions,
                                             tkt_acquiry_date,tkt_expiry_date,tkt_restrictions,
                                             m_acquiry_date,m_expiry_date,m_restrictions)
        VALUES(driver_owner_id,first_name,second_name,last_name,
                                             sex,nationality,birth_date,birth_place,residence,tel_num,email,remaining_pts,licence_issue_date,
                                             licence_expiry_date,licence_issued_by,
                                             a1_acquiry_date,a1_expiry_date,a1_restrictions,
                                             a_acquiry_date,a_expiry_date,a_restrictions,
                                             b1_acquiry_date,b1_expiry_date,b1_restrictions,
                                             b_acquiry_date,b_expiry_date,b_restrictions,
                                             c1_acquiry_date,c1_expiry_date,c1_restrictions,
                                             c_acquiry_date,c_expiry_date,c_restrictions,
                                             d1_acquiry_date,d1_expiry_date,d1_restrictions,
                                             d_acquiry_date,d_expiry_date,d_restrictions,
                                             be_acquiry_date,be_expiry_date,be_restrictions,
                                             c1e_acquiry_date,c1e_expiry_date,c1e_restrictions,
                                             ce_acquiry_date,ce_expiry_date,ce_restrictions,
                                             d1e_acquiry_date,d1e_expiry_date,d1e_restrictions,
                                             de_acquiry_date,de_expiry_date,de_restrictions,
                                             ttb_acquiry_date,ttb_expiry_date,ttb_restrictions,
                                             ttm_acquiry_date,ttm_expiry_date,ttm_restrictions,
                                             tkt_acquiry_date,tkt_expiry_date,tkt_restrictions,
                                             m_acquiry_date,m_expiry_date,m_restrictions);
END$$

CREATE DEFINER=`sql7112557`@`%` PROCEDURE `add_penalty_to_driverowner`(IN `user_id` BIGINT(10) UNSIGNED, IN `driver_owner_id` BIGINT(10) UNSIGNED, IN `date_time_issued` DATETIME, IN `penalty_date_time` DATE, IN `location` VARCHAR(100), IN `latitude` DOUBLE, IN `longtitude` DOUBLE, IN `description` TINYTEXT, IN `disagreement` MEDIUMTEXT)
    NO SQL
BEGIN
	INSERT INTO penalties (user_id,driver_owner_id,date_time_issued,penalty_date_time,
        			location,latitude,longtitude,description,disagreement)
		VALUES(
                	(SELECT users.user_id FROM users WHERE users.user_id=user_id),
                	(SELECT driver_owners.driver_owner_id FROM driver_owners WHERE driver_owners.driver_owner_id=driver_owner_id),
                        date_time_issued,
                        penalty_date_time,
                        location,
                        latitude,
                        longtitude,
                        description,
                        disagreement
                      );
                                                
                                                
                                                
 		
END$$

CREATE DEFINER=`sql7112557`@`%` PROCEDURE `add_registration`(IN `reg_num` VARCHAR(13), IN `driver_owner_id` BIGINT(10), IN `car_manufacturer` VARCHAR(15), IN `car_model` VARCHAR(15), IN `car_color` VARCHAR(15), IN `car_type` VARCHAR(50), IN `car_cubage` INT(6), IN `car_hp` INT(5), IN `car_vin` VARCHAR(17), IN `first_reg_date` DATE, IN `civil_insurance` BIT(1), IN `civil_insurer` VARCHAR(20), IN `civil_insurance_start_date` DATE, IN `civil_insurance_end_date` DATE, IN `has_vignette` BIT(1), IN `vignette_valid_until` DATE, IN `damage_insurance` BIT(1), IN `damage_insurer` VARCHAR(20), IN `damage_insurance_start_date` DATE, IN `damage_insurance_end_date` DATE, IN `recent_certificate_reg_date` DATE)
BEGIN
	INSERT INTO registrations (reg_num,driver_owner_id,car_manufacturer,car_model,
		car_color,car_type,car_cubage,car_hp,car_vin,first_reg_date,
                civil_insurance,civil_insurer,civil_insurance_start_date,civil_insurance_end_date,
                has_vignette,vignette_valid_until,
                damage_insurance,damage_insurer,damage_insurance_start_date,damage_insurance_end_date,
                recent_certificate_reg_date)
	VALUES(reg_num,driver_owner_id,car_manufacturer,car_model,
		car_color,car_type,car_cubage,car_hp,car_vin,first_reg_date,
                civil_insurance,civil_insurer,civil_insurance_start_date,civil_insurance_end_date,
                has_vignette,vignette_valid_until,
                damage_insurance,damage_insurer,damage_insurance_start_date,damage_insurance_end_date,
                recent_certificate_reg_date);
END$$

CREATE DEFINER=`sql7112557`@`%` PROCEDURE `add_user_and_get_id`(IN `first_name` VARCHAR(20), IN `second_name` VARCHAR(20), IN `last_name` VARCHAR(20), IN `is_traffic_policeman` BIT(1), IN `password` VARCHAR(12))
    NO SQL
BEGIN
	INSERT INTO users (first_name,second_name,last_name,is_traffic_policeman,
		password)
	VALUES(first_name,second_name,last_name,is_traffic_policeman,password);

	SELECT user_id FROM users WHERE (users.first_name = first_name AND 
        users.second_name=second_name AND users.last_name =last_name AND
        users.is_traffic_policeman = is_traffic_policeman AND
        users.password) ORDER BY user_id DESC LIMIT 1;
END$$

CREATE DEFINER=`sql7112557`@`%` PROCEDURE `get_available_car_types`()
SELECT COLUMN_TYPE FROM information_schema.`COLUMNS`  WHERE TABLE_NAME = 'registrations' AND COLUMN_NAME = 'car_type'$$

CREATE DEFINER=`sql7112557`@`%` PROCEDURE `get_driverowner_data_by_id`(IN `driver_owner_id` BIGINT(10))
BEGIN
	SELECT * FROM driver_owners
        WHERE driver_owners.driver_owner_id = driver_owner_id;
END$$

CREATE DEFINER=`sql7112557`@`%` PROCEDURE `get_driverowner_penalties_info`(IN `id` BIGINT)
    READS SQL DATA
BEGIN

	SELECT * FROM penalties WHERE driver_owner_id = id;

END$$

CREATE DEFINER=`sql7112557`@`%` PROCEDURE `get_reg_by_regnum`(IN `reg_num` VARCHAR(13))
    NO SQL
BEGIN
	SELECT * FROM registrations
        WHERE registrations.reg_num = reg_num;
END$$

CREATE DEFINER=`sql7112557`@`%` PROCEDURE `get_user_by_id_and_pass`(IN id BIGINT(10),IN pass VARCHAR(12))
BEGIN
	SELECT * FROM users WHERE user_id = id AND password = pass;
END$$

CREATE DEFINER=`sql7112557`@`%` PROCEDURE `remove_penalty_by_id`(IN `id` BIGINT(20))
BEGIN
	DELETE FROM penalties WHERE penalty_id = id;
END$$

CREATE DEFINER=`sql7112557`@`%` PROCEDURE `update_user`(IN usr_id BIGINT(10),IN first_n VARCHAR(20),IN sec_n VARCHAR(20), IN last_n VARCHAR(20), IN is_traffic_p BIT(1), 
										IN pass VARCHAR(12))
BEGIN
		UPDATE users SET first_name =first_n,second_name = sec_n, last_name = last_n,
                is_traffic_policeman = is_traffic_p,password = pass WHERE user_id = usr_id;
	END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `driver_owners`
--

CREATE TABLE IF NOT EXISTS `driver_owners` (
  `driver_owner_id` bigint(10) unsigned NOT NULL,
  `first_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `second_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `last_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `sex` enum('М','Ж') COLLATE utf8_unicode_ci NOT NULL,
  `nationality` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `birth_date` date NOT NULL,
  `birth_place` varchar(30) COLLATE utf8_unicode_ci NOT NULL,
  `residence` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `tel_num` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `email` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `remaining_pts` tinyint(2) unsigned NOT NULL DEFAULT '39',
  `licence_issue_date` date NOT NULL,
  `licence_expiry_date` date NOT NULL,
  `licence_issued_by` varchar(30) COLLATE utf8_unicode_ci NOT NULL,
  `a1_acquiry_date` date DEFAULT NULL,
  `a1_expiry_date` date DEFAULT NULL,
  `a1_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `a_acquiry_date` date DEFAULT NULL,
  `a_expiry_date` date DEFAULT NULL,
  `a_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `b1_acquiry_date` date DEFAULT NULL,
  `b1_expiry_date` date DEFAULT NULL,
  `b1_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `b_acquiry_date` date DEFAULT NULL,
  `b_expiry_date` date DEFAULT NULL,
  `b_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `c1_acquiry_date` date DEFAULT NULL,
  `c1_expiry_date` date DEFAULT NULL,
  `c1_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `c_acquiry_date` date DEFAULT NULL,
  `c_expiry_date` date DEFAULT NULL,
  `c_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `d1_acquiry_date` date DEFAULT NULL,
  `d1_expiry_date` date DEFAULT NULL,
  `d1_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `d_acquiry_date` date DEFAULT NULL,
  `d_expiry_date` date DEFAULT NULL,
  `d_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `be_acquiry_date` date DEFAULT NULL,
  `be_expiry_date` date DEFAULT NULL,
  `be_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `c1e_acquiry_date` date DEFAULT NULL,
  `c1e_expiry_date` date DEFAULT NULL,
  `c1e_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ce_acquiry_date` date DEFAULT NULL,
  `ce_expiry_date` date DEFAULT NULL,
  `ce_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `d1e_acquiry_date` date DEFAULT NULL,
  `d1e_expiry_date` date DEFAULT NULL,
  `d1e_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `de_acquiry_date` date DEFAULT NULL,
  `de_expiry_date` date DEFAULT NULL,
  `de_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ttb_acquiry_date` date DEFAULT NULL,
  `ttb_expiry_date` date DEFAULT NULL,
  `ttb_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ttm_acquiry_date` date DEFAULT NULL,
  `ttm_expiry_date` date DEFAULT NULL,
  `ttm_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `tkt_acquiry_date` date DEFAULT NULL,
  `tkt_expiry_date` date DEFAULT NULL,
  `tkt_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `m_acquiry_date` date DEFAULT NULL,
  `m_expiry_date` date DEFAULT NULL,
  `m_restrictions` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`driver_owner_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `driver_owners`
--

INSERT INTO `driver_owners` (`driver_owner_id`, `first_name`, `second_name`, `last_name`, `sex`, `nationality`, `birth_date`, `birth_place`, `residence`, `tel_num`, `email`, `remaining_pts`, `licence_issue_date`, `licence_expiry_date`, `licence_issued_by`, `a1_acquiry_date`, `a1_expiry_date`, `a1_restrictions`, `a_acquiry_date`, `a_expiry_date`, `a_restrictions`, `b1_acquiry_date`, `b1_expiry_date`, `b1_restrictions`, `b_acquiry_date`, `b_expiry_date`, `b_restrictions`, `c1_acquiry_date`, `c1_expiry_date`, `c1_restrictions`, `c_acquiry_date`, `c_expiry_date`, `c_restrictions`, `d1_acquiry_date`, `d1_expiry_date`, `d1_restrictions`, `d_acquiry_date`, `d_expiry_date`, `d_restrictions`, `be_acquiry_date`, `be_expiry_date`, `be_restrictions`, `c1e_acquiry_date`, `c1e_expiry_date`, `c1e_restrictions`, `ce_acquiry_date`, `ce_expiry_date`, `ce_restrictions`, `d1e_acquiry_date`, `d1e_expiry_date`, `d1e_restrictions`, `de_acquiry_date`, `de_expiry_date`, `de_restrictions`, `ttb_acquiry_date`, `ttb_expiry_date`, `ttb_restrictions`, `ttm_acquiry_date`, `ttm_expiry_date`, `ttm_restrictions`, `tkt_acquiry_date`, `tkt_expiry_date`, `tkt_restrictions`, `m_acquiry_date`, `m_expiry_date`, `m_restrictions`) VALUES
(1231233132, 'ьяаь', 'ьоаояоая', 'ояоаяоаяояо', 'М', 'България', '0001-01-01', 'аяояоо', 'яоаяо', NULL, NULL, 37, '2016-05-01', '2026-05-01', 'аяоаяоояа', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(1234555555, 'asdad', 'dadadadad', 'asdad', 'Ж', 'България', '0001-01-01', 'asdad', 'asdad', '+359 89 91 333', 'asdad@gmail.com', 39, '2016-04-24', '2026-04-24', 'asdadad', NULL, '2016-03-30', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(1234567890, 'Goshu', 'Loshu', 'Pleshu', 'Ж', 'Джибути', '2000-01-02', 'dp', 'inachenp', '123213213', 'fdf@abv.bg', 37, '2016-05-28', '2026-06-06', 'asdsdsadada', '2016-05-08', NULL, 'asd', NULL, '2016-05-11', '', '2016-05-21', NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, '', NULL, NULL, ''),
(3213213131, 'asdfds', 'fdsfsf', 'fsfsf', 'М', 'България', '2000-01-01', 'sdfdsf', 'dfsfdsf', 'fdsf', 'dsfdsff', 39, '2016-05-01', '2026-05-01', 'sdfdsfdf', NULL, NULL, 'sdfsdf', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL', NULL, NULL, 'NULL'),
(3333333333, 'dfsdf', 'sdfs', 'ffsffsffd', 'М', 'България', '0001-01-01', 'sdfs', 'ffsff', '+359 89 91 331', 'dfsdf@gmail.com', 39, '2016-04-23', '2026-04-23', 'sdffsf', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(4444444444, 'sdfsf', 'sfsff', 'sfsff', 'М', 'България', '0001-01-01', 'sdfsf', 'fsffs', NULL, 'sdfsf@gmail.com', 39, '2016-04-23', '2026-04-23', 'sdfsf', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(5121323131, 'asdasd', 'sdfdsf', 'ggfgd', 'М', 'Зимбабве', '2000-01-20', 'adasd', 'afdsf', '123123', 'dfsf@', 37, '2016-05-04', '2026-05-26', 'asdsad', '2016-05-03', '2016-05-10', '"asdsa"', '2016-05-04', '2016-05-04', '"asda"', '2016-04-27', '2016-05-17', '"sddad"', '2016-05-04', '2016-05-10', '"dsadasd"', '2016-05-13', '2016-05-10', '"dfs"', '2016-05-20', '2016-05-11', '"fdsfds"', '2016-05-14', '2016-05-11', '"ffsff"', '2016-05-28', '2016-05-24', '"dsf"', '2016-06-05', '2016-05-11', '"sdfdsf"', '2016-05-22', '2016-05-11', '"ffsf"', '2016-04-30', '2016-05-03', '"sdfdsfd"', '2016-05-06', '2016-04-27', '"sfdsf"', '2016-05-06', '2016-05-11', '"sdf"', '2016-05-27', '2016-05-11', '"sfdsfdsf"', '2016-04-29', '2016-05-03', '"sdfds"', '2016-05-13', '2016-05-11', '"fsf"', '2016-05-18', '2016-05-17', '"sdfsf"'),
(5555555555, 'vxvvcxv', 'xcvxvv', 'xvxvvv', 'Ж', 'България', '0001-01-01', 'xvvxvxv', 'xvxvcxv', NULL, 'vxvvcxv@gmail.com', 39, '2016-04-23', '2026-04-23', 'xcvxvxv', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'xcv', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(8603178437, 'Пешо', 'Георгиев', 'Стоянов', 'М', 'България', '1986-03-17', 'София', 'гр. София жк Надежда 2 бл 222 ет 3 ап 15', '+359 89 91 332', 'pesho@gmail.com', 39, '2014-02-22', '2024-02-22', 'МВР София', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2014-02-18', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2014-02-18', NULL, NULL),
(9205158427, 'Гергана', 'Ивайлова', 'Първанова', 'Ж', 'България', '1992-05-15', 'Враца', 'гр. Враца ул. Връшка чука 2', '+359 89 91 330', 'gergana@gmail.com', 39, '2015-02-23', '2025-02-23', 'МВР Враца', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2015-01-18', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2015-01-11', NULL, NULL, '2015-01-18', NULL, NULL),
(9209122431, 'Ивайло', 'Груев', 'Гелев', 'М', 'Бермуда', '2001-01-19', 'София', 'гр. София ул Иван Петков 2 бл 333 ет 6 ап 16', NULL, NULL, 37, '2016-05-01', '2026-05-01', 'asdad', NULL, NULL, 'asd', '2016-05-19', '2016-05-25', 'asdafds', NULL, '2016-05-10', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(9292929292, 'asfsdfsfsfg', 'fdgfdhg', 'hfhhh', 'М', 'България', '2016-03-01', 'fgjhfhj', 'gfdg', '+359 69 93 333', 'asfsdfsfsfg@gmail.com', 39, '2016-03-01', '2016-04-28', 'afdsf', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(9403122826, 'Георги', 'Валентинов', 'Челенков', 'М', 'България', '1994-03-12', 'Дупница', 'гр. София ул Иван Петков 2 бл 333 ет 6 ап 15', '+359 89 73 79 123', 'resiloveca94@gmail.com', 39, '2012-07-02', '2022-07-02', 'МВР Кюстендил', '2016-04-06', '2016-04-07', 'agafas', NULL, NULL, NULL, NULL, NULL, NULL, '2012-05-29', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2012-05-29', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `penalties`
--

CREATE TABLE IF NOT EXISTS `penalties` (
  `penalty_id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `user_id` bigint(10) unsigned NOT NULL,
  `driver_owner_id` bigint(10) unsigned NOT NULL,
  `date_time_issued` datetime NOT NULL,
  `penalty_date_time` datetime NOT NULL,
  `location` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `latitude` double DEFAULT NULL,
  `longtitude` double DEFAULT NULL,
  `description` tinytext COLLATE utf8_unicode_ci NOT NULL,
  `disagreement` mediumtext COLLATE utf8_unicode_ci,
  PRIMARY KEY (`penalty_id`),
  KEY `user_id` (`user_id`),
  KEY `driver_owner_id` (`driver_owner_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=26 ;

--
-- Dumping data for table `penalties`
--

INSERT INTO `penalties` (`penalty_id`, `user_id`, `driver_owner_id`, `date_time_issued`, `penalty_date_time`, `location`, `latitude`, `longtitude`, `description`, `disagreement`) VALUES
(14, 9, 9403122826, '2016-04-14 09:00:00', '2016-04-14 09:23:39', 'София, бул. Андрей Сахаров № 3', 42.653594, 23.367765, 'Преминаване на Червен светофар', 'Нарушението е извършено при намалена видимост и с неразрешена скорост'),
(24, 9, 9403122826, '2016-05-15 09:48:17', '2016-05-15 00:00:00', '', 47.6785619, -122.1311156, 'Отнемане на предимство', 'Не съм отнел предимство... Просто не им го дадох изобщо'),
(25, 9, 9403122826, '2016-05-18 10:17:01', '2016-05-18 00:00:00', '', 47.80484, -122.13726, 'afdsf', 'dasdsad');

-- --------------------------------------------------------

--
-- Table structure for table `registrations`
--

CREATE TABLE IF NOT EXISTS `registrations` (
  `reg_num` varchar(13) COLLATE utf8_unicode_ci NOT NULL,
  `driver_owner_id` bigint(10) unsigned NOT NULL,
  `car_manufacturer` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `car_model` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `car_color` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `car_type` enum('Лек автомобил','Мотоциклет','Мотопед','Товарен автомобил','Седлови влекач','Товарно ремарке','Багажно и къмпинг ремарке','Автобус','Тролейбус','Трамвайна мотриса','Строителна техника','Земеделска и горска техника') COLLATE utf8_unicode_ci NOT NULL,
  `car_cubage` int(6) NOT NULL,
  `car_hp` int(5) NOT NULL,
  `car_vin` varchar(17) COLLATE utf8_unicode_ci NOT NULL,
  `first_reg_date` date NOT NULL,
  `civil_insurance` bit(1) NOT NULL DEFAULT b'0',
  `civil_insurer` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `civil_insurance_start_date` date DEFAULT NULL,
  `civil_insurance_end_date` date DEFAULT NULL,
  `has_vignette` bit(1) NOT NULL DEFAULT b'0',
  `vignette_valid_until` date DEFAULT NULL,
  `damage_insurance` bit(1) NOT NULL DEFAULT b'0',
  `damage_insurer` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `damage_insurance_start_date` date DEFAULT NULL,
  `damage_insurance_end_date` date DEFAULT NULL,
  `recent_certificate_reg_date` date NOT NULL,
  PRIMARY KEY (`reg_num`),
  KEY `driver_owner_id` (`driver_owner_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `registrations`
--

INSERT INTO `registrations` (`reg_num`, `driver_owner_id`, `car_manufacturer`, `car_model`, `car_color`, `car_type`, `car_cubage`, `car_hp`, `car_vin`, `first_reg_date`, `civil_insurance`, `civil_insurer`, `civil_insurance_start_date`, `civil_insurance_end_date`, `has_vignette`, `vignette_valid_until`, `damage_insurance`, `damage_insurer`, `damage_insurance_start_date`, `damage_insurance_end_date`, `recent_certificate_reg_date`) VALUES
('asdsadsad', 9403122826, 'dsffsf', 'sffsf', 'sdfdsfs', 'Мотопед', 150, 40, 'sdfdsffdf', '2016-05-10', b'0', '', NULL, NULL, b'1', '2016-04-06', b'0', '', NULL, NULL, '2016-05-15'),
('KJ', 9403122826, 'dsfffsfsf', 'sfdfdsfsf', 'sdfdsff', 'Мотоциклет', 150, 50, 'sfdsfsfdsfsfdsf', '2000-12-27', b'1', 'sdfsf', '2016-03-31', '2016-04-30', b'1', '2016-04-06', b'0', '', NULL, NULL, '2001-01-28'),
('А3333АН', 9403122826, 'Lada', 'Niva', 'зелен', 'Лек автомобил', 1200, 43, '5HS445SD39Q23496Ф', '2001-01-11', b'1', 'Булстрад АД', '2016-04-16', '2016-04-08', b'1', '2016-04-22', b'1', 'Автокаск0 БГ', '2016-04-07', '2016-05-03', '2001-01-09'),
('ВР5555АН', 9205158427, 'Универсал', '1010', 'червен', 'Земеделска и горска техника', 1300, 85, '5HS445SD39Q23496A', '1985-04-21', b'1', 'ЛевИнс АД', '2016-01-01', '2017-01-01', b'0', NULL, b'0', NULL, NULL, NULL, '2014-06-28'),
('КН6666АН', 9403122826, 'Opel', 'Astra', 'зелен', 'Лек автомобил', 1400, 62, 'SJFSH46DSF45DS', '2016-05-04', b'1', 'Alianz', '2016-04-15', '2017-03-03', b'1', '2017-06-08', b'0', '', NULL, NULL, '2016-05-02'),
('СО5532АН', 9205158427, 'Opel', 'Corsa', 'тъмнозелен', 'Лек автомобил', 3000, 210, '1HG245SD12G23456Z', '1989-02-23', b'1', 'Виктория АД', '2016-01-01', '2017-01-01', b'1', '2016-06-30', b'1', 'Армеец АД', '2016-01-03', '2017-01-03', '2008-02-18');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `user_id` bigint(10) unsigned NOT NULL AUTO_INCREMENT,
  `first_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `second_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `last_name` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `is_traffic_policeman` bit(1) DEFAULT b'0',
  `password` varchar(12) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=19 ;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `first_name`, `second_name`, `last_name`, `is_traffic_policeman`, `password`) VALUES
(4, 'Петко', 'Проданов', 'Георгиев', b'0', 'qwerty'),
(5, 'Илиян', 'Стаматов', 'Шопов', b'0', '123456'),
(6, 'Иван', 'Страхилов', 'Ушев', b'1', 'q1w2e3'),
(7, 'Петко', 'Стоянов', 'Петров', b'1', 'asd2'),
(8, 'Георги', 'Валентинов', 'Челенков', b'1', '123'),
(9, 'Иван', 'Петров', 'Ангелов', b'1', '3333'),
(16, 'Николай', 'Ганчев', 'Ивайлов', b'1', '333'),
(18, 'Иван', 'Иванов', 'Иванов', b'1', '123');

--
-- Constraints for dumped tables
--

--
-- Constraints for table `penalties`
--
ALTER TABLE `penalties`
  ADD CONSTRAINT `penalties_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `penalties_ibfk_2` FOREIGN KEY (`driver_owner_id`) REFERENCES `driver_owners` (`driver_owner_id`) ON DELETE CASCADE;

--
-- Constraints for table `registrations`
--
ALTER TABLE `registrations`
  ADD CONSTRAINT `registrations_ibfk_1` FOREIGN KEY (`driver_owner_id`) REFERENCES `driver_owners` (`driver_owner_id`) ON DELETE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
