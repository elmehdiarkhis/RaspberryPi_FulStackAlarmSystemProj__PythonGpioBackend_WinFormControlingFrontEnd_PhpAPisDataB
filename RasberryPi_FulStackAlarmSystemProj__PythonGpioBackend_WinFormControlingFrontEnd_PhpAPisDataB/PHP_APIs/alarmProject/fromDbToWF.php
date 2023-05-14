<?php
// header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json");
// header("Access-Control-Allow-Headers: Content-Type");

// Remplacez ces informations par les détails de connexion à votre base de données
$servername = "localhost";
$username = "root";
$password = "123";
$dbname = "raspidb";

// Créer une connexion
$conn = new mysqli($servername, $username, $password, $dbname);

// Vérifier la connexion
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT * FROM alarmstate";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // Récupérer les données de la première ligne
  $row = $result->fetch_assoc();
  echo json_encode($row);
} else {
  echo json_encode(array());
}

$conn->close();
?>
