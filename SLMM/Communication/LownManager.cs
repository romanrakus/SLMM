using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using SLMM.Core;

namespace SLMM.Communication
{
    public class LownManager : ILownManager
    {
        private readonly ConcurrentQueue<Action> _commands = new ConcurrentQueue<Action>();

        private ILawnMowingMachine _machine;
        private Action _currentAction;

        private CancellationTokenSource _cancelSource;

        public void Init(ILawnMowingMachine machine)
        {
            _machine = machine;
            
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
            machineThread.Start();
        }

        public void MoveBy(int steps)
        {
            _commands.Enqueue(() => _machine.MoveBy(steps));
        }

        public KeyValuePair<int, int> GetLocation()
        {
            throw new NotImplementedException();
        }

        public void Rotate(Rotation rotation)
        {
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
            return _machine.Rotation;
        }

        public void Mow(bool on)
        {
            if(!on) return;
            _commands.Enqueue(_machine.Mow);
        }

        public bool IsMowing()
        {
            return _currentAction != null && _currentAction == _machine.Mow;
        }

        public void Dispose()
        {
            _cancelSource.Cancel();
            _cancelSource.Dispose();
        }
    }
}
