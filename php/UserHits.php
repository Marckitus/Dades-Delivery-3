<?php error_reporting (E_ALL ^ E_NOTICE);
 $servername = "localhost";
 $username = "polvp1";
 $password = "Ub7vUH4uQN";
 $database = "polvp1";

 $db = new mysqli($servername, $username, $password, $database);
 if($db->connect_error) {
     die("Connection failed: " . $db->connect_error);
}
$HitPosition = $_POST["hitPosition"];
$HitTime = $_POST["hitTime"];
$HitType = $_POST["hitType"];
$query = "INSERT INTO UserHit
        SET hitPosition = '$HitPosition',
        hitType = '$HitType',
        hitTime = '$HitTime'";

$result = mysqli_query($db,$query) or die('just  died');
print("$HitPosition , $HitTime , $HitType");

?>