<?php

include("checklogin.php");

?>

<html>
<head lang="en">
    <meta charset="UTF-8">
    <link type="text/css" rel="stylesheet" href="bootstrap-3.2.0-dist\css\bootstrap.css">
    <title>Login</title>
</head>
<style>
    .login-panel {
        margin-top: 10px;
	}
</style>

<body>


<div class="container" id="maindiv">
    <div class="row">
        <div class="col-md-4 col-md-offset-4">
            <div class="login-panel panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Sign In</h3>
                </div>
                <div class="panel-body">
                    <form role="form" method="post" action="login.php">
                        <fieldset>
                            <div class="form-group">
                                <input class="form-control" placeholder="Username" name="name" type="text" required autofocus>
                            </div>
                            <div class="form-group">
                                <input class="form-control" placeholder="Password" name="pass" type="password" value="" required autofocus>
                            </div>
							<div class="form-group">
								<input type="checkbox" name="remember" id="remember" value="yes" checked>
                                <label for="remember" style="cursor: pointer;">Remember me</label>
                            </div>


                                <input class="btn btn-lg btn-info btn-block" type="submit" value="Login" name="login" >

                            <!-- Change this to a button or input when using this as a form -->
                          <!--  <a href="index.html" class="btn btn-lg btn-success btn-block">Login</a> -->
                        </fieldset>
                    </form>
                    <center><b>Forgot password?</b> </b><a href="reset.php">Reset Password</a></center><!--for centered text-->
                    <center><b>Not registered?</b> </b><a href="registration.php">Register here</a></center><!--for centered text-->
                </div>
            </div>
        </div>
    </div>
</div>


</body>

</html>
