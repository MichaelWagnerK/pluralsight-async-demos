using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;

namespace MyLogin
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginButton.IsEnabled = false;
                BusyIndicator.Visibility = Visibility.Visible;

                var result = await LoginAsync();

                LoginButton.Content = result;
            }
            catch (Exception)
            {
                LoginButton.Content = "Login Failed!";
            }
            finally
            {
                LoginButton.IsEnabled = true;
                BusyIndicator.Visibility = Visibility.Hidden;
            }


        }

        private async Task<string> LoginAsync()
        {
            try
            {
                var loginTask = await Task.Run(() =>
                {
                    Thread.Sleep(2000);

                    return "Login Successful!";
                });

                return loginTask;
            }
            catch (Exception)
            {
                return "Login Failed!";
            }
        }


        //private void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    LoginButton.IsEnabled = false;
        //    var task = Task.Run(() => {

        //        Thread.Sleep(2000);

        //        return "Login Successful!";
        //    });

        //    //task.ContinueWith(t => {
        //    //    if (t.IsFaulted)
        //    //    {
        //    //        Dispatcher.Invoke(() =>
        //    //        {
        //    //            LoginButton.Content = "Login failed!";
        //    //            LoginButton.IsEnabled = true;
        //    //        });
        //    //    }
        //    //    else
        //    //    {
        //    //        Dispatcher.Invoke(() =>
        //    //        {
        //    //            LoginButton.Content = t.Result;
        //    //            LoginButton.IsEnabled = true;
        //    //        });
        //    //    }
        //    //});

        //    task.ConfigureAwait(true)
        //        .GetAwaiter()
        //        .OnCompleted(() =>
        //        {
        //            LoginButton.Content = task.Result;
        //            LoginButton.IsEnabled = true;
        //        });

        //}
    }
}
