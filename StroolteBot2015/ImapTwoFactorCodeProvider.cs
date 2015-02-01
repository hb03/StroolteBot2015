using AE.Net.Mail;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Services;
using System;

namespace StroolteBot2015
{
    public class ImapTwoFactorCodeProvider : ITwoFactorCodeProvider
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _hostName;
        private readonly string _emailto;
        private readonly int _port;
        private readonly bool _useUseSsl;

        public ImapTwoFactorCodeProvider(string username, string password, string hostname, int port, bool useSsl,string emailto)
        {
            _username = username;
            _password = password;
            _hostName = hostname;
            _port = port;
            _useUseSsl = useSsl;
            _emailto = emailto;
        }

        public Task<string> GetTwoFactorCodeAsync()
        {
            return Task.Run(async () =>
            {
                var regex = new Regex(@"(?<=<strong>)\d{6}");

                try {
                    using (var client = new ImapClient(_hostName, _username, _password, AuthMethods.Login, _port, _useUseSsl))
                    {
                        for (var i = 0; i < 5; i++)
                        {
                            var messages = client.SearchMessages(SearchCondition.Unseen().And(SearchCondition.To(_emailto)));
                            foreach (var match in messages.Select(message => regex.Match(message.Value.Body)).Where(match => match.Success))
                            {
                                return match.Value;
                            }
                            await Task.Delay(5000);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var fehler = ex.Message;
                }
                throw new FutException("Unable to find the two-factor authentication code.");
            });
        }
    } 
}
