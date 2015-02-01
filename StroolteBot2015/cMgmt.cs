using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace StroolteBot2015
{
    public class cMgmt
    {
        static public ObservableCollection<cBenutzer> Benutzerliste = new ObservableCollection<cBenutzer>();

        public int AnzahlAccounts
        {
            get { return Benutzerliste.Count(); }
        }
    }
}
