<?php
 $servername = "localhost";
 $username = "polvp1";
 $password = "Ub7vUH4uQN";
 $database = "polvp1";

 $db = new mysqli($servername, $username, $password, $database);
 if($db->connection_error) {
     die("Connection failed: " . $db->connect_error);
}
$HitPosition = $_POST["hitPosition"];
$HitTime = $_POST["hitTime"];
$query = "INSERT INTO UserHit
        SET hitPosition = '$HitPosition',
        hitTime = '$HitTime',
$result = mysqli_query($db,$query) or die('just  died');
$last_inserted = mysqli_insert_id($db);
print($last_inserted);
?>