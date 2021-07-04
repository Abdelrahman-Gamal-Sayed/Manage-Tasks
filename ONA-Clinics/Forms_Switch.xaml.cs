using System.Windows;
using System.Windows.Controls;

namespace ONA_Tasks {
    /// <summary>
    /// Interaction logic for Forms_Switch.xaml
    /// </summary>
    public partial class Forms_Switch : UserControl
    {
        public Forms_Switch()
        {
            InitializeComponent();

            checkouthotry();


         

         

        }


        private void checkouthotry()
        {

            if (!(User.ADMIN_FRM.ToUpper() == "Y" )) AdminBTN.Visibility = Visibility.Collapsed;

            if (!(User.DASHBOARD_FRM.ToUpper() == "Y" || User.DEVELOPER_FRM.ToUpper() == "Y" || User.TASK_FRM.ToUpper() == "Y" || User.TESTER_FRM.ToUpper() == "Y"))
                DentalBTN.Visibility = Visibility.Collapsed;

        }


        private void SwitchBTN_Click(object sender, RoutedEventArgs e)
        {

            MainWindow ss = new MainWindow();
            ss.Show();
            foreach (Window f in System.Windows.Application.Current.Windows)
            {
                if (f != ss)
                    f.Hide();
            }


        }

    

        private void LogOutBTN_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AdminBTN_Click(object sender, RoutedEventArgs e)
        {


            AdminFrm ss = new AdminFrm();
            ss.Show();
            foreach (Window f in System.Windows.Application.Current.Windows)
            {
                if (f != ss)
                    f.Hide();
            }
        }

  

        private void DentalBTN_Click(object sender, RoutedEventArgs e)
        {
           

            ClinicsFrm ss = new ClinicsFrm();
            ss.Show();
            foreach (Window f in System.Windows.Application.Current.Windows)
            {
                if (f != ss)
                   f.Hide();
            }
        }

   
    

        private void SettingBTN_Click(object sender, RoutedEventArgs e)
        {
            SettingPage a = new SettingPage();
            a.Show();
        }
    }
}
