<?php

/* organiza os elementos para a resposta aos serviços do WebService */
function storeElements($type, $elements) {

	if (count($elements) != 0) {
		$arr = Array();
		$i = 0;
		foreach ($elements as $elem) {
			$arr[$i] = Array();
			$arr[$i][$type] = Array();
			foreach ($elements[$i] as $key => $value)
				$arr[$i][$type][$key] = ($value) ? $value: null;
			$i++;
		}
		return $arr;
	}

	return null;
}

function encrypt_data($appID, $json) {

	global $appIDs;

	$key = base64_encode($appIDs[$appID]);
	$cipher = MCRYPT_RIJNDAEL_256; // cipher to encrypt data
	$mode = MCRYPT_MODE_CFB; // encrypts byte per byte

	/* encrypting */

	$iv_size = mcrypt_get_iv_size($cipher, $mode);
	$src = MCRYPT_RAND; // source or seed for the IV
	$iv = mcrypt_create_iv($iv_size, $src);

	$crypt_json = mcrypt_encrypt($cipher, $key, $json, $mode, $iv);

	$arr = Array(
			"data" => base64_encode($crypt_json),
			"iv" => base64_encode($iv)
	);

	return $arr;
}

function decrypt_data($appID, $crypt_data, $iv) {

	global $appIDs;

	$key = base64_encode($appIDs[$appID]);
	$cipher = MCRYPT_RIJNDAEL_256; // cipher to encrypt data
	$mode = MCRYPT_MODE_CFB; // encrypts byte per byte

	/* decrypting */

	$crypt_data = base64_decode($crypt_data);
	$iv = base64_decode($iv);
	$data = mcrypt_decrypt($cipher, $key, $crypt_data, $mode, $iv);

	return $data;
}

/* verifica se existe alguma informacao no corpo da mensagem */
function readRequestBody($request) {

	$body = $request->getBody();
	if (!$body) // fail!
		return null;

	$json = json_decode($request->getBody());
	return $json;
}

?>
