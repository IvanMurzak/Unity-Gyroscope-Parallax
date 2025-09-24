using UnityEngine;
using Gyroscope = UnityGyroscope.Manager.Gyroscope;

namespace UnityGyroscope.Parallax
{
    public class GyroMover2DGravity : GyroMover2D
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
        protected override void ApplyTransform(GyroTarget target, Vector2 offsetMultiplier)
        {
            target.target.localPosition = Vector3.Lerp
            (
                target.target.localPosition,
                new Vector3
                (
                    x: target.OriginalLocalPosition.x + Mathf.Lerp
                    (
                        -target.maxOffset.x * offsetMultiplier.x,
                        target.maxOffset.x * offsetMultiplier.x,
                        (target.inverseX ? -gravity.x : gravity.x) + 0.5f
                    ),
                    y: target.OriginalLocalPosition.y + Mathf.Lerp
                    (
                        -target.maxOffset.y * offsetMultiplier.y,
                        target.maxOffset.y * offsetMultiplier.y,
                        (target.inverseY ? -gravity.y : gravity.y) + 0.5f
                    ),
                    z: target.OriginalLocalPosition.z
                ),
                Time.deltaTime * target.speed * speedMultiplier
            );
        }
    }
}