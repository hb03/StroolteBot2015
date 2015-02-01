using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using UltimateTeam.Toolkit;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Services;
using UltimateTeam.Toolkit.Exceptions;
using System.IO;
using System.Windows;

namespace StroolteBot2015
{
    public class cMgmt
    {
        static public ObservableCollection<cBenutzer> Benutzerliste = new ObservableCollection<cBenutzer>();

        static public int AnzahlAccounts
        {
            get { return Benutzerliste.Count(); }
        }

        static async public void LoginAll()
        {
            if (Benutzerliste.Count > 0)
            {
                List<cBenutzer> tempBenutzerliste = new List<cBenutzer>(Benutzerliste);
                foreach (cBenutzer benutzer in tempBenutzerliste)
                {
                    try
                    {
                        benutzer.Status = LoginStatus.AmEinloggen;
                        ITwoFactorCodeProvider provider = new ImapTwoFactorCodeProvider("marc@hb03.de", "BVgr1a2x", "mail.hb03.de", 143, false, benutzer.Username);
                        benutzer.LoginResponse = await benutzer.Client.LoginAsync(benutzer.LoginDetails, provider);
                    }
                    catch (FutException ex)
                    {
                        var fehler = ex.Message;
                    }
                }
            }
        }

        static async public void ReadAccounts()
        {
            if (File.Exists("Accounts.txt"))
            {
                StreamReader sr = new StreamReader("Accounts.txt");
                while (!sr.EndOfStream)
                {
                    string zeile = await sr.ReadLineAsync();
                    string[] test = zeile.Split(new Char[] { ';' });
                    Benutzerliste.Add(new cBenutzer() { Username = test[0], Passwort = test[1], Sicherheitsfrage = test[2], Konsole = (Platform)Enum.Parse(typeof(Platform), test[3]) });
                }
                sr.Close();
            }
        }

        static async public void WriteAccounts()
        {
            using (StreamWriter sw = File.CreateText("Accounts.txt"))
            {
                foreach (cBenutzer benutzer in Benutzerliste)
                {
                    await sw.WriteLineAsync(benutzer.Username + ";" + benutzer.Passwort + ";" + benutzer.Sicherheitsfrage + ";" + benutzer.Konsole.ToString());
                }
            }
        }
    }
}
