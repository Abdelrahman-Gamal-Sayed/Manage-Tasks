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
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            oldData();

           // SnackbarThree2.Message.Content = "Welcome To Task APP";
           // SnackbarThree2.IsActive = true;
        }

        private static string background { get; set; }
        private static string ColorAccent { get; set; }
        private static string ColorPrimary { get; set; }
        private void oldData()
        {
            try
            {
                System.IO.Directory.CreateDirectory(@"Setting");

                if (File.Exists(@"Setting\background.txt"))
                    background = File.ReadAllText(@"Setting\background.txt");
                if (File.Exists(@"Setting\ColorAccent.txt"))
                    ColorAccent = File.ReadAllText(@"Setting\ColorAccent.txt");
                if (File.Exists(@"Setting\ColorPrimary.txt"))
                    ColorPrimary = File.ReadAllText(@"Setting\ColorPrimary.txt");
                changeTheme();

            }
            catch { }
        }
        private void changeTheme()
        {
            Uri uri0 = System.Windows.Application.Current.Resources.MergedDictionaries[0].Source;

            Uri uri2 = System.Windows.Application.Current.Resources.MergedDictionaries[2].Source;

            Uri uri3 = System.Windows.Application.Current.Resources.MergedDictionaries[3].Source;


            System.Windows.Application.Current.Resources.MergedDictionaries.Clear();
            Uri uri;
            if (!string.IsNullOrWhiteSpace(background))
                uri = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme." + background + ".xaml");
            else
                uri = uri0;
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(0, new ResourceDictionary() { Source = uri });

            uri = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml");
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(1, new ResourceDictionary() { Source = uri });

            if (!string.IsNullOrWhiteSpace(ColorAccent))
                uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor." + ColorAccent + ".xaml");
            else
                uri = uri2;
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(2, new ResourceDictionary() { Source = uri });

            if (!string.IsNullOrWhiteSpace(ColorPrimary))
                uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor." + ColorPrimary + ".xaml");
            else
                uri = uri3;
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(3, new ResourceDictionary() { Source = uri });




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            validationFun();
        }

        private async void  Button_Click_1(object sender, RoutedEventArgs e)
        {

             Application.Current.Shutdown();

            //var sampleMessageDialog = new SampleMessageDialog
            //{
            //    Message = { Text = ((ButtonBase)sender).Content.ToString() }
            //};

            //await DialogHost.Show(sampleMessageDialog, "RootDialog");

        }

        private readonly DB db = new DB();

        private void validationFun()
        {

            try
            {


                DataTable s = db.RunReader(@"select CODE,USER_NAME ,PASSWORD ,SHOW_NAME ,USER_AUTHORITY_CODE ,USER_TYPE_CODE ,USER_TYPE_NAME,CONFIRM_PASS  from TASKS_USER where USER_NAME = '" + txtEName.Text.Trim().ToUpper() + "'").Result;
                if (s.Rows.Count <= 0)
                {
                    SnackbarThree.Message.Content = "Please check your Name";
                    SnackbarThree.IsActive = true;

                  //  SnackbarThree.MessageQueue.Enqueue();
                    return;
                }

                if (s.Rows[0][2].ToString().Trim() != PasswordBox.Password.ToString().Trim())
                {
                   // MessageBox.Show("Please check your Password");
                  //  SnackbarThree.MessageQueue.Enqueue("Please check your Password");
                    SnackbarThree.Message.Content = "Please check your Password";
                    SnackbarThree.IsActive = true;
                    return;
                }

                User.Code = s.Rows[0][0].ToString();
                User.Name = s.Rows[0][1].ToString();
                User.Name_Show = s.Rows[0][3].ToString();

                User.AUTHORITY_CODE = s.Rows[0][4].ToString();
                User.TYPE_CODE = s.Rows[0][5].ToString();
                User.TYPE_NAME = s.Rows[0][6].ToString();
                User.CONFIRM_PASS = s.Rows[0][7].ToString();

                DataTable ss = db.RunReader("select NAME ,TASK_FRM ,DASHBOARD_FRM ,TESTER_FRM,DEVELOPER_FRM,ADMIN_FRM  from TASKS_AUTHORITYS WHERE CODE ='" + User.AUTHORITY_CODE + "'").Result;


                if(ss.Rows.Count <= 0)

              {
                    SnackbarThree.Message.Content = "غير مسموح ";
                    SnackbarThree.IsActive = true;
                }
               


                User.AUTHORITY_NAME = ss.Rows[0][0].ToString();
                User.TASK_FRM = ss.Rows[0][1].ToString();
                User.DASHBOARD_FRM = ss.Rows[0][2].ToString();
                User.TESTER_FRM = ss.Rows[0][3].ToString();
                User.DEVELOPER_FRM = ss.Rows[0][4].ToString();
                User.ADMIN_FRM = ss.Rows[0][5].ToString();


               
                if (User.CONFIRM_PASS =="N")
                new ConfirmPass().ShowDialog();



                if (User.ADMIN_FRM.ToUpper() == "Y" )
                {
                    new AdminFrm().Show();
                }
                else if (User.TASK_FRM.ToUpper() == "Y" || User.DASHBOARD_FRM.ToUpper() == "Y" || User.TESTER_FRM.ToUpper() == "Y" || User.DEVELOPER_FRM.ToUpper() == "Y")
                {
                    new ClinicsFrm().Show();
                }
                else
                {
                  //  MessageBox.Show("برجاء المحاولة مرة اخرى");
                 //   SnackbarThree.MessageQueue.Enqueue("برجاء المحاولة مرة اخرى");

                    SnackbarThree.Message.Content = "برجاء المحاولة مرة اخرى";
                    SnackbarThree.IsActive = true;

                    

                }

                Close();
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
    }
}
