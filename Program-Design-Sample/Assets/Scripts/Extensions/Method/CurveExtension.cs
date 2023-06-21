using UnityEngine;

namespace ExtensionMethod
{
    public static class CurveExtension
    {
        public static float GetSlope(this AnimationCurve curve, float time)
        {
            var tangent = curve.Evaluate(time)-curve.Evaluate(time - 0.0001f);
            var slope = tangent / 0.0001f;
            return slope;
        }
    }
}