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
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using Emprestae.Models;
using System.Diagnostics;

namespace Emprestae.Pages
{
    public partial class AddObjeto : PhoneApplicationPage
    {
        EmprestaeWebService emprestae;
        PhotoChooserTask photoChooserTask;
        CameraCaptureTask cameraCaptureTask;
        PhotoResult photo;
        string photoStr;

        public AddObjeto()
        {
            InitializeComponent();
            emprestae = (Application.Current as App).emprestae;

            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += new EventHandler<PhotoResult>(cameraCaptureTask_Completed);
        }

        private void loadPicture_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                photoChooserTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("An error occurred.");
            }
        }

        private void capturePicture_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                cameraCaptureTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("An error occurred.");
            }
        }

        void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.ChosenPhoto.Length.ToString());
                photo = e;

                //Code to display the photo on the page in an image control named myImage.
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                //myImage.Source = bmp;
            }
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.ChosenPhoto.Length.ToString());
                photo = e;

                //Code to display the photo on the page in an image control named myImage.
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                //myImage.Source = bmp;
            }
        }

        void createLivro()
        {
            //BitmapImage bimg = new BitmapImage();
            //bimg.SetSource(photo.ChosenPhoto); //photoStream is a stream containing data for a photo

            //byte[] bytearray = null;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    WriteableBitmap wbitmp = new WriteableBitmap(bimg);
            //    WriteableBitmap wbimg = new WriteableBitmap(bimg);

            //    wbimg.SaveJpeg(ms, wbitmp.PixelWidth, wbitmp.PixelHeight, 0, 100);
            //    ms.Seek(0, SeekOrigin.Begin);
            //    bytearray = ms.GetBuffer();
            //}
            //photoStr = Convert.ToBase64String(bytearray);
            
            Dictionary<string, object> args = new Dictionary<string, object>() 
            {
                {"titulo", livroTitulo.Text },
                {"autor",livroAutor.Text},
                {"edicao",livroEdicao.Text},
                {"editora",livroEditora.Text}
                //,
                //{"image", photoStr}
            };

            emprestae.CreateObjeto(args, success, error);
        }

        void success(Response response) 
        {
            Dispatcher.BeginInvoke(() => {
                if (response.status == 1)
                {
                    MessageBox.Show("Livro adicionado com sucesso");
                    NavigationService.Navigate(new Uri("/Pages/Home.xaml", UriKind.Relative));
                }
            });
        }

        void error()
        {
            Dispatcher.BeginInvoke(() => { });
        }

        private void AddButton_OnClick(object sender, EventArgs e)
        {
            createLivro();
        }
    }
}