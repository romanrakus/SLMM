namespace SLMM.Core
{
    public interface ILawnMowingMachine
    {
        int PositionX { get; }
        int PositionY { get; }
        Rotation Rotation { get; }

        void TrunLeft();
        void TurnRight();
        void MoveBy();
        void Mow();
    }
}