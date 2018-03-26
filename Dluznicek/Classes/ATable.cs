using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznicek.Classes
{
    public abstract class ATable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
