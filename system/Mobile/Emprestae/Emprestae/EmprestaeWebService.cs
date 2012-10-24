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

        private static string host = "http://service.emprestae.com";

        #endregion

        #region User Info

        public User userInfo { get; set; }

        #endregion

        #endregion

        #region Métodos

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
        public void GetUserInfo(Action<UserResponse> success, Action error)
        {
            get<UserResponse>(
                host, 
                (result) => 
                {
                    userInfo = result.users[0].user;
                    success(result);
                },
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getUserInfo"},
                    {"email",this.userInfo.email}
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
            {
                this.userInfo = new User();
            }
            this.userInfo.email = userData["email"].ToString();

            Dictionary<string, object> arg = new Dictionary<string, object>()
            {
                {"nome",userData["nome"]},
                {"email",userData["email"]},
                {"senha",userData["pwd"]},
                {"addressLat",123},
                {"addressLong",123},
                {"image","oioioi"}
            };

            post<Response>(host+"/createUser", arg, success, error);
        }

        public void UpdateUser(Action<Response> success, Action error)
        {
            put<Response>(host + "/updateUser",
                ConvertUserToDictionary(),
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
            {
                this.userInfo = new User();
            }

            this.userInfo.email = userData["email"].ToString();

            Dictionary<string, object> arg = new Dictionary<string, object>()
            {
                {"email",userData["email"]},
                {"senha",userData["pwd"]}
            };

            post<Response>(
                host + "/checkUser",
                arg,
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
        /// Busca usuários por email
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void GetAllUserByEmail(string email, Action<UserResponse> success, Action error)
        {
            get<UserResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getAllUsersByEmail"},
                    {"email", email},
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
        public void GetCloseUsers(string userLat, string userLong, Action<UserResponse> success, Action error)
        {
            get<UserResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getCloseUsers"},
                    {"userLat", userLat},
                    {"userLong", userLong}
                });

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
        public void GetUserObjs(Action<ObjResponse> success, Action error)
        {
            get<ObjResponse>(
                host,
                success,
                error,
                new Dictionary<string, object>()
                {
                    {"metodo","getUserObjs"},
                    {"userID", this.userInfo.idusuario},
                });
        }

        public void AddUserObj(Dictionary<string, object> objData, Action<Response> success, Action error)
        {
            post<Response>(
                host + "/addUserObj",
                objData,
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
        /// Recupera todos os livros do banco
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

        #endregion

        #region Filmes

        public void CreateFilm(Dictionary<string, object> filmData, Action<Response> success, Action error)
        {
            post<Response>(
                host + "/createFilm",
                filmData,
                success,
                error);
        }
        
        #endregion

        #region Jogos

        public void CreateGame(Dictionary<string, object> gameData, Action<Response> success, Action error)
        {
            post<Response>(
                host + "/createGame",
                gameData,
                success,
                error);
        }

        #endregion

        #endregion

        #endregion
    }
}
