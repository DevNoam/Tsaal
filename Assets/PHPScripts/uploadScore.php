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
    $name = $_GET['name'];
    $score = $_GET['score'];
    $time = $_GET['time'];
    $encryptKey = $_GET['encryptKey'];
    if($encryptKey == $encryptKeyValue)
    {
        $sql = "INSERT INTO $table (id, name, score, time)
        VALUES (null, $name, $score, $time)";

        if ($conn->query($sql) === TRUE) {
        $last_id = $conn->insert_id;
          echo "New record created successfully, ID: \n$last_id";
        } else {
          echo "Error: " . $sql . "<br>" . $conn->error;
        }
    }else
    {
        echo "Error: Wrong Encyption key. Make sure you are using the latest Game client!";
    }

    $conn->close();
?>