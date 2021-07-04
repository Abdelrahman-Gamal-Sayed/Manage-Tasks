using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ONA_Tasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ConfirmPass : Window
    {
      
        public ConfirmPass()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
          
       
        }

 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            validationFun();
        }

 

        private readonly DB db = new DB();

        private void validationFun()
        {

            try
            {


                if (string.IsNullOrWhiteSpace(PasswordBox1.Password) || string.IsNullOrWhiteSpace(PasswordBox.Password))
                    return;

                if(PasswordBox1.Password.Trim()== PasswordBox.Password.Trim())
                {
                    done.Visibility = Visibility.Visible;
                    falsea.Visibility = Visibility.Collapsed;

                }
                else
                {
                    falsea.Visibility = Visibility.Visible;
                    done.Visibility = Visibility.Collapsed;

                }

           
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                validationFun();
            }

        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e) {
            SnackbarThree.IsActive = false;
        }

     

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if(string.IsNullOrWhiteSpace(PasswordBox1.Password)|| string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Enter New Password");
                return;
            }
            if (PasswordBox1.Password.Trim() == PasswordBox.Password.Trim() )
            {
                done.Visibility = Visibility.Visible;
                falsea.Visibility = Visibility.Collapsed;

                db.RunNonQuery("UPDATE TASKS_USER SET CONFIRM_PASS = 'Y' ,PASSWORD='" + PasswordBox.Password.Trim() + "' WHERE CODE = '" + User.Code + "'","Done");

                Close();

            }
            else
            {
                falsea.Visibility = Visibility.Visible;
                done.Visibility = Visibility.Collapsed;
                MessageBox.Show("Password doesn't match");

            }
        }

        private void PasswordBox1_KeyUp(object sender, KeyEventArgs e)
        {
            validationFun();
        }
    }
}
