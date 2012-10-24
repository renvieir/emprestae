<?php

function readImage ($path) {

	/* for image encode */
	$image_binary = fread(fopen($path, "r"), filesize($path));
	if ($image_binary)
		$tmp = base64_encode($image_binary);
	else
		$tmp = null;

	return $tmp;
}

function getImageType ($image_binary) {

	$data = base64_decode($image_binary);
	$finfo = new finfo(FILEINFO_MIME_TYPE);
	$type = $finfo->buffer($data);
	return $type;
}

function checkImageType ($image_binary) {

	/* check if the image type is acceptable */
	$type = getImageType($image_binary);
	$ext = Array("image/png", "image/jpeg", "image/jpg", "image/gif");

	return (in_array($type, $ext)) ? 1 : 0;
}

function createImage($elem, $image64, $isUser) {

	$path = "www.services.emprestae.com";
	$localPath = "services/images/";
	$localPath .= ($isUser) ? "users/" : "objects/";

	if($image64) {
		$image_binary = base64_decode($image64);
		$type = getImageType($image_binary);
		$im = imagecreatefromstring($image_binary);
		$localPath .= "$elem.$type";
	}
	else if ($isUser)
		$localPath .= "defaultUserImage.png";
	else
		$localPath .= "defaultObjectImage.jpg";

	if (!file_exists($localPath)) {
		$f = fopen($localPath, "wb");
		fwrite($f, $image_binary);
		fclose($f);
	}

	return ($path . $localPath);
}

?>
