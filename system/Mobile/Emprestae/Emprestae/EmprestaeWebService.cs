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
        private static string host = "www.emprestei.dev/";

        /// <summary>
        /// Método GET genérico a ser utilizada pelos outros métodos
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de resposta</typeparam>
        /// <param name="args">Dicionário de parâmetros da chamada</param>
        /// <param name="success">Callback de Sucesso</param>
        /// <param name="error">Callback de erro</param>
        /// <author>Renato Vieira</author>
        public void get<T>(Dictionary<string, object> args, Action<T> success,Action error)
        {
            get(new Uri(host, UriKind.Absolute),
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
                        //T response = (T)(object)typeof(T);
                        string result = Util.decodeResponseToString(requestArgs.result as HttpWebResponse);
                        success(Util.deserializeObject<T>(result));
                    }
                });
        }

        public void getUser(string email, Action<Usuario> success, Action error)
        {
            Dictionary<string, object> arg = new Dictionary<string, object>()
            {
                {"email",email}
            };

            get<Usuario>(arg, success, error);
 
        }
    }
}
