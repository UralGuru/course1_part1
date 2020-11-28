using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            float shoulderX = Manipulator.UpperArm * (float)Math.Cos(shoulder);
            float shoulderY = Manipulator.UpperArm * (float)Math.Sin(shoulder);
            var elbowPos = new PointF(shoulderX, shoulderY);
            
            float elbowX = shoulderX + Manipulator.Palm * (float)Math.Cos(shoulder - Math.PI + elbow);
            float elbowY = shoulderY + Manipulator.Palm * (float)Math.Sin(shoulder - + Math.PI + elbow);
            var wristPos = new PointF(elbowX, elbowY);

            float wristX = elbowX + Manipulator.Forearm * (float)Math.Cos(wrist - + Math.PI + elbow);
            float wristY = elbowY + Manipulator.Forearm * (float)Math.Sin(wrist - + Math.PI + elbow);
            var palmEndPos = new PointF(wristX, wristY);
            
            
            
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        public static double Distance(PointF a, PointF b)
        {
            double dx = b.X - a.Y;
            double dy = b.Y - a.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            
            Assert.AreEqual(3, joints.Length);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(Manipulator.UpperArm, Distance(joints[0], new PointF(0,0)),1e-5, "upper arm length");
            Assert.AreEqual(Manipulator.Palm, Distance(joints[2], joints[1]), 1e-5, "upper arm length");
            Assert.AreEqual(Manipulator.Forearm, Distance(joints[1], joints[0]), 1e-5, "upper arm length");
        }
    }
}
