-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : ven. 12 mai 2023 à 22:41
-- Version du serveur : 10.4.27-MariaDB
-- Version de PHP : 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `raspidb`
--

-- --------------------------------------------------------

--
-- Structure de la table `alarmstate`
--

CREATE TABLE `alarmstate` (
  `id` int(11) NOT NULL,
  `state` int(11) NOT NULL,
  `zone1` tinyint(1) NOT NULL,
  `zone2` tinyint(1) NOT NULL,
  `zone3` tinyint(1) NOT NULL,
  `time` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `alarmstate`
--

INSERT INTO `alarmstate` (`id`, `state`, `zone1`, `zone2`, `zone3`, `time`) VALUES
(27, 1, 1, 0, 0, '2023-04-01 13:48:59');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `alarmstate`
--
ALTER TABLE `alarmstate`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `alarmstate`
--
ALTER TABLE `alarmstate`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
