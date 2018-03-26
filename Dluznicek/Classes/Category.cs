using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznicek.Classes
{
    public class Category : ATable
    {
        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Outlay> Outlays { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
