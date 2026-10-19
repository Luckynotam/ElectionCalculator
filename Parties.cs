using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionCalculator
{
    public class Party
    {
        public string PartyName { get; set;  }
        public int Votes { get; set; }

        public int Seats { get; set; }

        public double Quotient => Votes / (Seats + 1.0);

        public double SaintLeagueQuotient => Votes / (2.0 * Seats + 1.0);

        public double KiesdelerQuotient => Votes / (Seats + 1.0);

        public Party (string partyName, int votes)
        {
            PartyName = partyName; 
            Votes = votes;
            Seats = 0;
            
        }
    }
}
    