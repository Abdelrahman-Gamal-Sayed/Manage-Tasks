using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ONA_Tasks
{

    /// <summary>
    /// Interaction logic for form1.xaml
    /// </summary>
    public partial class ClinicsFrm : Window
    {
        private readonly DB db = new DB();
        
        private readonly DispatcherTimer disafter = new DispatcherTimer();
        public ClinicsFrm()
        {
            InitializeComponent();
            hideAll();

            

            if (User.DASHBOARD_FRM.ToUpper() != "Y")
            {
                lv_Dashboard.Visibility = Visibility.Collapsed;
            }

            if (User.DEVELOPER_FRM.ToUpper() != "Y")
            {
                lv_Developer.Visibility = Visibility.Collapsed;
            }



            if (User.TESTER_FRM.ToUpper() != "Y")
            {
                lv_Tester.Visibility = Visibility.Collapsed;
            }

            if (User.TASK_FRM.ToUpper() != "Y")
            {
                lv_Task.Visibility = Visibility.Collapsed;
            }

            Username.Text = User.Name_Show;


            disafter.Tick += new EventHandler(dis_Ticks);
            disafter.Start();
            disafter.Interval = new TimeSpan(0, 0, 2);

        }

        private void dis_Ticks(object sender, EventArgs e)
        {
            if (g_task.Visibility == Visibility.Visible)
                taskref();
            else if (g_dashboard.Visibility == Visibility.Visible)
                InitializeDataDashboard();
            else if (g_developer.Visibility == Visibility.Visible)
                InitializeDataDeveloper();
            else if (g_tester.Visibility == Visibility.Visible)
                InitializeDataTester();
        }
        private void taskref()
        {
            try
            {
                sp_tasksS.Items.Clear();
                DataTable ss = db.RunReader(@"SELECT CODE ,TITLE,FROM_DATE,TO_DATE,DURATION,DETAILS,FLAGE,TO_DEVELOPER,STARTED_DATE,ENDED_DATE,DURATION_TAKED,TO_TESTER,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE,DEVELOPER_NOTE,TESTER_NOTE FROM TASK_DETAILS where flage = 'S' order by code DESC").Result;

                for (int i = 0; i < ss.Rows.Count; i++)
                {
                    TaskDemo td = new TaskDemo();
                    td.Code.Text = ss.Rows[i][0].ToString();
                    td.Title.Text = ss.Rows[i][1].ToString();

                    if (ss.Rows[i][2].ToString().Trim() != "")
                        td.From.Text = Convert.ToDateTime(ss.Rows[i][2].ToString()).ToString("dd-MM-yyyy");
                    else
                        td.From.Text = "";
                    
                    if (ss.Rows[i][3].ToString().Trim() != "")
                        td.To.Text = Convert.ToDateTime(ss.Rows[i][3].ToString()).ToString("dd-MM-yyyy");
                    else
                        td.To.Text = "";


                    td.Duration.Text = ss.Rows[i][4].ToString();
                  //  td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Task_Details.Text = ss.Rows[i][5].ToString();
                    td.Task_Flage.Text = ss.Rows[i][6].ToString();
                    td.To_Developer.Text = ss.Rows[i][7].ToString();
                    td.Started_Date.Text = ss.Rows[i][8].ToString();
                    td.Ended_Date.Text = ss.Rows[i][9].ToString();
                    td.Duration_Taked.Text = ss.Rows[i][10].ToString();
                    td.To_Tester.Text = ss.Rows[i][11].ToString();
                    td.Created_By.Text = ss.Rows[i][12].ToString();

                    td.DEVELOPER_NOTE.Text = ss.Rows[i][16].ToString();
                    td.TESTER_NOTE.Text = ss.Rows[i][17].ToString();
                    td.Margin = new Thickness(5);
                    td.HorizontalAlignment = HorizontalAlignment.Center;
                    sp_tasksS.Items.Add(td);
                    //   sp_tasksS.Children.Add(td);


                }
            }
            catch { }
        }

        private void InitializeDataDeveloper()
        {
            try
            {
                //lv_D_task
                lv_D_task.Items.Clear();
                DataTable ss = db.RunReader(@"SELECT CODE ,TITLE,FROM_DATE,TO_DATE,DURATION,DETAILS,FLAGE,TO_DEVELOPER,STARTED_DATE,ENDED_DATE,DURATION_TAKED,TO_TESTER,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE,DEVELOPER_NOTE,TESTER_NOTE FROM TASK_DETAILS where flage = 'D' and TO_DEVELOPER='" + User.Name + "' order by code DESC").Result;

                for (int i = 0; i < ss.Rows.Count; i++)
                {
                    TaskDemo td = new TaskDemo();
                    td.Code.Text = ss.Rows[i][0].ToString();
                    td.Title.Text = ss.Rows[i][1].ToString();

                    if (ss.Rows[i][2].ToString().Trim() != "")
                    {
                        td.From.Text = Convert.ToDateTime(ss.Rows[i][2].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.From.Text = "";
                    }

                    if (ss.Rows[i][3].ToString().Trim() != "")
                    {
                        td.To.Text = Convert.ToDateTime(ss.Rows[i][3].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.To.Text = "";
                    }

                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Task_Details.Text = ss.Rows[i][5].ToString();
                    td.Task_Flage.Text = ss.Rows[i][6].ToString();
                    td.To_Developer.Text = ss.Rows[i][7].ToString();


                    if (ss.Rows[i][8].ToString().Trim() == "")
                    {
                        db.RunNonQuery("UPDATE TASK_DETAILS SET STARTED_DATE = sysdate WHERE CODE = '" + ss.Rows[i][0].ToString() + "'");
                        td.Started_Date.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.Started_Date.Text = ss.Rows[i][8].ToString();
                    }

                    td.Ended_Date.Text = ss.Rows[i][9].ToString();
                    td.Duration_Taked.Text = ss.Rows[i][10].ToString();
                    td.To_Tester.Text = ss.Rows[i][11].ToString();
                    td.Created_By.Text = ss.Rows[i][12].ToString();
                    td.DEVELOPER_NOTE.Text = ss.Rows[i][16].ToString();
                    td.TESTER_NOTE.Text = ss.Rows[i][17].ToString();
                    td.Margin = new Thickness(5);
                    td.HorizontalAlignment = HorizontalAlignment.Center;
                    lv_D_task.Items.Add(td);




                }
            }
            catch { }
        }

        private void InitializeDataTask()
        {
            cbx_to.ItemsSource = db.RunReader("select DISTINCT CODE,USER_NAME from  TASKS_USER where USER_TYPE_CODE='1'").Result.DefaultView;
            refNewTask();
        }

        private void refNewTask()
        {
            try
            {
                if (txt_search.Text.Trim() != "")
                    dg_task.ItemsSource = db.RunReader(@"SELECT CODE ,TITLE,FROM_DATE,TO_DATE,DURATION,DETAILS,FLAGE,TO_DEVELOPER,STARTED_DATE,ENDED_DATE,DURATION_TAKED,TO_TESTER,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE,DEVELOPER_NOTE,TESTER_NOTE FROM TASK_DETAILS where flage = 'S' and CODE like '%" + txt_search.Text.Trim() + "%'  order by code DESC").Result.DefaultView;
                else
                    dg_task.ItemsSource = null;
                

                taskref();
            }
            catch { }
        }

        private void hideAll()
        {
            g_task.Visibility = Visibility.Hidden;
            g_dashboard.Visibility = Visibility.Hidden;
            g_developer.Visibility = Visibility.Hidden;
            g_tester.Visibility = Visibility.Hidden;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.Grid move = sender as System.Windows.Controls.Grid;
            Window win = Window.GetWindow(move);
            win.DragMove();

        }


        private void NumericOnly(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9.]+");
            e.Handled = reg.IsMatch(e.Text);

        }
        private void NumberOnly(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            e.Handled = reg.IsMatch(e.Text);

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





        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {

            WindowState = WindowState.Maximized;
            btnMax.Visibility = Visibility.Collapsed;
            btnRestore.Visibility = Visibility.Visible;

        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            btnMax.Visibility = Visibility.Visible;
            btnRestore.Visibility = Visibility.Collapsed;
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void UpIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (txt_Doration.Text.Trim() == "")
                {
                    txt_Doration.Text = "1";
                }
                else
                {
                    txt_Doration.Text = (Convert.ToInt16(txt_Doration.Text) + 1).ToString();
                }
            }
            catch { }



        }

        private void DowenIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (txt_Doration.Text.Trim() == "")
                {
                    txt_Doration.Text = "0";
                }
                else
                {
                    txt_Doration.Text = (Convert.ToInt16(txt_Doration.Text) - 1).ToString();
                }

                if (Convert.ToInt16(txt_Doration.Text) < 0)
                {
                    txt_Doration.Text = "0";
                }
            }
            catch { }
        }


        private void dp_from_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dp_from.Text.Trim() == "" || dp_to.Text.Trim() == "")
                {
                    return;
                }

                txt_Doration.Text = ((Convert.ToDateTime(dp_to.Text) - Convert.ToDateTime(dp_from.Text)).TotalDays).ToString();
            }
            catch { }





        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            lbl_code.Content = "******";
            txt_title.Text = "";
            dp_from.Text = "";
            dp_to.Text = "";
            cbx_to.Text = "";
            txt_Doration.Text = "";
            txt_details.Text = "";
            txt_search.Text = "";
            dg_task.ItemsSource = null;

        }

        private void lv_main_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            hideAll();
            switch (lv_main.SelectedIndex)
            {
                case 0:
                    g_dashboard.Visibility = Visibility.Visible;
                    InitializeDataDashboard();
                    break;
                case 1:
                    g_task.Visibility = Visibility.Visible;
                    InitializeDataTask();
                    break;
                case 2:
                    g_developer.Visibility = Visibility.Visible;
                    InitializeDataDeveloper();
                    break;
                case 3:
                    g_tester.Visibility = Visibility.Visible;
                    InitializeDataTester();
                    break;
                default:
                    break;
            }
        }

        private void InitializeDataDashboard()
        {
            try
            {

                sp_created.Children.Clear();
                DataTable ss = db.RunReader(@"SELECT CODE ,TITLE,FROM_DATE,TO_DATE,DURATION,DETAILS,FLAGE,TO_DEVELOPER,STARTED_DATE,ENDED_DATE,DURATION_TAKED,TO_TESTER,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE,DEVELOPER_NOTE,TESTER_NOTE FROM TASK_DETAILS where flage = 'S' order by code DESC").Result;

                for (int i = 0; i < ss.Rows.Count; i++)
                {
                    TaskDemo td = new TaskDemo();
                    td.Code.Text = ss.Rows[i][0].ToString();
                    td.Title.Text = ss.Rows[i][1].ToString();

                    if (ss.Rows[i][2].ToString().Trim() != "")
                    {
                        td.From.Text = Convert.ToDateTime(ss.Rows[i][2].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.From.Text = "";
                    }

                    if (ss.Rows[i][3].ToString().Trim() != "")
                    {
                        td.To.Text = Convert.ToDateTime(ss.Rows[i][3].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.To.Text = "";
                    }

                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Task_Details.Text = ss.Rows[i][5].ToString();
                    td.Task_Flage.Text = ss.Rows[i][6].ToString();
                    td.To_Developer.Text = ss.Rows[i][7].ToString();
                    td.Started_Date.Text = ss.Rows[i][8].ToString();
                    td.Ended_Date.Text = ss.Rows[i][9].ToString();
                    td.Duration_Taked.Text = ss.Rows[i][10].ToString();
                    td.To_Tester.Text = ss.Rows[i][11].ToString();
                    td.Created_By.Text = ss.Rows[i][12].ToString();



                    td.DEVELOPER_NOTE.Text = ss.Rows[i][16].ToString();
                    td.TESTER_NOTE.Text = ss.Rows[i][17].ToString();


                    td.Margin = new Thickness(5);
                    td.HorizontalAlignment = HorizontalAlignment.Center;
                    sp_created.Children.Add(td);
                    //   sp_tasksS.Children.Add(td);


                }




                sp_developer.Children.Clear();
                ss = db.RunReader(@"SELECT CODE ,TITLE,FROM_DATE,TO_DATE,DURATION,DETAILS,FLAGE,TO_DEVELOPER,STARTED_DATE,ENDED_DATE,DURATION_TAKED,TO_TESTER,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE,DEVELOPER_NOTE,TESTER_NOTE FROM TASK_DETAILS where flage = 'D' order by code DESC").Result;

                for (int i = 0; i < ss.Rows.Count; i++)
                {
                    TaskDemo td = new TaskDemo();
                    td.Code.Text = ss.Rows[i][0].ToString();
                    td.Title.Text = ss.Rows[i][1].ToString();

                    if (ss.Rows[i][2].ToString().Trim() != "")
                    {
                        td.From.Text = Convert.ToDateTime(ss.Rows[i][2].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.From.Text = "";
                    }

                    if (ss.Rows[i][3].ToString().Trim() != "")
                    {
                        td.To.Text = Convert.ToDateTime(ss.Rows[i][3].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.To.Text = "";
                    }

                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Task_Details.Text = ss.Rows[i][5].ToString();
                    td.Task_Flage.Text = ss.Rows[i][6].ToString();
                    td.To_Developer.Text = ss.Rows[i][7].ToString();
                    td.Started_Date.Text = ss.Rows[i][8].ToString();
                    td.Ended_Date.Text = ss.Rows[i][9].ToString();
                    td.Duration_Taked.Text = ss.Rows[i][10].ToString();
                    td.To_Tester.Text = ss.Rows[i][11].ToString();
                    td.Created_By.Text = ss.Rows[i][12].ToString();


                    td.DEVELOPER_NOTE.Text = ss.Rows[i][16].ToString();
                    td.TESTER_NOTE.Text = ss.Rows[i][17].ToString();
                    td.Margin = new Thickness(5);
                    td.HorizontalAlignment = HorizontalAlignment.Center;
                    sp_developer.Children.Add(td);
                    //   sp_tasksS.Children.Add(td);


                }



                sp_tester.Children.Clear();
                ss = db.RunReader(@"SELECT CODE ,TITLE,FROM_DATE,TO_DATE,DURATION,DETAILS,FLAGE,TO_DEVELOPER,STARTED_DATE,ENDED_DATE,DURATION_TAKED,TO_TESTER,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE,DEVELOPER_NOTE,TESTER_NOTE FROM TASK_DETAILS where flage = 'T' order by code DESC").Result;

                for (int i = 0; i < ss.Rows.Count; i++)
                {
                    TaskDemo td = new TaskDemo();
                    td.Code.Text = ss.Rows[i][0].ToString();
                    td.Title.Text = ss.Rows[i][1].ToString();

                    if (ss.Rows[i][2].ToString().Trim() != "")
                    {
                        td.From.Text = Convert.ToDateTime(ss.Rows[i][2].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.From.Text = "";
                    }

                    if (ss.Rows[i][3].ToString().Trim() != "")
                    {
                        td.To.Text = Convert.ToDateTime(ss.Rows[i][3].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.To.Text = "";
                    }

                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Task_Details.Text = ss.Rows[i][5].ToString();
                    td.Task_Flage.Text = ss.Rows[i][6].ToString();
                    td.To_Developer.Text = ss.Rows[i][7].ToString();
                    td.Started_Date.Text = ss.Rows[i][8].ToString();
                    td.Ended_Date.Text = ss.Rows[i][9].ToString();
                    td.Duration_Taked.Text = ss.Rows[i][10].ToString();
                    td.To_Tester.Text = ss.Rows[i][11].ToString();
                    td.Created_By.Text = ss.Rows[i][12].ToString();

                    td.DEVELOPER_NOTE.Text = ss.Rows[i][16].ToString();
                    td.TESTER_NOTE.Text = ss.Rows[i][17].ToString();
                    td.Margin = new Thickness(5);
                    td.HorizontalAlignment = HorizontalAlignment.Center;
                    sp_tester.Children.Add(td);
                    //   sp_tasksS.Children.Add(td);


                }

                
                sp_done.Children.Clear();
                ss = db.RunReader(@"SELECT CODE ,TITLE,FROM_DATE,TO_DATE,DURATION,DETAILS,FLAGE,TO_DEVELOPER,STARTED_DATE,ENDED_DATE,DURATION_TAKED,TO_TESTER,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE,DEVELOPER_NOTE,TESTER_NOTE FROM TASK_DETAILS where flage = 'Y' and rownum <=10 order by ENDED_DATE DESC ").Result;

                for (int i = 0; i < ss.Rows.Count; i++)
                {
                    TaskDemo td = new TaskDemo();
                    td.Code.Text = ss.Rows[i][0].ToString();
                    td.Title.Text = ss.Rows[i][1].ToString();

                    if (ss.Rows[i][2].ToString().Trim() != "")
                    {
                        td.From.Text = Convert.ToDateTime(ss.Rows[i][2].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.From.Text = "";
                    }

                    if (ss.Rows[i][3].ToString().Trim() != "")
                    {
                        td.To.Text = Convert.ToDateTime(ss.Rows[i][3].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.To.Text = "";
                    }

                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Task_Details.Text = ss.Rows[i][5].ToString();
                    td.Task_Flage.Text = ss.Rows[i][6].ToString();
                    td.To_Developer.Text = ss.Rows[i][7].ToString();
                    td.Started_Date.Text = ss.Rows[i][8].ToString();
                    td.Ended_Date.Text = ss.Rows[i][9].ToString();
                    td.Duration_Taked.Text = ss.Rows[i][10].ToString();
                    td.To_Tester.Text = ss.Rows[i][11].ToString();
                    td.Created_By.Text = ss.Rows[i][12].ToString();


                    td.DEVELOPER_NOTE.Text = ss.Rows[i][16].ToString();
                    td.TESTER_NOTE.Text = ss.Rows[i][17].ToString();
                    td.Margin = new Thickness(5);
                    td.HorizontalAlignment = HorizontalAlignment.Center;
                    sp_done.Children.Add(td);
                    //   sp_tasksS.Children.Add(td);


                }



                sp_end.Children.Clear();
                ss = db.RunReader(@"SELECT CODE ,TITLE,FROM_DATE,TO_DATE,DURATION,DETAILS,FLAGE,TO_DEVELOPER,STARTED_DATE,ENDED_DATE,DURATION_TAKED,TO_TESTER,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE,DEVELOPER_NOTE,TESTER_NOTE FROM TASK_DETAILS where flage != 'Y' and To_date<sysdate order by To_date DESC").Result;

                for (int i = 0; i < ss.Rows.Count; i++)
                {
                    TaskDemo td = new TaskDemo();
                    td.Code.Text = ss.Rows[i][0].ToString();
                    td.Title.Text = ss.Rows[i][1].ToString();

                    if (ss.Rows[i][2].ToString().Trim() != "")
                    {
                        td.From.Text = Convert.ToDateTime(ss.Rows[i][2].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.From.Text = "";
                    }

                    if (ss.Rows[i][3].ToString().Trim() != "")
                    {
                        td.To.Text = Convert.ToDateTime(ss.Rows[i][3].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.To.Text = "";
                    }

                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Task_Details.Text = ss.Rows[i][5].ToString();
                    td.Task_Flage.Text = ss.Rows[i][6].ToString();
                    td.To_Developer.Text = ss.Rows[i][7].ToString();
                    td.Started_Date.Text = ss.Rows[i][8].ToString();
                    td.Ended_Date.Text = ss.Rows[i][9].ToString();
                    td.Duration_Taked.Text = ss.Rows[i][10].ToString();
                    td.To_Tester.Text = ss.Rows[i][11].ToString();
                    td.Created_By.Text = ss.Rows[i][12].ToString();


                    td.DEVELOPER_NOTE.Text = ss.Rows[i][16].ToString();
                    td.TESTER_NOTE.Text = ss.Rows[i][17].ToString();
                    td.Margin = new Thickness(5);
                    td.HorizontalAlignment = HorizontalAlignment.Center;
                    sp_end.Children.Add(td);
                    //   sp_tasksS.Children.Add(td);


                }
            }
            catch { }
        }

        private void InitializeDataTester()
        {
            try
            {

                lv_T_task.Items.Clear();
                DataTable ss = db.RunReader(@"SELECT CODE ,TITLE,FROM_DATE,TO_DATE,DURATION,DETAILS,FLAGE,TO_DEVELOPER,STARTED_DATE,ENDED_DATE,DURATION_TAKED,TO_TESTER,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE,DEVELOPER_NOTE,TESTER_NOTE FROM TASK_DETAILS where flage = 'T'  order by code DESC").Result;

                for (int i = 0; i < ss.Rows.Count; i++)
                {
                    TaskDemo td = new TaskDemo();
                    td.Code.Text = ss.Rows[i][0].ToString();
                    td.Title.Text = ss.Rows[i][1].ToString();

                    if (ss.Rows[i][2].ToString().Trim() != "")
                    {
                        td.From.Text = Convert.ToDateTime(ss.Rows[i][2].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.From.Text = "";
                    }

                    if (ss.Rows[i][3].ToString().Trim() != "")
                    {
                        td.To.Text = Convert.ToDateTime(ss.Rows[i][3].ToString()).ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.To.Text = "";
                    }

                    td.Duration.Text = ss.Rows[i][4].ToString();
                    td.Task_Details.Text = ss.Rows[i][5].ToString();
                    td.Task_Flage.Text = ss.Rows[i][6].ToString();
                    td.To_Developer.Text = ss.Rows[i][7].ToString();


                    if (ss.Rows[i][8].ToString().Trim() == "")
                    {
                        db.RunNonQuery("UPDATE TASK_DETAILS SET STARTED_DATE = sysdate WHERE CODE = '" + ss.Rows[i][0].ToString() + "'");
                        td.Started_Date.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        td.Started_Date.Text = ss.Rows[i][8].ToString();
                    }

                    td.Ended_Date.Text = ss.Rows[i][9].ToString();
                    td.Duration_Taked.Text = ss.Rows[i][10].ToString();
                    td.To_Tester.Text = ss.Rows[i][11].ToString();
                    td.Created_By.Text = ss.Rows[i][12].ToString();
                    td.DEVELOPER_NOTE.Text = ss.Rows[i][16].ToString();
                    td.TESTER_NOTE.Text = ss.Rows[i][17].ToString();


                    td.DEVELOPER_NOTE.Text = ss.Rows[i][16].ToString();
                    td.TESTER_NOTE.Text = ss.Rows[i][17].ToString();
                    td.Margin = new Thickness(5);
                    td.HorizontalAlignment = HorizontalAlignment.Center;
                    lv_T_task.Items.Add(td);




                }
            }
            catch { }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                if (txt_title.Text.Trim() == "")
                {
                    MessageBox.Show("برجاء ادخال العنوان");
                    return;
                }

                //if(cbx_to.Text.Trim() == "") {
                //    MessageBox.Show("برجاء تحديد شخص ");
                //    return;
                //}

                if (txt_details.Text.Trim() == "")
                {
                    MessageBox.Show("برجاء ادخال التفاصيل ");
                    return;
                }

                if (!short.TryParse(lbl_code.Content.ToString(), out short s) || db.RunReader("select code from TASK_DETAILS where code='" + lbl_code.Content + "'").Result.Rows.Count <= 0)
                {
                    lbl_code.Content = db.RunReader("select nvl( max(CODE)+1,1) from TASK_DETAILS ").Result.Rows[0][0].ToString();

                    db.RunNonQuery(@"INSERT INTO TASK_DETAILS (CODE, TITLE,  DURATION, DETAILS, FLAGE, TO_DEVELOPER, CREATED_BY, CREATED_DATE) VALUES('" + lbl_code.Content.ToString() + "', '" + txt_title.Text.Trim() + "', '" + txt_Doration.Text.Trim() + "', '" + txt_details.Text.Trim() + "', 'S', '" + cbx_to.Text.Trim() + "', '" + User
                        .Name + "', sysdate)", "تم الحفظ");



                }
                else
                {

                    db.RunNonQuery(@"UPDATE TASK_DETAILS SET TITLE =  '" + txt_title.Text.Trim() + "', DURATION = '" + txt_Doration.Text.Trim() + "', DETAILS = '" + txt_details.Text.Trim() + "', FLAGE = 'S', TO_DEVELOPER = '" + cbx_to.Text + "', UPDATED_BY = '" + User.Name + "', UPDATED_DATE = sysdate WHERE code='" + lbl_code.Content + "'", "تم التعديل");


                }



                if (dp_from.Text.Trim() != "")
                {
                    db.RunNonQuery("UPDATE TASK_DETAILS SET FROM_DATE = '" + Convert.ToDateTime(dp_from.Text.Trim()).ToString("dd-MMM-yy") + "' WHERE CODE = '" + lbl_code.Content.ToString() + "'");
                }

                if (dp_to.Text.Trim() != "")
                {
                    db.RunNonQuery("UPDATE TASK_DETAILS SET To_date = '" + Convert.ToDateTime(dp_to.Text.Trim()).ToString("dd-MMM-yy") + "' WHERE CODE = '" + lbl_code.Content.ToString() + "'");
                }

                refNewTask();
            }
            catch { }

        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txt_title.Text.Trim() == "")
                {
                    MessageBox.Show("برجاء ادخال العنوان");
                    return;
                }

                if (cbx_to.Text.Trim() == "")
                {
                    MessageBox.Show("برجاء تحديد شخص ");
                    return;
                }

                if (txt_details.Text.Trim() == "")
                {
                    MessageBox.Show("برجاء ادخال التفاصيل ");
                    return;
                }

                if (!short.TryParse(lbl_code.Content.ToString(), out short s) || db.RunReader("select code from TASK_DETAILS where code='" + lbl_code.Content + "'").Result.Rows.Count <= 0)
                {
                    lbl_code.Content = db.RunReader("select nvl( max(CODE)+1,1) from TASK_DETAILS ").Result.Rows[0][0].ToString();

                    db.RunNonQuery(@"INSERT INTO TASK_DETAILS (CODE, TITLE,  DURATION, DETAILS, FLAGE, TO_DEVELOPER, CREATED_BY, CREATED_DATE) VALUES('" + lbl_code.Content.ToString() + "', '" + txt_title.Text.Trim() + "', '" + txt_Doration.Text.Trim() + "', '" + txt_details.Text.Trim() + "', 'D', '" + cbx_to.Text.Trim() + "', '" + User
                        .Name + "', sysdate)", "تم الحفظ");



                }
                else
                {

                    db.RunNonQuery(@"UPDATE TASK_DETAILS SET TITLE =  '" + txt_title.Text.Trim() + "', DURATION = '" + txt_Doration.Text.Trim() + "', DETAILS = '" + txt_details.Text.Trim() + "', FLAGE = 'D', TO_DEVELOPER = '" + cbx_to.Text + "', UPDATED_BY = '" + User.Name + "', UPDATED_DATE = sysdate WHERE code='" + lbl_code.Content + "'", "تم التعديل");



                }

                db.RunNonQuery(@"INSERT INTO TASK_STATE (CODE, TASK_CODE, STATE_NAME,  FROM_STATE, TO_STATE, CREATED_BY, CREATED_DATE) VALUES ((select nvl( max(CODE)+1,1) from TASK_STATE ), '" + lbl_code.Content + "', 'Creat And send', 'Task', 'Developer', '" + User.Name + "', sysdate)");

                if (dp_from.Text.Trim() != "")
                {
                    db.RunNonQuery("UPDATE TASK_DETAILS SET FROM_DATE = '" + Convert.ToDateTime(dp_from.Text.Trim()).ToString("dd-MMM-yy") + "' WHERE CODE = '" + lbl_code.Content.ToString() + "'");
                }

                if (dp_to.Text.Trim() != "")
                {
                    db.RunNonQuery("UPDATE TASK_DETAILS SET To_date = '" + Convert.ToDateTime(dp_to.Text.Trim()).ToString("dd-MMM-yy") + "' WHERE CODE = '" + lbl_code.Content.ToString() + "'");
                }

                refNewTask();

                lbl_code.Content = "******";
                txt_title.Text = "";
                dp_from.Text = "";
                dp_to.Text = "";
                cbx_to.Text = "";
                txt_Doration.Text = "";
                txt_details.Text = "";
                txt_search.Text = "";
                dg_task.ItemsSource = null;
            }
            catch { }
        }

        private void sp_tasksS_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {



            try
            {
                TaskDemo td = (TaskDemo)sp_tasksS.SelectedItem;
                lbl_code.Content = td.Code.Text;
                txt_title.Text = td.Title.Text;
                dp_from.Text = td.From.Text;
                dp_to.Text = td.To.Text;
                cbx_to.Text = td.To_Developer.Text;
                txt_Doration.Text = td.Duration.Text;
                txt_details.Text = td.Task_Details.Text;
            }
            catch { }
        }

        private void Txt_search_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                dg_task.ItemsSource = db.RunReader(@"SELECT CODE ,TITLE,FROM_DATE,TO_DATE,DURATION,DETAILS,FLAGE,TO_DEVELOPER,STARTED_DATE,ENDED_DATE,DURATION_TAKED,TO_TESTER,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE,DEVELOPER_NOTE,TESTER_NOTE FROM TASK_DETAILS where flage = 'S' and CODE like '%" + txt_search.Text.Trim() + "%'  order by code DESC").Result.DefaultView;



            }
            catch { }
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (db.RunReader("select code from TASK_DETAILS where code='" + lbl_code.Content + "'").Result.Rows.Count <= 0)
                {
                    MessageBox.Show("برجاء اختيار الذى تريد حذفة");
                }
                else if (MessageBox.Show("هل تريد الحذف ؟", "حذف", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    db.RunNonQuery("UPDATE TASK_DETAILS SET FLAGE = 'N' WHERE CODE = '" + lbl_code.Content.ToString() + "'", "تم الحذف بنجاح");
                }
                else
                {
                    return;
                }

                refNewTask();
                lbl_code.Content = "******";
                txt_title.Text = "";
                dp_from.Text = "";
                dp_to.Text = "";
                cbx_to.Text = "";
                txt_Doration.Text = "";
                txt_details.Text = "";
                txt_search.Text = "";
                dg_task.ItemsSource = null;
            }
            catch { }
        }

        private void Dg_task_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dg_task.SelectedItem;
                lbl_code.Content = row[0].ToString();
                txt_title.Text = row[1].ToString();
                dp_from.Text = row[2].ToString();
                dp_to.Text = row[3].ToString();
                txt_Doration.Text = row[4].ToString();
                txt_details.Text = row[5].ToString();
                cbx_to.Text = row[7].ToString();
                //  txt_title.Text = row[0].ToString();
            }
            catch { }
        }

        private void Lv_D_task_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {




            try
            {
                TaskDemo td = (TaskDemo)lv_D_task.SelectedItem;
                code.Content = td.Code.Text;
                Title.Content = td.Title.Text;
                By_Tester.Content = td.To_Tester.Text;
                Created_By.Content = td.Created_By.Text;
                Developer_Note.Text = td.DEVELOPER_NOTE.Text;
                if (td.TESTER_NOTE.Text.Trim() == "")
                {
                    Tester_Note.Visibility = Visibility.Collapsed;
                    sas.Visibility = Visibility.Collapsed;
                    dvvv.Visibility = Visibility.Collapsed;
                    eaa.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Tester_Note.Visibility = Visibility.Visible;
                    sas.Visibility = Visibility.Visible;
                    dvvv.Visibility = Visibility.Visible;
                    eaa.Visibility = Visibility.Visible;
                    Tester_Note.Text = td.TESTER_NOTE.Text;


                }

                if (td.Duration.Text.Trim() != "")
                {
                    Duration.Content = td.Duration.Text;
                }
                else if (td.From.Text.Trim() != "" && td.To.Text.Trim() != "")
                {
                    Duration.Content = ((Convert.ToDateTime(td.To.Text.Trim()) - Convert.ToDateTime(td.From.Text.Trim())).TotalDays).ToString();

                    db.RunNonQuery("UPDATE TASK_DETAILS SET DURATION = '" + Duration.Content.ToString() + "' WHERE CODE = '" + td.Code.Text + "'");
                }
                else
                {
                    Duration.Content = "غير محدد";
                }

                if (td.From.Text.Trim() != "")
                {
                    From_Date.Content = td.From.Text;
                }
                else
                {
                    From_Date.Content = DateTime.Now.ToString("dd-MM-yyyy");
                    db.RunNonQuery("UPDATE TASK_DETAILS SET FROM_DATE = sysdate WHERE CODE = '" + td.Code.Text + "'");
                }



                if (td.To.Text.Trim() != "")
                {
                    To_Date.Content = td.To.Text;
                }
                else if (td.Duration.Text.Trim() != "")
                {
                    To_Date.Content = DateTime.Now.AddDays(Convert.ToDouble(td.Duration.Text.Trim()) - 1).ToString("dd-MM-yyyy");

                    db.RunNonQuery("UPDATE TASK_DETAILS SET TO_DATE = (SELECT from_date+duration-1 FROM TASK_DETAILS WHERE CODE='" + td.Code.Text + "') WHERE CODE = '" + td.Code.Text + "'");
                }
                else if (Duration.Content.ToString() == "غير محدد")
                {
                    To_Date.Content = "غير محدد";
                }
                else
                {
                    To_Date.Content = "ERROR 404 !";
                }

                Details.Text = td.Task_Details.Text;

                if (To_Date.Content.ToString() == "غير محدد")
                {
                    CountDown.Content = "غير محدد";
                }
                else
                {
                    CountDown.Content = Math.Ceiling((DateTime.ParseExact(To_Date.Content.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.Now).TotalDays + 1).ToString();
                }
            }
            catch { }
        }

        private void btn_refresh1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                code.Content = "*****";
                From_Date.Content = "*****";
                To_Date.Content = "*****";
                Title.Content = "*****";
                Created_By.Content = "*****";
                Duration.Content = "*****";
                CountDown.Content = "*****";
                Details.Text = "";
                Developer_Note.Text = "";
                By_Tester.Content = "*****";
                InitializeDataDeveloper();
            }
            catch { }
        }

        private void btn_save1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Developer_Note.Text.Trim() != "")
                {
                    db.RunNonQuery("UPDATE TASK_DETAILS SET DEVELOPER_NOTE = '" + Developer_Note.Text + "' WHERE CODE = '" + code.Content.ToString() + "'");
                    db.RunNonQuery("INSERT INTO TASK_STATE (CODE, TASK_CODE, STATE_NAME, DEVELOPER_COMMENT, FROM_STATE, TO_STATE, CREATED_BY, CREATED_DATE) VALUES ((select nvl( max(CODE)+1,1) from TASK_STATE ), '" + code.Content.ToString() + "', 'Developer Save', '" + Developer_Note.Text.Trim() + "', 'Developer', 'Developer', '" + User.Name + "',sysdate)", "تم الحفظ بنجاح");

                    InitializeDataDeveloper();




                }
                else
                {
                    MessageBox.Show("برجاء ادخال ملاحظات");
                }
            }
            catch { }
        }

        private void btn_send1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Developer_Note.Text.Trim() != "")
                {
                    db.RunNonQuery("UPDATE TASK_DETAILS SET DEVELOPER_NOTE = '" + Developer_Note.Text + "', FLAGE = 'T' WHERE CODE = '" + code.Content.ToString() + "'");


                    db.RunNonQuery("INSERT INTO TASK_STATE (CODE, TASK_CODE, STATE_NAME, DEVELOPER_COMMENT, FROM_STATE, TO_STATE, CREATED_BY, CREATED_DATE) VALUES ((select nvl( max(CODE)+1,1) from TASK_STATE ), '" + code.Content.ToString() + "', 'Developer Done', '" + Developer_Note.Text.Trim() + "', 'Developer', 'Tester', '" + User.Name + "',sysdate)");
                    if (To_Date.Content.ToString() != "غير محدد" && Convert.ToInt32(CountDown.Content.ToString()) > 0)
                    {
                        MessageBox.Show("Great");
                    }
                    else
                    {
                        MessageBox.Show("Good Luck");
                    }

                    InitializeDataDeveloper();

                    code.Content = "*****";
                    From_Date.Content = "*****";
                    To_Date.Content = "*****";
                    Title.Content = "*****";
                    Created_By.Content = "*****";
                    Duration.Content = "*****";
                    CountDown.Content = "*****";
                    Details.Text = "";
                    Developer_Note.Text = "";
                    InitializeDataDeveloper();


                }
            }
            catch { }
        }

        private void Lv_T_task_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                TaskDemo td = (TaskDemo)lv_T_task.SelectedItem;
                code1.Content = td.Code.Text;
                Title1.Content = td.Title.Text;
                Created_By1.Content = td.Created_By.Text;
                Developer_Note1.Text = td.DEVELOPER_NOTE.Text;
                By_developer1.Content = td.To_Developer.Text;
                Tester_Note1.Text = td.TESTER_NOTE.Text;
                if (td.Duration.Text.Trim() != "")
                {
                    Duration1.Content = td.Duration.Text;
                }
                else if (td.From.Text.Trim() != "" && td.To.Text.Trim() != "")
                {
                    Duration1.Content = ((Convert.ToDateTime(td.To.Text.Trim()) - Convert.ToDateTime(td.From.Text.Trim())).TotalDays).ToString();

                    db.RunNonQuery("UPDATE TASK_DETAILS SET DURATION = '" + Duration1.Content.ToString() + "' WHERE CODE = '" + td.Code.Text + "'");
                }
                else
                {
                    Duration1.Content = "غير محدد";
                }

                if (td.From.Text.Trim() != "")
                {
                    From_Date1.Content = td.From.Text;
                }
                else
                {
                    From_Date1.Content = DateTime.Now.ToString("dd-MM-yyyy");
                    db.RunNonQuery("UPDATE TASK_DETAILS SET FROM_DATE = sysdate WHERE CODE = '" + td.Code.Text + "'");
                }



                if (td.To.Text.Trim() != "")
                {
                    To_Date1.Content = td.To.Text;
                }
                else if (td.Duration.Text.Trim() != "")
                {
                    To_Date1.Content = DateTime.Now.AddDays(Convert.ToDouble(td.Duration.Text.Trim()) - 1).ToString("dd-MM-yyyy");

                    db.RunNonQuery("UPDATE TASK_DETAILS SET TO_DATE = (SELECT from_date+duration-1 FROM TASK_DETAILS WHERE CODE='" + td.Code.Text + "') WHERE CODE = '" + td.Code.Text + "'");
                }
                else if (Duration1.Content.ToString() == "غير محدد")
                {
                    To_Date1.Content = "غير محدد";
                }
                else
                {
                    To_Date1.Content = "ERROR 404 !";
                }

                Details1.Text = td.Task_Details.Text;

                if (To_Date1.Content.ToString() == "غير محدد")
                {
                    CountDown1.Content = "غير محدد";
                }
                else
                {
                    CountDown1.Content = Math.Ceiling((DateTime.ParseExact(To_Date1.Content.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.Now).TotalDays + 1).ToString();
                }
            }
            catch { }
        }

        private void refreshTester()
        {
            try
            {
                code1.Content = "*****";
                From_Date1.Content = "*****";
                To_Date1.Content = "*****";
                Title1.Content = "*****";
                Created_By1.Content = "*****";
                By_developer1.Content = "*****";
                Duration1.Content = "*****";
                CountDown1.Content = "*****";
                Details1.Text = "";
                Developer_Note1.Text = "";
                Tester_Note1.Text = "";
                InitializeDataTester();
            }
            catch { }
        }
        private void Btn_refresh2_Click(object sender, RoutedEventArgs e)
        {
            refreshTester();
        }

        private void Btn_save2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Tester_Note1.Text.Trim() != "")
                {
                    db.RunNonQuery("UPDATE TASK_DETAILS SET TESTER_NOTE = '" + Tester_Note1.Text + "' ,TO_TESTER='" + User.Name + "' WHERE CODE = '" + code1.Content.ToString() + "'");
                    db.RunNonQuery("INSERT INTO TASK_STATE (CODE, TASK_CODE, STATE_NAME, TESTER_COMMENT, FROM_STATE, TO_STATE, CREATED_BY, CREATED_DATE) VALUES" + " ((select nvl( max(CODE)+1,1) from TASK_STATE ), '" + code1.Content.ToString() + "', 'Tester Save', '" + Tester_Note1.Text.Trim() + "', 'Tester', 'Tester', '" + User.Name + "',sysdate)", "تم الحفظ بنجاح");

                    InitializeDataTester();

                }
                else
                {
                    MessageBox.Show("برجاء ادخال ملاحظات");
                }
            }
            catch { }
        }

        private void Btn_refresh2_Copy1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Tester_Note1.Text.Trim() != "")
                {
                    db.RunNonQuery("UPDATE TASK_DETAILS SET TESTER_NOTE = '" + Tester_Note1.Text + "' ,TO_TESTER='" + User.Name + "', FLAGE = 'Y',ENDED_DATE=sysdate,DURATION_TAKED=ceil(sysdate- STARTED_DATE) WHERE CODE = '" + code1.Content.ToString() + "'");
                    db.RunNonQuery("INSERT INTO TASK_STATE (CODE, TASK_CODE, STATE_NAME, TESTER_COMMENT, FROM_STATE, TO_STATE, CREATED_BY, CREATED_DATE) VALUES" + " ((select nvl( max(CODE)+1,1) from TASK_STATE ), '" + code1.Content.ToString() + "', 'Tester Done', '" + Tester_Note1.Text.Trim() + "', 'Tester', 'Done', '" + User.Name + "',sysdate)", "تم الحفظ بنجاح");
                    refreshTester();

                }
                else
                {
                    MessageBox.Show("برجاء ادخال ملاحظات");
                }
            }
            catch { }
        }

        private void Btn_refresh2_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Tester_Note1.Text.Trim() != "")
                {
                    db.RunNonQuery("UPDATE TASK_DETAILS SET TESTER_NOTE = '" + Tester_Note1.Text + "' ,TO_TESTER='" + User.Name + "', FLAGE = 'D' WHERE CODE = '" + code1.Content.ToString() + "'");
                    db.RunNonQuery("INSERT INTO TASK_STATE (CODE, TASK_CODE, STATE_NAME, TESTER_COMMENT, FROM_STATE, TO_STATE, CREATED_BY, CREATED_DATE) VALUES" + " ((select nvl( max(CODE)+1,1) from TASK_STATE ), '" + code1.Content.ToString() + "', 'Test Error', '" + Tester_Note1.Text.Trim() + "', 'Tester', 'Developer', '" + User.Name + "',sysdate)", "تم الحفظ بنجاح");
                    refreshTester();

                }
                else
                {
                    MessageBox.Show("برجاء ادخال ملاحظات");
                }
            }
            catch { }
        }
    }

}
