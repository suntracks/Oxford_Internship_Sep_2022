<?php
header('Access-Control-Allow-Origin: *');

header('Access-Control-Allow-Methods: GET, POST');

header("Access-Control-Allow-Headers: X-Requested-With");

$servername = "sql687.main-hosting.eu";
$username = "u374538722_sql";
$password = "Sql@2023";
$dbname="u374538722_newsql";

//variables submitted
$loginUser=$_POST["loginUser"];
$loginPass=$_POST["loginPass"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}


$sql = "SELECT A_id,A_name,A_email,A_password,A_age,A_gender,A_education,A_designation,A_mobilenumber,A_address,A_city,A_expereince FROM admin WHERE A_name = '" .$loginUser. "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if($row["A_password"] == $loginPass){
        //echo "Login Success.";
        echo "nice_".$row["A_id"] . "_" . $row["A_name"] . "_" . $row["A_email"] . "_" . $row["A_password"] . "_" . $row["A_age"] . "_" . $row["A_gender"] . "_" . $row["A_education"] . "_" . $row["A_designation"] . "_" . $row["A_mobilenumber"] . "_" . $row["A_address"] . "_" . $row["A_city"] . "_" . $row["A_expereince"];
    }
    else
    {
        echo "Wrong Credentials.";
    }
  }
} else {
  echo "Username does not exists.";
}
$conn->close();


?>