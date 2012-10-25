using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using INdT.Services;
using System.Collections.Generic;
using System.Diagnostics;
using Emprestae.Models;
using System.Security.Cryptography;
using System.Windows.Media.Imaging;


namespace Emprestae
{
    public class EmprestaeWebService: Services
    {
        #region Variáveis

        #region Variáveis Estáticas

        private static string host = "http://services.emprestae.com";

        #endregion

        #region User Info

        public User userInfo { get; 
            set; }

        public UserArray[] userFriends { get; set; }

        public string password { get; set; }

        #endregion

        #endregion

        #region Métodos

        /// <summary>
        /// Converte as informações do usuário para um dicionário
        /// </summary>
        /// <returns>Dicionario de {string, ojeto}</returns>
        private Dictionary<string, object> ConvertUserToDictionary()
        {
            return new Dictionary<string, object>()
            {
                {"idusuario",this.userInfo.idusuario},
                {"email",this.userInfo.email},
                {"nome",this.userInfo.nome},
                {"addressLat",this.userInfo.addressLat},
                {"addressLong",this.userInfo.addressLong},
                {"imagePath",this.userInfo.imagePath}
            };
        }

        #region Métodos HTTP Genéricos

        /// <summary>
        /// Método GET genérico a ser utilizado pelos outros métodos
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de resposta</typeparam>
        /// <param name="url">Url da chamada</param>
        /// <param name="args">Dicionário de parâmetros da chamada</param>
        /// <param name="success">Callback de Sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        private void get<T>(string url, Action<T> success, Action error, Dictionary<string, object> args = null)
        {
            get(new Uri(url, UriKind.Absolute),
                args,
                (sender, requestArgs) => 
                {
                    if (requestArgs.failed)
                    {
                        if (error != null)
                            error();
                        else
                            Debug.WriteLine("EmprestaeServices - An error occurred but no error handling was defined");
                    }
                    else
                    {
                        string data = Util.decodeResponseToString(requestArgs.result as HttpWebResponse);
                        T response = default(T);
                        if (typeof(T) == typeof(string))
                        {
                            response = (T)(object)data;
                        }
                        else
                        {
                            response = Util.deserializeObject<T>(data);
                        }
                        success(response);
                    }
                });
        }

        /// <summary>
        /// Método POST genérico a ser utilizado pelos outros métodos
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de resposta</typeparam>
        /// <param name="url">Url da chamada</param>
        /// <param name="args">Dicionário de parâmetros da chamada</param>
        /// <param name="success">Callback de Sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        private void post<T>(string url, Dictionary<string, object> args, Action<T> success, Action error)
        {
            post(new Uri(url, UriKind.Absolute),
                args,
                (sender, requestArgs) =>
                {
                    if (requestArgs.failed)
                    {
                        if (error != null)
                            error();
                        else
                            Debug.WriteLine("EmprestaeServices - An error occurred but no error handling was defined");
                    }
                    else
                    {
                        string data = Util.decodeResponseToString(requestArgs.result as HttpWebResponse);
                        T response = default(T);
                        if (typeof(T) == typeof(string))
                        {
                            response = (T)(object)data;
                        }
                        else
                        {
                            response = Util.deserializeObject<T>(data);
                        }
                        success(response);
                    }
                });
        }

        /// <summary>
        /// Método PUT genérico a ser utilizado pelos outros métodos
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de resposta</typeparam>
        /// <param name="url">Url da chamada</param>
        /// <param name="args">Dicionário de parâmetros da chamada</param>
        /// <param name="success">Callback de Sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        private void put<T>(string url, Dictionary<string, object> args, Action<T> success, Action error)
        {
            put(new Uri(url, UriKind.Absolute),
                args,
                (sender, requestArgs) =>
                {
                    if (requestArgs.failed)
                    {
                        if (error != null)
                            error();
                        else
                            Debug.WriteLine("EmprestaeServices - An error occurred but no error handling was defined");
                    }
                    else
                    {
                        string data = Util.decodeResponseToString(requestArgs.result as HttpWebResponse);
                        T response = default(T);
                        if (typeof(T) == typeof(string))
                        {
                            response = (T)(object)data;
                        }
                        else
                        {
                            response = Util.deserializeObject<T>(data);
                        }
                        success(response);
                    }
                });
        }

        /// <summary>
        /// Método DELETE genérico a ser utilizado pelos outros métodos
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de resposta</typeparam>
        /// <param name="url">Url da chamada</param>
        /// <param name="args">Dicionário de parâmetros da chamada</param>
        /// <param name="success">Callback de Sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        private void delete<T>(string url, Dictionary<string, object> args, Action<T> success, Action error)
        {
            delete(new Uri(url, UriKind.Absolute),
                args,
                (sender, requestArgs) =>
                {
                    if (requestArgs.failed)
                    {
                        if (error != null)
                            error();
                        else
                            Debug.WriteLine("EmprestaeServices - An error occurred but no error handling was defined");
                    }
                    else
                    {
                        string data = Util.decodeResponseToString(requestArgs.result as HttpWebResponse);
                        T response = default(T);
                        if (typeof(T) == typeof(string))
                        {
                            response = (T)(object)data;
                        }
                        else
                        {
                            response = Util.deserializeObject<T>(data);
                        }
                        success(response);
                    }
                });
        }

        #endregion

        #region Métodos do Usuário

        /// <summary>
        /// Recupera as informações de um usuário
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetUserInfo(string email, Action<UserResponse> success, Action error)
        {
            get<UserResponse>(
                host, 
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getUserInfo"},
                    {"email",email}
                });
 
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="userData">Dicionário de parâmetros com dados do usuário</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void CreateUser(Dictionary<string, object> userData, Action<Response> success, Action error)
        {
            if (this.userInfo == null)
                userInfo = new User() { email = userData["email"].ToString() };
            password = userData["senha"].ToString();

            post<Response>(
                host+"/createUser",
                new Dictionary<string, object>()
                {
                    {"nome",userData["nome"]},
                    {"email",userData["email"]},
                    {"senha",userData["senha"]},
                    {"addressLat",""},
                    {"addressLong",""}
                },
                success,
                error);
        }

        /// <summary>
        /// Atualiza as informações do usuário com as informações do celular
        /// </summary>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void UpdateUser(string senha, Action<Response> success, Action error)
        {
            Dictionary<string, object> arg = ConvertUserToDictionary();
            arg.Add("senha",senha);

            put<Response>(
                host + "/updateUser",
                arg,
                success,
                error);
        }

        /// <summary>
        /// Verifica se o usuário possui conta no sistema
        /// </summary>
        /// <param name="userData">Dicionário de parâmetros com dados do usuário</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void CheckUser(Dictionary<string, object> userData, Action<Response> success, Action error)
        {
            if (this.userInfo == null)
                userInfo = new User() { email = userData["email"].ToString() };
            password = userData["senha"].ToString();

            post<Response>(
                host + "/checkUser",
                new Dictionary<string, object>()
                {
                    {"email",userData["email"]},
                    {"senha",userData["senha"]}
                },
                success,
                error);
        }

        /// <summary>
        /// Busca todos os outros usuários do sistema
        /// </summary>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetAllUsersButMe(Action<UserResponse> success, Action error)
        {
            get<UserResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getAllUsersBut"},
                    {"email",this.userInfo.email},
                });
        }

        /// <summary>
        /// Busca usuários por nome
        /// </summary>
        /// <param name="name">Nome do usuário</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetAllUserByName(string name, Action<UserResponse> success, Action error)
        {
            get<UserResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getAllUsersByName"},
                    {"email", name},
                });
        }

        /// <summary>
        /// Recupera os usuarios mais próximos
        /// </summary>
        /// <param name="userLat">Latitude do usuário</param>
        /// <param name="userLong">Longitude do usuário</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetCloseUsers(Action<UserResponse> success, Action error)
        {
            get<UserResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getCloseUsers"},
                    {"userId", this.userInfo.idusuario},
                    {"userLat", this.userInfo.addressLat},
                    {"userLong", this.userInfo.addressLong}
                });

        }

        /// <summary>
        /// Remove o usuario do sistema
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void RemoveUser(string email, Action<Response> success, Action error)
        {
            delete<Response>(
                host + "/removeUser",
                new Dictionary<string, object>() { { "email", email } },
                success,
                error);
        }

        #endregion

        #region Métodos de Amizade

        /// <summary>
        /// Recupera os amigos do usuário
        /// </summary>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetFriends(Action<UserResponse> success, Action error)
        {
            get<UserResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getFriends"},
                    {"userID", this.userInfo.idusuario},
                });
        }

        /// <summary>
        /// Envia uma solicitação de amizade ao usuário especificado pelo id
        /// </summary>
        /// <param name="friendId">Id do amigo</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void RequestFriend(string friendId, Action<Response> success, Action error)
        {
            post<Response>(
                host + "/requestFriend",
                new Dictionary<string, object> { 
                    {"id1", this.userInfo.idusuario},
                    {"id2", friendId}
                },
                success,
                error);
        }

        public void GetFriendsRequest(string id, Action<UserResponse> success, Action error)
        {
            get<UserResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                { 
                    {"metodo", "getFriendsRequest"},
                    {"friendId", id}
                });
        }
        
        /// <summary>
        /// Aceita a solicitação de amizade do usuário especificado pelo id
        /// </summary>
        /// <param name="friendId">Id do amigo</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void AcceptFriend(string friendId, Action<Response> success, Action error)
        {
            put<Response>(
                host + "/acceptFriend",
                new Dictionary<string, object> { 
                    {"id1", this.userInfo.idusuario},
                    {"id2", friendId}
                },
                success,
                error);
        }

        /// <summary>
        /// Desfaz amizade com o amigo especificado pelo id
        /// </summary>
        /// <param name="friendId">Id do amigo</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void UnFriend(string friendId, Action<Response> success, Action error)
        {
            delete<Response>(
                host + "/acceptFriend",
                new Dictionary<string, object> { 
                    {"id1", this.userInfo.idusuario},
                    {"id2", friendId}
                },
                success,
                error);
        }
        
        #endregion

        #region Métodos de Objetos

        #region Patrimônio

        /// <summary>
        /// Recupera os objetos do usuário
        /// </summary>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetUserObjs(int userId, Action<ObjResponse> success, Action error)
        {
            get<ObjResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getUserObjs"},
                    {"userID", userId},
                });
        }

        /// <summary>
        /// Adciona objetos ao patrimonio do usuario
        /// </summary>
        /// <param name="objData">Dados do Objeto</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void AddUserObj(Dictionary<string, object> objData, Action<Response> success, Action error)
        {
            objData.Add("userId", this.userInfo.idusuario);
            post<Response>(
                host + "/addUserObj",
                objData,
                success,
                error);
        }

        /// <summary>
        /// Remove um objeto do patrimônio do usuário
        /// </summary>
        /// <param name="userId">Id do usuário</param>
        /// <param name="objId">Id do objeto</param>
        /// <param name="objType">Tipo do objeto</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void RemoveUserObj(string userId, string objId, string objType, Action<Response> success, Action error)
        {
            delete<Response>(
                host + "/removeUserObj",
                new Dictionary<string, object>()
                {
                    {"userId", userId},
                    {"objId", objId},
                    {"objType", objType}
                },
                success,
                error);
        }

        #endregion

        #region Livros

        /// <summary>
        /// Cria um objeto livro
        /// </summary>
        /// <param name="bookData">Dados do Livro</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void CreateBook(Dictionary<string, object> bookData, Action<Response> success, Action error)
        {            
            post<Response>(
                host + "/createBook",
                bookData, 
                success,
                error);
        }

        /// <summary>
        /// Recupera informações de um livro especificado pelo seu id
        /// </summary>
        /// <param name="bookId">Id do livro</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetBookInfo(string bookId, Action<Response> success, Action error)
        {
            get<Response>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo", "getBookInfo"},
                    {"id", bookId}
                });
        }

        /// <summary>
        /// Recupera todos os livros do banco de dados
        /// </summary>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetAllBooks(Action<ObjResponse> success, Action error)
        {
            get<ObjResponse>(
                host + "/getAllBooks",
                success,
                error);
        }

        /// <summary>
        /// Busca livros pelo titulo 
        /// </summary>
        /// <param name="title">Titulo do livro</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetSimilarBooks(string title, Action<ObjResponse> success, Action error)
        {
            get<ObjResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getSimilarBooks"},
                    {"titulo", title}
                });
        }

        /// <summary>
        /// Atualiza as informações do livro
        /// </summary>
        /// <param name="bookData">Dados do livro</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void UpdateBook(Dictionary<string, object> bookData, Action<Response> success, Action error)
        {
            put<Response>(
                host + "/updateBook",
                bookData,
                success,
                error);
        }

        /// <summary>
        /// Remove o livro do banco de dados
        /// </summary>
        /// <param name="idLivro">Id do livro</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void RemoveBook(string idLivro, Action<Response> success, Action error)
        {
            delete<Response>(
                host + "/removeBook",
                new Dictionary<string, object>() { { "idLivro", idLivro } },
                success,
                error);
        }

        #endregion

        #region Filmes

        /// <summary>
        /// Cria um objeto filme
        /// </summary>
        /// <param name="filmData">Dados do filme</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void CreateFilm(Dictionary<string, object> filmData, Action<Response> success, Action error)
        {
            post<Response>(
                host + "/createFilm",
                filmData,
                success,
                error);
        }

        /// <summary>
        /// Recupera informações de um livro especificado pelo seu Id
        /// </summary>
        /// <param name="filmId">Id do filme</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetFilmInfo(string filmId, Action<ObjResponse> success, Action error)
        {
            get<ObjResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>() 
                {
                    {"metodo","getFilmInfo"},
                    {"filmId", filmId}
                });
        }

        /// <summary>
        /// Busca todos os filmes do banco de dados
        /// </summary>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetAllFilms(Action<ObjResponse> success, Action error)
        {
            get<ObjResponse>(
                host + "/getAllFilms",
                success,
                error);
        }

        /// <summary>
        /// Busca filmes pelo titulo 
        /// </summary>
        /// <param name="title">Titulo do filme</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetSimilarFilms(string title, Action<ObjResponse> success, Action error)
        {
            get<ObjResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getSimilarFilms"},
                    {"titulo", title}
                });
        }

        /// <summary>
        /// Atualiza as informações do filme
        /// </summary>
        /// <param name="filmData">Dados do filme</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void UpdateFilm(Dictionary<string, object> filmData, Action<Response> success, Action error)
        {
            put<Response>(
                host + "/updateFilm",
                filmData,
                success,
                error);
        }

        /// <summary>
        /// Remove um filme do banco de dados
        /// </summary>
        /// <param name="filmId">Id do filme</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void RemoveFilm(string filmId, Action<Response> success, Action error)
        {
            delete<Response>(
                host + "/removeFilm",
                new Dictionary<string, object>() { { "idFilme", filmId } },
                success,
                error);
        }

        #endregion

        #region Jogos

        /// <summary>
        /// Cria um objeto jogo
        /// </summary>
        /// <param name="gameData">Dados do Jogo</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void CreateGame(Dictionary<string, object> gameData, Action<Response> success, Action error)
        {
            post<Response>(
                host + "/createGame",
                gameData,
                success,
                error);
        }

        /// <summary>
        /// Recupera informações de um jogo especificado pelo seu Id
        /// </summary>
        /// <param name="gameId">Id do jogo</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetGameInfo(string gameId, Action<ObjResponse> success, Action error)
        {
            get<ObjResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>() 
                {
                    {"metodo","getGameInfo"},
                    {"gameId",gameId}
                });
        }

        /// <summary>
        /// Busca todos os jogos do banco de dados
        /// </summary>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetAllGames(Action<ObjResponse> success, Action error)
        {
            get<ObjResponse>(
                host + "/getAllGames",
                success,
                error
                );
        }


        /// <summary>
        /// Busca jogos pelo titulo 
        /// </summary>
        /// <param name="title">Titulo do jogo</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetSimilarGames(string title, Action<ObjResponse> success, Action error)
        {
            get<ObjResponse>(
                host + "/getSimilarGames",
                success,
                error);
        }

        /// <summary>
        /// Atualiza as informações de um jogo
        /// </summary>
        /// <param name="gameData">Dados do jogo</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void UpdateGame(Dictionary<string, object> gameData, Action<Response> success, Action error)
        {
            put<Response>(
                host + "/updateGame",
                gameData,
                success,
                error);
        }

        /// <summary>
        /// Remove um jogo do banco de dados
        /// </summary>
        /// <param name="gameId">Id do jogo</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void RemoveGame(string gameId, Action<Response> success, Action error)
        {
            delete<Response>(
                host + "/removeGame",
                new Dictionary<string, object>() { { "idJogo", gameId } },
                success,
                error);
        }

        #endregion

        #endregion

        #region Métodos de Empréstimo

        public void RequestEmp(Dictionary<string,object> arg, Action<Response> success, Action error)
        {
            post<Response>(
                host + "/requestEmp",
                arg,
                success,
                error);
        }

        public void AcceptEmp(string idEmp, Action<Response> success, Action error)
        {
            put<Response>(
                host + "/acceptEmp",
                new Dictionary<string, object>()
                { {"idemprestimo", idEmp} },
                success, 
                error);
        }

        public void GetEmpIMade(Action<EmpReponse> success, Action error)
        {
            get<EmpReponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getEmpPorMim"},
                    {"id", this.userInfo.idusuario}
                });
        }

        public void GetEmpThemMade(Action<EmpReponse> success, Action error)
        {
            get<EmpReponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getEmpDeMim"},
                    {"id", this.userInfo.idusuario}
                });
        }

        public void RemoveEmp(string idEmp, Action<Response> success, Action error)
        {
            delete<Response>(
                host + "/removeEmp",
                new Dictionary<string, object>() { { "idemprestimo", idEmp } },
                success, 
                error);
        }

        public void ChangeEmpDate(string idEmp, string date, Action<Response> success, Action error)
        {
            put<Response>(
                host + "/updateEmpDate",
                new Dictionary<string, object>()
                {
                    {"idemprestimo", idEmp},
                    {"dtDevolucao", date}
                },
                success,
                error);
        }

        public void ChangEmpStatus(string idEmp, string status, Action<Response> success, Action error)
        {
            put<Response>(
                host + "/changeEmpStatus",
                new Dictionary<string, object>() 
                {
                    {"idemprestimo",idEmp},
                    {"status",status},
                },
                success,
                error);
        }

        #endregion

        #endregion
    }
}
