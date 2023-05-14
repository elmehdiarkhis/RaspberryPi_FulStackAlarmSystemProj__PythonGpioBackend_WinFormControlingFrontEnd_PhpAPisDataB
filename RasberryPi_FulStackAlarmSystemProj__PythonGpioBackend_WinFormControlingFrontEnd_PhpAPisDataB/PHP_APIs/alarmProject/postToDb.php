<?php
header('Content-Type: application/json');

// Remplacez les informations suivantes par les détails de connexion à votre base de données
$db_host = 'localhost';
$db_username = 'root';
$db_password = '123';
$db_name = 'raspidb';

// Connexion à la base de données
$conn = new mysqli($db_host, $db_username, $db_password, $db_name);

// Vérifier la connexion
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $state = isset($_POST['state']) ? intval($_POST['state']) : 0;
    $zone1 = isset($_POST['zone1']) ? filter_var($_POST['zone1'], FILTER_VALIDATE_BOOLEAN) : false;
    $zone2 = isset($_POST['zone2']) ? filter_var($_POST['zone2'], FILTER_VALIDATE_BOOLEAN) : false;
    $zone3 = isset($_POST['zone3']) ? filter_var($_POST['zone3'], FILTER_VALIDATE_BOOLEAN) : false;

    // Mettre à jour la première ligne
    $sql = "UPDATE alarmstate SET state=?, zone1=?, zone2=?, zone3=? WHERE id = (SELECT MIN(id) FROM alarmstate)";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("iiii", $state, $zone1, $zone2, $zone3);
    $success_msg = "Le status a été mis à jour avec succès sur PHP";

    if ($stmt->execute()) {
        $response = array(
            'message' => $success_msg,
            'etat' => $state,
            'zone1' => $zone1,
            'zone2' => $zone2,
            'zone3' => $zone3
        );
    } else {
        $response = array(
            'message' => "Erreur lors de la mise à jour des données: " . $conn->error
        );
    }
    $stmt->close();
} else {
    $response = array('message' => "Invalid request method. Use POST to update alarm state.");
}

$conn->close();
echo json_encode($response);

// header('Content-Type: application/json');

// $state = isset($_GET['state']) ? intval($_GET['state']) : 0;
// $zone1 = isset($_GET['zone1']) ? filter_var($_GET['zone1'], FILTER_VALIDATE_BOOLEAN) : false;
// $zone2 = isset($_GET['zone2']) ? filter_var($_GET['zone2'], FILTER_VALIDATE_BOOLEAN) : false;
// $zone3 = isset($_GET['zone3']) ? filter_var($_GET['zone3'], FILTER_VALIDATE_BOOLEAN) : false;

// // Remplacez les informations suivantes par les détails de connexion à votre base de données
// $db_host = 'localhost';
// $db_username = 'root';
// $db_password = '123';
// $db_name = 'raspidb';

// // Connexion à la base de données
// $conn = new mysqli($db_host, $db_username, $db_password, $db_name);

// // Vérifier la connexion
// if ($conn->connect_error) {
//     die("Connection failed: " . $conn->connect_error);
// }

// // Vérifier si la table est vide
// $sql_check = "SELECT COUNT(*) AS count FROM alarmstate";
// $result = $conn->query($sql_check);
// $row = $result->fetch_assoc();

// if ($row['count'] == 0) {
//     // Insérer les données dans la base de données
//     $sql = "INSERT INTO alarmstate (state, zone1, zone2, zone3) VALUES (?, ?, ?, ?)";
//     $stmt = $conn->prepare($sql);
//     $stmt->bind_param("iiii", $state, $zone1, $zone2, $zone3);
//     $success_msg = "Les données ont été insérées avec succès";
// } else {
//     // Mettre à jour la première ligne
//     $sql = "UPDATE alarmstate SET state=?, zone1=?, zone2=?, zone3=? WHERE id = (SELECT MIN(id) FROM alarmstate)";
//     $stmt = $conn->prepare($sql);
//     $stmt->bind_param("iiii", $state, $zone1, $zone2, $zone3);
//     $success_msg = "Le status a été mis à jour avec succès sur PHP";
// }

// if ($stmt->execute()) {
//     $response = array(
//         'message' => $success_msg,
//         'etat' => $state,
//         'zone1' => $zone1,
//         'zone2' => $zone2,
//         'zone3' => $zone3
//     );
// } else {
//     $response = array(
//         'message' => "Erreur lors de la mise à jour des données: " . $conn->error
//     );
// }

// $stmt->close();
// $conn->close();

// echo json_encode($response);
?>
