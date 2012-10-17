<?php

/* verifica se existe alguma informacao no corpo da mensagem */
function readRequestBody($request) {

	$body = $request->getBody();
	if (!$body) // fail!
		return null;

	return json_decode($request->getBody());
}

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

?>
