<?php error_reporting (E_ALL ^ E_NOTICE);
 $servername = "localhost";
 $username = "polvp1";
 $password = "Ub7vUH4uQN";
 $database = "polvp1";

 $db = new mysqli($servername, $username, $password, $database);
 if($db->connect_error) {
     die("Connection failed: " . $db->connect_error);
}
$Position = $_POST["deathPosition"];
$DeathTime = $_POST["deathTime"];
$DeathType = $_POST["deathType"];
$query = "INSERT INTO UserDeaths
        SET deathPosition = '$Position',
        deathTime = '$DeathTime',
        deathType = '$DeathType'";
$result = mysqli_query($db,$query) or die('just  died');

print("$Position , $DeathTime , $DeathType");
?>