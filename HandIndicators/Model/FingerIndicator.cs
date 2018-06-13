using GalaSoft.MvvmLight;
using System;

namespace HandIndicators.Model
{
    public class FingerIndicator: ObservableObject
    {
        private string _pi;
        public string PI
        {
            get { return _pi; }
            set
            {
                _pi = value;
                RaisePropertyChanged(nameof(PI));
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
                    CAP = "Không xác định";
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
                    return !string.IsNullOrEmpty(Type) && (Type.StartsWith("W") || Type.StartsWith("U") || Type.StartsWith("A")) && double.Parse(PI) > 0 && (CAP.Equals("Không xác định") || double.Parse(CAP) > 0);
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
                RaisePropertyChanged();
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
                    double temp1 = 0;
                    double temp2 = 0;

                    if (!string.IsNullOrEmpty(_delta1))
                        temp1 = double.Parse(_delta1);
                    PI = temp1.ToString();

                    if (!string.IsNullOrEmpty(_delta2))
                        temp2 = double.Parse(_delta2);
                    else
                    {
                        CAP = "1";
                        return;
                    }
                    CAP = (temp1 > temp2) ? (Math.Round((temp2 / temp1 + 1),2).ToString()) : (Math.Round((temp1 / temp2 + 1),2).ToString());
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
                    CAP = (temp1 > temp2) ? (Math.Round((temp2 / temp1 + 1),2).ToString()) : (Math.Round((temp1 / temp2 + 1),2).ToString());
                }
            }
        }
    }
}
