<?php
    $servername = "localhost";
    $username = "TsaalApp";
    $password = "1q2w3e4r";
    $dbname = "Tsaal";
    $encryptKeyValue = "FuckHamas";

    // Create connection
    $conn = new mysqli($servername, $username, $password, $dbname);
    // Check connection
    if ($conn->connect_error) {
      die("Connection failed: " . $conn->connect_error);
    }

    $table = $_GET['table'];
    $ID = $_GET['id'];
    $name = $_GET['name'];
    $score = $_GET['score'];
    $time = $_GET['time'];
    $encryptKey = $_GET['encryptKey'];
    if($encryptKey == $encryptKeyValue)
    {
      $sql = "SELECT name FROM $table WHERE id=$ID";
      $results = $conn->query($sql);
      $row = $results->fetch_assoc();
      //if("\"" . $row["name"] . "\"" == $name)
      //{ 
        $sql = "UPDATE $table SET name = $name, score = $score, time = $time WHERE id=$ID";

        if ($conn->query($sql) === TRUE) {
          echo "Record ($ID) has been updated";
        } 
        else 
        {
          echo "Error: " . $sql . "<br>" . $conn->error;
        }
      //}else
      //{
      //  echo "The name $name does not match the records!";
      //}
    }
    else
    {
      echo "Error: Wrong Encyption key. Make sure you are using the latest Game client!";
    }
    $conn->close();
?>