using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace poyosu.Geometry
{
    public static class PolygonRounder
    {
        public static IPath Round(PointF[] points, float cornerRadius, int vertices)
        {
            // I wrote this like two months ago in JavaScript and I don't care enough to document it.
            // Found a bug? Good luck, idiot.

            var roundedPoints = new List<PointF>();

            for (int i = 0; i < points.Length; i++)
            {
                PointF previousPoint = i == 0 ? points.Last() : points[i - 1];
                PointF nextPoint = i == points.Length - 1 ? points[0] : points[i + 1];
                PointF thisPoint = points[i];

                var previousVector = new PointF(
                    thisPoint.X - previousPoint.X,
                    thisPoint.Y - previousPoint.Y
                );
                var nextVector = new PointF(
                    thisPoint.X - nextPoint.X,
                    thisPoint.Y - nextPoint.Y
                );

                float previousLength = (float)Math.Sqrt((previousVector.X * previousVector.X) + (previousVector.Y * previousVector.Y));
                float nextLength = (float)Math.Sqrt((nextVector.X * nextVector.X) + (nextVector.Y * nextVector.Y));

                float effectiveRadius = cornerRadius;
                if (Math.Min(previousLength, nextLength) / 2 < cornerRadius)
                {
                    effectiveRadius = Math.Min(previousLength, nextLength) / 2;
                }

                var previousVectorNormalized = new PointF(previousVector.X / previousLength, previousVector.Y / previousLength);
                var nextVectorNormalized = new PointF(nextVector.X / nextLength, nextVector.Y / nextLength);

                var arcStart = new PointF(
                    thisPoint.X - (effectiveRadius * previousVectorNormalized.X),
                    thisPoint.Y - (effectiveRadius * previousVectorNormalized.Y)
                );
                var arcEnd = new PointF(
                    thisPoint.X - (effectiveRadius * nextVectorNormalized.X),
                    thisPoint.Y - (effectiveRadius * nextVectorNormalized.Y)
                );

                float angle = (float)Math.Atan2(nextVector.Y, nextVector.X) - (float)Math.Atan2(previousVector.Y, previousVector.X);

                float circleRadius = effectiveRadius * (float)Math.Tan(angle / 2);

                var circleCenter = new PointF(arcStart.X + (circleRadius * previousVectorNormalized.Y), arcStart.Y - (circleRadius * previousVectorNormalized.X));

                float startAngle = (float)Math.Atan2(arcStart.Y - circleCenter.Y, arcStart.X - circleCenter.X);
                float endAngle = (float)Math.Atan2(arcEnd.Y - circleCenter.Y, arcEnd.X - circleCenter.X);

                if (startAngle - endAngle >= Math.PI)
                {
                    endAngle += 2 * (float)Math.PI;
                }
                else if (startAngle - endAngle <= -Math.PI)
                {
                    endAngle -= 2 * (float)Math.PI;
                }

                for (int j = 0; j < vertices; j++)
                {
                    double currentAngle = startAngle + ((endAngle - startAngle) * ((float)j / (vertices - 1)));

                    if (startAngle > endAngle)
                    {
                        roundedPoints.Add(new PointF(
                            circleCenter.X + (circleRadius * (float)Math.Cos(currentAngle)),
                            circleCenter.Y + (circleRadius * (float)Math.Sin(currentAngle))
                        ));
                    }
                    else
                    {
                        roundedPoints.Add(new PointF(
                            circleCenter.X - (circleRadius * (float)Math.Cos(currentAngle)),
                            circleCenter.Y - (circleRadius * (float)Math.Sin(currentAngle))
                        ));
                    }
                }

            }

            var builder = new PathBuilder();

            builder.AddLines(roundedPoints.ToArray());
            builder.CloseFigure();

            return builder.Build();
        }
    }
}
