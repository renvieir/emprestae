
Objeto JSON usuario:
User {
	{informações usuário 1},
	{informações usuário 2},
	{informações usuário 3},
			...
	{informações usuário n},
}

======== USUÁRIO =========

$app->post("/createUser", "createUser");
parâmetros: nome, email, senha, addressLat, addressLong.

$app->put("/updateUser", "updateUser");
parâmetros: nome, email, senha, addressLat, addressLong.

$app->delete("/removeUser", "removeUser");
parâmetro: email.

$app->post("/checkUser", "checkUser");
parâmetro: email, senha.
retorno: informa se o usuário existe ou não.

$app->get("/getUserInfo/:email", "getUserInfo");
parâmetro: email.
retorno: retorna as informações do usuário.

$app->get("/getAllUsersBut/:email", "getAllUsersBut");
parâmetro: email.
retorno: todos os usuários cadastrados menos o informado.

$app->get("/getAllUsersByName/:nome", "getAllUsersByName");
parâmetro: nome.
retorno: todos os usuários com nome parecido.

$app->get("/getCloseUsers/:userId/:lat/:long", "getCloseUsers");
parâmetros: id do usuário, latitude atual, longitude atual.
retorno: todos os usuários próximos ao informado em um raio de 100 km.


======== AMIZADE =========

$app->post("/requestFriend", "requestFriendship");
parâmetros: id do usuário 1, id do usuário 2.
requisição de amizade faz com que seja adicionado uma nova relação no banco,
porém com status zero. Quando o id2 aceitar a solicitação do id1, o status será
1.

$app->put("/acceptFriend", "addFriendship");
parâmetros: id do usuário 1, id do usuário 2.
id1 aceita solicitação de id2.

$app->get("/getFriendsRequest/:id1", "getFriendsRequest");
parâmetros: id do usuário 1.
retorna todas as solicitações de amizades feitas para o usuário informado.

$app->delete("/removeFriends", "deleteFriendship");
parâmetros: id do usuário 1, id do usuário 2.
relação de amizade é removida.

$app->get("/getFriends/:id1", "getFriends");
parâmetros: id do usuário 1.
retorna todos os usuários amigos do informado.


======== PATRIMONIO =========

tipos de objeto: 
	"a" => "Livro";
	"b" => "Jogo";
	"c" => "Filme".

$app->get("/getUserObjs/:userID", "getUserObjs");
parâmetros: id do usuário.
retorna todos os objetos do usuário informado.

$app->post("/addUserObj", "addUserObj");
parâmetros: id do usuário, id do objeto, tipo do objeto.
adiciona um novo objeto ao patrimônio do usuário informado.

$app->delete("/removeUserObj", "removeUserObj");
parâmetros: id do usuário, id do objeto, tipo do objeto.
remove o objeto informado do patrimônio do usuário.


======== EMPRESTIMO =========

Objeto JSON emprestimo:
Emprestimo {
	{informações emprestimo 1},
	{informações emprestimo 2},
	{informações emprestimo 3},
			...
	{informações emprestimo n},
}

$app->post("/requestEmp", "solicitaEmprestimo");
parâmetros: id do usuário que solicita o empréstimo - id1, id do usuário que
emprestará o objeto - id2, id do objeto, tipo do objeto, data de empréstimo e
data da devolução.
cria a solicitação do empréstimo com status 0, pois ainda não foi emprestado.

$app->put("/acceptEmp", "aceitaEmprestar");
parâmetros: id do empréstimo.
objeto emprestado e status 1.

$app->get("/getEmpPorMim/:id1", "getEmpPorMim");
parâmetros: id do usuário.
retorna todos os empréstimos feitos pelo usuário.

$app->get("/getEmpDeMim/:id1", "getEmpDeMim");
parâmetros: id do usuário.
retorna todos os empréstimos em que o usuário emprestou algum objeto.

$app->get("/getEmpRequestDeMim/:id1", "getEmpRequestDeMim");
parâmetros: id do usuário.
retorna todas as solicitações de empréstimo feitas ao usuário informado.

$app->delete("/removeEmp", "removeEmprestimo");
parâmetros: id do empréstimo.
cancela o empréstimo, status 3.

$app->put("/updateEmpDate", "updateEmprestimo");
parâmetros: id do empréstimo, data da devolução.
atualiza a data da devolução do empréstimo feito.

$app->put("/changeEmpStatus", "changeEmprestimoStatus");
parâmetros: id do empréstimo, status a atualizar.
atualiza o status do empréstimo para qualquer valor:

	- 0: solicitado empréstimo de id1 para id2;
	- 1: empréstimo realizado;
	- 2: empréstimo finalizado (devolvido);
	- 3: empréstimo cancelado.
