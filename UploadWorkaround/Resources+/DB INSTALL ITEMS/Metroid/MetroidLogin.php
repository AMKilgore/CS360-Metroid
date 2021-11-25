<?php
  $servername = "localhost";
  $username = "root";
  $password = "";
  $dbname = "metroiddb";

  //vars submitted by the user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

  // Create connection
  $conn = new mysqli($servername, $username, $password, $dbname);

  // Check connection
  if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
  }
  //echo "Connected successfully. ";

  //echo $loginUser . " is your username and " . $loginPass . " is your password.";

  $sql = "SELECT password FROM userinfo WHERE username = '".$loginUser."' ";
  $result = mysqli_query($conn, $sql);
  $message = "";

  if(mysqli_num_rows($result)>0){
    while($row = mysqli_fetch_assoc($result)){
      if($row['password'] == $loginPass){
        $message = "Success";
      }
      else{
        $message = "Password incorrect";
       }
     }
  }
  else{
    $message = "User not found";
  }

  //you might be able to just use the if
  //and return success only
  //and otherwise, the return of "" means
  //there was an error

  echo $message;


  $conn->close();
?>