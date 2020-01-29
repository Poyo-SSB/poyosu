using SixLabors.ImageSharp;
using System;

namespace poyosu.Geometry
{
    public static class RegularPolygonGenerator
    {
        public static PointF[] Generate(int points, float radius, float rotation = 0, PointF center = default)
        {
            if (points < 3)
            {
                throw new ArgumentOutOfRangeException(nameof(points), "may not be less than 3");
            }

            if (radius < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(radius), "may not be less than 0");
            }

            var vertices = new PointF[points];

            float x = radius;
            float y = 0;

            for (int i = 0; i < points; i++)
            {
                float angle = i * 2 * (float)Math.PI / points;

                float sin = (float)Math.Sin(angle + rotation);
                float cos = (float)Math.Cos(angle + rotation);

                vertices[i] = new PointF(
                    (x * cos) - (y * sin) + center.X,
                    (x * sin) + (y * cos) + center.Y);
            }

            return vertices;
        }
    }
}
