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
    public class Emprestimo
    {
        [DataMember]
        public int idemprestimo { get; set; }

        [DataMember]
        public int fk_idUser1 { get; set; }

        [DataMember]
        public int fk_idUser2 { get; set; }

        [DataMember]
        public string tipoObjeto { get; set; }

        [DataMember]
        public string dtEmprestimo { get; set; }

        [DataMember]
        public string dtDevolucao { get; set; }

        [DataMember]
        public int status { get; set; }
    }
}
