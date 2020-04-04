using UnityEngine;

namespace DeBox.Unitoolz.Extensions
{
    public static class Vector2Extensions
    {
        private const float UNITY_ROTATION_ANGLE_OFFSET = 90;
        private const float HALF_CIRCLE_DEGS = 180;
        private const float CIRCLE_DEGS = 360;

        /// <summary>
        /// Returns a signed smallest delta angle between two vectors
        /// </summary>
        /// <returns>Signed delta angle.</returns>
        /// <param name="source">Source vector</param>
        /// <param name="dest">Target vector</param>
        public static float SmallestDeltaAngle(this Vector2 source, Vector2 target)
        {
            var currentAngle = source.ToAngle();
            var targetAngle = target.ToAngle();
            var deltaAngle = currentAngle - targetAngle;
            if (Mathf.Abs(deltaAngle) > HALF_CIRCLE_DEGS)
            {
                deltaAngle = Mathf.Sign(deltaAngle)*((Mathf.Abs(deltaAngle) - CIRCLE_DEGS));
            }
            return deltaAngle;
        }

        /// <summary>
        /// Returns the angle;
        /// </summary>
        public static float ToAngle(this Vector2 v)
        {
            return (CIRCLE_DEGS - UNITY_ROTATION_ANGLE_OFFSET + Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg) % CIRCLE_DEGS;
        }

        public static Vector2 FromAngle(this Vector2 v, float angle)
        {
            var radians = (angle + UNITY_ROTATION_ANGLE_OFFSET) * Mathf.Deg2Rad;
            var magnitude = v.magnitude;
            v.x = Mathf.Cos(radians);
            v.y = Mathf.Sin(radians);
            v = v * magnitude;
            return v;
        }
    }
}




