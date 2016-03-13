using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PsychoTest.ViewModels
{
    public class ParticipantViewModel : ViewModelBase
    {
        private string _firstName;
        private string _secondName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                Set(ref _firstName, value);
                this.RaisePropertyChanged(() => this.FullName);
            }
        }

        public string SecondName
        {
            get { return _secondName; }
            set
            {
                Set(ref _secondName, value);
                this.RaisePropertyChanged(() => this.FullName);
            }
        }

        public string FullName => $"{SecondName} {FirstName}";

        public bool NotEmpty => !string.IsNullOrEmpty(FirstName)
                                && !string.IsNullOrEmpty(SecondName);
    }
}
