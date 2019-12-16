using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSoftware
{
    class Staff
    {
        //Amount staff are paid per hour
        private float hourlyRate;
        //Backing field for HoursWorked
        private int hWorked;

        /* Public variables including the total and basic pay for staff,
         * the name of staff members, and the hours worked.
         * These variables are essential to calculating pay,
         * and providing an accurate payroll
         */
        public float TotalPay { get; protected set; }
        public float BasicPay { get; private set; }
        public string NameOfStaff { get; private set; }
        public int HoursWorked
        {
            get
            {
                return hWorked
            }
            set
            {
                if(hWorked > 0)
                {
                    hWorked = value;
                }
                else
                {
                    hWorked = 0;
                }
            }
        }

        public Staff (string name, float rate)
        {
            NameOfStaff = name;
            hourlyRate = rate;
        }

        public virtual void CalculatePay()
        {
            Console.WriteLine("Calculating Pay...");
            BasicPay = hWorked * hourlyRate;
            TotalPay = BasicPay;
        }

        public override string ToString()
        {
            return "\nNameOfStaff = " + NameOfStaff + "\nhourlyRate = " + hourlyRate + "\nhWorked = " + hWorked + "\nBasicPay = " + BasicPay + "\nTotalPay = " + TotalPay;
        }
    }

    class Manager : Staff
    {
        private const float managerHourlyRate = 50f;

        private int Allowance { get; set; }

        public Manager(string name): base(name,managerHourlyRate)
        {

        }

        public override void CalculatePay()
        {
            Allowance = 1000;
            base.CalculatePay();
            if (HoursWorked > 160)
            {
                TotalPay = TotalPay + Allowance;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        

    }

    class Admin : Staff
    {
        private const float overtimeRate = 15.5f;
        private const float adminHourlyrate = 30f;

        public float Overtime { get; set; }

        public Admin(string name) : base(name, adminHourlyrate)
        {

        }

        public override void CalculatePay()
        {
            base.CalculatePay();
            if (HoursWorked > 160)
            {

            }
            Overtime = overtimeRate * (HoursWorked - 160);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    class FileReader
    {

    }

    class PaySlip
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
