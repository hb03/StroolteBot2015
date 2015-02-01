using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace StroolteBot2015
{
    public class cBenutzer : INotifyPropertyChanged 
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        private LoginDetails _Logindetails = new LoginDetails("Username", "Passwort", "Sicherheitsfrage", Platform.Xbox360);
        public LoginDetails Logindetails
        {
            get { return _Logindetails; }
            set {
                _Logindetails = value;
                NotifyPropertyChanged();
            }
        }

        public cBenutzer()
        {
            NotifyPropertyChanged();
        }
        public string Username
        {
            get { return _Logindetails.Username; }
            set { Logindetails = new LoginDetails(value, Logindetails.Password, Logindetails.SecretAnswer, Logindetails.Platform); }
        }

        public string Passwort
        {
            get { return _Logindetails.Password; }
            set { Logindetails = new LoginDetails(Logindetails.Username, value, Logindetails.SecretAnswer, Logindetails.Platform); }
        }

        public string Sicherheitsfrage
        {
            get { return _Logindetails.SecretAnswer; }
            set { Logindetails = new LoginDetails(Logindetails.Username, Logindetails.Password, value, Logindetails.Platform); }
        }

        public Platform Konsole
        {
            get { return _Logindetails.Platform; }
            set { Logindetails = new LoginDetails(Logindetails.Username, Logindetails.Password, Logindetails.SecretAnswer, value); 
            }
            
        }
        private int _Coins = 0;
        public int Coins { get { return _Coins; } set { _Coins = value; } }
    }
}
