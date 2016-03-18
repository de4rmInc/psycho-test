using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PsychoTest.Messages;
using PsychoTest.Models;

namespace PsychoTest.ViewModels
{
    public class MainWindowViewModel : TabViewModelBase
    {
        private RelayCommand<object> _createNewTestCommand;

        private RelayCommand<object> _loadTestResutlsCommand;
        private TestViewModel _testTab;
        private TestResultViewModel _resultsTab;

        public MainWindowViewModel()
        {
            TestTab = new TestViewModel(TestInformation.Questions);
            ResultsTab = new TestResultViewModel();
            Messenger.Default.Register<ShowResultsMessage>(this, ShowResults);
            Selected = true;
        }

        public TestViewModel TestTab
        {
            get { return _testTab; }
            set { Set(ref _testTab, value); }
        }

        public TestResultViewModel ResultsTab
        {
            get { return _resultsTab; }
            set { Set(ref _resultsTab, value); }
        }

        public RelayCommand<object> CreateNewTestCommand
        {
            get
            {
                return _createNewTestCommand ??
                       (_createNewTestCommand = new RelayCommand<object>(CreateNewTest, CanCreateNewTest));
            }
        }

        public RelayCommand<object> LoadTestResutlsCommand
        {
            get
            {
                return _loadTestResutlsCommand ??
                       (_loadTestResutlsCommand =
                           new RelayCommand<object>(LoadTestResutls, CanLoadTestResutls));
            }
        }

        private bool CanLoadTestResutls(object o)
        {
            return true;
        }

        private void LoadTestResutls(object o)
        {
            SelectTab(MainWindowTab.ResultsPage);
        }

        private bool CanCreateNewTest(object o)
        {
            return true;
        }

        private void CreateNewTest(object o)
        {
            TestTab = new TestViewModel(TestInformation.Questions) {Selected = true};
        }

        private void ShowResults(ShowResultsMessage resultsMessage)
        {
            ResultsTab = new TestResultViewModel(resultsMessage.TestAnswers);
            TestTab = new TestViewModel(TestInformation.Questions);
            SelectTab(MainWindowTab.ResultsPage);
        }

        private void SelectTab(MainWindowTab tab)
        {
            switch (tab)
            {
                case MainWindowTab.StartPage:
                    Selected = true;
                    break;
                case MainWindowTab.TestPage:
                    if (TestTab != null)
                        TestTab.Selected = true;
                    break;
                case MainWindowTab.ResultsPage:
                    if (ResultsTab != null)
                        ResultsTab.Selected = true;
                    break;
                default:
                    Selected = true;
                    break;
            }
        }
    }

    public enum MainWindowTab
    {
        StartPage,
        TestPage,
        ResultsPage
    }
}