using UnityEngine;
using Gyroscope = UnityGyroscope.Manager.Gyroscope;

namespace UnityGyroscope.Parallax
{
    public class GyroRotator2DGravity : GyroRotator2D
    {
        Vector3 gravity;
        Vector3 originGravity;

        Vector3 Correct(Vector3 gravity)
        {
            return gravity;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
        protected override void OnDisable()
        {
            if (!Gyroscope.Instance.HasGyroscope)
                return;

            base.OnDisable();
        }
        protected override void Subscribe()
        {
            Gyroscope.Instance.SubscribeGravity();
        }

        protected override void Unsubscribe()
        {
            Gyroscope.Instance.UnsubscribeGravity();
        }

        protected override void OnUpdatePrepare()
        {
            gravity = (Correct(Gyroscope.Instance.Gravity.Value) - originGravity).normalized;
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
            return target.OriginalLocalRotation.x + Mathf.Lerp
            (
                a: -target.maxOffset.x * offsetMultiplier.x,
                b: target.maxOffset.x * offsetMultiplier.x,
                t: (target.inverseX ? -gravity.x : gravity.x) + 0.5f
            );
        }

        protected override float CalcToY(GyroTarget target, Vector2 offsetMultiplier)
        {
            return target.OriginalLocalRotation.y + Mathf.Lerp
            (
                a: -target.maxOffset.y * offsetMultiplier.y,
                b: target.maxOffset.y * offsetMultiplier.y,
                t: (target.inverseY ? -gravity.y : gravity.y) + 0.5f
            );
        }

        protected override float CalcToZ(GyroTarget target, Vector2 offsetMultiplier)
        {
            return target.OriginalLocalRotation.z;
        }
    }
}