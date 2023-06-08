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
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        private string brwName, brwDate, pass;
        private decimal brwAmnt, brwPercent;
        private int brwAccID;

        List<PersonInfo> PInfo = new List<PersonInfo>();
        List<PastInfo> PastTrans = new List<PastInfo>();

        public Payment(List<PersonInfo> PStore, List<PersonInfo> PInfo, List<PastInfo> PastTrans, string pass)
        {
            InitializeComponent();
            foreach (PersonInfo x in PStore) 
            {
                brwName = x.Name;
                brwAmnt = Convert.ToDecimal(x.Amnt);
                brwDate = x.Loan_Date;
                brwPercent = Convert.ToDecimal(x.Percentage);
                brwAccID = x.AccountID;
            }
            
            this.PInfo = PInfo;
            this.PastTrans = PastTrans;
            this.pass = pass;
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            decimal TotalBill = Convert.ToDecimal(lblTotal.Content);
            decimal PaidAmount = Convert.ToDecimal(txtPaidAmount.Text);
            string[] ldate = brwDate.Split('/');

            if (TotalBill >= PaidAmount && PaidAmount > 0)
            {
                brwAmnt = TotalBill - PaidAmount;
                MessageBox.Show(String.Format("Paid Amount: {0} \nRemaining Balance: {1}", txtPaidAmount.Text, brwAmnt));

                    int r = 0;
                    foreach (PersonInfo x in this.PInfo)
                    {
                        if (x.AccountID == brwAccID)
                        {
                            PastTrans.Add(new PastInfo { Name = x.Name, Amnt = x.Amnt, Loan_Date = x.Loan_Date, Percentage = x.Percentage, DatePaid = DateTime.Now.ToString("dd/MM/yyyy"), PaidAmnt = PaidAmount.ToString(), IntAmnt = TotalBill.ToString(), ReceiptNo = lblReceipt.Content.ToString(), AccountID = brwAccID});
                            x.Amnt = Convert.ToString(brwAmnt);
                            x.Loan_Date = Convert.ToString(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                        }
                        r += 1;
                    }
                MainForm y = new MainForm(this.PInfo , this.PastTrans, this.pass);
                y.Show();
                this.Close();
            }

            else { MessageBox.Show("The customer can't pay beyond the interested amount or nothing ", "Warning!", MessageBoxButton.OK, MessageBoxImage.Stop); }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            lblName.Content = brwName;
            lblamnt.Content = brwAmnt;
            lblLoanDate.Content = brwDate;
            lblpercent.Content = brwPercent;

            decimal TotalBill = interestTotal();
            lblTotal.Content = TotalBill;

            Random x = new Random();
            lblReceipt.Content = x.Next(100000, 999999);

            lblDateToday.Content = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainForm y = new MainForm(this.PInfo, this.PastTrans, this.pass);
            y.Show();
            this.Close();
        }

        public decimal interestTotal()
        {
            decimal interestMonth = 0;
            decimal interestDay = 0;
            decimal addAmount;

            string[] ldate = brwDate.Split('/');
            int[] date = new int[3];
            for (int x = 0; x <= (ldate.Length - 1); x++)
            {
                date[x] = Convert.ToInt32(ldate[x]);
            }

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            Console.WriteLine(month + " " + date[1]);

            //year
            interestMonth = (year - date[2]) * 12;
            //month
            if (month >= date[1])
            {
                interestMonth += month - date[1];
            }
            else if(month <= date[1])
            {
                interestMonth += date[1] - month;
            }
            //day
            if (day < date[0])
            {
                month -= 1;
                interestMonth -= 1;
                interestDay = (date[0] - day);
            }
            else if (day > date[0])
            {
                month -= 1;
                interestMonth -= 1;
                interestDay = (day - date[0]);
            }


            addAmount = Math.Round(((brwAmnt * (brwPercent/100)) * (((interestMonth*30) + interestDay)/30)),3);
            return (addAmount + brwAmnt);
        }

        private void txtPaidAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9.]+").IsMatch(e.Text);
        }
    }
}
