using System;
using NUnit.Framework;
using SLMM.Core;

namespace SLMM.UnitTests.Core
{
    [TestFixture]
    class WhenLawnMowingMachine
    {
        [Test]
        public void IsCreatedWithValidParameters_OrientationSHouldBeNorth()
        {
            var machine = new LawnMowingMachine(10, 10, 0, 0);
            Assert.That(machine.Rotation, Is.EqualTo(Rotation.North));
        }

        [Test]
        [TestCase(0, 0), TestCase(-1, 10), TestCase(10, -1), TestCase(-1, -1)]
        public void CreatingWithInvalidSize_ArgumentExceptionShouldBeThrown(int sizeX, int sizeY)
        {
            Assert.Throws<ArgumentException>(() => new LawnMowingMachine(sizeX, sizeY, 0, 0));
        }

        [Test]
        [TestCase(-1, 0), TestCase(0, -1), TestCase(-1, -1)]
        public void CreatingWithNedativeBasedPosition_ArgumentExceptionShouldBeThrown(int positionX, int positionY)
        {
            Assert.Throws<ArgumentException>(() => new LawnMowingMachine(2, 3, positionX, positionY));
        }

        [Test]
        [TestCase(0, 4), TestCase(3, 0), TestCase(3, 4)]
        public void CreatingOutsideOfGarden_ArgumentOutOfRangeExceptionShouldBeThrown(int positionX, int positionY)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LawnMowingMachine(2, 3, positionX, positionY));
        }

        [Test]
        public void TurnsRightAndTurnsLeft_ShouldGoToInitialRotation()
        {
            var machine = new LawnMowingMachine(10 ,10, 10, 10);
            for (int i = 0; i < 4; i++)
            {
                var rotation = machine.Rotation;
                machine.TrunLeft();
                machine.TurnRight();
                Assert.That(machine.Rotation, Is.EqualTo(rotation));

                machine.TurnRight();
            }
        }
    }
}
