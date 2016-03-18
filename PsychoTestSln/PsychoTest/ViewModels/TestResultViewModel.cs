using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using PsychoTest.Models;

namespace PsychoTest.ViewModels
{
    public class TestResultViewModel : TabViewModelBase
    {
        private TestAnswers _testAnswers;

        public TestResultViewModel()
        {
            
        }

        public TestResultViewModel(TestAnswers testAnswers)
        {
            this._testAnswers = testAnswers;
        }
    }
}
