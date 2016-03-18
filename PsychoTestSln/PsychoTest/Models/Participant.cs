using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoTest.Models
{
    public class Participant : IEquatable<Participant>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string FullName { get; set; }

        public bool NotEmpty => !string.IsNullOrEmpty(FirstName)
                                && !string.IsNullOrEmpty(SecondName);

        public bool Equals(Participant other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Participant) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static IEqualityComparer<Participant> IdComparer { get; } = new IdEqualityComparer();

        private sealed class IdEqualityComparer : IEqualityComparer<Participant>
        {
            public bool Equals(Participant x, Participant y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id);
            }

            public int GetHashCode(Participant obj)
            {
                return obj.Id.GetHashCode();
            }
        }

    }
}
