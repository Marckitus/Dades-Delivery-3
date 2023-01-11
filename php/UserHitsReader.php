<?php error_reporting (E_ALL ^ E_NOTICE);
 $servername = "localhost";
 $username = "polvp1";
 $password = "Ub7vUH4uQN";
 $database = "polvp1";

 $db = new mysqli($servername, $username, $password, $database);
 if($db->connect_error) {
     die("Connection failed: " . $db->connect_error);
}

$query = "SELECT hitPosition, hitTime, hitType FROM UserHit";
$result = mysqli_query($db,$query) or die('just  died');

if ($result->num_rows > 0) {
        $table = array();
        while($row = $result->fetch_assoc()) {
            $table[] = $row;
        }
        echo json_encode($table);
    } else {
        echo "0 results";
    }
    
    $db->close();
?>