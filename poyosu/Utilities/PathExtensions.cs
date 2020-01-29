using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace poyosu.Utilities
{
    public static class PathExtensions
    {
        public static IPath Rotate(this IPath path, float radians, PointF center)
            => path.Transform(Matrix3x2.CreateRotation(radians, center));

        public static IPath RotateDegree(this IPath path, float degrees, PointF center)
            => path.Rotate((float)(3.1415926535897931 * degrees / 180.0), center);
    }
}
