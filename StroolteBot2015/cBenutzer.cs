using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using UltimateTeam.Toolkit;

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
        private FutClient _Client = new FutClient();
        public FutClient Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        private LoginDetails _LoginDetails = new LoginDetails("Username", "Passwort", "Sicherheitsfrage", Platform.Xbox360);
        public LoginDetails LoginDetails
        {
            get { return _LoginDetails; }
            set {
                _LoginDetails = value;
                NotifyPropertyChanged();
            }
        }

        public cBenutzer()
        {
            NotifyPropertyChanged();
        }
        public string Username
        {
            get {
                if (_LoginDetails.Username == "leer") return "";
                return _LoginDetails.Username;
            }
            set {
                if (value == "") value = "leer";
                LoginDetails = new LoginDetails(value, LoginDetails.Password, LoginDetails.SecretAnswer, LoginDetails.Platform);
            }
        }

        public string Passwort
        {
            get {
                if (_LoginDetails.Password == "leer") return ""; 
                return _LoginDetails.Password;
            }
            set {
                if (value == "") value = "leer"; 
                LoginDetails = new LoginDetails(LoginDetails.Username, value, LoginDetails.SecretAnswer, LoginDetails.Platform);
            }
        }

        public string Sicherheitsfrage
        {
            get {
                if (_LoginDetails.SecretAnswer == "leer") return "";
                return _LoginDetails.SecretAnswer; 
            }
            set {
                if (value == "") value = "leer"; 
                LoginDetails = new LoginDetails(LoginDetails.Username, LoginDetails.Password, value, LoginDetails.Platform);
            }
        }

        public Platform Konsole
        {
            get { return _LoginDetails.Platform; }
            set { 
                LoginDetails = new LoginDetails(LoginDetails.Username, LoginDetails.Password, LoginDetails.SecretAnswer, value); 
            }
            
        }
        private int _Coins = 0;
        public int Coins { get { return _Coins; } set { _Coins = value; } }

        private LoginStatus _Status = 0;
        public LoginStatus Status { get { return _Status; } set { _Status = value; } }

        private LoginResponse _LoginResponse;
        public LoginResponse LoginResponse { get { return _LoginResponse; } set { _LoginResponse = value; } }
    }

    public enum LoginStatus
    {
        Ausgeloggt,
        Eingeloggt,
        AmEinloggen,
        Fehler
    }
}
