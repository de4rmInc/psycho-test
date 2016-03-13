using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using PsychoTest.Messages;

namespace PsychoTest.ViewModels
{
    public class TestItemViewModel : ViewModelBase
    {
        private string _question;
        private ObservableCollection<TestAnswerViewModel> _answers;

        public TestItemViewModel(string question)
        {
            _question = question;
            MessengerInstance.Register<AnsweredMessage>(this, Answered);
        }

        public bool GotAnswer { get; private set; }

        public ObservableCollection<TestAnswerViewModel> Answers
        {
            get { return _answers; }
            private set { Set(ref _answers, value); }
        }

        public string Question
        {
            get { return _question; }
            set { Set(ref _question, value); }
        }

        public void LoadAnswers(List<TestAnswerViewModel> answers)
        {
            Answers = new ObservableCollection<TestAnswerViewModel>(answers);
        }

        private void Answered(AnsweredMessage answeredMessage)
        {
            GotAnswer = _answers.Any(model => model.Selected);
            MessengerInstance.Send(new GotAnswersMessage { Anwsered = GotAnswer });
        }
    }

    public class TestAnswerViewModel : ViewModelBase
    {
        private bool _selected;

        public string Answer { get; set; }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                Set(ref _selected, value, true);
                MessengerInstance.Send(new AnsweredMessage {Anwsered = value });
            }
        }
    }
}
