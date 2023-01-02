<?php
 $servername = "localhost";
 $username = "polvp1";
 $password = "Ub7vUH4uQN";
 $database = "polvp1";

 $db = new mysqli($servername, $username, $password, $database);
 if($db->connection_error) {
     die("Connection failed: " . $db->connect_error);
}
$EnemyPosition = $_POST["enemyPosition"];
$EnemyKillTime = $_POST["enemyKillTime"];
$EnemyKillType = $_POST["enemyKillType"];
$BrokenBlockPos = $_POST["brokenBlockPos"];
$BrokenBlockTime = $_POST["brokenBlockTime"];
$query = "INSERT INTO UserHit
        SET enemyPosition = '$EnemyPosition',
        enemyKillTime = '$EnemyKillTime',
        enemyKillType = '$EnemyKillType',
        brokenBlockTime = '$BrokenBlockTime',
        brokenBlockPos = '$BrokenBlockPos'";
        
$result = mysqli_query($db,$query) or die('just  died');
$last_inserted = mysqli_insert_id($db);
print($last_inserted);
?>