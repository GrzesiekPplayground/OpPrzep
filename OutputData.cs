using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OporyPrzeplywu
{
    class OutputData
    {
        private static OutputData _instance = null;

        private OutputData() {}

        public static OutputData INSTANCE
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OutputData();
                }
                return _instance;
            }
        }

        private double _dHmeters;
        private double _gamma;

        public double gamma
        {
            get
            {
                return _gamma;
            }
            set
            {
                _gamma = value;
            }
        }

        public double dH
        {
            get
            {
                return _dHmeters * _gamma; //LOGIC ERROR
            }
        }

        public double dHmeters
        {
            get
            {
                return _dHmeters;
            }
        }

        public void CalculatedHmeters(double[] inArray)
        {
            double lambda = inArray[0];
            double vSr = inArray[1];
            double h = inArray[2];
            double d = inArray[3];
            double g = inArray[4];

            _dHmeters = lambda * ((vSr * h) / 2 * d * g);
        }
    }
}
