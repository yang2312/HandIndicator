using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    return !string.IsNullOrEmpty(Type) && double.Parse(PI) > 0 && double.Parse(CAP) > 0;
                }
                catch
                {
                    return false;
                }
                
            }
        }
    }
}
