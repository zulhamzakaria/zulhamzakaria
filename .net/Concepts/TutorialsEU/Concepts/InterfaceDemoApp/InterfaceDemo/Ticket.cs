using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace InterfaceDemo
{
    internal class Ticket : IEquatable<Ticket>
    {
        public int DurationHours { get; set; }
        public Ticket(int durationHours)
        {
            DurationHours = durationHours;
        }

        public bool Equals([AllowNull] Ticket other)
        {
            return this.DurationHours == other.DurationHours;
        }
    }
}
