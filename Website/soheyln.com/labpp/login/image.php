<?php
   header('Content-Type: image/png');
   $user_name=$_GET['id'];
   $im = @imagecreate(1, 1);
   $background_color = imagecolorallocate($im, 255, 155, 55);  // make it white
   imagepng($im,"mailsopened/image_$user_name.png");
   imagedestroy($im);
?>