<?php
// Remplacez les valeurs ci-dessous par celles de votre base de données
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

// Récupérer les données de la base de données
$sql = "SELECT * FROM alarmstate WHERE id=27";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // Récupérer les données de la première ligne (vous pouvez modifier cette logique en fonction de vos besoins)
    $row = $result->fetch_assoc();
    echo json_encode($row);
} else {
    echo "0 results";
}

// Fermer la connexion
$conn->close();
?>
