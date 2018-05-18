﻿using GalaSoft.MvvmLight;

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
                _type = value;
                if (_type.StartsWith("U"))
                {
                    IsCAPEnabled = false;
                    CAP = "1.00";
                }
                else if (_type.StartsWith("A"))
                {
                    IsCAPEnabled = false;
                    CAP = "Không xác định";
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
    }
}