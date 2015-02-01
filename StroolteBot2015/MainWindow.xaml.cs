using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UltimateTeam.Toolkit.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace StroolteBot2015
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        cSchnittstelle Schnittstelle = new cSchnittstelle();

        private string _TempText = "";
        public string TempText { get { return _TempText; } set { _TempText = value; } }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Schnittstelle;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cMgmt.Benutzerliste.Add(Schnittstelle.TempBenutzer);
            Schnittstelle.TempBenutzer = new cBenutzer();
            Schnittstelle.NotifyPropertyChanged("");
        }

        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TempText = ((TextBox)sender).Text;
            if (((TextBox)sender).Text == "Username" || ((TextBox)sender).Text == "Passwort" || ((TextBox)sender).Text == "Sicherheitsfrage")
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = TempText;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Schnittstelle.AnzahlAccounts > 0)
            {
                cMgmt.LoginAll();
            }
            else
            {
                MessageBox.Show("Keine Accounts eingetragen!");
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            cMgmt.ReadAccounts();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            cMgmt.WriteAccounts();
        }
    }

    public class cSchnittstelle : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private cBenutzer _TempBenutzer = new cBenutzer() { };
        public cBenutzer TempBenutzer
        {
            get { return _TempBenutzer; }
            set { _TempBenutzer = value; }
        }

        public int AnzahlAccounts
        {
            get { return cMgmt.Benutzerliste.Count(); }
        }

        public ObservableCollection<cBenutzer> Benutzerliste
        {
            get {
                return cMgmt.Benutzerliste;
            }
            set {
                cMgmt.Benutzerliste = value;
            }
        }

    }
}
