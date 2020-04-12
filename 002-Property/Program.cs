using System;

namespace CSharp_001
{
    public class DrinkMachine {

        // Property of private field
        private string _location;
        public string Location {
            get {
                return _location;
            }
            set {
                if (value != null)
                    _location = value;
            }
        }

        // Property of public field
        public string Make {get; set;}
        public string Model {get; set;}

        public void MakeCappucino () {
            Console.WriteLine ("Making cappucino");
        }

        public void MakeEspresso () {
            Console.WriteLine ("Making espresso");
        }

    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            DrinkMachine dMachine = new DrinkMachine();
            dMachine.MakeCappucino();
            dMachine.MakeEspresso();

            dMachine.Make = "ok";
            dMachine.Location = "there";
                       

        }
    }

}
