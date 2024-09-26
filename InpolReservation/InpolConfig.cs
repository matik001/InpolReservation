using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InpolReservation
{
    class LoginInfo 
    {
        public string Username { get; set; }
        public string Pass { get; set; }
    }

    internal class InpolConfig
    {
        public int CheckIntervalMs { get; set; }
        public string CaseName { get; set; }
        public string OfficeName { get; set; }
        public string DBCKey { get; set; }
        public int NthDay { get; set; }
        public bool Headless { get; set; } = false;
        public List<LoginInfo> Logins { get; set; }
        
        public static InpolConfig Load(string path) 
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<InpolConfig>(json);
        }
    }
}
