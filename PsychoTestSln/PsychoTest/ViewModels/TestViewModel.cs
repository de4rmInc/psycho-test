using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.CommandWpf;
using PsychoTest.Messages;
using PsychoTest.Models;

namespace PsychoTest.ViewModels
{
    public class TestViewModel : TabViewModelBase
    {
        private const int REGISTRATION_PAGES_COUNT = 2;
#if DEBUG
        private const int MIN_PARTICIPANTS_COUNT = 3;
#else
        private const int MIN_PARTICIPANTS_COUNT = 5;
#endif
        private int _registrationPageNo = 1;
        private int _testRound = 0;

        private List<ParticipantViewModel> _participants;
        private Queue<TestItemViewModel> _testItems;
        private List<TestItemViewModel> _answeredTestItems;
        private TestAnswers _testAnswers;

        private RelayCommand<object> _nextTestItemCommand;
        private TestItemViewModel _currentTestItem;
        private string _testName;
        private int? _participantsCount;

        public TestViewModel(List<TestQuestion> questions)
        {
            _testItems = new Queue<TestItemViewModel>(questions.Select(question => new TestItemViewModel(question)));
            _answeredTestItems = new List<TestItemViewModel>();
            _testAnswers = new TestAnswers();
            RegistrationButtonName = "Далее";
            TestButtonName = "Продолжить";
            IsRegistrationState = true;

            MessengerInstance.Register<GotAnswersMessage>(this, message => NextTestItemCommand.RaiseCanExecuteChanged());
        }

        public TestAnswers TestAnswers
        {
            get { return _testAnswers; }
        }

        public string TestName
        {
            get { return _testName; }
            set
            {
                TestAnswers.TestName = value;
                Set(ref _testName, value);
            }
        }

        public string RegistrationButtonName
        {
            get { return _registrationButtonName; }
            set { Set(ref _registrationButtonName, value); }
        }

        public string TestButtonName
        {
            get { return _testButtonName; }
            set { Set(ref _testButtonName, value); }
        }

        public bool IsRegistrationState
        {
            get { return _isRegistrationState; }
            set { Set(ref _isRegistrationState, value); }
        }

        public bool IsRegistrationFinishing
        {
            get { return _isRegistrationFinishing; }
            set { Set(ref _isRegistrationFinishing, value); }
        }

        public int? ParticipantsCount
        {
            get { return _participantsCount; }
            set { Set(ref _participantsCount, value); }
        }

        public TestItemViewModel CurrentTestItem
        {
            get { return _currentTestItem; }
            set { Set(ref _currentTestItem, value); }
        }
        
        public ParticipantViewModel CurrentParticipant
        {
            get { return _currentParticipant; }
            set { Set(ref _currentParticipant, value); }
        }
        
        public RelayCommand<object> NextTestItemCommand
        {
            get
            {
                return _nextTestItemCommand ??
                       (_nextTestItemCommand = new RelayCommand<object>(NextTestItem, CanNextTestItem));
            }
        }

        private RelayCommand<object> _nextRegistrationPageCommand;
        private string _registrationButtonName;
        private string _testButtonName;
        private bool _isRegistrationState;
        private bool _isRegistrationFinishing;
        private ParticipantViewModel _currentParticipant;

        public RelayCommand<object> NextRegistrationPageCommand
        {
            get
            {
                return _nextRegistrationPageCommand ??
                       (_nextRegistrationPageCommand = new RelayCommand<object>(NextRegistrationPage, CanNextRegistrationPage));
            }
        }

        public List<ParticipantViewModel> Participants
        {
            get { return _participants; }
            set { Set(ref _participants, value); }
        }

        private bool CanNextRegistrationPage(object o)
        {
            if (_registrationPageNo == 1)
            {
                return ParticipantsCount.HasValue && ParticipantsCount >= MIN_PARTICIPANTS_COUNT;
            }
            else
            {
                return _participants !=null && _participants.All(participant => participant.NotEmpty);
            }
        }

        private void NextRegistrationPage(object o)
        {
            if (_registrationPageNo == 1)
            {
                RegistrationButtonName = "Начать тест";
                var participants = new List<ParticipantViewModel>();
                for (int i = 0; i < ParticipantsCount; i++)
                {
                    participants.Add(new ParticipantViewModel());
                }
                Participants = participants;
                IsRegistrationFinishing = true;
            }
            else if (_registrationPageNo == REGISTRATION_PAGES_COUNT)
            {
                IsRegistrationState = false;
                CurrentParticipant = Participants[_testRound];
                NextTestItem(null);
            }
            _registrationPageNo++;
        }

        private bool CanNextTestItem(object o)
        {
            return (CurrentTestItem == null || CurrentTestItem.GotAnswer) && _testItems.Any();
        }

        private void NextTestItem(object o)
        {
            if (CurrentTestItem != null && CurrentTestItem.GotAnswer)
            {
                _answeredTestItems.Add(CurrentTestItem);
                _testItems.Dequeue();
            }

            if (_testItems.Any())
            {
                NextTestItemInternal();
            }
            else if (TestAnswers.Count != ParticipantsCount)
            {
                TestAnswers.AddAnswers(
                    CurrentParticipant.MapTo(new Participant()),
                    _answeredTestItems
                        .Select(
                            model =>
                                new TestAnswer(model.Question,
                                    model.SelectedAnswers.Select(answer => answer.MapTo(new Participant())).ToList()))
                        .ToList());

                _answeredTestItems.ForEach(test => test.CleanTest());
                _testItems = new Queue<TestItemViewModel>(_answeredTestItems);
                _answeredTestItems = new List<TestItemViewModel>();

                TestButtonName = "Продолжить";
                CurrentTestItem = null;

                if (_testRound < Participants.Count - 1)
                {
                    CurrentParticipant = Participants[++_testRound];
                    NextTestItemInternal();
                }
                else
                {
                    MessengerInstance.Send(new ShowResultsMessage(TestAnswers));
                }
            }
            else
            {
                MessengerInstance.Send(new ShowResultsMessage(TestAnswers));
            }
        }

        private void NextTestItemInternal()
        {
            CurrentTestItem = _testItems.Peek();
            CurrentTestItem.LoadAnswers(
                Participants
                    .Where(participant => participant.Id != CurrentParticipant.Id)
                    .Select(participant => new TestAnswerViewModel(participant))
                    .ToList());

            if (TestAnswers.Count == ParticipantsCount - 1 && _testItems.Count == 1)
            {
                TestButtonName = "Завершить тест";
            }
            else if (_testItems.Count == 1)
            {
                TestButtonName = "Завершить и перейти к следующему участнику";
            }
        }
    }
}
