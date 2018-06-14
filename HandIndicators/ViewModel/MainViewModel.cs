using GalaSoft.MvvmLight;
using HandIndicators.Model;
using System.Collections.ObjectModel;

namespace HandIndicators.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Properties
        private FingerIndicator _fingerL1;
        public FingerIndicator FingerL1
        {
            get { return _fingerL1; }
            set
            {
                _fingerL1 = value;
                RaisePropertyChanged();
            }
        }
        private FingerIndicator _fingerL2;
        public FingerIndicator FingerL2
        {
            get { return _fingerL2; }
            set
            {
                _fingerL2 = value;
                RaisePropertyChanged();
            }
        }
        private FingerIndicator _fingerL3;
        public FingerIndicator FingerL3
        {
            get { return _fingerL3; }
            set
            {
                _fingerL3 = value;
                RaisePropertyChanged();
            }
        }
        private FingerIndicator _fingerL4;
        public FingerIndicator FingerL4
        {
            get { return _fingerL4; }
            set
            {
                _fingerL4 = value;
                RaisePropertyChanged();
            }
        }
        private FingerIndicator _fingerL5;
        public FingerIndicator FingerL5
        {
            get { return _fingerL5; }
            set
            {
                _fingerL5 = value;
                RaisePropertyChanged();
            }
        }
        private FingerIndicator _fingerR1;
        public FingerIndicator FingerR1
        {
            get { return _fingerR1; }
            set
            {
                _fingerR1 = value;
                RaisePropertyChanged();
            }
        }
        private FingerIndicator _fingerR2;
        public FingerIndicator FingerR2
        {
            get { return _fingerR2; }
            set
            {
                _fingerR2 = value;
                RaisePropertyChanged();
            }
        }
        private FingerIndicator _fingerR3;
        public FingerIndicator FingerR3
        {
            get { return _fingerR3; }
            set
            {
                _fingerR3 = value;
                RaisePropertyChanged();
            }
        }
        private FingerIndicator _fingerR4;
        public FingerIndicator FingerR4
        {
            get { return _fingerR4; }
            set
            {
                _fingerR4 = value;
                RaisePropertyChanged();
            }
        }
        private FingerIndicator _fingerR5;
        public FingerIndicator FingerR5
        {
            get { return _fingerR5; }
            set
            {
                _fingerR5 = value;
                RaisePropertyChanged();
            }
        }

        private string _atdL;
        public string ATDL
        {
            get { return _atdL; }
            set
            {
                _atdL = value;
                RaisePropertyChanged();
            }
        }
        private string _atdR;
        public string ATDR
        {
            get { return _atdR; }
            set
            {
                _atdR = value;
                RaisePropertyChanged();
            }
        }
        private IndicatorResult _indicatorResult;
        public IndicatorResult IndicatorResult
        {
            get { return _indicatorResult; }
            set
            {
                _indicatorResult = value;
                RaisePropertyChanged();
            }
        }
        private string _year;
        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
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
        #endregion

        #region Constructor
        public MainViewModel()
        {
            Year = "1987";
            ATDL = "37.5";
            ATDR = "36.5";
            FingerL1 = new FingerIndicator() { Type="U",Delta1="19.5"};
            FingerL2 = new FingerIndicator() { Type = "U", Delta1 = "11.5" };
            FingerL3 = new FingerIndicator() { Type = "A",PI="3.8" };
            FingerL4 = new FingerIndicator() { Type = "U", Delta1 = "11.2" };
            FingerL5 = new FingerIndicator() { Type = "A", PI="5" };
            FingerR1 = new FingerIndicator() { Type = "W", Delta1 = "21.4",Delta2="18.2" };
            FingerR2 = new FingerIndicator() { Type = "W", Delta1 = "13.8",Delta2="17.2" };
            FingerR3 = new FingerIndicator() { Type = "A", PI="6.2" };
            FingerR4 = new FingerIndicator() { Type = "W", Delta1 = "16.4",Delta2="8.5" };
            FingerR5 = new FingerIndicator() { Type = "U", Delta1 = "12" };
        }
        #endregion

        #region Methods
        public void Calculate()
        {
            IndicatorResult = new IndicatorResult(new HandIndicator(new ObservableCollection<FingerIndicator>() { FingerL1,FingerL2,FingerL3,FingerL4,FingerL5},ATDL), 
                                                  new HandIndicator(new ObservableCollection<FingerIndicator>() { FingerR1, FingerR2, FingerR3, FingerR4, FingerR5}, ATDL),Year);
            RaisePropertyChanged(nameof(IndicatorResult));
        }
        public bool IsDataValidated()
        {
            try
            {
                return !string.IsNullOrEmpty(Year) && (double.Parse(ATDL) > 0) && (double.Parse(ATDR) > 0) && FingerL1.IsValidated && FingerL2.IsValidated && FingerL3.IsValidated
                && FingerL4.IsValidated && FingerL5.IsValidated && FingerR1.IsValidated && FingerR2.IsValidated && FingerR3.IsValidated && FingerR4.IsValidated && FingerR5.IsValidated;
            }
            catch
            {
                return false;
            }
            
        }
        #endregion
        
    }
}