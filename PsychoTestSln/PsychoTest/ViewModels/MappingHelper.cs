using PsychoTest.Models;

namespace PsychoTest.ViewModels
{
    public static class MappingHelper
    {
        public static Participant MapTo(this ParticipantViewModel mapFrom, Participant mapTo)
        {
            mapTo.SecondName = mapFrom.SecondName;
            mapTo.FirstName = mapFrom.FirstName;
            mapTo.Id = mapFrom.Id;
            mapTo.FullName = mapFrom.FullName;
            return mapTo;
        }

        public static ParticipantViewModel MapTo(this Participant mapFrom, ParticipantViewModel mapTo)
        {
            mapTo.SecondName = mapFrom.SecondName;
            mapTo.FirstName = mapFrom.FirstName;
            mapTo.Id = mapFrom.Id;
            return mapTo;
        }
    }
}
