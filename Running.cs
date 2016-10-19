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

        public void Start ()
        {


            InputData data = InputData.INSTANCE;

            //sample data
            data.q1 = 100;
            data.rho = 0.845;
            data.mi = 2;
            data.dW = 474;
            data.h = 2600;
            data.pK = 285;


            // fill data
            data.ConvertToSi();
            data.CalculateVSr("q1");
            data.CalculateRe();
            data.CalculateLambda();

            //return result
            OutputData outData = OutputData.INSTANCE;
            outData.CalculatedHmeters(data.arrayForResult);
            outData.gamma = data.rho * data.g;
            Console.WriteLine(outData.dHmeters);
            Console.WriteLine(outData.dH);

            var x = outData.dH;

            isRunning = false;
        }
    }
}
