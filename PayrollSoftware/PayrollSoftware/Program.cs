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
        //hWorked is the backing field for HoursWorked
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

        //Constructor for the staff class
        public Staff (string name, float rate)
        {
            NameOfStaff = name;
            hourlyRate = rate;
        }

        //CalculatePay Method
        public virtual void CalculatePay()
        {
            Console.WriteLine("Calculating Pay...");
            BasicPay = hWorked * hourlyRate;
            TotalPay = BasicPay;
        }

        //Message displayed on the payroll receipt
        public override string ToString()
        {
            return "\nNameOfStaff = " + NameOfStaff + "\nhourlyRate = " + hourlyRate + "\nhWorked = " + hWorked + "\nBasicPay = " + BasicPay + "\nTotalPay = " + TotalPay;
        }
    }

    //Child of the staff class
    class Manager : Staff
    {
        //Manager's hourly rate
        private const float managerHourlyRate = 50f;

        //Allowance the manager gets
        public int Allowance { get; private set; }

        //Constructor for the manager class
        public Manager(string name): base(name,managerHourlyRate)
        {

        }

        //Same as staff, except it takes the managers allowance into account
        public override void CalculatePay()
        {
            base.CalculatePay();
            Allowance = 1000;
            if (HoursWorked > 160)
            {
                TotalPay = BasicPay + Allowance;
            }
        }

        //Message displayed on the payroll receipt
        public override string ToString()
        {
            return "\nNameOfStaff = " + NameOfStaff + "\nmanagerHourlyRate = " + managerHourlyRate + "\nHoursWorked = " + HoursWorked + "\nBasicPay = " + BasicPay + "\nAllowance = " + Allowance + "\nTotalPay = " + TotalPay;
        }

        

    }

    class Admin : Staff
    {
        //Rate for overtime worked
        private const float overtimeRate = 15.5f;
        //Admin's hourly rate
        private const float adminHourlyrate = 30f;

        //Auto-implemented property for Overtime
        public float Overtime { get; private set; }

        //Constructor for the Admin class
        public Admin(string name) : base(name, adminHourlyrate)
        {

        }

        //Same as Staff and Manager, except it takes overtime into account
        public override void CalculatePay()
        {
            base.CalculatePay();
            if (HoursWorked > 160)
            {
                Overtime = overtimeRate * (HoursWorked - 160);
            }
        }

        //Message displayed on the payroll receipt
        public override string ToString()
        {
            return "\nNameOfStaff = " + NameOfStaff + "\nadminHourlyRate = " + adminHourlyrate + "\nHoursWorked = " + HoursWorked + "\nBasicPay = " + BasicPay + "\nOvertime = " + Overtime + "\nTotalPay = " + TotalPay;

        }
    }

    class FileReader
    {
        public List<Staff> ReadFile()
        {
            List<Staff> myStaff = new List<Staff>;
            string[] result = new string[2];
            string path = "/Users/21Millisorz/Documents/GitHub/Payroll-Software/staff.txt";
            string seperator = ", ";

            if (File.Exists(path))
            {
                using(StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        result = sr.ReadLine().Split(seperator, StringSplitOptions.RemoveEmptyEntries);

                        if(result[1] == "Manager")
                        {
                            myStaff.Add(new Manager(result[0]));
                        }
                        else if(result[1] == "Admin")
                        {
                            myStaff.Add(new Admin(result[0]));
                        }
                        sr.Close();
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: File does not exist");
            }
            return myStaff;
        }
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
