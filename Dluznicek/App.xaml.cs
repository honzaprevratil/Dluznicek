using Dluznicek.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Dluznicek
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static DBcache _DBcache;

        public static DBcache DBcache
        {
            get
            {
                if (_DBcache == null)
                {
                    _DBcache = new DBcache();
                }
                return _DBcache;
            }
        }
    }
}
