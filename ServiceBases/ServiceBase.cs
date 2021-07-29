using System;

namespace L.ServiceBases
{
    public abstract class LServiceBase: IStateService, IDisposable
    {
        readonly object _locker = new object();
        protected LServiceBase(){Name = ToString();}
        public virtual Action<bool> OnStatusChanged { get; set; }
        public virtual string Name { get; set; }

        public int _errorCounter;
        public int _TickingCounter;

        public bool IsRunning
        {
            get { return _isRunning; }
            private set
            {
                if(_isRunning == value)return;
                _isRunning = value;
                OnStatusChanged?.Invoke(value);
            }
        }

        public bool IsDisposed => _isDisposed;

        protected abstract bool OnOpen();
        public event Action OnOpenedSuccess;
        public virtual void Open()
        {
            lock (_locker)
            {
                if (IsRunning) return;
                IsRunning = true;
                _isDisposed = false;
                _errorCounter = 0;
                _TickingCounter = 0;
                try
                {
                    IsRunning = OnOpen();
                    if (IsRunning) OnOpenedSuccess?.Invoke();
                }
                catch(Exception e) { IsRunning = false; OnDispose(); throw; }
            }
        }

        public void Close()
        {
            if (!IsRunning) return;
            IsRunning = false;
            OnClose();
        }
        private bool _isDisposed;
        private bool _isRunning;

        protected virtual void OnClose()
        {
            Dispose();
        }
        public void Dispose()
        {
            lock (_locker)
            {
                IsRunning = false;
                if (!_isDisposed) OnDispose();
                _isDisposed = true;
            }
        }
        protected abstract void OnDispose();
    }
}
