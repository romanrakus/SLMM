using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using SLMM.Common.Logging;
using SLMM.Core;

namespace SLMM.Communication
{
    public class LownManager : ILownManager
    {
        private static readonly ILogger Logger = LogFactory.GetLogger<LownManager>();

        private static readonly object SyncRoot = new object();
        private readonly ConcurrentQueue<Action> _commands = new ConcurrentQueue<Action>();

        private ILawnMowingMachine _machine;
        private Action _currentAction;
        private bool _initialized;

        private CancellationTokenSource _cancelSource;

        public void Init(ILawnMowingMachine machine)
        {
            Logger.Info($"Starting LownManager initialization.");
            lock (SyncRoot)
            {
                //Cancel old execution
                if (_initialized)
                    _cancelSource.Cancel();

                _cancelSource = new CancellationTokenSource();
                var machineThread = new Thread(() =>
                {
                    while (true)
                    {
                        Action action;
                        while (_commands.TryDequeue(out action))
                        {
                            if (_cancelSource.Token.IsCancellationRequested)
                                return;

                            _currentAction = action;
                            _currentAction();
                        }

                        if (_cancelSource.Token.IsCancellationRequested)
                            return;

                        Thread.Sleep(1000);
                    }
                });
                machineThread.IsBackground = true;
                machineThread.Start();

                _machine = machine;
                _initialized = true;
            }
        }

        public void MoveBy(int steps)
        {
            EnsureIntialized();
            _commands.Enqueue(() =>
            {
                for (var i = 0; i < steps; i++)
                {
                    _machine.MoveBy();
                }
            });
        }

        public KeyValuePair<int, int> GetLocation()
        {
            EnsureIntialized();
            return new KeyValuePair<int, int>(_machine.PositionX, _machine.PositionY);
        }

        public void Rotate(Rotation rotation)
        {
            EnsureIntialized();
            _commands.Enqueue(() =>
            {
                //TODO: Replace this logic by smart algorithm
                while (_machine.Rotation != rotation)
                {
                    _machine.TurnRight();
                }
            });
    }

        public Rotation GetCurrentPosition()
        {
            EnsureIntialized();
            return _machine.Rotation;
        }

        public void Mow(bool on)
        {
            EnsureIntialized();
            if (!on) return;
            _commands.Enqueue(_machine.Mow);
        }

        public bool IsMowing()
        {
            EnsureIntialized();
            return _currentAction != null && _currentAction == _machine.Mow;
        }

        public void Dispose()
        {
            _cancelSource.Cancel();
            _cancelSource.Dispose();
        }

        public void EnsureIntialized()
        {
            if (!_initialized)
                throw new InvalidOperationException("Initialization required.");
        }
    }
}
 