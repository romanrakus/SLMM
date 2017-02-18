namespace SLMM.Core
{
    public interface ILawnMowingMachine
    {
        int PositionX { get; }
        int PositionY { get; }
        Rotation Rotation { get; }

        void TrunLeft();
        void TurnRight();
        object MoveBy(int steps);
        void Mow();
    }
}