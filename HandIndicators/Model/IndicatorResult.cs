using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace HandIndicators.Model
{
    public class IndicatorResult : ObservableObject
    {
        private HandIndicator _indicatorLeft;
        private HandIndicator _indicatorRight;
        private double _atd = 0;
        private double _sumLastFingers = 0;
        private Gender _gender;
        public double x
        {
            get
            {
                double _x = double.Parse(_indicatorLeft.ATD) * 100 / 35;
                if (_x > 100) return Math.Round(200 - _x, 2);
                return Math.Round(_x, 2);
            }
        }
        public double y
        {
            get
            {
                double _y = double.Parse(_indicatorRight.ATD) * 100 / 35;
                if (_y > 100) return Math.Round(200 - _y, 2);
                return Math.Round(_y, 2);
            }
        }
        public double HSL
        {
            get
            {
                double sumRI = 0;
                double sumPI = 0;
                foreach (var finger in _indicatorLeft.ListFingers)
                {
                    sumRI += finger.RI;
                    sumPI += double.Parse(finger.PI);
                }
                return Math.Round((sumRI * 100) / sumPI, 2);
            }
        }
        public double HSR
        {
            get
            {
                double sumRI = 0;
                double sumPI = 0;
                foreach (var finger in _indicatorRight.ListFingers)
                {
                    sumRI += finger.RI;
                    sumPI += double.Parse(finger.PI);
                }
                return Math.Round((sumRI * 100) / sumPI, 2);
            }
        }
        private double _sumCAPL = 0;
        private double _sumCAPR = 0;

        public string OverallRIL1 { get; set; }
        public string OverallRIL2 { get; set; }
        public string OverallRIL3 { get; set; }
        public string OverallRIL4 { get; set; }
        public string OverallRIL5 { get; set; }
        public string OverallRIR1 { get; set; }
        public string OverallRIR2 { get; set; }
        public string OverallRIR3 { get; set; }
        public string OverallRIR4 { get; set; }
        public string OverallRIR5 { get; set; }
        public IndicatorResult(HandIndicator indicatorLeft, HandIndicator indicatorRight, string year, Gender gender)
        {
            _indicatorLeft = indicatorLeft;
            _indicatorRight = indicatorRight;
            _gender = gender;

            double atd = (double.Parse(_indicatorLeft.ATD) + double.Parse(_indicatorRight.ATD)) / 2;
            if ((DateTime.Now.Year - int.Parse(year)) < 10)
            {
                _atd = atd - (10 - (DateTime.Now.Year - int.Parse(year)));
            }
            else _atd = atd;

            foreach (FingerIndicator finger in indicatorLeft.ListFingers)
            {
                _sumCAPL += finger.CAP.Equals("Không xác định") ? 1.00 : double.Parse(finger.CAP);
            }
            foreach (FingerIndicator finger in indicatorRight.ListFingers)
            {
                _sumCAPR += finger.CAP.Equals("Không xác định") ? 1.00 : double.Parse(finger.CAP);
            }

            for (int i = 0; i < _indicatorLeft.ListFingers.Count; i++)
            {
                _indicatorLeft.ListFingers[i].AI = Math.Round(CalculateAI(_indicatorLeft.ListFingers[i], i), 2);
                if (i >= 2)
                {
                    _sumLastFingers += double.Parse(_indicatorLeft.ListFingers[i].PI);
                }

                double result;
                if (_indicatorLeft.ListFingers[i].Type.StartsWith("A"))
                {
                    result = Math.Round((x * double.Parse(_indicatorLeft.ListFingers[i].PI) * 2.7) / 100, 2);
                }
                else result = Math.Round((x * double.Parse(_indicatorLeft.ListFingers[i].PI)) / 100, 2);
                _indicatorLeft.ListFingers[i].RI = result;
                switch (i)
                {
                    case 0:
                        OverallRIL1 = _indicatorLeft.ListFingers[i].OverallRI;
                        break;
                    case 1:
                        OverallRIL2 = _indicatorLeft.ListFingers[i].OverallRI;
                        break;
                    case 2:
                        OverallRIL3 = _indicatorLeft.ListFingers[i].OverallRI;
                        break;
                    case 3:
                        OverallRIL4 = _indicatorLeft.ListFingers[i].OverallRI;
                        break;
                    default:
                        OverallRIL5 = _indicatorLeft.ListFingers[i].OverallRI;
                        break;
                }
                
            }
            for (int i = 0; i < _indicatorRight.ListFingers.Count; i++)
            {
                _indicatorRight.ListFingers[i].AI = Math.Round(CalculateAI(_indicatorRight.ListFingers[i], i), 2);
                if (i >= 2)
                {
                    _sumLastFingers += double.Parse(_indicatorRight.ListFingers[i].PI);
                }

                double result;
                if (_indicatorRight.ListFingers[i].Type.StartsWith("A"))
                {
                    result = Math.Round((y * double.Parse(_indicatorRight.ListFingers[i].PI) * 2.7) / 100, 2);
                }
                else result = Math.Round((y * double.Parse(_indicatorRight.ListFingers[i].PI)) / 100, 2);
                _indicatorRight.ListFingers[i].RI = result;
                
                switch (i)
                {
                    case 0:
                        OverallRIR1 = _indicatorRight.ListFingers[i].OverallRI;
                        break;
                    case 1:
                        OverallRIR2 = _indicatorRight.ListFingers[i].OverallRI;
                        break;
                    case 2:
                        OverallRIR3 = _indicatorRight.ListFingers[i].OverallRI;
                        break;
                    case 3:
                        OverallRIR4 = _indicatorRight.ListFingers[i].OverallRI;
                        break;
                    default:
                        OverallRIR5 = _indicatorRight.ListFingers[i].OverallRI;
                        break;
                }
            }
        }

        private double CalculateAI(FingerIndicator finger, int index)
        {
            switch (finger.Type.First())
            {
                case 'W':
                    if (1.45 > double.Parse(finger.CAP))
                    {
                        double tempA = (1.45 - double.Parse(finger.CAP)) / 3;
                        return double.Parse(finger.PI) * (1.45 - tempA);
                    }
                    else if (1.45 == double.Parse(finger.CAP))
                        return double.Parse(finger.PI) * 1.45;
                    else
                    {
                        double tempB = (double.Parse(finger.CAP) - 1.45) / 3;
                        return double.Parse(finger.PI) * (1.45 + tempB);
                    }
                case 'U':
                    if (index == 0 || index == 1 || index == 3) return double.Parse(finger.PI) * 1.15;
                    else if (index == 2) return double.Parse(finger.PI) * 1.1;
                    else return double.Parse(finger.PI) * 1.2;
                default:
                    double temp = (x * 1.65) / 100;
                    if (double.Parse(finger.PI) <= 7)
                    {
                        double a = (7 - double.Parse(finger.PI)) * 0.04;
                        return double.Parse(finger.PI) * (temp + a);
                    }
                    else
                    {
                        double b = (double.Parse(finger.PI) - 7) * 0.04;
                        return double.Parse(finger.PI) * (temp - b);
                    }
            }
        }

        public double TFRC
        {
            get
            {
                double result = 0;
                foreach (FingerIndicator finger in _indicatorLeft.ListFingers)
                {
                    result += double.Parse(finger.PI);
                }
                foreach (FingerIndicator finger in _indicatorRight.ListFingers)
                {
                    result += double.Parse(finger.PI);
                }

                switch (_gender)
                {
                    case Gender.Male:
                        if(result < 60)
                        {
                            OverallTFRC = "Rất thấp";
                        }
                        else if (result >= 60 && result <= 90)
                        {
                            OverallTFRC = "Thấp";
                        }
                        else if (result > 90 && result < 125)
                        {
                            OverallTFRC = "TB";
                        }
                        else if (result >= 125 && result < 145)
                        {
                            OverallTFRC = "Khá";
                        }
                        else if (result >= 145 && result <= 190)
                        {
                            OverallTFRC = "Cao";
                        }
                        else
                        {
                            OverallTFRC = "Rất cao";
                        }
                        break;
                    default:
                        if (result < 50)
                        {
                            OverallTFRC = "Rất thấp";
                        }
                        else if (result >= 50 && result <= 80)
                        {
                            OverallTFRC = "Thấp";
                        }
                        else if (result > 80 && result < 110)
                        {
                            OverallTFRC = "TB";
                        }
                        else if (result >= 110 && result < 125)
                        {
                            OverallTFRC = "Khá";
                        }
                        else if (result >= 125 && result <= 170)
                        {
                            OverallTFRC = "Cao";
                        }
                        else
                        {
                            OverallTFRC = "Rất cao";
                        }
                        break;
                }

                return Math.Round(result, 2);
            }
        }
        private string _overallTFRC;
        public string OverallTFRC
        {
            get { return _overallTFRC; }
            set
            {
                _overallTFRC = value;
                RaisePropertyChanged();
            }

        }
        private string _overallTAFRC;
        public string OverallTAFRC
        {
            get { return _overallTAFRC; }
            set
            {
                _overallTAFRC = value;
                RaisePropertyChanged();
            }

        }
        private string _overallSumRI;
        public string OverallSumRI
        {
            get { return _overallSumRI; }
            set
            {
                _overallSumRI = value;
                RaisePropertyChanged();
            }

        }
        public double TAFRC
        {
            get
            {
                double result = 0;
                foreach (FingerIndicator finger in _indicatorLeft.ListFingers)
                {
                    result += (double.Parse(finger.PI) * (finger.CAP.Equals("Không xác định") ? 1.00 : double.Parse(finger.CAP)));
                }
                foreach (FingerIndicator finger in _indicatorRight.ListFingers)
                {
                    result += (double.Parse(finger.PI) * (finger.CAP.Equals("Không xác định") ? 1.00 : double.Parse(finger.CAP)));
                }
                switch (_gender)
                {
                    case Gender.Male:
                        if (result < 60)
                        {
                            OverallTAFRC = "Rất thấp";
                        }
                        else if (result >= 60 && result <= 90)
                        {
                            OverallTAFRC = "Thấp";
                        }
                        else if (result > 90 && result < 125)
                        {
                            OverallTAFRC = "TB";
                        }
                        else if (result >= 125 && result < 145)
                        {
                            OverallTAFRC = "Khá";
                        }
                        else if (result >= 145 && result <= 190)
                        {
                            OverallTAFRC = "Cao";
                        }
                        else
                        {
                            OverallTAFRC = "Rất cao";
                        }
                        break;
                    default:
                        if (result < 50)
                        {
                            OverallTAFRC = "Rất thấp";
                        }
                        else if (result >= 50 && result <= 80)
                        {
                            OverallTAFRC = "Thấp";
                        }
                        else if (result > 80 && result < 110)
                        {
                            OverallTAFRC = "TB";
                        }
                        else if (result >= 110 && result < 125)
                        {
                            OverallTAFRC = "Khá";
                        }
                        else if (result >= 125 && result <= 170)
                        {
                            OverallTAFRC = "Cao";
                        }
                        else
                        {
                            OverallTAFRC = "Rất cao";
                        }
                        break;
                }
                return Math.Round(result, 2);
            }
        }
        public string SumPercentL
        {
            get
            {
                double sumL = 0;
                foreach (FingerIndicator finger in _indicatorLeft.ListFingers)
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
                return _indicatorLeft.ListFingers[0].RI;
            }
        }
        public double RIL2
        {
            get
            {
                return _indicatorLeft.ListFingers[1].RI;
            }
        }
        public double RIL3
        {
            get
            {
                return _indicatorLeft.ListFingers[2].RI;
            }
        }
        public double RIL4
        {
            get
            {
                return _indicatorLeft.ListFingers[3].RI;
            }
        }
        public double RIL5
        {
            get
            {
                return _indicatorLeft.ListFingers[4].RI;
            }
        }
        public double RIR1
        {
            get
            {
                return _indicatorRight.ListFingers[0].RI;
            }
        }
        public double RIR2
        {
            get
            {
                return _indicatorRight.ListFingers[1].RI;
            }
        }
        public double RIR3
        {
            get
            {
                return _indicatorRight.ListFingers[2].RI;
            }
        }
        public double RIR4
        {
            get
            {
                return _indicatorRight.ListFingers[3].RI;
            }
        }
        public double RIR5
        {
            get
            {
                return _indicatorRight.ListFingers[4].RI;
            }
        }

        public double SumRI
        {
            get
            {
                var result = Math.Round(RIL1 + RIL2 + RIL3 + RIL4 + RIL5 + RIR1 + RIR2 + RIR3 + RIR4 + RIR5, 2);
                switch (_gender)
                {
                    case Gender.Male:
                        if (result < 60)
                        {
                            OverallSumRI = "Rất thấp";
                        }
                        else if (result >= 60 && result <= 90)
                        {
                            OverallSumRI = "Thấp";
                        }
                        else if (result > 90 && result < 125)
                        {
                            OverallSumRI = "TB";
                        }
                        else if (result >= 125 && result < 145)
                        {
                            OverallSumRI = "Khá";
                        }
                        else if (result >= 145 && result <= 190)
                        {
                            OverallSumRI = "Cao";
                        }
                        else
                        {
                            OverallSumRI = "Rất cao";
                        }
                        break;
                    default:
                        if (result < 50)
                        {
                            OverallSumRI = "Rất thấp";
                        }
                        else if (result >= 50 && result <= 80)
                        {
                            OverallSumRI = "Thấp";
                        }
                        else if (result > 80 && result < 110)
                        {
                            OverallSumRI = "TB";
                        }
                        else if (result >= 110 && result < 125)
                        {
                            OverallSumRI = "Khá";
                        }
                        else if (result >= 125 && result <= 170)
                        {
                            OverallSumRI = "Cao";
                        }
                        else
                        {
                            OverallSumRI = "Rất cao";
                        }
                        break;
                }
                return result;
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
                if(_indicatorLeft.ListFingers.Any(x=>x.Type.StartsWith("A")) && _indicatorRight.ListFingers.Any(x => x.Type.StartsWith("A")))
                {
                    //Include A
                    return Math.Round((_indicatorLeft.ListFingers[0].RI +_indicatorRight.ListFingers[0].RI) * 100 / SumRI, 2);
                }
                else
                {
                    //Not include A
                    return Math.Round((double.Parse(_indicatorLeft.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[0].PI)) * 100 / TFRC, 2);
                }
            }
        }
        public double T_PB
        {
            get
            {
                if (_indicatorLeft.ListFingers.Any(x => x.Type.StartsWith("A")) && _indicatorRight.ListFingers.Any(x => x.Type.StartsWith("A")))
                {
                    //Include A
                    return Math.Round((_indicatorLeft.ListFingers[1].RI + _indicatorRight.ListFingers[1].RI) * 100 / SumRI, 2);
                }
                else
                {
                    //Not include A
                    return Math.Round((double.Parse(_indicatorLeft.ListFingers[1].PI) + double.Parse(_indicatorRight.ListFingers[1].PI)) * 100 / TFRC, 2);
                }
            }
        }
        public double D_PB
        {
            get
            {
                if (_indicatorLeft.ListFingers.Any(x => x.Type.StartsWith("A")) && _indicatorRight.ListFingers.Any(x => x.Type.StartsWith("A")))
                {
                    //Include A
                    return Math.Round((_indicatorLeft.ListFingers[2].RI + _indicatorRight.ListFingers[2].RI) * 100 / SumRI, 2);
                }
                else
                {
                    //Not include A
                    return Math.Round((double.Parse(_indicatorLeft.ListFingers[2].PI) + double.Parse(_indicatorRight.ListFingers[2].PI)) * 100 / TFRC, 2);
                }
            }
        }
        public double TD_PB
        {
            get
            {
                if (_indicatorLeft.ListFingers.Any(x => x.Type.StartsWith("A")) && _indicatorRight.ListFingers.Any(x => x.Type.StartsWith("A")))
                {
                    //Include A
                    return Math.Round((_indicatorLeft.ListFingers[3].RI + _indicatorRight.ListFingers[3].RI) * 100 / SumRI, 2);
                }
                else
                {
                    //Not include A
                    return Math.Round((double.Parse(_indicatorLeft.ListFingers[3].PI) + double.Parse(_indicatorRight.ListFingers[3].PI)) * 100 / TFRC, 2);
                }
            }
        }
        public double C_PB
        {
            get
            {
                if (_indicatorLeft.ListFingers.Any(x => x.Type.StartsWith("A")) && _indicatorRight.ListFingers.Any(x => x.Type.StartsWith("A")))
                {
                    //Include A
                    return Math.Round((_indicatorLeft.ListFingers[4].RI + _indicatorRight.ListFingers[4].RI) * 100 / SumRI, 2);
                }
                else
                {
                    //Not include A
                    return Math.Round((double.Parse(_indicatorLeft.ListFingers[4].PI) + double.Parse(_indicatorRight.ListFingers[4].PI)) * 100 / TFRC, 2);
                }
            }
        }
        public double E_CS
        {
            get
            {
                return Math.Round((_indicatorLeft.ListFingers[0].RI + _indicatorRight.ListFingers[0].RI + _indicatorLeft.ListFingers[0].AI + _indicatorRight.ListFingers[0].AI + _indicatorLeft.ListFingers[1].RI + _indicatorLeft.ListFingers[1].AI), 2);
            }
        }
        public double I_CS
        {
            get
            {
                return Math.Round((_indicatorLeft.ListFingers[0].RI + _indicatorRight.ListFingers[0].RI + _indicatorLeft.ListFingers[0].AI + _indicatorRight.ListFingers[0].AI + _indicatorRight.ListFingers[1].AI + _indicatorRight.ListFingers[1].RI), 2);
            }
        }
        public double A_CS
        {
            get
            {
                return Math.Round((_indicatorLeft.ListFingers[0].RI + _indicatorRight.ListFingers[0].RI + _indicatorLeft.ListFingers[0].AI + _indicatorRight.ListFingers[0].AI + ((_indicatorLeft.ListFingers[2].AI + _indicatorRight.ListFingers[2].AI) / 2) + ((_indicatorLeft.ListFingers[2].RI + _indicatorRight.ListFingers[2].RI) / 2)), 2);
            }
        }
        public double C_CS
        {
            get
            {
                return Math.Round((_indicatorLeft.ListFingers[0].RI + _indicatorRight.ListFingers[0].RI + _indicatorLeft.ListFingers[0].AI + _indicatorRight.ListFingers[0].AI + ((_indicatorLeft.ListFingers[3].AI + _indicatorRight.ListFingers[3].AI) / 2) + ((_indicatorLeft.ListFingers[3].RI + _indicatorRight.ListFingers[3].RI) / 2)), 2);
            }
        }
        public double P_CS
        {
            get
            {
                return Math.Round((_indicatorLeft.ListFingers[0].RI + _indicatorRight.ListFingers[0].RI + _indicatorLeft.ListFingers[0].AI + _indicatorRight.ListFingers[0].AI + ((_indicatorLeft.ListFingers[4].AI + _indicatorRight.ListFingers[4].AI) / 2) + ((_indicatorLeft.ListFingers[4].RI + _indicatorRight.ListFingers[4].RI) / 2)), 2);
            }
        }

        public ObservableCollection<SortedItemWithIndex> ListTMBS
        {
            get
            {
                var list = new ObservableCollection<SortedItemWithIndex>() { new SortedItemWithIndex { Label="XH",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[0].PI),2)},
                                                                    new SortedItemWithIndex { Label="LG",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[1].PI),2)},
                                                                    new SortedItemWithIndex { Label="VĐ",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[2].PI) + double.Parse(_indicatorRight.ListFingers[2].PI)) / 2),2)},
                                                                    new SortedItemWithIndex { Label="NN",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[3].PI),2)},
                                                                    new SortedItemWithIndex { Label="TN",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[1].PI),2)},
                                                                    new SortedItemWithIndex { Label="TG",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[4].PI) + double.Parse(_indicatorRight.ListFingers[4].PI)) / 2),2)},
                                                                    new SortedItemWithIndex { Label="AN",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[3].PI),2)},
                                                                    new SortedItemWithIndex { Label="NT",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[0].PI),2)}};

                foreach (var item in list)
                {
                    item.Index = list.OrderByDescending(x => x.Value).ToList().IndexOf(item) + 1;
                }

                return list;
            }
        }

        public ObservableCollection<SortedItemWithIndex> ListTMTD
        {
            get
            {
                var list = new ObservableCollection<SortedItemWithIndex>() { new SortedItemWithIndex { Label="XH",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[0].PI) * (_indicatorLeft.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[0].CAP)),2)},
                                                                    new SortedItemWithIndex { Label="LG",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[1].PI) * (_indicatorRight.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[1].CAP)),2)},
                                                                    new SortedItemWithIndex { Label="VĐ",
                                                                                    Value = Math.Round((((double.Parse(_indicatorLeft.ListFingers[2].PI) * (_indicatorLeft.ListFingers[2].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[2].CAP))) + (double.Parse(_indicatorRight.ListFingers[2].PI) * (_indicatorRight.ListFingers[2].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[2].CAP)))) / 2),2)},
                                                                    new SortedItemWithIndex { Label="NN",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[3].PI) * (_indicatorRight.ListFingers[3].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[3].CAP)),2)},
                                                                    new SortedItemWithIndex { Label="TN",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[1].PI) * (_indicatorLeft.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[1].CAP)),2)},
                                                                    new SortedItemWithIndex { Label="TG",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[4].PI) * (_indicatorLeft.ListFingers[4].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[4].CAP))) + (double.Parse(_indicatorRight.ListFingers[4].PI) * (_indicatorRight.ListFingers[4].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[4].CAP)))) / 2,2)},
                                                                    new SortedItemWithIndex { Label="AN",Value= Math.Round(double.Parse(_indicatorLeft.ListFingers[3].PI) * (_indicatorLeft.ListFingers[3].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[3].CAP)),2)},
                                                                    new SortedItemWithIndex { Label="NT",Value= Math.Round(double.Parse(_indicatorRight.ListFingers[0].PI) * (_indicatorRight.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[0].CAP)),2)}};

                foreach (var item in list)
                {
                    item.Index = list.OrderByDescending(x => x.Value).ToList().IndexOf(item) + 1;
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
                    item.Index = list.OrderByDescending(x => x.Value).ToList().IndexOf(item) + 1;
                }

                return list;
            }
        }

        public ObservableCollection<FingerIndicator> SortedListData
        {
            get
            {
                var list = new ObservableCollection<FingerIndicator>();
                for (int i = 0; i < _indicatorLeft.ListFingers.Count; i++)
                {
                    _indicatorLeft.ListFingers[i].Name = $"L{i + 1}";
                    list.Add(_indicatorLeft.ListFingers[i]);
                }
                for (int i = 0; i < _indicatorRight.ListFingers.Count; i++)
                {
                    _indicatorRight.ListFingers[i].Name = $"R{i + 1}";
                    list.Add(_indicatorRight.ListFingers[i]);
                }

                var listOrderByDescendingPI = list.OrderByDescending(x => double.Parse(x.PI)).ToList();

                foreach (var item in listOrderByDescendingPI)
                {
                    item.IndexPI = listOrderByDescendingPI.IndexOf(item) + 1;
                }


                var listOrderByDescendingRI = list.OrderByDescending(x => x.RI).ToList();
                foreach (var item in listOrderByDescendingRI)
                {
                    item.IndexRI = listOrderByDescendingRI.IndexOf(item) + 1;
                }

                return list;
            }
        }
        public ObservableCollection<SortedItemWithIndex> ListNLNN
        {
            get
            {
                var list = new ObservableCollection<SortedItemWithIndex>() { new SortedItemWithIndex { Label="KT _ CN",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[1].PI) + double.Parse(_indicatorRight.ListFingers[4].PI))) / 4,2)},
                                                                             new SortedItemWithIndex { Label="QT",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[1].PI))) / 3,2)},
                                                                             new SortedItemWithIndex { Label="TC",Value= Math.Round(((double.Parse(_indicatorRight.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[1].PI) + double.Parse(_indicatorRight.ListFingers[4].PI))) / 3,2)},
                                                                             new SortedItemWithIndex { Label="YK",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[1].PI) + double.Parse(_indicatorRight.ListFingers[2].PI) + double.Parse(_indicatorRight.ListFingers[4].PI))) / 5,2)},
                                                                             new SortedItemWithIndex { Label="DV",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[1].PI) + double.Parse(_indicatorRight.ListFingers[3].PI))) / 3,2)},
                                                                             new SortedItemWithIndex { Label="MT",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[1].PI) + double.Parse(_indicatorLeft.ListFingers[4].PI) + double.Parse(_indicatorRight.ListFingers[4].PI))) / 3,2)},
                                                                             new SortedItemWithIndex { Label="AN",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[1].PI) + double.Parse(_indicatorLeft.ListFingers[3].PI) + double.Parse(_indicatorRight.ListFingers[3].PI))) / 3,2)},
                                                                             new SortedItemWithIndex { Label="TT",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[2].PI) + double.Parse(_indicatorRight.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[1].PI))) / 3,2)},
                                                                             new SortedItemWithIndex { Label="L",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[1].PI) + double.Parse(_indicatorRight.ListFingers[3].PI))) / 4,2)},
                                                                             new SortedItemWithIndex { Label="TK _ KT",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[1].PI) + double.Parse(_indicatorLeft.ListFingers[4].PI) + double.Parse(_indicatorRight.ListFingers[1].PI) + double.Parse(_indicatorRight.ListFingers[2].PI) + double.Parse(_indicatorRight.ListFingers[4].PI))) / 5,2)},
                                                                             new SortedItemWithIndex { Label="BC",Value=  Math.Round(((double.Parse(_indicatorLeft.ListFingers[4].PI) + double.Parse(_indicatorRight.ListFingers[1].PI) + double.Parse(_indicatorRight.ListFingers[3].PI))) / 3,2)},
                                                                             new SortedItemWithIndex { Label="NN",Value=  Math.Round(((double.Parse(_indicatorLeft.ListFingers[3].PI) + double.Parse(_indicatorRight.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[3].PI))) / 3,2)},
                                                                             new SortedItemWithIndex { Label="SP",Value=  Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[3].PI) + double.Parse(_indicatorRight.ListFingers[4].PI))) / 3,2)}};

                foreach (var item in list)
                {
                    item.Index = list.OrderByDescending(x => x.Value).ToList().IndexOf(item) + 1;
                }

                return list;
            }
        }

        public ObservableCollection<SortedItemWithIndex> ListNLNN_Sub
        {
            get
            {
                var list = new ObservableCollection<SortedItemWithIndex>() { new SortedItemWithIndex { Label="KT _ CN",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) * (_indicatorLeft.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[0].CAP)))
                                                                                 + (double.Parse(_indicatorRight.ListFingers[0].PI) * (_indicatorRight.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[0].CAP)))
                                                                                 + (double.Parse(_indicatorRight.ListFingers[1].PI) * (_indicatorRight.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[1].CAP)))
                                                                                 + (double.Parse(_indicatorRight.ListFingers[4].PI) * (_indicatorRight.ListFingers[4].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[4].CAP))))
                                                                                 / 4,2)},
                                                                             new SortedItemWithIndex { Label="QT",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) * (_indicatorLeft.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[0].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[0].PI) * (_indicatorRight.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[0].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[1].PI) * (_indicatorRight.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[1].CAP))))
                                                                                / 3,2)},
                                                                             new SortedItemWithIndex { Label="TC",Value= Math.Round(((double.Parse(_indicatorRight.ListFingers[0].PI) * (_indicatorRight.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[0].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[1].PI) * (_indicatorRight.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[1].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[4].PI) * (_indicatorRight.ListFingers[4].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[4].CAP))))
                                                                                / 3,2)},
                                                                             new SortedItemWithIndex { Label="YK",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) * (_indicatorLeft.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[0].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[0].PI) * (_indicatorRight.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[0].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[1].PI) * (_indicatorRight.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[1].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[2].PI) * (_indicatorRight.ListFingers[2].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[2].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[4].PI) * (_indicatorRight.ListFingers[4].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[4].CAP))))
                                                                                / 5,2)},
                                                                             new SortedItemWithIndex { Label="DV",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) * (_indicatorLeft.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[0].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[1].PI) * (_indicatorRight.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[1].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[3].PI) * (_indicatorRight.ListFingers[3].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[3].CAP)))) 
                                                                                / 3,2)},
                                                                             new SortedItemWithIndex { Label="MT",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[1].PI) * (_indicatorLeft.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[1].CAP)))
                                                                                + (double.Parse(_indicatorLeft.ListFingers[4].PI) * (_indicatorLeft.ListFingers[4].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[4].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[4].PI) * (_indicatorRight.ListFingers[4].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[4].CAP)))) 
                                                                                / 3,2)},
                                                                             new SortedItemWithIndex { Label="AN",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[1].PI) * (_indicatorLeft.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[1].CAP)))
                                                                                + (double.Parse(_indicatorLeft.ListFingers[3].PI) * (_indicatorLeft.ListFingers[3].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[3].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[3].PI) * (_indicatorRight.ListFingers[3].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[3].CAP)))) 
                                                                                / 3,2)},
                                                                             new SortedItemWithIndex { Label="TT",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[2].PI) * (_indicatorLeft.ListFingers[2].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[2].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[0].PI) * (_indicatorRight.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[0].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[1].PI) * (_indicatorRight.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[1].CAP))))
                                                                                / 3,2)},
                                                                             new SortedItemWithIndex { Label="L",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) * (_indicatorLeft.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[0].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[0].PI) * (_indicatorRight.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[0].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[1].PI) * (_indicatorRight.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[1].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[3].PI) * (_indicatorRight.ListFingers[3].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[3].CAP))))
                                                                                / 4,2)},
                                                                             new SortedItemWithIndex { Label="TK _ KT",Value= Math.Round(((double.Parse(_indicatorLeft.ListFingers[1].PI) * (_indicatorLeft.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[1].CAP)))
                                                                                + (double.Parse(_indicatorLeft.ListFingers[4].PI) * (_indicatorLeft.ListFingers[4].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[4].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[1].PI) * (_indicatorRight.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[1].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[2].PI) * (_indicatorRight.ListFingers[2].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[2].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[4].PI) * (_indicatorRight.ListFingers[4].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[4].CAP)))) 
                                                                                / 5,2)},
                                                                             new SortedItemWithIndex { Label="BC",Value=  Math.Round(((double.Parse(_indicatorLeft.ListFingers[4].PI) * (_indicatorLeft.ListFingers[4].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[4].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[1].PI) * (_indicatorRight.ListFingers[1].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[1].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[3].PI) * (_indicatorRight.ListFingers[3].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[3].CAP))))
                                                                                / 3,2)},
                                                                             new SortedItemWithIndex { Label="NN",Value=  Math.Round(((double.Parse(_indicatorLeft.ListFingers[3].PI) * (_indicatorLeft.ListFingers[3].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[3].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[0].PI) * (_indicatorRight.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[0].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[3].PI) * (_indicatorRight.ListFingers[3].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[3].CAP))))
                                                                                / 3,2)},
                                                                             new SortedItemWithIndex { Label="SP",Value=  Math.Round(((double.Parse(_indicatorLeft.ListFingers[0].PI) * (_indicatorLeft.ListFingers[0].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorLeft.ListFingers[0].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[3].PI) * (_indicatorRight.ListFingers[3].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[3].CAP)))
                                                                                + (double.Parse(_indicatorRight.ListFingers[4].PI) * (_indicatorRight.ListFingers[4].CAP.Equals("Không xác định")? 1.00 :double.Parse(_indicatorRight.ListFingers[4].CAP)))) 
                                                                                / 3,2)}};

                foreach (var item in list)
                {
                    item.Index = list.OrderByDescending(x => x.Value).ToList().IndexOf(item) + 1;
                }

                return list;
            }
        }

        public ObservableCollection<SortedItemWithIndex> ListNKBS
        {
            get
            {
                var list = new ObservableCollection<SortedItemWithIndex>() { new SortedItemWithIndex { Label="AN",Value= Math.Round((double.Parse(_indicatorLeft.ListFingers[3].PI) + double.Parse(_indicatorRight.ListFingers[3].PI)),2)},
                                                                             new SortedItemWithIndex { Label="MT",Value= Math.Round((double.Parse(_indicatorLeft.ListFingers[4].PI) + double.Parse(_indicatorRight.ListFingers[4].PI)),2)},
                                                                             new SortedItemWithIndex { Label="TD _ ST",Value=  Math.Round((double.Parse(_indicatorLeft.ListFingers[1].PI) + double.Parse(_indicatorRight.ListFingers[1].PI)),2)},
                                                                             new SortedItemWithIndex { Label="LĐ",Value=  Math.Round((double.Parse(_indicatorLeft.ListFingers[0].PI) + double.Parse(_indicatorRight.ListFingers[0].PI)),2)},
                                                                             new SortedItemWithIndex { Label="VĐ",Value=  Math.Round((double.Parse(_indicatorLeft.ListFingers[2].PI) + double.Parse(_indicatorRight.ListFingers[2].PI)),2)}};

                foreach (var item in list)
                {
                    item.Index = list.OrderByDescending(x => x.Value).ToList().IndexOf(item) + 1;
                }

                return list;
            }
        }

        public double ND
        {
            get
            {
                return Math.Round(((_indicatorLeft.ListFingers[0].RI + _indicatorLeft.ListFingers[1].RI) * 100) / (_indicatorLeft.ListFingers[0].RI + _indicatorLeft.ListFingers[1].RI + _indicatorRight.ListFingers[0].RI + _indicatorRight.ListFingers[1].RI),2);
            }
        }
        public double PT
        {
            get
            {
                return Math.Round(((_indicatorRight.ListFingers[0].RI + _indicatorRight.ListFingers[1].RI) * 100) / (_indicatorLeft.ListFingers[0].RI + _indicatorLeft.ListFingers[1].RI + _indicatorRight.ListFingers[0].RI + _indicatorRight.ListFingers[1].RI),2);
            }
        }
    }
}
