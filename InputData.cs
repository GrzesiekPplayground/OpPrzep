using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OporyPrzeplywu
{
    class InputData
    {
        private static InputData _instance = null;

        private InputData ()
        {
            _isSI = false;
        }

        public static InputData INSTANCE
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputData();
                }
                return _instance;
            }
        }

        private const double _pi = Math.PI;
        private const double _g = 9.80665;
        private double _vSr;
        private double _D; // '' > m
        private double _delta; // mm > m
        private double _h; // m 
        private double _k;
        private double _q; // kg/d > m3/s
        private double _rho; // g/cm3 > kg/m3
        private double _mi; // cP > Pa*S
        private double _pK; //
        private double _re; 
        private double _lambda;
        private double _dP;
        private double _x;

        private bool _isSI;

        public double pi
        {
            get
            {
                return _pi;
            }
        }

        public double g
        {
            get
            {
                return _g;
            }
        }

        public double dW
        {
            get
            {
                return _D - (2 * _delta);
            }
        }

        public double D
        {
            get
            {
                return _D;
            }
            set
            {
                _D = value;
            }
        }
        public double delta
        {
            get
            {
                return _delta;
            }
            set
            {
                _delta = value;
            }
        }

        public double h
        {
            get
            {
                return _h;
            }

            set
            {
                _h = value;
            }
        }

        public double k
        {
            get
            {
                return _k;
            }
            set
            {
                _k = value;
            }
        }

        public double mi
        {
            get
            {
                return _mi;
            }

            set
            {
                _mi = value;
            }
        }

        public double ni
        {
            get
            {
                return _mi / _rho;
            }
        }

        public double pK
        {
            get
            {
                return _pK;
            }

            set
            {
                _pK = value;
            }
        }

        public double q
        {
            get
            {
                return _q;
            }

            set
            {
                _q = value;
            }
        }

        public double rho
        {
            get
            {
                return _rho;
            }

            set
            {
                _rho = value;
            }
        }

        public double vSr
        {
            get
            {
                return _vSr;
            }
        }

        public double A
        {
            get
            {
                return pi * Math.Pow((dW / 2), 2);
            }
        }

        public double re
        {
            get
            {
                return _re;
            }
        }

        public double lambda
        {
            get
            {
                return _lambda;
            }
        }

        public double gamma
        {
            get
            {
                return _rho * _g;
            }
        }

        public double x
        {
            get
            {
                return _x;
            }
        }

        public double dP
        {
            get
            {
                return _dP;
            }
        }

        public bool isSI
        {
            get
            {
                return _isSI;
            }
        }

        public double[] arrayForResult
        {
            get
            {
                double[] array = new double[5] { _lambda, _vSr, _h, dW, _g};
                return array;
            }
        }

        private Dictionary<string, string> _formulas
        {
            get
            {
                var _formulas = new Dictionary<string, string>
                {
                    {"gamma", "_rho * _g" },
                    {"ni", "_mi / _rho" },
                    {"re", "(dW * vSr) / ni" },
                    {"Vśr", "_qv / A;" },
                    {"lambda re < 2320", "64/re" },
                    {"lambda re >= 2320 && re < 100000", "(0.3164 / (Math.Pow(re, 0.25)))" },
                    {"dP", "(_lambda * _rho * _vSr) / (2 * dW)" }
                };
                return _formulas;
            }
        }

        public Dictionary<string, string> formulas
        {
            get
            {
                return _formulas;
            }
        }

        private Dictionary<string, double> _inputValues
        {
            get
            {
                var inputValues = new Dictionary<string, double>
                {       
                    {"q", q },
                    {"rho", rho },
                    {"mi", mi },
                    {"D", D },
                    {"delta", delta },
                    {"dW", dW },
                    {"A", A },
                    {"h", h }
                };
                return inputValues;
            }
        }

        public Dictionary<string, double> inputValues
        {
            get
            {
                return _inputValues;
            }
        }

        private Dictionary<string, double> _calculatedValues
        {
            get
            {
                var calculatedValues = new Dictionary<string, double>
                {
                    {"gamma", gamma },
                    {"lambda", lambda },
                    {"vSr", vSr },
                    {"Re", re },
                    {"ni", ni },
                    {"x*", x },
                    {"dP", dP }
                };
                return calculatedValues;
            }
        }

        public Dictionary<string, double> calculatedValues
        {
            get
            {
                return _calculatedValues;
            }
        }
        public void Print(Dictionary<string, string> dic)
        {
            Console.WriteLine("\n ====================================");
            foreach (KeyValuePair<string, string> entry in dic)
            {
                Console.WriteLine(entry);
            }
        }
        public void PrintValues(Dictionary<string, double> values) // abstract classes, inheritance (!)
        {
            Console.WriteLine("\n ====================================");
            foreach (KeyValuePair<string, double> entry in values)
            {
                Console.WriteLine(entry);
            }
        }

        public void CalculateVSr ()
        {
            var _qv = q;

            _vSr = _qv / A;
        }

        public void CalculateRe()
        {
            _re = (dW * vSr) / ni;
        }

        public void CalculateLambda()
        {
            if (re < 2320)
            {
                _lambda = 64/re;
            }
            else if (re >= 2320 && re < 100000)
            {
                _lambda = (0.3164 / (Math.Pow(re, 0.25)));
            }
            else
            {

                var inLog = (3.13 * dW) / _k;
                var number = 2 * Math.Log(inLog);

                var _lambdaKw = Math.Pow(number, -2);
                _x = re * (_k / dW) * Math.Pow(_lambdaKw, 0.5);

                if (_x<200)
                {
                    _lambda = 0.0032 + (0.221 / Math.Pow(re, 0.237));
                }
                else
                {
                    _lambda = _lambdaKw;
                }
            }
            
        }

        public void CalculatedP()
        {
            _dP = (_lambda * _rho * _vSr) / (2 * dW);
        }

        private void Calculate()
        {
            CalculateVSr();
            CalculateRe();
            CalculateLambda();
            CalculatedP();
            Console.WriteLine("Calculations conpleted \n");
        }

        public void MakeCalculations(bool SI)
        {
            if (SI == true)
            {
                Calculate();
            }
            else
            {
                Start:
                Console.WriteLine("Input values not in SI units! Do you want to continue? Write y or n");
                string go = Console.ReadLine();

                switch (go)
                {
                    case "y":
                        Calculate();
                        break;
                    case "n":
                        break;
                    default:
                        Console.WriteLine("Wrong! Type y or no.");
                        goto Start;
                }
            }
        }

        public void reset()
        {
            _vSr = 0;
            _D = 0; 
            _delta = 0; 
             _h = 0; 
            _k = 0;
            _q = 0; 
            _rho = 0; 
            _mi = 0; 
            _pK = 0; 
            _re = 0;
            _lambda = 0;
            _dP = 0;
            _x = 0;
            Console.WriteLine("Values resetted to 0.");
        }

        public void ConvertToSi () // define convert values in another class (interface?)
        {
            // '' to m
            _D *= 0.0254;

            // mm to m
            _delta /= 1000;

            // cP to Pa*s
            _mi /= 1000;

            // g/cm3 to kg/cm3
            _rho *= 1000;

            // kg/d tp m3/s
            _q /= 86400*_rho;

            // at to Pa
            _pK *= 98066.5;


            // update status
            _isSI = true;

            Console.WriteLine("Succesfully converted to SI units.");
        }

    }
}
