namespace SLMM.Core
{
    public interface ILawnMowingMachine
    {
        int PositionX { get; }
        int PositionY { get; }

        void TrunLeft();
        void TurnRight();
        void MoveBy(int steps);
        void Mow();
    }
}