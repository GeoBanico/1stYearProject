using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OOP_Project
{
    /// <summary>
    /// Interaction logic for CustomerInfo.xaml
    /// </summary>
    public partial class CustomerInfo : Window
    {
        List<PersonInfo> PInfo = new List<PersonInfo>();
        List<PastInfo> PastTrans = new List<PastInfo>();

        private int AccID;
        private bool enable = false;
        private string pass;

        private void CustomerEnable(List<PersonInfo> PInfo, List<PastInfo> PastTrans, string pass) 
        {
            InitializeComponent();
            this.PInfo = PInfo;
            this.PastTrans = PastTrans;
            this.pass = pass;

        }

        public CustomerInfo(List<PersonInfo> PInfo, List<PastInfo> PastTrans, string pass)
        {
            CustomerEnable(PInfo,PastTrans,pass);
            txtAmnt.IsEnabled = true;
            txtpercent.IsEnabled = true;
            dpLoanDate.IsEnabled = true;
            lblAccID.Content = accountID();
        }
       
        public CustomerInfo(List<PersonInfo> PStore, List<PersonInfo> PInfo, List<PastInfo> PastTrans, string pass)
        {
            CustomerEnable(PInfo, PastTrans, pass);
            foreach (PersonInfo x in PStore) 
            {
                txtName.Text = x.Name;
                dpBday.Text = x.Bday;
                txtAge.Text = x.Age;
                txtAddress.Text = x.address;
                txtCno.Text = x.Cno;
                txtCnoPer.Text = x.CnoPer;
                txtpID.Text = x.PId;
                txtAmnt.Text = x.Amnt;
                txtpercent.Text = x.Percentage;
                dpLoanDate.Text = x.Loan_Date;
                lblAccID.Content = AccID = x.AccountID;
            }
            enable = true;
            txtAmnt.IsEnabled = false;
            txtpercent.IsEnabled = false;
            dpLoanDate.IsEnabled = false;
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtAge.Text) || string.IsNullOrEmpty(dpBday.Text) || string.IsNullOrEmpty(txtCno.Text) || string.IsNullOrEmpty(txtCnoPer.Text) || string.IsNullOrEmpty(txtpercent.Text) || string.IsNullOrEmpty(txtAmnt.Text) || string.IsNullOrEmpty(txtpID.Text) || string.IsNullOrEmpty(dpLoanDate.Text))
            {
                MessageBox.Show("There are Empty Fields");
            }
            else 
            {
                if (Convert.ToDecimal(txtAmnt.Text) > 0)
                {
                    EditMode();
                    this.PInfo.Add(new PersonInfo { Name = txtName.Text, Bday = dpBday.Text, Age = txtAge.Text, address = txtAddress.Text, Cno = txtCno.Text, CnoPer = txtCnoPer.Text, PId = txtpID.Text, Amnt = txtAmnt.Text, Loan_Date = dpLoanDate.Text, Percentage = txtpercent.Text, AccountID = Convert.ToInt32(lblAccID.Content) });
                    MainForm y = new MainForm(this.PInfo, this.PastTrans, this.pass);
                    MessageBox.Show("Info Saved");
                    y.Show();
                    this.Close();
                }
                else { MessageBox.Show("Amount Error"); }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            datecalculate();
        }

        private void txtAmnt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9.]+").IsMatch(e.Text);
        }

        private void txtpercent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9].+").IsMatch(e.Text);
        }
        private void txtCno_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void dpBday_CalendarClosed(object sender, RoutedEventArgs e)
        {
            datecalculate();
        }

        private void EditMode()
        {
            int SelIndex = -1;
            if (enable == true)
            {
                int r = 0;
                foreach (PersonInfo b in this.PInfo)
                {
                    if (b.AccountID.ToString().Contains(AccID.ToString()))
                    {
                        SelIndex = r;
                    }
                    r++;
                }
                this.PInfo.RemoveAt(SelIndex);
            }
        }

        private void clearfields()
        {
            txtName.Clear();
            txtAge.Clear();
            dpBday.Text = null;
            txtAddress.Clear();
            txtCno.Clear();
            txtCnoPer.Clear();
            txtpercent.Clear();
            txtpID.Clear();
            txtAmnt.Clear();
            dpLoanDate.Text = null;
        }

        private void datecalculate()
        {
            string[] ldate = dpBday.Text.Split('/');
            int[] date = new int[3];
            for (int x = 0; x <= (ldate.Length - 1); x++)
            {
                date[x] = Convert.ToInt32(ldate[x]);
            }

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int age = 0;

            while (year > date[2])
            {
                year -= 1;
                age += 1;
            }
            if (year == date[2] && month == date[1] && day < date[0])
            {
                age -= 1;
            }
            if (year == date[2] && month < date[1] && day >= date[0])
            {
                age -= 1;
            }

            txtAge.Text = age.ToString();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearfields();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainForm x = new MainForm(PInfo,PastTrans, pass);
            x.Show();
            this.Close();
        }

        private int accountID()
        {
            Random x = new Random();
            int AccNo = x.Next(1000, 9999);

            foreach (PersonInfo y in PInfo)
            { 
                if (Convert.ToInt32(y.AccountID) == AccNo)
                {
                    AccNo = x.Next(1000, 9999);
                }
            }

            return AccNo;
        }
    }

}
