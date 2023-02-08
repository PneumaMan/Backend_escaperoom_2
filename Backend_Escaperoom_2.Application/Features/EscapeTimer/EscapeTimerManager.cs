using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.EscapeTimer
{
    public class EscapeTimerManager
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;

        public DateTime TimerStarted { get; set; }
        public bool IsTimerStarted { get; set; }




        /* Metodos */
        public void PrepareTimer(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, 2000);
            TimerStarted = DateTime.Now;
            IsTimerStarted = true;
        }

        public void Execute(object stateInfo)
        {
            _action();
            if ((DateTime.Now - TimerStarted).TotalSeconds > 60)
            {
                IsTimerStarted = false;
                _timer.Dispose();
            }
        }
    }
}
