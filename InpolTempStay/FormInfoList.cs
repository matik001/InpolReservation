using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InpolTempStay
{
    internal class FormInfoList
    {
        public List<FormInfo> FormsInfo { get; set; }

        public void Load(string path)
        {
            var json = File.ReadAllText(path);
            FormsInfo = JsonConvert.DeserializeObject<List<FormInfo>>(json);
        }
        public void Save(string path)
        {
            var text = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, text);
        }

        public ConcurrentQueue<FormInfo> GetQueue()
        {
            var queue = new ConcurrentQueue<FormInfo>();
            foreach(var form in FormsInfo)
            {
                queue.Enqueue(form);
            }
            return queue;
        }


    }
}
