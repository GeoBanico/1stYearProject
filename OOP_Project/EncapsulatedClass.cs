using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    class EncapsulatedClass
    {
        //*all classes here
    }

    public class BasicInfo
    {
        public string Name { get; set; }
        public string Amnt { get; set; }
        public string Percentage { get; set; }
        public string Loan_Date { get; set; }
        public int AccountID { get; set; }
    }

    public class PersonInfo : BasicInfo
    {
        public string Age { get; set; }
        public string Bday { get; set; }
        public string address { get; set; }
        public string Cno { get; set; }
        public string CnoPer { get; set; }
        public string PId { get; set; }
    }

    public class PastInfo : BasicInfo
    {
        public string DatePaid { get; set; }
        public string PaidAmnt { get; set; }
        public string IntAmnt { get; set; }
        public string ReceiptNo { get; set; }
    }
}
