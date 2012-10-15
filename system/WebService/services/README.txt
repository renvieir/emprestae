/** Conexões com o Banco de Dados **/

function getConnection();
function closeConnection($dbh);

/******* Funções para Controle de Usuário ********/

-- cria usuário
function createUser(nome, email, senha);

-- atualiza informações do usuário
function updateUser(nome, email, senha);

-- retorna todas as informações do usuário em que cada coluna é um elemento no
json
function getUserInfo(email);

-- remove-se o usuário
function removeUser(email);

-- retorna 1 se o usuario está cadastrado, senao 0
function checkUser(email, pwd);

-- retorna todos os usuarios menos o que estah atualmente logado.
-- O retorno eh composto por 3 elementos: usersEmail - uma lista, usersName -
uma lista e status - se houve retorno ou nao.
function getAllUsers(email)

/******** Funções para Controle de Amizade ******/

-- cria relação de amizade entre dois ids de usuários
function createFriendship(userId1, userId2);

-- remove relação de amizade entre dois ids de usuários
function deleteFriendship(userId1, userId2);

-- retorna uma lista com o id de todos os amigos de userId1
function getFriends(userId1);


/******** Funções para Controle de Livros ******/

-- adiciona um livro ao banco
function createObjBook(titulo, autor, edicao, editora);

-- atualiza as informações do livro
function updateObjBook(titulo, autor, edicao, editora);

-- retorna as informações do livro em que cada coluna é um elemento json
function getObjBookInfo(titulo);

-- remove-se um livro do banco
function removeObjBook(titulo);


/******** Funções para Controle de Jogos *******/

-- adiciona um jogo ao banco
function createObjJogo(titulo, plataforma, produtora);

-- atualiza as informações do livro
function updateObjJogo(titulo, plataforma, produtora);

-- retorna as informações do livro em que cada coluna é um elemento json
function getObjJogoInfo(titulo);

-- remove-se um jogo do banco
function removeObjJogo(titulo);


/******** Funções para Controle de Filmes *******/

-- adiciona um filme ao banco
function createObjFilme(titulo, diretor, distribuidora);

-- atualiza as informações do livro
function updateObjFilme(titulo, diretor, distribuidora);

-- retorna as informações do livro em que cada coluna é um elemento json
function getObjFilmeInfo(titulo);

-- remove-se um filme do banco
function removeObjFilme(titulo);


/******** Funções para Controle de Objetos de um Usuário ******/

-- retorna uma lista de elementos por objeto pertencente a um usuário, o json
será: "livro" => listaLivro, "jogo" => listaJogo, "filme" => listaFilme
function getUserObj(userID);

-- adiciona um novo objeto ao patrimônio de um usuário
function addUserObj(userID, ObjID, objTitulo);

-- remove-se um objeto do patrimônio do usuário
function removeUserObj(userID, objID, objTitulo);


/******** Funções para Controle de Empréstimo ********/

function createEmprestimo(user1, user2, tipoObj, objID, dataEmprestimo, dataDevolucao);
function updateEmprestimo(user1, user2, tipoObj, objID, dataEmprestimo, dataDevolucao);
function getEmprestimoInfo(emprestimoID);
function changeEmprestimoStatus(emprestimoID);
function removeEmprestimo(emprestimoID);
