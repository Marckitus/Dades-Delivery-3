<?php error_reporting (E_ALL ^ E_NOTICE);
 $servername = "localhost";
 $username = "polvp1";
 $password = "Ub7vUH4uQN";
 $database = "polvp1";

 $db = new mysqli($servername, $username, $password, $database);
 if($db->connect_error) {
     die("Connection failed: " . $db->connect_error);
}
$EnemyPosition = $_POST["enemyPosition"];
$EnemyKillTime = $_POST["enemyKillTime"];
$EnemyKillType = $_POST["enemyKillType"];
$query = "INSERT INTO UserKills
        SET enemyPosition = '$EnemyPosition',
        enemyKillTime = '$EnemyKillTime',
        enemyKillType = '$EnemyKillType'";
$result = mysqli_query($db,$query) or die('just  died');
print("$EnemyPosition , $EnemyKillTime , $EnemyKillType");
?>