using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InpolTempStay
{
    internal class Config
    {
        public string DBCLogin { get; set; }
        public string DBCPass { get; set; }
        public bool Headless { get; set; }
        public int Workers { get; set; }
        public bool SolveFormCaptcha { get; set; }

        public static Config Load(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Config>(json);
        }
        public void Save(string path)
        {
            var text = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, text);
        }
    }
}
