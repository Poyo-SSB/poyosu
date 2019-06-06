using System;

namespace poyosu.Utilities
{
    public static class ImageMath
    {
        /// <summary>A function in the form ae^(-(x - b)² / 2c²).</summary>
        /// <param name="a">The height of the curve's peak.</param>
        /// <param name="b">The position of the center of the curve's peak (or the mean in a normal distribution).</param>
        /// <param name="c">The standard deviation of the curve, affecting the width of the curve.</param>
        public static float GaussianFunction(float x, float a, float b, float c)
            => (float)(a * Math.Exp(-(x - b) * (x - b) / (2 * c * c)));
    }
}
