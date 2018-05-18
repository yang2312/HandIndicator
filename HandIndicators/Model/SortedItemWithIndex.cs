using GalaSoft.MvvmLight;

namespace HandIndicators.Model
{
    public class SortedItemWithIndex: ObservableObject
    {
        public string Label { get; set; }
        public double Value { get; set; }
        public int Index { get; set; }
    }
}
