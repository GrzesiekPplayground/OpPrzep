using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OporyPrzeplywu
{
    class Running
    {
        private static Running _instance = null;

        private Running() { }

        public static Running INSTANCE
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Running();
                }
                return _instance;
            }
        }

        private bool _isRunning;

        public bool isRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
            }
        }

        public object Start ()
        {
            isRunning = true;

            InputData data = InputData.INSTANCE;
            OutputData outData = OutputData.INSTANCE;

            while (isRunning == true)
            {

                //sample data
                data.q1 = 100000000000000000;
                data.rho = 0.845;
                data.mi = 2;
                data.dW = 47400000;
                data.h = 2600;
                data.pK = 285;


                // fill data
                data.ConvertToSi();
                data.CalculateVSr("q1");
                data.CalculateRe();
                data.CalculateLambda();

                //return result
                
                outData.CalculatedHmeters(data.arrayForResult);
                outData.gamma = data.rho * data.g;

                AskUser(data, outData);
            }

            return outData;      
        }
        private void AskUser(InputData instance, OutputData outInstance)
        {
            Console.Write("What next? ");
            string next = Console.ReadLine();
            switch (next)
            {
                case "exit":
                    isRunning = false;
                    break;
                case "input values":
                    instance.PrintInputValues();
                    break;
                case "calculated values":
                    instance.PrintCalculatedValues();
                    break;
                case "result":
                    outInstance.PrintResult();
                    break;
                default:
                    Console.WriteLine("Wrong command. Try again.");
                    break;
            }
        }

        private void ShowInputValues()
        {
        }
    }
}
