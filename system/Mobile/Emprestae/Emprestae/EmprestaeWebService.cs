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
        private void get<T>(string url, Dictionary<string, object> args, Action<T> success, Action error)
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
            Dictionary<string, object> arg = new Dictionary<string, object>()
            {
                {"metodo","getUserInfo"},
                {"email",this.userInfo.email}
            };

            get<UserResponse>(host, arg, 
                (result) => 
                {
                    userInfo = result.users[0].user;
                    success(result);
                } , error);
 
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="userData">Dicionário de parâmetros com dados do usuário</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void CreateUser(Dictionary<string, object> userData, Action<UserInfo> success, Action error)
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

            post<UserInfo>(host+"/createUser", arg, success, error);
        }

        /// <summary>
        /// Verifica se o usuário possui conta no sistema
        /// </summary>
        /// <param name="userData">Dicionário de parâmetros com dados do usuário</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        /// <email>vieirarenato.rpv@gmail.com</email>
        public void CheckUser(Dictionary<string, object> userData, Action<UserInfo> success, Action error)
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

            post<UserInfo>(host + "/checkUser", arg,
                (result) =>
                {
                    if (result.user != null)
                        userInfo = result.user;
                    success(result);
                }, error);
        }

        /// <summary>
        /// Busca todos os outros usuários do sistema
        /// </summary>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        public void GetAllUsersButMe(Action<UserResponse> success, Action error)
        {
            Dictionary<string, object> arg = new Dictionary<string, object>()
            {
                {"metodo","getAllUsersBut"},
                {"email",this.userInfo.email},
            };

            get<UserResponse>(host, arg, success, error);
        }

        /// <summary>
        /// Busca usuários por email
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <param name="success">Callback de sucesso</param>
        /// <param name="error">Callback de erro</param>
        public void GetAllUserByEmail(string email, Action<UserResponse> success, Action error)
        {
            Dictionary<string, object> arg = new Dictionary<string, object>()
            {
                {"metodo","getAllUsersByEmail"},
                {"email", email},
            };
            get<UserResponse>(host, arg, success, error);
        }

        #endregion

        #region Métodos de Objetos

        public void GetUserObjs(Action<ObjResponse> success, Action error)
        {
            Dictionary<string, object> arg = new Dictionary<string, object>()
            {
                {"metodo","getAllUsersByEmail"},
                {"userID", this.userInfo.idusuario},
            };
            get<ObjResponse>(host, arg, success, error);
        }

        #endregion

        #region Métodos de Livros

        #endregion

        #endregion
    }
}
