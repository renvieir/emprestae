using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Emprestae.Models;

namespace Emprestae.Pages
{
    public partial class SearchObj : PhoneApplicationPage
    {
        EmprestaeWebService emprestae;
        public SearchObj()
        {
            InitializeComponent();
            emprestae = (Application.Current as App).emprestae;
        }

        private void BuscarButton_OnClick(object sender, EventArgs e)
        {
            loadingLayer.Visibility = Visibility.Visible;
            switch (objType.SelectedIndex)
	        {
                // Todos os objetos
                case 0:
                    break;

                // livro
                case 1:
                    emprestae.GetSimilarBooks(inputText.Text, successLivro, error);
                    break;
                
                // filme
                case 2:
                    break;

                // jogo
                case 3:
                    break;
	        }

        }

        #region Callbacks

        void successLivro(ObjResponse response)
        {
            Dispatcher.BeginInvoke(() =>
            {
                loadingLayer.Visibility = Visibility.Collapsed;
                if (response != null)
                {
                    livrosListBox.ItemsSource = response.livros;
                }
            });
        }

        void successFilme(ObjResponse response)
        {
            Dispatcher.BeginInvoke(() =>
            {
                loadingLayer.Visibility = Visibility.Collapsed;
                if (response != null)
                {
                    filmesListBox.ItemsSource = response.filmes;
                }
            });
        }

        void successJogo(ObjResponse response)
        {
            Dispatcher.BeginInvoke(() =>
            {
                loadingLayer.Visibility = Visibility.Collapsed;
                if (response != null)
                {
                    jogosListBox.ItemsSource = response.jogos;
                }
            });
        }

        void error()
        { }

        #endregion
    }
}