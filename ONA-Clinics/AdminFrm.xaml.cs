using System;
using System.Windows;
using System.Windows.Input;



namespace ONA_Tasks
{
 
    /// <summary>
    /// Interaction logic for form1.xaml
    /// </summary>
    public partial class AdminFrm : Window
    {
        DB db = new DB();
       

        public AdminFrm()
        {
            InitializeComponent();
            cbxServies3.ItemsSource = db.RunReader("select CODE,NAME from  TASKS_AUTHORITYS ").Result.DefaultView;
            txt_code_user1.Text = db.RunReader("select nvl(Max(CODE),1)+1 from TASKS_USER").Result.Rows[0][0].ToString();
            cbxServies2.ItemsSource = db.RunReader("select CODE,NAME from TASKS_USER_TYPE ").Result.DefaultView;
            txt_occerty_code_main_user.Text = db.RunReader("select nvl(Max(CODE),0)+1 from TASKS_AUTHORITYS").Result.Rows[0][0].ToString();
            Username.Text = User.Name_Show;
        }


        //Dentaltooth.subservies = cbxServies.Text;
        #region MainRegion

        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9.]+");
            e.Handled = reg.IsMatch(e.Text);

        }
        private void NumberOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            e.Handled = reg.IsMatch(e.Text);

        }
        private void LogOutBTN_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenMenueBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenMenueBTN.Visibility = Visibility.Collapsed;
            CloseMenueBTN.Visibility = Visibility.Visible;
        }

        private void CloseMenueBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenMenueBTN.Visibility = Visibility.Visible;
            CloseMenueBTN.Visibility = Visibility.Collapsed;

        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as System.Windows.Controls.Grid;
            var win = Window.GetWindow(move);
            win.DragMove();

        }


        private void PackIcon_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void PackIcon_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

   

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
         
                this.WindowState = WindowState.Maximized;
            btnMax.Visibility = Visibility.Collapsed;
            btnRestore.Visibility = Visibility.Visible;

        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            btnMax.Visibility = Visibility.Visible;
            btnRestore.Visibility = Visibility.Collapsed;
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void AdminBTN_Click(object sender, RoutedEventArgs e)
        {
            new AdminFrm().Show();
            this.Close();
        }

        private void clinicBTN_Click(object sender, RoutedEventArgs e)
        {
            new ClinicsFrm().Show();
            this.Close();

        }

        private void hospitalBTN_Click(object sender, RoutedEventArgs e)
        {
            //new form1().Show();
            //this.Close();
        }

        private void SwitchBTN_Click(object sender, RoutedEventArgs e)
        {
          
                new MainWindow().Show();
            this.Close();
        }        
   
        private void refBTN_Click(object sender, RoutedEventArgs e)
        {
         
            Console.WriteLine("SAMPLE 2: Closing dialog with parameter:");

          
        }





        #endregion

   
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // DataTable ss = db.RunReader("select NAME ,DENTAL_FRM ,HOSPITAL_FRM ,PHYSICAL_THERAPY_FRM,DOCTOR_FRM  from TASKS_AUTHORITYS WHERE CODE ='" + User.AUTHORITY_CODE + "'");



            txt_occerty_name_main_user.Text = "";
            chb_main.IsChecked = false;
            chb_cacher.IsChecked = false;

            chb_katchen.IsChecked = false;
            chb_view.IsChecked = false;
            chb_report.IsChecked = false;
            //TASKS_AUTHORITYS
            txt_occerty_code_main_user.Text = db.RunReader("select nvl(Max(CODE),1)+1 from TASKS_AUTHORITYS").Result.Rows[0][0].ToString();
            dg_user_main1.ItemsSource = null;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txt_code_user1.Text = "";
            txtAName3.Text = "";
            txtAName4.Password = "";
            txtAName5.Text = "";
      
            txt_user_main_seach2.Text = "";
            cbxServies2.Text = "";
          //  cbx_osserty_user_Copy3.Text = "";
           
            cbxServies3.Text = "";
            txt_code_user1.Text = db.RunReader("select nvl(Max(CODE),1)+1 from TASKS_USER").Result.Rows[0][0].ToString();
            dgTotal.ItemsSource = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

         

                  if (txt_occerty_name_main_user.Text.Trim() == string.Empty)
                MessageBox.Show("اكمل البيانات");
            else
            {
                string TASK_FRM = "N", DASHBOARD_FRM = "N", TESTER_FRM = "N", DEVELOPER_FRM = "N", ADMIN_FRM = "N";
                if (chb_cacher.IsChecked == true)
                    DASHBOARD_FRM = "Y";

                if (chb_report.IsChecked == true)
                    TESTER_FRM = "Y";


                if (chb_view.IsChecked == true)
                    DEVELOPER_FRM = "Y";


                if (chb_katchen.IsChecked == true)
                    TASK_FRM = "Y";


                if (chb_main.IsChecked == true)
                    ADMIN_FRM = "Y";

                if (db.RunReader(" select CODE  from TASKS_AUTHORITYS  where CODE ='" + txt_occerty_code_main_user.Text + "' ").Result.Rows.Count == 0)
                    db.RunNonQuery(@"insert into TASKS_AUTHORITYS (CODE, NAME , TASK_FRM, DASHBOARD_FRM, TESTER_FRM, DEVELOPER_FRM, ADMIN_FRM,CREATEDBY ,CREATEDDATE) 
 VALUES ('" + txt_occerty_code_main_user.Text + "','" + txt_occerty_name_main_user.Text + "','" + TASK_FRM + "','" + DASHBOARD_FRM + "','" + TESTER_FRM + "','" + DEVELOPER_FRM + "','" + ADMIN_FRM + "','" + User.Name + "',sysdate)", "تم الحفظ");
                else
                    db.RunNonQuery("UPDATE TASKS_AUTHORITYS SET NAME ='" + txt_occerty_name_main_user.Text + "'  ,TASK_FRM = '" + TASK_FRM + "', DASHBOARD_FRM = '" + DASHBOARD_FRM + "', TESTER_FRM ='" + TESTER_FRM + "', DEVELOPER_FRM = '" + DEVELOPER_FRM + "', ADMIN_FRM = '" + ADMIN_FRM + "', UPDATEDBY = '" + User.Name + "', UPDATEDDATE = sysdate WHERE CODE='" + txt_occerty_code_main_user.Text + "' ", "تم التعديل");
            }
                  cbxServies3.ItemsSource = db.RunReader("select CODE,NAME from  TASKS_AUTHORITYS ").Result.DefaultView;
      
        }

        private void txt_user_main_seach1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            dg_user_main1.ItemsSource = db.RunReader("select CODE, NAME , TASK_FRM, DASHBOARD_FRM, TESTER_FRM, DEVELOPER_FRM, ADMIN_FRM from  TASKS_AUTHORITYS where CODE like '%" + txt_user_main_seach1.Text.Trim() + "%' or NAME like '%" + txt_user_main_seach1.Text.Trim() + "%' ").Result.DefaultView;
        }

        private void txt_user_main_seach2_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            dgTotal.ItemsSource = db.RunReader("select CODE, USER_NAME , PASSWORD, SHOW_NAME, USER_AUTHORITY_CODE, USER_TYPE_CODE, USER_TYPE_NAME,CREATED_BY ,CREATED_DATE,UPDATED_BY ,UPDATED_DATE from  TASKS_USER where CODE like '%" + txt_user_main_seach2.Text.Trim() + "%' or USER_NAME like '%" + txt_user_main_seach2.Text.Trim() + "%' ").Result.DefaultView;
     
        }

       

      
        private void Dg_user_main1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {  //      dg_user_main1.ItemsSource = db.RunReader("select CODE, NAME , DENTAL_FRM, HOSPITAL_FRM, PHYSICAL_THERAPY_FRM, DOCTOR_FRM, ADMIN_FRM from  TASKS_AUTHORITYS where CODE like '%" + txt_user_main_seach1.Text.Trim() + "%' or NAME like '%" + txt_user_main_seach1.Text.Trim() + "%' ").DefaultView;
            try
            {

                //CODE, NAME , TASK_FRM, DASHBOARD_FRM, TESTER_FRM, DEVELOPER_FRM, ADMIN_FRM 
                if (dg_user_main1.SelectedIndex > -1)
                {
                    System.Data.DataRowView row = (System.Data.DataRowView)dg_user_main1.SelectedItem;
                    txt_occerty_code_main_user.Text = row[0].ToString();
                    txt_occerty_name_main_user.Text = row[1].ToString();
                    chb_katchen.IsChecked = (row[2].ToString() == "Y") ? true : false;
                    chb_cacher.IsChecked = (row[3].ToString() == "Y") ? true : false;
                    chb_report.IsChecked = (row[4].ToString() == "Y") ? true : false;
                    chb_view.IsChecked = (row[5].ToString() == "Y") ? true : false;
                    chb_main.IsChecked = (row[6].ToString() == "Y") ? true : false;

                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {



            if(cbxServies3.Text.Trim() == string.Empty) {
                MessageBox.Show("برجاء اختيار صلاحية");
                return;
            }
            if (txtAName3.Text.Trim() == string.Empty)
            {
                MessageBox.Show("اكمل البيانات");
                return;
            }

            if(cbxServies2.Text.Trim()==string.Empty) {
                MessageBox.Show("برجاء اختيار نوع المستخدم");
                return;
            }
         string USER_TYPE_NAME=  db.RunReader("select  NAME from TASKS_USER_TYPE where CODE='" + cbxServies2.Text+ "'").Result.Rows[0][0].ToString();




            if (db.RunReader(" select CODE  from TASKS_USER  where CODE ='" + txt_code_user1.Text + "' ").Result.Rows.Count == 0)
                db.RunNonQuery(@"insert into TASKS_USER (CODE, USER_NAME , PASSWORD, SHOW_NAME, USER_AUTHORITY_CODE, USER_TYPE_CODE, USER_TYPE_NAME,CREATED_BY ,CREATED_DATE)
VALUES ('" + txt_code_user1.Text + "','" + txtAName3.Text.Trim().ToUpper() + "','" + txtAName4.Password.Trim() + "','" + txtAName5.Text.Trim() + "','" + cbxServies3.Text + "','" + cbxServies2.Text + "','" + USER_TYPE_NAME + "','" + User.Name + "',sysdate)", "تم الحفظ");
            else
                db.RunNonQuery("UPDATE TASKS_USER SET USER_NAME ='" + txtAName3.Text.Trim().ToUpper() + "'  ,PASSWORD = '" + txtAName4.Password.Trim() + "', SHOW_NAME = '" + txtAName5.Text.Trim() 
                    + "', USER_AUTHORITY_CODE ='" + cbxServies3.Text + "', USER_TYPE_CODE = '" + cbxServies2.Text + "', USER_TYPE_NAME = '" + USER_TYPE_NAME + "', UPDATED_BY  = '" + User.Name + "', UPDATED_DATE = sysdate WHERE CODE='" + txt_code_user1.Text + "' ", "تم التعديل");


        }

        private void DgTotal_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (dgTotal.SelectedIndex > -1)
                {
                    System.Data.DataRowView row = (System.Data.DataRowView)dgTotal.SelectedItem;
                    txt_code_user1.Text = row[0].ToString();
                    txtAName3.Text = row[1].ToString();
                    txtAName4 .Password= row[2].ToString();
                    txtAName5.Text = row[3].ToString();
                    // cbx_osserty_user_Copy3.Text = row[4].ToString();
                    cbxServies2.Text = row[5].ToString();
                    cbxServies3.Text = row[4].ToString();
              

                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }

     
    }
}
