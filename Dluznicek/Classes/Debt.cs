using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznicek.Classes
{
    public class Debt : ATable
    {
        public string Debtor { get; set; }
        public DateTime ReturnDate { get; set; }
        public int InterestRate { get; set; }
        public int Interest { get; set; } = 0;

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Outlay> Outlays { get; set; }

        public override string ToString()
        {
            return Debtor;
        }
    }
}
