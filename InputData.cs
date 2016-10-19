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

        private InputData () {}

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
        private double _dW;
        private double _d;
        private double _h;
        private double _q1;
        private double _q2;
        private double _q3;
        private double _rho;
        private double _mi;
        private double _pK;
        private double _re;
        private double _lambda;

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
                return _dW;
            }
            set
            {
                _dW = value;
            }
        }
        public double d
        {
            get
            {
                return _d;
            }
            set
            {
                _d = value;
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

        public double q1
        {
            get
            {
                return _q1;
            }

            set
            {
                _q1 = value;
            }
        }

        public double q2
        {
            get
            {
                return _q2;
            }

            set
            {
                _q2 = value;
            }
        }

        public double q3
        {
            get
            {
                return _q3;
            }

            set
            {
                _q3 = value;
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

        public double[] arrayForResult
        {
            get
            {
                double[] array = new double[5] { _lambda, _vSr, _h, _dW, _g};
                return array;
            }
        }

        public void CalculateVSr (string q)
        {
            var _qv = new Double();
            var _a = pi  * Math.Pow((dW/2), 2);
            var isOk = new Boolean();

            while (!isOk)
            {
                var _q = new Double();
                switch (q)
                {
                    case "q1":
                        _q = _q1;
                        isOk = true;
                        break;
                    case "q2":
                        _q = _q2;
                        isOk = true;
                        break;
                    case "q3":
                        _q = _q3;
                        isOk = true;
                        break;
                    default:
                        Console.WriteLine("Błąd! Podaj q1, q2 lub q3");
                        break;
                }
                _qv = _q / rho;
            }
            _vSr = _qv / _a;
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
                // CO TO JEST D???
                var _k = 0.00135;

                var inLog = (3.13 * dW) / _k;
                var number = 2 * Math.Log(inLog);

                var _lambdaKw = Math.Pow(number, -2);
                var x = re * (_k / dW) * Math.Pow(_lambdaKw, 0.5);

                if (x<200)
                {
                    _lambda = 0.0032 + (0.221 / Math.Pow(re, 0.237));
                }
                else
                {
                    _lambda = _lambdaKw;
                }
            }
        }

        public void ConvertToSi () // define convert values in another class (interface?)
        {
            // '' to m
            _dW /= 10000;

            // cP to Pa*s
            _mi /= 1000;

            // m3/h tp m3/s
            _q1 /= 86400;
            _q2 /= 86400;
            _q3 /= 86400;

            // at to Pa
            _pK *= 98066.5;

            // g/cm3 to kg/cm3
            _rho *= 1000;
        }

    }
}
