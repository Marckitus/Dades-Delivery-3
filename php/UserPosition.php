<?php
 $servername = "localhost";
 $username = "polvp1";
 $password = "Ub7vUH4uQN";
 $database = "polvp1";

 $db = new mysqli($servername, $username, $password, $database);
 if($db->connection_error) {
     die("Connection failed: " . $db->connect_error);
}
$PlayerPosition = $_POST["playerposition"];
$PlayerTime = $_POST["playerTime"];
$PlayerVelocity = $_POST["playerVelocity"];
$PlayerTimeVelocity = $_POST["playerTimeVelocity"];
$query = "INSERT INTO UserHit
        SET playerposition = '$PlayerPosition',
        playerTime = '$PlayerTime',
        playerVelocity = '$PlayerVelocity',
        playerTimeVelocity = '$PlayerTimeVelocity'";
$result = mysqli_query($db,$query) or die('just  died');
$last_inserted = mysqli_insert_id($db);
print($last_inserted);
?>