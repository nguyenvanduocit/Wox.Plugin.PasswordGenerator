using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wox.Plugin;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using PasswordUtility.PasswordGenerator;

namespace Wox.Plugin.PasswordGenerator
{
    class Main : IPlugin
    {
        public List<Result> Query(Query query)
        {
            var result = new Result
            {
                Title = "Generate Strong Password",
                SubTitle = "Password will be copied to your clipboard.",
                IcoPath = "Images\\app.png",
                Action = c =>
                {
                    GenerateStrongPassword();
                    return true;
                }
            };
            return new List<Result> {result};
        }

        public void Init(PluginInitContext context)
        {
            
        }

        protected void GenerateStrongPassword() {
            string password = PwGenerator.Generate(15, true, false, true).ReadString();
            Thread thread = new Thread(() => Clipboard.SetText(password));
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();
        }
    }
}
