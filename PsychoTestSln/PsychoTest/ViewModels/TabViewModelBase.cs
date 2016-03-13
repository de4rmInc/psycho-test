using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PsychoTest.ViewModels
{
    public class TabViewModelBase : ViewModelBase
    {
        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }
    }
}
