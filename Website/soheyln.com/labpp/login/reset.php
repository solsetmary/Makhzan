<?php
if(session_id() == '' || !isset($_SESSION)) {
    // session isn't started
    session_start();
}
?>



<html>
<head lang="en">
    <meta charset="UTF-8">
    <link type="text/css" rel="stylesheet" href="bootstrap-3.2.0-dist\css\bootstrap.css">
    <title>Login</title>
</head>
<style>
    .login-panel {
        margin-top: 40px;
	}
</style>

<body>


<div class="container">
    <div class="row">
        <div class="col-md-4 col-md-offset-4">
            <div class="login-panel panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Reset Password</h3>
                </div>
                <div class="panel-body">
                    <form role="form" method="post" action="reset.php">
                        <fieldset>
                            <div class="form-group"  >
                                <input class="form-control" placeholder="Enter your E-mail" name="email" type="email" required autofocus>
                            </div>

                            <input class="btn btn-lg btn-info btn-block" type="submit" value="Reset" name="reset" >

                            <!-- Change this to a button or input when using this as a form -->
                          <!--  <a href="index.html" class="btn btn-lg btn-success btn-block">Login</a> -->
                        </fieldset>
                    </form>
                    <center><b>Want to Login?</b> </b><a href="login.php">Login here</a></center><!--for centered text-->
                </div>
            </div>
        </div>
    </div>
</div>


</body>

</html>

<?php

include("database/db_conection.php");

if(isset($_POST['reset']))
{
    $user_email=$_POST['email'];

    $check_user="select * from lppusers WHERE user_email='$user_email'";

    $run=mysql_query($check_user);

    if(mysql_num_rows($run))
    {
        echo "<script type='text/javascript'>window.parent.location.reload();</script>";

    }
    else
    {
        echo "<script>alert('Your email is not registerd!')</script>";
    }
}
?>