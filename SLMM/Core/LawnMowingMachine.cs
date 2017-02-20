using System;
using System.Threading;
using SLMM.Common;
using SLMM.Common.Extensions;
using SLMM.Common.Logging;

namespace SLMM.Core
{
    public class LawnMowingMachine : ILawnMowingMachine
    {
        private static readonly ILogger Logger = LogFactory.GetLogger<LawnMowingMachine>();

        private int _orientation;

        public LawnMowingMachine(int sizeX, int sizeY, int positionX, int positionY)
        {
            Ensure.GreaterZero(sizeX, nameof(sizeX));
            Ensure.GreaterZero(sizeY, nameof(sizeY));
            Ensure.GreaterOrEqualZero(positionX, nameof(positionX));
            Ensure.GreaterOrEqualZero(positionY, nameof(positionY));

            if (positionX > sizeX)
                throw new ArgumentOutOfRangeException(nameof(positionX));
            if (positionY > sizeY)
                throw new ArgumentOutOfRangeException(nameof(positionY));

            SizeX = sizeX;
            SizeY = sizeY;
            PositionX = positionX;
            PositionY = positionY;
        }

        public int SizeX { get; }
        public int SizeY { get; }

        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        public Rotation Rotation => (Rotation) (_orientation%4);

        public void TurnRight()
        {
            Logger.Info($"{DateTime.UtcNow} - Start turn right - {Rotation} ({PositionX}, {PositionY})");
            Thread.Sleep(15000);
            unchecked
            {
                _orientation++;
            }
            Logger.Info($"{DateTime.UtcNow} - End turn right - {Rotation} ({PositionX}, {PositionY})");
        }

        public void TrunLeft()
        {
            Logger.Info($"{DateTime.UtcNow} - Start turn left - {Rotation} ({PositionX}, {PositionY})");
            Thread.Sleep(15000);
            unchecked
            {
                _orientation--;
            }
            Logger.Info($"{DateTime.UtcNow} - End turn left - {Rotation} ({PositionX}, {PositionY})");
        }

        public void MoveBy()
        {
            Logger.Info($"{DateTime.UtcNow} - Start move - {Rotation} ({PositionX}, {PositionY})");

            int newY;
            int newX;
            switch (Rotation)
            {
                case Rotation.North:
                    newY = PositionY + 1;
                    if (newY > SizeY)
                        throw new IndexOutOfRangeException("Cannot move out of garden.");
                    PositionY = newY;
                    break;

                case Rotation.East:
                    newX = PositionX + 1;
                    if (newX > SizeX)
                        throw new IndexOutOfRangeException("Cannot move out of garden.");
                    PositionX = newX;
                    break;

                case Rotation.South:
                    newY = PositionY - 1;
                    if (newY < 0)
                        throw new IndexOutOfRangeException("Cannot move out of garden.");
                    PositionY = newY;
                    break;

                case Rotation.West:
                    newX = PositionX - 1;
                    if (newX < 0)
                        throw new IndexOutOfRangeException("Cannot move out of garden.");
                    PositionX = newX;
                    break;

                default:
                    throw new NotSupportedException();
            }

            Thread.Sleep(30000);
            Logger.Info($"{DateTime.UtcNow} - End move - {Rotation} ({PositionX}, {PositionY})");
        }

        public void Mow()
        {
            Logger.Info($"{DateTime.UtcNow} - Start mow - {Rotation} ({PositionX}, {PositionY})");
            Thread.Sleep(120000);
            Logger.Info($"{DateTime.UtcNow} - End mow - {Rotation} ({PositionX}, {PositionY})");
        }
    }
}