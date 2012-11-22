
Objeto JSON objeto, onde objeto é o tipo pesquisado, filme, jogo ou livro:
Objeto {
	{informações objeto 1},
	{informações objeto 2},
	{informações objeto 3},
			...
	{informações objeto n},
}

/**** FILMES ***/

$app->post("/createFilm", "createObjFilm");
parâmetros: titulo, diretor, distribuidora.

$app->put("/updateFilm", "updateObjFilm");
parâmetros: titulo, diretor, distribuidora.

$app->get("/getFilmInfo/:id", "getObjFilmInfo");
parâmetro: id.
retorna as informações de um filme específico.

$app->get("/getAllFilms", "getAllObjFilms");
retorna todos os filmes cadastrados no sistema.

$app->get("/getSimilarFilms/:titulo", "getSimilarObjFilms");
parâmetro: titulo.
retorna os objetos com titulos parecidos.

$app->delete("/removeFilm", "removeObjFilm");
parâmetro: id.
remove um objeto.


/**** JOGOS ***/

$app->post("/createGame", "createObjGame");
parâmetros: titulo, plataforma, produtora.

$app->put("/updateGame", "updateObjGame");
parâmetros: titulo, plataforma, produtora.

$app->get("/getGameInfo/:id", "getObjGameInfo");
parâmetro: id.
retorna as informações de um jogo específico.

$app->get("/getAllGames", "getAllObjGames");
retorna todos os jogos cadastrados no sistema.

$app->get("/getSimilarGames/:titulo", "getSimilarObjGames");
parâmetro: titulo.
retorna os objetos com titulos parecidos.

$app->delete("/removeGame", "removeObjGame");
parâmetro: id.
remove um objeto.


/**** LIVROS ***/

$app->post("/createBook", "createObjBook");
parâmetros: titulo, autor, edicao, editora.

$app->put("/updateBook", "updateObjBook");
parâmetros: titulo, autor, edicao, editora.

$app->get("/getBookInfo/:id", "getObjBookInfo");
parâmetro: id.
retorna as informações de um jogo específico.

$app->get("/getAllBooks", "getAllObjBooks");
retorna todos os jogos cadastrados no sistema.

$app->get("/getSimilarBooks/:titulo", "getSimilarObjBooks");
parâmetro: titulo.
retorna os objetos com titulos parecidos.

$app->delete("/removeBook", "removeObjBook");
parâmetro: id.
remove um objeto.

