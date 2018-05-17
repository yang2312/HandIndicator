using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIndicators.Model
{
    public class SortedItem: ObservableObject
    {
        public string Label { get; set; }
        public double Value { get; set; }
    }
}
