using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznicek.Classes
{
    public class Outlay : ATable
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        [ForeignKey(typeof(Category))]     // Specify the foreign key
        public int CategoryId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Category Category { get; set; }

        [ForeignKey(typeof(Debt))]     // Specify the foreign key
        public int DebtId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Debt Debt { get; set; }

        public override string ToString()
        {
            return Date.ToLongDateString() + " : " + Name + " : " + Price + " Kč";
        }
    }
}
