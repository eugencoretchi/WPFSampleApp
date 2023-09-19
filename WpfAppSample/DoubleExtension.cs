using System;

namespace WpfAppSample
{
    static class DoubleExtension
    {
        static public double Lerp(this double srcVal, double dstVal, float epsilon)
        {
            epsilon = Math.Clamp(epsilon, 0f, 1f);

            var delta = dstVal - srcVal;

            return srcVal + delta * epsilon;
        }
    }
}
