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
    public class User
    {
        [DataMember]
        public int idusuario { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string nome { get; set; }

        [DataMember]
        public string addressLat { get; set; }

        [DataMember]
        public string addressLong { get; set; }

        [DataMember]
        public string imagePath { get; set; }
    }
}
