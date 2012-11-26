using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace Minecraft_Sim
{
    class IntersectionAlgorithm
    {
        public bool CheckForCollisionSquare(Vector3 TrianglePointOne, Vector3 TrianglePointTwo, Vector3 TrianglePointThree, Vector3 TrianglePointFour)
        {
            //Translation += Motion.TranslationValue();
            //Matrix totalMatrix = Motion.WorldMatrixValue() * Matrix.CreateTranslation(Translation);
            //TrianglePointOne = Vector3.Transform(TrianglePointOne, totalMatrix);
            //TrianglePointTwo = Vector3.Transform(TrianglePointTwo, totalMatrix);
            //TrianglePointThree = Vector3.Transform(TrianglePointThree, totalMatrix);
            //TrianglePointFour = Vector3.Transform(TrianglePointFour, totalMatrix);

            bool i = GetsBoolWithinBounds(TrianglePointOne, TrianglePointTwo, TrianglePointThree, GetLocationOfCollision(TrianglePointOne, TrianglePointTwo, TrianglePointThree));
            if (i == true)
            {
                return true;
            }
            else
            {
                return GetsBoolWithinBounds(TrianglePointFour, TrianglePointOne, TrianglePointThree, GetLocationOfCollision(TrianglePointFour, TrianglePointOne, TrianglePointThree));
            }
        }

        bool GetsBoolWithinBounds(Vector3 TrianglePointOne, Vector3 TrianglePointTwo, Vector3 TrianglePointThree, Vector3 PointToTest)
        {

            Vector3 VectorOne = PointToTest - TrianglePointOne;
            Vector3 VectorTwo = PointToTest - TrianglePointTwo;
            Vector3 VectorThree = PointToTest - TrianglePointThree;
            if (PointToTest == TrianglePointOne || PointToTest == TrianglePointTwo || PointToTest == TrianglePointThree)
            {
                return true;
            }
            VectorOne.Normalize();
            VectorThree.Normalize();
            VectorTwo.Normalize();

            double AngleOne = Math.Acos(Vector3.Dot(VectorOne, VectorTwo));
            double AngleTwo = Math.Acos(Vector3.Dot(VectorTwo, VectorThree));
            double AngleThree = Math.Acos(Vector3.Dot(VectorThree, VectorOne));
            double Degrees = (double)MathHelper.ToDegrees(((float)AngleOne + (float)AngleTwo + (float)AngleThree));
            if (Degrees < 359)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        Vector3 GetLocationOfCollision(Vector3 PointOne, Vector3 PointTwo, Vector3 PointThree)
        {
            //Gets Normal Of Plane

            Vector3 VectorOne = PointOne - PointTwo;
            Vector3 VectorTwo = PointThree - PointTwo;
            Vector3 CrossProductResult = Vector3.Cross(VectorOne, VectorTwo);

            //Sets Up Screen Space Plane
            Vector3 PointOnePlane = new Vector3(0, 0, 0);
            Vector3 PointTwoPlane = new Vector3(10, 0, 0);
            Vector3 PointThreePlane = new Vector3(10, 10, 0);

            Vector3 VectorOnePlane = PointOnePlane - PointTwoPlane;
            Vector3 VectorTwoPlane = PointThreePlane - PointTwoPlane;
            Vector3 PlaneNormal = Vector3.Cross(VectorOnePlane, VectorTwoPlane);

            //sets up equation
            Vector3 Vector = PlaneNormal;
            Vector3 PointOnLine = PointOnePlane;
            Vector3 Normal = CrossProductResult;
            Vector3 PointOnPlain = PointOne;

            // makes all vectors the same length
            Normal.Normalize();
            Vector.Normalize();

            //computes plane collision
            float DistanceFromPoint = Vector3.Dot((PointOnPlain - PointOnLine), Normal) / Vector3.Dot(Vector, Normal);
            Vector3 Answer = DistanceFromPoint * Vector + PointOnLine;

            return Answer;
        }

        public float GetDistanceFromCollision(Vector3 PointOne, Vector3 PointTwo, Vector3 PointThree)
        {
            //Gets Normal Of Plane

            Vector3 VectorOne = PointOne - PointTwo;
            Vector3 VectorTwo = PointThree - PointTwo;
            Vector3 CrossProductResult = Vector3.Cross(VectorOne, VectorTwo);

            //Sets Up Screen Space Plane
            Vector3 PointOnePlane = new Vector3(0, 0, 0);
            Vector3 PointTwoPlane = new Vector3(10, 0, 0);
            Vector3 PointThreePlane = new Vector3(10, 10, 0);

            Vector3 VectorOnePlane = PointOnePlane - PointTwoPlane;
            Vector3 VectorTwoPlane = PointThreePlane - PointTwoPlane;
            Vector3 PlaneNormal = Vector3.Cross(VectorOnePlane, VectorTwoPlane);

            //sets up equation
            Vector3 Vector = PlaneNormal;
            Vector3 PointOnLine = PointOnePlane;
            Vector3 Normal = CrossProductResult;
            Vector3 PointOnPlain = PointOne;

            // makes all vectors the same length
            Normal.Normalize();
            Vector.Normalize();

            //computes plane collision
            float DistanceFromPoint = Vector3.Dot((PointOnPlain - PointOnLine), Normal) / Vector3.Dot(Vector, Normal);

            return DistanceFromPoint;
        }
    }
}
