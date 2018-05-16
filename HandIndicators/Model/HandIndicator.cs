using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIndicators.Model
{
    public class HandIndicator: ObservableObject
    {
        private string _atd;
        public string ATD
        {
            get { return _atd; }
            set
            {
                _atd = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<FingerIndicator> _listFingers;
        public ObservableCollection<FingerIndicator> ListFingers
        {
            get { return _listFingers ?? new ObservableCollection<FingerIndicator>(); }
            set
            {
                _listFingers = value;
                RaisePropertyChanged();
            }
        }

        public HandIndicator(ObservableCollection<FingerIndicator> listFingers, string atd)
        {
            ListFingers = listFingers;
            ATD = atd;
        }
    }
}
