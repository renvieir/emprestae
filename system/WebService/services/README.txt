Para todos os serviços deve-se informar três campos: appID, data e iv, onde iv
corresponde ao vetor de inicialização. Assim, no método GET, eles serão os
parâmetros. Já nos demais métodos, os parâmetros são repassados em um JSON.

Cada aplicação tem tanto uma ID quanto uma chave diferente, sendo:

	- mobile: appID = mobileID, key = m0b1l3@@;
	- site: appID = siteID, key = s1t3@@.

(fiz isso apenas para diferenciá-los, não é tão necessário, mas dá um maior
controle)

O campo data deve conter o objeto json criptografado da seguinte forma:
	1 - a chave deve ser codificada segundo a base64;
	2 - os dados a serem enviados para o webservice devem estar do mesmo modo
	definido anteriormente como objeto json, então se deve criptografar esse
	objeto segundo a criptografia de Rijndael com o modo CFB;
	3 - os dados criptografados devem ser convertidos para base64;
	4 - com o objeto json criptografado e convertido, cria-se um novo json como
	o modelo: appID => ID da plataforma, data => objeto em base64, iv => vetor
	de inicialização na base64;
	5 - enviar a mensagem.
		
Obs.: No método GET, para que as informações possam estar presentes na url,
tem-se que codificá-las segundo o charset de url. (em php isso acontece com a
função urlencode, em C# tem algo parecido)

Obs.: para ver como funciona a criptografia de Rijndael e o modo CFB em wp:
	- http://msdn.microsoft.com/en-us/library/system.security.cryptography.rijndael.aspx
	- http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx

O WebService irá ler o ID da plataforma, pegar a chave correspondente e
descriptografar os dados. Ao enviar alguma mensagem como resposta, o padrão será
data e iv, então se deve descriptografar da mesma forma como foi dito acima.
