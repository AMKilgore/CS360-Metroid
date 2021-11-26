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

  $sql = "SELECT username FROM userinfo WHERE username = '".$loginUser."' ";
  $result = mysqli_query($conn, $sql);

  if(mysqli_num_rows($result)>0){
    //the name already exists
    //echo "Username already taken. ";
    echo "";
  }
  else{
    //echo "Creating user... ";
    //insert user and pass into the table
    $sql2 = "INSERT INTO userinfo (username, password) VALUES('" . $loginUser . "', '" . $loginPass . "')";
      if($conn->query($sql2) === TRUE){
        echo "Success";
      }else{
        //echo "Error: " . $sql2 . "<br>" . $conn->error;
        echo "";
      }
  }


  $conn->close();

?>