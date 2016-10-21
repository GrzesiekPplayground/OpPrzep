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
        private bool _restart;

        public object Start ()
        {
            _isRunning = true;
            _restart = true;           

            InputData data = InputData.INSTANCE;
            OutputData outData = OutputData.INSTANCE;

            while (_isRunning == true)
            {
                if (_restart == true)
                {
                    StartAsk(data);
                }

                data.ConvertToSi();
                data.CalculateVSr();
                data.CalculateRe();
                data.CalculateLambda();
                data.CalculatedP();

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
                case "calculated values":
                    instance.PrintValues(instance.calculatedValues);
                    break;
                case "exit":
                    _isRunning = false;
                    break;
                case "formulas":
                    instance.Print(instance.formulas);
                    break;
                case "input values":
                    instance.PrintValues(instance.inputValues);
                    break;
                case "result":
                    outInstance.PrintResult();
                    break;
                case "restart":
                    _restart = true;
                    break;
                default:
                    Console.WriteLine("Wrong command. Try again.");
                    break;
            }
        }
        private void StartAsk(InputData instance)
        {
            var _running = true;

            while(_running == true)
            {
                Console.WriteLine("Insert data: ");

                instance.q = InsertValue("q [kg/d]: ");
                instance.rho = InsertValue("rho [g/cm^3]: ");
                instance.mi = InsertValue("mi [cP]: ");
                instance.D = InsertValue("D - srednica zewn. rur ['']: ");
                instance.delta = InsertValue("delta - gr. scianki [mm]: ");
                instance.h = InsertValue("H [m]: ");
                instance.k = InsertValue("współczynnik k [m]: ");

                _running = false;
                _restart = false;
            }
            
            

        }

        private double InsertValue(string message)
        {
            var _repeat = true;
            var _value = 0.0;

            while (_repeat == true)
            {
                Console.Write(message);

                var _input = Console.ReadLine();

                try
                {
                    _value = Convert.ToDouble(_input);
                    _repeat = false;
                }
                catch(FormatException) {
                    Console.WriteLine("Unable to convert '{0}' to a Double.", _input);
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'{0}' is outside the range of a Double.", _input);
                }
            }

            return _value;
        }
    }
}
