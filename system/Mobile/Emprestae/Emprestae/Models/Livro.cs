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
    public class Livro
    {
        [DataMember]
        public int idLivro { get; set; }

        [DataMember]
        public string titulo { get; set; }

        [DataMember]
        public string autor { get; set; }

        [DataMember]
        public int edicao { get; set; }

        [DataMember]
        public string editora { get; set; }

        [DataMember]
        public string imagePath { get; set; }
    }
}
