<?php
$upload_dir = "uploads"; 				// The directory for the images to be saved in
$upload_path = $upload_dir."/";				// The path to where the image will be saved
$large_image_prefix =  	'';		// The prefix name to large image
$thumb_image_prefix = "thumb_";		// The prefix name to the thumb image
$large_image_name = $large_image_prefix.$_POST['filename'];     // New name of the large image (append the timestamp to the filename)
$thumb_image_name = $thumb_image_prefix.$_POST['filename'];    // New name of the thumbnail image (append the timestamp to the filename)

//Image Locations
$large_image_location = $upload_path.$large_image_name;
$thumb_image_location = $upload_path.$thumb_image_name;

if (isset($_POST["filename"])) {
	unlink($thumb_image_location);
	unlink($large_image_location);
}else if (isset($_POST["name"])) {
	unlink($upload_path.$_POST["name"]);
}

?>