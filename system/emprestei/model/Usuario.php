<?php

class Usuario
{
	private var $id;
	private var $nome;
	private var $email;
	private var $senha;
	
	public function __construct($valueId, $valueNome, $valueEmail, $valueSenha)
	{
		$this->id = $valueId;
		$this->nome = $valueNome;
		$this->email = $valueEmail;
		$this->senha = $valueSenha;
	}
	
	public function getId()
	{
		return $this->id;
	}
	
	public function setId($value)
	{
		$this->id = $value
	}

	public function getNome()
	{
		return $this->nome;
	}
	
	public function setNome($value)
	{
		$this->nome = $value
	}

	public function getEmail()
	{
		return $this->email;
	}
	
	public function setEmail($value)
	{
		$this->email = $value
	}

	public function getSenha()
	{
		return $this->senha;
	}
	
	public function setSenha($value)
	{
		$this->senha = $value
	}
	
}
?>