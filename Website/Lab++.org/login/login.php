<?php

include("checklogin.php");

?>

<html>
<head lang="en">
    <meta charset="UTF-8">
    <link type="text/css" rel="stylesheet" href="bootstrap-3.2.0-dist/css/bootstrap.css">
    <title>Login</title>
</head>
<style>
    .login-panel {
        margin-top: 10px;
	}
	.mycheckbox {
		display: inline-block;
		cursor: pointer;
		font-size: 13px; margin-right:10px; line-height:18px;
	}
	input[type=checkbox] {
		display:none; 
	}
	.mycheckbox:before {
		content: "";
		display: inline-block;
		width: 18px;
		height: 18px;
		vertical-align:middle;
		background-color: #0088cc;
		color: #f3f3f3;
		text-align: center;
		box-shadow: inset 0px 2px 3px 0px rgba(0, 0, 0, .3), 0px 1px 0px 0px rgba(255, 255, 255, .8); 
		border-radius: 3px;
	}
	input[type=checkbox]:checked + .mycheckbox:before {
		content: "\2713";
		text-shadow: 1px 1px 1px rgba(0, 0, 0, .2);
		font-size: 15px;
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
								<input class="mycheckbox" type="checkbox" name="remember" id="remember" value="yes" checked></input>
                                <label class="mycheckbox" for="remember" style="cursor: pointer;">  Remember me</label>
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
