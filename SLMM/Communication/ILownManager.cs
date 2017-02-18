using System;
using System.Collections.Generic;
using SLMM.Core;

namespace SLMM.Communication
{
    public interface ILownManager : IDisposable
    {
        void Init(ILawnMowingMachine machine);
        void MoveBy(int steps);
        KeyValuePair<int, int> GetLocation();
        void Rotate(Rotation rotation);
        Rotation GetCurrentPosition();
        void Mow(bool on);
        bool IsMowing();
    }
}