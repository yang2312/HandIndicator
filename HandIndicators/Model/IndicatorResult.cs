using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private double _sumLastFingers = 0;
        public double x
        {
            get
            {
                double _x = double.Parse(_indicatorLeft.ATD) * 100 / 35;
                if (_x > 100) return 200 - _x;
                return Math.Round(_x,2);
            }
        }
        public double y
        {
            get
            {
                double _y = double.Parse(_indicatorRight.ATD) * 100 / 35;
                if (_y > 100) return 200 - _y;
                return Math.Round(_y, 2);
            }
        }
        private double _sumCAPL = 0;
        private double _sumCAPR = 0;
       
        public IndicatorResult(HandIndicator indicatorLeft, HandIndicator indicatorRight,string year)
        {
            _indicatorLeft = indicatorLeft;
            _indicatorRight = indicatorRight;

            double atd = (double.Parse(_indicatorLeft.ATD) + double.Parse(_indicatorRight.ATD)) / 2;
            if ((DateTime.Now.Year - int.Parse(year)) < 10)
            {
                _atd = atd - (10 - (DateTime.Now.Year - int.Parse(year)));
            }
            else _atd = atd;

            foreach(FingerIndicator finger in indicatorLeft.ListFingers)
            {
                _sumCAPL += double.Parse(finger.CAP);
            }
            foreach (FingerIndicator finger in indicatorRight.ListFingers)
            {
                _sumCAPR += double.Parse(finger.CAP);
            }

            for(int i = 0; i < _indicatorLeft.ListFingers.Count; i++)
            {
                _indicatorLeft.ListFingers[i].AI = Math.Round(CalculateAI(_indicatorLeft.ListFingers[i], i),2);
                if (i >= 2)
                {
                    _sumLastFingers += double.Parse(_indicatorLeft.ListFingers[i].PI);
                }
            }
            for (int i = 0; i < _indicatorRight.ListFingers.Count; i++)
            {
                _indicatorRight.ListFingers[i].AI = Math.Round(CalculateAI(_indicatorRight.ListFingers[i], i),2);
                if (i >= 2)
                {
                    _sumLastFingers += double.Parse(_indicatorRight.ListFingers[i].PI);
                }
            }
        }

        private double CalculateAI(FingerIndicator finger,int index)
        {
            double result = 0;
            switch (finger.Type.First())
            {
                case 'W':
                    if(1.45 > double.Parse(finger.CAP))
                    {
                        double tempA = (1.45 - double.Parse(finger.CAP)) / 3;
                        return 1.45 - tempA;
                    }
                    else if(1.45 == double.Parse(finger.CAP))
                        return 1.45;
                    else
                    {
                        double tempB = (double.Parse(finger.CAP) - 1.45) / 3;
                        return 1.45 + tempB;
                    }
                case 'U':
                    if (index == 0 || index == 1 || index == 3) return 1.15;
                    else if (index == 2) return 1.1;
                    else return 1.2;
                default:
                    double temp = (x * 1.65) / 100;
                    if(double.Parse(finger.PI) <= 7)
                    {
                        double a = (7 - double.Parse(finger.PI)) * 0.04;
                        return temp + a;
                    }
                    else
                    {
                        double b = (double.Parse(finger.PI) - 7) * 0.04;
                        return temp - b;
                    }
            }
        }

        public double TFRC
        {
            get
            {
                double result = 0;
                foreach(FingerIndicator finger in _indicatorLeft.ListFingers)
                {
                    result += double.Parse(finger.PI);
                }
                foreach (FingerIndicator finger in _indicatorRight.ListFingers)
                {
                    result += double.Parse(finger.PI);
                }
                return Math.Round(result, 2);
            }
        }
        public double TAFRC
        {
            get
            {
                double result = 0;
                foreach (FingerIndicator finger in _indicatorLeft.ListFingers)
                {
                    result += (double.Parse(finger.PI) * double.Parse(finger.CAP));
                }
                foreach (FingerIndicator finger in _indicatorRight.ListFingers)
                {
                    result += (double.Parse(finger.PI) * double.Parse(finger.CAP));
                }
                return Math.Round(result, 2);
            }
        }
        public string SumPercentL
        {
            get
            {
                double sumL = 0;
                foreach(FingerIndicator finger in _indicatorLeft.ListFingers)
                {
                    sumL += double.Parse(finger.PI);
                }
                double result = (sumL * 100) / TFRC;

                return $"{Math.Round(result, 2)} %";
            }
        }

        public string SumPercentR
        {
            get
            {
                double sumR = 0;
                foreach (FingerIndicator finger in _indicatorRight.ListFingers)
                {
                    sumR += double.Parse(finger.PI);
                }
                double result = (sumR * 100) / TFRC;

                return $"{Math.Round(result, 2)} %";
            }
        }

        public double RIL1
        {
            get
            {
                double result = Math.Round((x * double.Parse(_indicatorLeft.ListFingers[0].PI)) / 100,2);
                _indicatorLeft.ListFingers[0].RI = result;
                return result;
            }
        }
        public double RIL2
        {
            get
            {
                double result = Math.Round((x * double.Parse(_indicatorLeft.ListFingers[1].PI)) / 100,2);
                _indicatorLeft.ListFingers[1].RI = result;
                return result;
            }
        }
        public double RIL3
        {
            get
            {
                double result = Math.Round((x * double.Parse(_indicatorLeft.ListFingers[2].PI)) / 100,2);
                _indicatorLeft.ListFingers[2].RI = result;
                return result;
            }
        }
        public double RIL4
        {
            get
            {
                double result = Math.Round((x * double.Parse(_indicatorLeft.ListFingers[3].PI)) / 100,2);
                _indicatorLeft.ListFingers[3].RI = result;
                return result;
            }
        }
        public double RIL5
        {
            get
            {
                double result = Math.Round((x * double.Parse(_indicatorLeft.ListFingers[4].PI)) / 100,2);
                _indicatorLeft.ListFingers[4].RI = result;
                return result;
            }
        }
        public double RIR1
        {
            get
            {
                double result = Math.Round((x * double.Parse(_indicatorRight.ListFingers[0].PI)) / 100,2);
                _indicatorRight.ListFingers[0].RI = result;
                return result;
            }
        }
        public double RIR2
        {
            get
            {
                double result = Math.Round((x * double.Parse(_indicatorRight.ListFingers[1].PI)) / 100,2);
                _indicatorRight.ListFingers[1].RI = result;
                return result;
            }
        }
        public double RIR3
        {
            get
            {
                double result = Math.Round((x * double.Parse(_indicatorRight.ListFingers[2].PI)) / 100,2);
                _indicatorRight.ListFingers[2].RI = result;
                return result;
            }
        }
        public double RIR4
        {
            get
            {
                double result = Math.Round((x * double.Parse(_indicatorRight.ListFingers[3].PI)) / 100,2);
                _indicatorRight.ListFingers[3].RI = result;
                return result;
            }
        }
        public double RIR5
        {
            get
            {
                double result = Math.Round((x * double.Parse(_indicatorRight.ListFingers[4].PI)) / 100,2);
                _indicatorRight.ListFingers[4].RI = result;
                return result;
            }
        }

        public double SumRI
        {
            get
            {
                return Math.Round(RIL1 + RIL2 + RIL3 + RIL4 + RIL5 + RIR1 + RIR2 + RIR3 + RIR4 + RIR5, 2);
            }
        }

        public double NL
        {
            get { return Math.Round(TAFRC - TFRC, 2); }
        }

        public double TT_NL
        {
            get { return Math.Round((SumRI * 100) / TAFRC, 2); }
        }

        public double AL
        {
            get
            {
                return Math.Round((_sumCAPL * 100) / (_sumCAPL + _sumCAPR), 2);
            }
        }
        public double AR
        {
            get
            {
                return Math.Round((_sumCAPR * 100) / (_sumCAPL + _sumCAPR), 2);
            }
        }
        
        public double TT_PB
        {
            get
            {
                return Math.Round((double.Parse(_indicatorLeft.ListFingers[0].PI) * 100 / TFRC) + (double.Parse(_indicatorRight.ListFingers[0].PI) * 100 / TFRC),2);
            }
        }
        public double T_PB
        {
            get
            {
                return Math.Round((double.Parse(_indicatorLeft.ListFingers[1].PI) * 100 / TFRC) + (double.Parse(_indicatorRight.ListFingers[1].PI) * 100 / TFRC),2);
            }
        }
        public double D_PB
        {
            get
            {
                return Math.Round((double.Parse(_indicatorLeft.ListFingers[2].PI) * 100 / TFRC) + (double.Parse(_indicatorRight.ListFingers[2].PI) * 100 / TFRC),2);
            }
        }
        public double TD_PB
        {
            get
            {
                return Math.Round((double.Parse(_indicatorLeft.ListFingers[3].PI) * 100 / TFRC) + (double.Parse(_indicatorRight.ListFingers[3].PI) * 100 / TFRC),2);
            }
        }
        public double C_PB
        {
            get
            {
                return Math.Round((double.Parse(_indicatorLeft.ListFingers[4].PI) * 100 / TFRC) + (double.Parse(_indicatorRight.ListFingers[4].PI) * 100 / TFRC),2);
            }
        }
        public double E_CS
        {
            get
            {
                return Math.Round((double.Parse(_indicatorLeft.ListFingers[0].PI) * double.Parse(_indicatorLeft.ListFingers[0].CAP)) + (double.Parse(_indicatorRight.ListFingers[0].PI) * double.Parse(_indicatorRight.ListFingers[0].CAP))
                    + (70 - _atd) + (_indicatorLeft.ListFingers[0].AI + _indicatorRight.ListFingers[0].AI),2);
            }
        }
        public double I_CS
        {
            get
            {
                return Math.Round((double.Parse(_indicatorLeft.ListFingers[1].PI) * double.Parse(_indicatorLeft.ListFingers[1].CAP)) + (double.Parse(_indicatorRight.ListFingers[1].PI) * double.Parse(_indicatorRight.ListFingers[1].CAP))
                    + (70 - _atd) + (_indicatorLeft.ListFingers[1].AI + _indicatorRight.ListFingers[1].AI),2);
            }
        }
        public double A_CS
        {
            get
            {
                return Math.Round((double.Parse(_indicatorLeft.ListFingers[2].PI) * double.Parse(_indicatorLeft.ListFingers[2].CAP)) + (double.Parse(_indicatorRight.ListFingers[2].PI) * double.Parse(_indicatorRight.ListFingers[2].CAP))
                    + (70 - _atd) + (_indicatorLeft.ListFingers[2].AI + _indicatorRight.ListFingers[2].AI),2);
            }
        }
        public double C_CS
        {
            get
            {
                return Math.Round((double.Parse(_indicatorLeft.ListFingers[3].PI) * double.Parse(_indicatorLeft.ListFingers[3].CAP)) + (double.Parse(_indicatorRight.ListFingers[3].PI) * double.Parse(_indicatorRight.ListFingers[3].CAP))
                    + (70 - _atd) + (_indicatorLeft.ListFingers[3].AI + _indicatorRight.ListFingers[3].AI),2);
            }
        }
        public double P_CS
        {
            get
            {
                return Math.Round((double.Parse(_indicatorLeft.ListFingers[4].PI) * double.Parse(_indicatorLeft.ListFingers[4].CAP)) + (double.Parse(_indicatorRight.ListFingers[4].PI) * double.Parse(_indicatorRight.ListFingers[4].CAP))
                    + (70 - _atd) + (_indicatorLeft.ListFingers[4].AI + _indicatorRight.ListFingers[4].AI),2);
            }
        }

        public ObservableCollection<SortedItemWithIndex> ListTMBS
        {
            get
            {
                var list = new ObservableCollection<SortedItemWithIndex>() { new SortedItemWithIndex { Label="XH",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[0].PI),2)},
                                                                    new SortedItemWithIndex { Label="LG",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[1].PI),2)},
                                                                    new SortedItemWithIndex { Label="VD",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[2].PI) + double.Parse(_indicatorRight.ListFingers[2].PI)) / 2),2)},
                                                                    new SortedItemWithIndex { Label="NN",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[3].PI),2)},
                                                                    new SortedItemWithIndex { Label="TN",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[1].PI),2)},
                                                                    new SortedItemWithIndex { Label="TG",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[4].PI) + double.Parse(_indicatorRight.ListFingers[4].PI)) / 2),2)},
                                                                    new SortedItemWithIndex { Label="AN",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[0].PI),2)},
                                                                    new SortedItemWithIndex { Label="NT",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[0].PI),2)}};

                foreach (var item in list)
                {
                    item.Index = list.OrderBy(x => x.Value).ToList().IndexOf(item) + 1;
                }

                return list;
            }
        }

        public ObservableCollection<SortedItemWithIndex> ListTMTD
        {
            get
            {
                var list = new ObservableCollection<SortedItemWithIndex>() { new SortedItemWithIndex { Label="XH",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[0].PI) * double.Parse(_indicatorLeft.ListFingers[0].CAP),2)},
                                                                    new SortedItemWithIndex { Label="LG",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[1].PI) * double.Parse(_indicatorLeft.ListFingers[1].CAP),2)},
                                                                    new SortedItemWithIndex { Label="VD",
                                                                                    Value = Math.Round((((double.Parse(_indicatorLeft.ListFingers[2].PI) * double.Parse(_indicatorLeft.ListFingers[2].CAP)) + (double.Parse(_indicatorRight.ListFingers[2].PI) * double.Parse(_indicatorRight.ListFingers[2].PI))) / 2),2)},
                                                                    new SortedItemWithIndex { Label="NN",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[3].PI) * double.Parse(_indicatorLeft.ListFingers[3].CAP),2)},
                                                                    new SortedItemWithIndex { Label="TN",Value= Math.Round((((double.Parse(_indicatorLeft.ListFingers[4].PI) * double.Parse(_indicatorLeft.ListFingers[4].CAP)) + (double.Parse(_indicatorRight.ListFingers[4].PI) * double.Parse(_indicatorRight.ListFingers[4].PI))) / 2),2)},
                                                                    new SortedItemWithIndex { Label="TG",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[0].PI) * double.Parse(_indicatorRight.ListFingers[0].CAP),2)},
                                                                    new SortedItemWithIndex { Label="AN",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[1].PI) * double.Parse(_indicatorRight.ListFingers[1].CAP),2)},
                                                                    new SortedItemWithIndex { Label="NT",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[3].PI) * double.Parse(_indicatorRight.ListFingers[3].CAP),2)}};

                foreach(var item in list)
                {
                    item.Index = list.OrderBy(x => x.Value).ToList().IndexOf(item) + 1;
                }

                return list;
            }
        }
        public ObservableCollection<SortedItemWithIndex> ListVAK
        {
            get
            {
                var list = new ObservableCollection<SortedItemWithIndex>() { new SortedItemWithIndex { Label="V",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[4].PI) + double.Parse(_indicatorRight.ListFingers[4].PI)) * 100) / _sumLastFingers,2)},
                                                                             new SortedItemWithIndex { Label="A",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[3].PI) + double.Parse(_indicatorRight.ListFingers[3].PI)) * 100) / _sumLastFingers,2)},
                                                                             new SortedItemWithIndex { Label="K",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[2].PI) + double.Parse(_indicatorRight.ListFingers[2].PI)) * 100) / _sumLastFingers,2)}};

                foreach (var item in list)
                {
                    item.Index = list.OrderBy(x => x.Value).ToList().IndexOf(item) + 1;
                }

                return list;
            }
        }

        public ObservableCollection<FingerIndicator> SortedListData
        {
            get
            {
                var list = new ObservableCollection<FingerIndicator>();
                for(int i =0; i < _indicatorLeft.ListFingers.Count;i++)
                {
                    _indicatorLeft.ListFingers[i].Name = $"L{i + 1}";
                    list.Add(_indicatorLeft.ListFingers[i]);
                }
                for (int i = 0; i < _indicatorRight.ListFingers.Count; i++)
                {
                    _indicatorRight.ListFingers[i].Name = $"R{i + 1}";
                    list.Add(_indicatorRight.ListFingers[i]);
                }

                var listOrderbyPI = list.OrderBy(x => double.Parse(x.PI)).ToList();

                foreach (var item in listOrderbyPI)
                {
                    item.IndexPI = listOrderbyPI.IndexOf(item) + 1;
                }
                

                var listOrderbyRI = list.OrderBy(x => x.RI).ToList();
                foreach (var item in listOrderbyRI)
                {
                    item.IndexRI = listOrderbyRI.IndexOf(item) + 1;
                }
                
                return list;
            }
        }
    }
}
