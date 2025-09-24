using System.Collections;
using UnityEngine;
using Gyroscope = UnityGyroscope.Manager.Gyroscope;

namespace UnityGyroscope.Parallax
{
    public class GyroRotator2DAttitude : GyroRotator2D
    {
        Vector3 gyroEuler;
        Vector3 originGyroEulerAngles;

        Vector3 Correct(Quaternion attitude)
        {
            var euler = attitude.eulerAngles;
            euler.x = (euler.x + 180f) % 360 - 180;
            euler.y = (euler.y + 180f) % 360 - 180;

            return -euler;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            StartCoroutine(InitializeAfterFrame());
        }

        IEnumerator InitializeAfterFrame()
        {
            yield return null;
            originGyroEulerAngles = Correct(Gyroscope.Instance.Attitude.Value);
        }
        protected override void OnDisable()
        {
            if (!Gyroscope.Instance.HasGyroscope)
                return;

            originGyroEulerAngles = Correct(Gyroscope.Instance.Attitude.Value);

            base.OnDisable();
        }
        protected override void Subscribe()
        {
            Gyroscope.Instance.SubscribeAttitude();
        }

        protected override void Unsubscribe()
        {
            Gyroscope.Instance.UnsubscribeAttitude();
        }

        private float RoundInRange(float min, float max, float value)
            => Mathf.Max(min, Mathf.Min(max, value));
        private float SmoothPower(float min, float max, float value)
            => 1f - (Mathf.Min(Mathf.Abs(value - min), Mathf.Abs(value - max)) / ((max - min) / 2f));

        protected override void OnUpdatePrepare()
        {
            gyroEuler = Correct(Gyroscope.Instance.Attitude.Value) - originGyroEulerAngles;
        }
        protected override void ApplyTransform(GyroTarget target, Vector2 offsetMultiplier, float toX, float toY, float toZ)
        {
            target.target.localRotation = Quaternion.Lerp
            (
                a: target.target.localRotation,
                b: Quaternion.Euler(toX, toY, toZ),
                t: Time.deltaTime * target.speed * speedMultiplier
            );
        }

        protected override float CalcToX(GyroTarget target, Vector2 offsetMultiplier)
        {
            var maxOffsetX = Mathf.Abs(target.maxOffset.x);
            return target.OriginalLocalRotation.x + RoundInRange
            (
                min: -maxOffsetX * offsetMultiplier.x,
                max: maxOffsetX * offsetMultiplier.x,
                value: target.inverseX ? -gyroEuler.x : gyroEuler.x
            );
        }

        protected override float CalcToY(GyroTarget target, Vector2 offsetMultiplier)
        {
            var maxOffsetY = Mathf.Abs(target.maxOffset.y);
            return target.OriginalLocalRotation.y + RoundInRange
            (
                min: -maxOffsetY * offsetMultiplier.y,
                max: maxOffsetY * offsetMultiplier.y,
                value: target.inverseY ? -gyroEuler.y : gyroEuler.y
            );
        }

        protected override float CalcToZ(GyroTarget target, Vector2 offsetMultiplier)
        {
            return target.OriginalLocalRotation.z;
        }
    }
}