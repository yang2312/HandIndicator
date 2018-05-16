using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIndicators.Model
{
    public class IndicatorResult: ObservableObject
    {
        private HandIndicator _indicatorLeft;
        private HandIndicator _indicatorRight;
        private double _atd = 0;
        public double x
        {
            get
            {
                double _x = _indicatorLeft.ATD * 100 / 35;
                if (_x > 100) return 200 - _x;
                return _x;
            }
        }
        private double y
        {
            get
            {
                double _y = _indicatorRight.ATD * 100 / 35;
                if (_y > 100) return 200 - _y;
                return _y;
            }
        }
        private double _sumCAPL = 0;
        private double _sumCAPR = 0;
       
        public IndicatorResult(HandIndicator indicatorLeft, HandIndicator indicatorRight,string year)
        {
            _indicatorLeft = indicatorLeft;
            _indicatorRight = indicatorRight;

            double atd = (_indicatorLeft.ATD + _indicatorRight.ATD) / 2;
            if ((DateTime.Now.Year - int.Parse(year)) < 10)
            {
                _atd = atd - (10 - (DateTime.Now.Year - int.Parse(year)));
            }
            else _atd = atd;

            foreach(FingerIndicator finger in indicatorLeft.ListFingers)
            {
                _sumCAPL += finger.CAP;
            }
            foreach (FingerIndicator finger in indicatorRight.ListFingers)
            {
                _sumCAPR += finger.CAP;
            }
        }

        public double TFRC
        {
            get
            {
                double result = 0;
                foreach(FingerIndicator finger in _indicatorLeft.ListFingers)
                {
                    result += finger.PI;
                }
                foreach (FingerIndicator finger in _indicatorRight.ListFingers)
                {
                    result += finger.PI;
                }
                return result;
            }
        }
        public double TAFRC
        {
            get
            {
                double result = 0;
                foreach (FingerIndicator finger in _indicatorLeft.ListFingers)
                {
                    result += (finger.PI * finger.CAP);
                }
                foreach (FingerIndicator finger in _indicatorRight.ListFingers)
                {
                    result += (finger.PI * finger.CAP);
                }
                return result;
            }
        }
        public string SumPercentL
        {
            get
            {
                double sumL = 0;
                foreach(FingerIndicator finger in _indicatorLeft.ListFingers)
                {
                    sumL += finger.PI;
                }
                double result = (sumL * 100) / TFRC;

                return $"{result} %";
            }
        }

        public string SumPercentR
        {
            get
            {
                double sumR = 0;
                foreach (FingerIndicator finger in _indicatorRight.ListFingers)
                {
                    sumR += finger.PI;
                }
                double result = (sumR * 100) / TFRC;

                return $"{result} %";
            }
        }

        public double RIL1
        {
            get
            {
                return (x * _indicatorLeft.ListFingers[0].PI) / 100;
            }
        }
        public double RIL2
        {
            get
            {
                return (x * _indicatorLeft.ListFingers[1].PI) / 100;
            }
        }
        public double RIL3
        {
            get
            {
                return (x * _indicatorLeft.ListFingers[2].PI) / 100;
            }
        }
        public double RIL4
        {
            get
            {
                return (x * _indicatorLeft.ListFingers[3].PI) / 100;
            }
        }
        public double RIL5
        {
            get
            {
                return (x * _indicatorLeft.ListFingers[4].PI) / 100;
            }
        }
        public double RIR1
        {
            get
            {
                return (y * _indicatorRight.ListFingers[0].PI) / 100;
            }
        }
        public double RIR2
        {
            get
            {
                return (x * _indicatorRight.ListFingers[1].PI) / 100;
            }
        }
        public double RIR3
        {
            get
            {
                return (x * _indicatorRight.ListFingers[2].PI) / 100;
            }
        }
        public double RIR4
        {
            get
            {
                return (x * _indicatorRight.ListFingers[3].PI) / 100;
            }
        }
        public double RIR5
        {
            get
            {
                return (x * _indicatorRight.ListFingers[4].PI) / 100;
            }
        }

        public double SumRI
        {
            get
            {
                return RIL1 + RIL2 + RIL3 + RIL4 + RIL5 + RIR1 + RIR2 + RIR3 + RIR4 + RIR5;
            }
        }

        public double NL
        {
            get { return TAFRC - TFRC; }
        }

        public double TT_NL
        {
            get { return (SumRI * 100) / TAFRC; }
        }

        public double AL
        {
            get
            {
                return (_sumCAPL * 100) / (_sumCAPL + _sumCAPR);
            }
        }
        public double AR
        {
            get
            {
                return (_sumCAPR * 100) / (_sumCAPL + _sumCAPR);
            }
        }
        
        public double TT_PB
        {
            get
            {
                return (_indicatorLeft.ListFingers[0].PI * 100 / TFRC) + (_indicatorRight.ListFingers[0].PI * 100 / TFRC);
            }
        }
        public double T_PB
        {
            get
            {
                return (_indicatorLeft.ListFingers[1].PI * 100 / TFRC) + (_indicatorRight.ListFingers[1].PI * 100 / TFRC);
            }
        }
        public double D_PB
        {
            get
            {
                return (_indicatorLeft.ListFingers[2].PI * 100 / TFRC) + (_indicatorRight.ListFingers[2].PI * 100 / TFRC);
            }
        }
        public double TD_PB
        {
            get
            {
                return (_indicatorLeft.ListFingers[3].PI * 100 / TFRC) + (_indicatorRight.ListFingers[3].PI * 100 / TFRC);
            }
        }
        public double C_PB
        {
            get
            {
                return (_indicatorLeft.ListFingers[4].PI * 100 / TFRC) + (_indicatorRight.ListFingers[4].PI * 100 / TFRC);
            }
        }
    }
}
