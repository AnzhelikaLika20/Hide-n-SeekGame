using UnityEngine;

namespace CodeLibraries
{
    public class Utils
    {
        /// <summary>
        /// Get Vector3 in direction of angle
        /// </summary>
        /// <param name="angleInDegrees"></param>
        /// <returns></returns>
        public static Vector3 GetVector3FromAngle(float angleInDegrees)
        {
            // angle 0 - 360
            var angleInRad = angleInDegrees * Mathf.Deg2Rad;
            return new Vector3(Mathf.Sin(angleInRad), 0, Mathf.Cos(angleInRad));
        }

        public static float GetAngleFromVector3(Vector3 vector3)
        {
            vector3 = vector3.normalized;
            var n = Mathf.Atan2(vector3.y, vector3.x) * Mathf.Rad2Deg;
            if (n < 0)
            {
                n += 360;
            }

            return n;
        }
    }
}