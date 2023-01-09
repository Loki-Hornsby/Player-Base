/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// Version: 09.01.23

public static class Unitilities {
    public static class Probability {
        /// <summary>
        /// Check the probability of something
        /// </summary>
        public static bool Check(int percentage, int range = 100){
            int var = UnityEngine.Random.Range(0, range + 1);

            return (var == percentage);
        }
    }

    public static class Enums {
        /// <summary>
        /// Get a random enum value
        /// </summary>
        public static Enum GetRandomEnumValue(Type t){
            return Enum.GetValues(t)          // get values from Type provided
                .OfType<Enum>()               // casts to Enum
                .OrderBy(e => Guid.NewGuid()) // mess with order of results
                .FirstOrDefault();            // take first item in result
        }
    }

    public static class Angles {
        /// <summary>
        /// Calculate the angle between 2 points
        /// </summary>
        public static float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Wrap an angle (fix an angle)
        /// </summary>
        public static float WrapAngle(float angle){
            angle%=360;
            if(angle >180)
                return angle - 360;

            return angle;
        }

        /// <summary>
        /// Unwrap an angle (break an angle)
        /// </summary>
        public static float UnwrapAngle(float angle){
            if(angle >=0)
                return angle;

            angle = -angle%360;

            return 360-angle;
        }
    }

    public static class Trigger {
        /// <summary>
        /// Queries if layer is in selected Layer Mask
        /// </summary>
        public static bool IsInLayerMask(int layer, LayerMask layermask){
            return layermask == (layermask | (1 << layer));
        }
    }

    public static class Maths {
        /// <summary>
        /// Check the distance between 2 floats
        /// </summary>
        public static float DistanceBetween2Floats(float a, float b){
            return Mathf.Abs(Mathf.Abs(a) - Mathf.Abs(b));
        }

        /// <summary>
        /// https://forum.unity.com/threads/how-do-i-detect-if-an-object-is-in-front-of-another-object.53188/
        /// Check where a target is in relation to an npc
        /// -1 if the target is directly behind
        /// +1 if directly ahead
        /// 0 if it is side-on to the NPC
        /// </summary>
        public static float GetDotProduct(Vector3 target, Vector3 npc, Vector3 direction){
            Vector3 heading = (target - npc).normalized;
            float dot = Vector3.Dot(heading, direction);

            return dot;
        }

        /// <summary>
        /// Simulate a curve
        /// </summary>
        public static Vector3 GetCurve(Vector3 start, Vector3 end, Vector3 heightdir, Vector2 t){
            return Vector3.Lerp(
                start,
                end + (heightdir * Mathf.PingPong(t.x * 2f, 1f)),
                t.y
            );
        }

        /// <summary>
        /// Calculate GCD
        /// </summary>
        public static int GCD (int a, int b){
            while (a != 0 && b != 0){
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        /// <summary>
        /// Clamp a vector3
        /// </summary>
        public static Vector3 ClampVector3(Vector3 value, Vector3 min, Vector3 max){
            value.x = Mathf.Clamp(value.x, min.x, max.x);
            value.y = Mathf.Clamp(value.y, min.y, max.y);
            value.z = Mathf.Clamp(value.z, min.z, max.z);

            return value;
        }

        /// <summary>
        /// Clamp a vector2
        /// </summary>
        public static Vector2 ClampVector2(Vector2 value, Vector2 min, Vector2 max){
            value.x = Mathf.Clamp(value.x, min.x, max.x);
            value.y = Mathf.Clamp(value.y, min.y, max.y);

            return value;
        }

        /// <summary>
        /// Calculate sin or cos (similar to Mathf.PingPong but with more control)
        /// </summary>
        public static float CalculateSinOrCos(float speed, float amplitude, bool cos){
            float val = 0f;
            float p = Time.fixedTime * Mathf.PI * speed;

            if (cos){
                val += Mathf.Cos(p);
            } else {
                val += Mathf.Sin(p);
            }

            return (val * amplitude);
        }
    }
}
