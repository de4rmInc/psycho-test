using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using PsychoTest.Common;
using PsychoTest.Models;

namespace PsychoTest.ViewModels
{
    public class TestResultViewModel : TabViewModelBase
    {
        private TestAnswers _testAnswers;
        private TestProcessor _testProcessor;
        private List<TestResultItem> _profQualities;
        private List<TestResultItem> _personalQualities;
        private RelayCommand<object> _saveTestResultCommand;

        
        public TestResultViewModel(TestAnswers testAnswers)
        {
            this._testAnswers = testAnswers;
            this.TestName = testAnswers.TestName;
            this._testProcessor = new TestProcessor();
        }

        public string TestName { get; }

        public List<TestResultItem> ProfQualities
        {
            get { return _profQualities ?? (_profQualities = CalculateProQualities()); }
        }

        public List<TestResultItem> PersonalQualities
        {
            get { return _personalQualities ?? (_personalQualities = CalculatePersonalQualities()); }
        }

        private List<TestResultItem> CalculateProQualities()
        {
            var proQualities = _testProcessor.CalcProQualities(_testAnswers);
            return proQualities;

            return new List<TestResultItem>()
            {
                new TestResultItem(new Participant() {FirstName = "alex 1"}) {MutualSum = 2, TotalSum = 3},
                new TestResultItem(new Participant() {FirstName = "alex 2"}) { MutualSum = 4, TotalSum = 1},
                new TestResultItem(new Participant() {FirstName = "alex 3"}) { MutualSum = 1, TotalSum = 2},
            };
        }

        private List<TestResultItem> CalculatePersonalQualities()
        {
            var personalQualities = _testProcessor.CalcPersonalQualities(_testAnswers);
            return personalQualities;

            return new List<TestResultItem>()
            {
                new TestResultItem(new Participant() {FirstName = "hex 2"}) {MutualSum = 2, TotalSum = 3},
                new TestResultItem(new Participant() {FirstName = "hex 1"}) {MutualSum = 1, TotalSum = 2},
                new TestResultItem(new Participant() {FirstName = "hex 3"}) {MutualSum = 3, TotalSum = 4},
            };
        }


        public RelayCommand<object> SaveTestResultCommand
        {
            get
            {
                return _saveTestResultCommand ??
                       (_saveTestResultCommand = new RelayCommand<object>(SaveTestResult, CanSaveTestResult));
            }
        }

        private bool CanSaveTestResult(object o)
        {
            return true;
        }

        private void SaveTestResult(object o)
        {
            var saveDialog = new SaveFileDialog()
            {
                Filter = Constants.TEST_EXT_FILTER,
                CheckFileExists = false,
                Title = $"Сохранить результат теста \"{TestName}\"",
                AddExtension = true,
            };

            if (saveDialog.ShowDialog(Application.Current.MainWindow) == true)
            {
                try
                {
                    using (var stream = saveDialog.OpenFile())
                    {
                        SerializationHelper.Serialize(stream, _testAnswers);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

    }

    public class TestResultItem
    {
        public TestResultItem(Participant participant)
        {
            Participant = participant;
        }

        public Participant Participant { get; }
        public string FullName { get { return Participant.FullName; } }
        public int TotalSum { get; set; }
        public int MutualSum { get; set; }
    }
}
