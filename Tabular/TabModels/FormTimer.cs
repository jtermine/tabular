using System;
using System.Threading;

namespace Tabular.TabModels
{
    public class FormTimer
    {
        private readonly SynchronizationContext _syncContext;
        private readonly Timer _timer;

        public FormTimer()
        {
            _syncContext = SynchronizationContext.Current;
            _timer = new Timer(OnTick, null, 0, 100);
        }

        public event EventHandler Tick;

        private void OnTick(object sender)
        {
            _syncContext.Send(state =>
            {
                if (Tick != null)
                    Tick(this, EventArgs.Empty);
            }, null);
        }
    }
}