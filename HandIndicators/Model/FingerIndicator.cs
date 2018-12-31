using GalaSoft.MvvmLight;
using System;

namespace HandIndicators.Model
{
    public class FingerIndicator : ObservableObject
    {
        public Gender Gender { get; set; }
        private string _pi;
        public string PI
        {
            get { return _pi; }
            set
            {
                _pi = value;
                if (!string.IsNullOrEmpty(_pi))
                    switch (Gender)
                    {
                        case Gender.Male:
                            if (double.Parse(_pi) < 6)
                            {
                                OverallPI = "Rất thấp";
                            }
                            else if (double.Parse(_pi) >= 6 && double.Parse(_pi) <= 9)
                            {
                                OverallPI = "Thấp";
                            }
                            else if (double.Parse(_pi) > 9 && double.Parse(_pi) < 12.5)
                            {
                                OverallPI = "TB";
                            }
                            else if (double.Parse(_pi) >= 12.5 && double.Parse(_pi) < 14.5)
                            {
                                OverallPI = "Khá";
                            }
                            else if (double.Parse(_pi) >= 14.5 && double.Parse(_pi) <= 19)
                            {
                                OverallPI = "Cao";
                            }
                            else
                            {
                                OverallPI = "Rất cao";
                            }
                            break;
                        default:
                            if (double.Parse(_pi) < 5)
                            {
                                OverallPI = "Rất thấp";
                            }
                            else if (double.Parse(_pi) >= 5 && double.Parse(_pi) <= 8)
                            {
                                OverallPI = "Thấp";
                            }
                            else if (double.Parse(_pi) > 8 && double.Parse(_pi) < 11)
                            {
                                OverallPI = "TB";
                            }
                            else if (double.Parse(_pi) >= 11 && double.Parse(_pi) < 12.5)
                            {
                                OverallPI = "Khá";
                            }
                            else if (double.Parse(_pi) >= 12.5 && double.Parse(_pi) <= 17)
                            {
                                OverallPI = "Cao";
                            }
                            else
                            {
                                OverallPI = "Rất cao";
                            }
                            break;
                    }
                RaisePropertyChanged(nameof(PI));
                if (Type.StartsWith("A"))
                {
                    IsCAPEnabled = false;
                    if (!string.IsNullOrEmpty(PI) && double.Parse(PI) > 0)
                        CAP = Math.Round(AI / double.Parse(PI), 2).ToString();
                    else CAP = "Không xác định";
                    Delta1 = "Không xác định";
                    Delta2 = "Không xác định";
                    RaisePropertyChanged(nameof(CAP));
                }
            }
        }
        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value.ToUpper();
                if (_type.StartsWith("U"))
                {
                    IsCAPEnabled = false;
                    CAP = "1.00";
                    Delta1 = "0";
                    Delta2 = "0";
                }
                else if (_type.StartsWith("A"))
                {
                    IsCAPEnabled = false;
                    if (!string.IsNullOrEmpty(PI) && double.Parse(PI) > 0)
                        CAP = Math.Round(AI / double.Parse(PI), 2).ToString();
                    else CAP = "Không xác định";
                    Delta1 = "Không xác định";
                    Delta2 = "Không xác định";
                }
                else
                {
                    IsCAPEnabled = true;
                    if (!string.IsNullOrEmpty(CAP) && CAP.Equals("Không xác định"))
                        CAP = string.Empty;

                }
                RaisePropertyChanged(nameof(Type));
            }
        }
        private string _cap;
        public string CAP
        {
            get { return _cap; }
            set
            {
                _cap = value;
                RaisePropertyChanged(nameof(CAP));
            }
        }

        public bool IsValidated
        {
            get
            {
                try
                {
                    return !string.IsNullOrEmpty(Type) && (Type.StartsWith("W") || Type.StartsWith("U") || Type.StartsWith("A")) && double.Parse(PI) > 0 && (CAP.Equals("Không xác định") || double.Parse(CAP) >= 0);
                }
                catch
                {
                    return false;
                }

            }
        }
        private double _ai;
        public double AI
        {
            get
            {
                return _ai;
            }
            set
            {
                _ai = value;
                if (_ai < 10)
                {
                    OverallAI = "Thấp";
                }
                else if (_ai >= 10 && _ai <= 15)
                {
                    OverallAI = "TB";
                }
                else if (_ai > 15 && _ai <= 22)
                {
                    OverallAI = "Khá";
                }
                else if (_ai > 22 && _ai <= 30)
                {
                    OverallAI = "Tốt";
                }
                else
                {
                    OverallAI = "Xuất sắc";
                }
                RaisePropertyChanged();
                if (Type.StartsWith("A"))
                {
                    IsCAPEnabled = false;
                    if (!string.IsNullOrEmpty(PI) && double.Parse(PI) > 0)
                        CAP = Math.Round(AI / double.Parse(PI), 2).ToString();
                    else CAP = "Không xác định";
                    Delta1 = "Không xác định";
                    Delta2 = "Không xác định";
                    RaisePropertyChanged(nameof(CAP));
                }
            }
        }

        private double _ri;
        public double RI
        {
            get
            {
                return _ri;
            }
            set
            {
                _ri = value;
                switch (Gender)
                {
                    case Gender.Male:
                        if (_ri < 6)
                        {
                            OverallRI = "Rất thấp";
                        }
                        else if (_ri >= 6 && _ri <= 9)
                        {
                            OverallRI = "Thấp";
                        }
                        else if (_ri > 9 && _ri < 12.5)
                        {
                            OverallRI = "TB";
                        }
                        else if (_ri >= 12.5 && _ri < 14.5)
                        {
                            OverallRI = "Khá";
                        }
                        else if (_ri >= 14.5 && _ri <= 19)
                        {
                            OverallRI = "Cao";
                        }
                        else
                        {
                            OverallRI = "Rất cao";
                        }
                        break;
                    default:
                        if (_ri < 5)
                        {
                            OverallRI = "Rất thấp";
                        }
                        else if (_ri >= 5 && _ri <= 8)
                        {
                            OverallRI = "Thấp";
                        }
                        else if (_ri > 8 && _ri < 11)
                        {
                            OverallRI = "TB";
                        }
                        else if (_ri >= 11 && _ri < 12.5)
                        {
                            OverallRI = "Khá";
                        }
                        else if (_ri >= 12.5 && _ri <= 17)
                        {
                            OverallRI = "Cao";
                        }
                        else
                        {
                            OverallRI = "Rất cao";
                        }
                        break;
                }
                RaisePropertyChanged();
            }
        }
        private int _indexPI;
        public int IndexPI
        {
            get
            {
                return _indexPI;
            }
            set
            {
                _indexPI = value;
                RaisePropertyChanged();
            }
        }

        private int _indexRI;
        public int IndexRI
        {
            get
            {
                return _indexRI;
            }
            set
            {
                _indexRI = value;
                RaisePropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private bool _isCAPEnabled = true;
        public bool IsCAPEnabled
        {
            get { return _isCAPEnabled; }
            set
            {
                _isCAPEnabled = value;
                RaisePropertyChanged();
            }
        }

        private string _delta1;
        public string Delta1
        {
            get { return _delta1; }
            set
            {
                _delta1 = value;
                RaisePropertyChanged();
                if (Type.StartsWith("U"))
                {
                    PI = _delta1;
                }
                else if (Type.StartsWith("W"))
                {
                    PI = "0";

                    double temp1 = 0;
                    double temp2 = 0;

                    if (!string.IsNullOrEmpty(_delta1))
                        temp1 = double.Parse(_delta1);

                    if (!string.IsNullOrEmpty(_delta2))
                        temp2 = double.Parse(_delta2);
                    else
                    {
                        CAP = "1";
                        return;
                    }
                    CAP = (temp1 > temp2) ? (Math.Round((temp2 / temp1 + 1), 2).ToString()) : (Math.Round((temp1 / temp2 + 1), 2).ToString());
                }
            }
        }
        private string _delta2;
        public string Delta2
        {
            get { return _delta2; }
            set
            {
                _delta2 = value;
                RaisePropertyChanged();
                if (Type.StartsWith("W"))
                {
                    double temp1 = 0;
                    double temp2 = 0;

                    if (!string.IsNullOrEmpty(_delta1))
                        temp1 = double.Parse(_delta1);
                    else
                    {
                        CAP = "1";
                        return;
                    }
                    if (!string.IsNullOrEmpty(_delta2))
                        temp2 = double.Parse(_delta2);
                    CAP = (temp1 > temp2) ? (Math.Round((temp2 / temp1 + 1), 2).ToString()) : (Math.Round((temp1 / temp2 + 1), 2).ToString());
                }
            }
        }
        private string _overallPI;
        public string OverallPI
        {
            get { return _overallPI; }
            set
            {
                _overallPI = value;
                RaisePropertyChanged();
            }

        }
        private string _overallRI;
        public string OverallRI
        {
            get { return _overallRI; }
            set
            {
                _overallRI = value;
                RaisePropertyChanged();
            }

        }
        private string _overallAI;
        public string OverallAI
        {
            get { return _overallAI; }
            set
            {
                _overallAI = value;
                RaisePropertyChanged();
            }

        }
    }
}
