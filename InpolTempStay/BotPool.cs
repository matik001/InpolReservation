using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InpolTempStay
{
    internal class BotPool
    {
        private readonly Config _config;
        private readonly Action<string> _log;
        private readonly FormInfoList _formList;
        private CancellationTokenSource _cts;
        public BotPool(Config config, FormInfoList formList, Action<string> log)
        {
            _config = config;
            _log = log;
            _formList = formList;
        }

        public event Action<int> OnChangedFinishedCnt;
        public async Task Start()
        {
            _cts = new CancellationTokenSource();
            var queue = _formList.GetQueue();
            int finished = 0;
            OnChangedFinishedCnt?.Invoke(finished);
            await Task.Factory.StartNew(async () =>
            {
                try
                {
                    int workers = _config.Workers;
                    List<Task> tasks = new List<Task>();
                    while(!queue.IsEmpty)
                    {
                        if(!queue.TryDequeue(out var form))
                            continue;
                        if(_cts.Token.IsCancellationRequested)
                            return;

                        var bot = new BotTempStay((info) => _log(form.NazwiskoCudzoziemca + ": " + info));
                        var task = bot.Start(_config, form, _cts.Token);
                        tasks.Add(task);

                        if(tasks.Count >= workers)
                        {
                            finished++;
                            OnChangedFinishedCnt?.Invoke(finished);

                            var idx = Task.WaitAny(tasks.ToArray(), _cts.Token);
                            tasks.Remove(tasks[idx]);
                        }
                    }
                    while(tasks.Count > 0)
                    {
                        finished++;
                        OnChangedFinishedCnt?.Invoke(finished);

                        var idx = Task.WaitAny(tasks.ToArray(), _cts.Token);
                        tasks.Remove(tasks[idx]);
                    }
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                
            }, _cts.Token);
        }

        public void Stop()
        {
            _cts.Cancel();
        }
    }
}
