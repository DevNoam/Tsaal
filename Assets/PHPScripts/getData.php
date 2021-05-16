<?php
    // Configuration
    $hostname = 'localhost';
    $username = 'TsaalApp';
    $password = '1q2w3e4r';
    $database = 'Tsaal';



    // Create connection
    $conn = new mysqli($hostname, $username, $password, $database);
    // Check connection
    if ($conn->connect_error) {
      die("Connection failed: " . $conn->connect_error);
    }

    $table = $_GET['table'];
    $orderBy = $_GET['orderBy'];

    if($orderBy == 'score')
    {
      $sql = "SELECT * FROM $table ORDER BY score DESC /*LIMIT 5*/";
      $result = $conn->query($sql);

      if ($result->num_rows > 0) {
          echo "Database values for: $table \n";
          foreach($result as $r) {
            echo $r['name'], " ", $r['score'], " ", $r['time'], " ", $r['id'] ,"\n";
          }
      } else {
        echo "No results";
      }
    }else if($orderBy == 'time')
    {
      $sql = "SELECT * FROM $table ORDER BY time DESC /*LIMIT 5*/";
      $result = $conn->query($sql);
      if ($result->num_rows > 0) {
          echo "Database values for: $table \n";
          foreach($result as $r) {
              echo $r['name'], " ", $r['score'], " ", $r['time'], " ", $r['id'] ,"\n";
          }
      } else {
        echo "No results";
      }
    }
    else
    {
      echo "You havn't specified request order.";
    }
    $conn->close();
?>