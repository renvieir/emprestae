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
using System.Runtime.Serialization;

namespace Emprestae.Models
{
    [DataContract]
    public class ObjResponse
    {
        [DataMember]
        public string status { get; set; }

        [DataMember]
        public LivroArray[] livros { get; set; }

        [DataMember]
        public JogoArray[] jogos { get; set; }

        [DataMember]
        public FilmeArray[] filmes { get; set; }
    }
}
