using System;

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
        public float TotalPay { get; set; }
        public float BasicPay { get; set; }
        public string NameOfStaff { get; set; }
        public int HoursWorked;

        public Staff (string name, float rate)
        {
            name = NameOfStaff;
            rate = hourlyRate;
        }

        public virtual void CalculatePay()
        {
            TotalPay = BasicPay;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    class Manager : Staff
    {
        private const float managerHourlyRate = 50f;

        private int Allowance { get; set; }

        public Manager(string name): base(name,managerHourlyRate)
        {
            if(HoursWorked >= 160)
            {
                TotalPay = TotalPay + Allowance;
            }
        }

        public override void CalculatePay()
        {
            Allowance = 1000;
            base.CalculatePay();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        

    }

    class Admin : Staff
    {

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
