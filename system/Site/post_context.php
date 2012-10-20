<?php

function createMsgContext($arr, $method) {

	$data = json_encode($arr);
	$httpOpts =
		array("http" =>
			array(
				"method" => $method,
				"header" => "Content-Type: application/json",
				"content" => $data
			)
		);

	return stream_context_create($httpOpts);
}

?>
