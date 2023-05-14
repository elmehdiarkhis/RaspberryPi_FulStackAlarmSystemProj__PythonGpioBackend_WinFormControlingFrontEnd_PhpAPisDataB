<?php
// Database connection
$servername = "localhost";
$username = "root";
$password = "123";
$dbname = "raspidb";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Get data from the Python script
$state = $_GET['state'];
$zone1 = $_GET['zone1'];
$zone2 = $_GET['zone2'];
$zone3 = $_GET['zone3'];

// Update the database
$sql = "UPDATE alarmstate SET state=$state, zone1=$zone1, zone2=$zone2, zone3=$zone3 WHERE id=27";

if ($conn->query($sql) === TRUE) {
    echo json_encode(array("status" => "success"));
} else {
    echo json_encode(array("status" => "error", "message" => "Error updating record: " . $conn->error));
}

// Close the connection
$conn->close();
?>
