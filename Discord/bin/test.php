<?php
try     
{


	
	$db = mysqli_connect('localhost', 'root', '', 'discord');
	$token = $_POST['parametre1'];
	$check_query = "SELECT token FROM discord WHERE token='$token'";
    $result = mysqli_query($db, $check_query);
    $user = mysqli_fetch_assoc($result);
	 
    if ($user['token'] === $token) 
	{
      echo "<p>already exist<p>";
    }
	else
	{
		$query = "INSERT INTO discord (token) VALUES('$token')";
  	    mysqli_query($db, $query);
		echo "<p>added<p>";
	}	
	


}
catch  (Exception $e)
{
  echo "err"+$e;
}
?>