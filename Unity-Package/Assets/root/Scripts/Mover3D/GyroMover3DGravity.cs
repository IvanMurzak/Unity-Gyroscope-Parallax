using UnityEngine;
using Gyroscope = UnityGyroscope.Manager.Gyroscope;

namespace UnityGyroscope.Parallax
{
    public class GyroMover3DGravity : GyroMover3D
    {
        Vector3 gravity;
        Vector3 originGravity;

        protected override void OnEnable()
        {
            base.OnEnable();
            originGravity = Gyroscope.Instance.Gravity.Value;
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
            gravity = (Gyroscope.Instance.Gravity.Value - originGravity).normalized;
        }

        protected override void ApplyTransform(GyroTarget target, Vector2 offsetMultiplier)
        {
            Vector3 targetPosition = new Vector3(
                target.OriginalLocalPosition.x + Mathf.Lerp(
                    -target.maxOffset.x * offsetMultiplier.x,
                    target.maxOffset.x * offsetMultiplier.x,
                    (target.inverseX ? -gravity.x : gravity.x) + 0.5f
                ),
                target.OriginalLocalPosition.y + Mathf.Lerp(
                    -target.maxOffset.y * offsetMultiplier.y,
                    target.maxOffset.y * offsetMultiplier.y,
                    (target.inverseY ? -gravity.y : gravity.y) + 0.5f
                ),
                target.OriginalLocalPosition.z + Mathf.Lerp(
                    -target.maxOffset.z,
                    target.maxOffset.z,
                    (target.inverseZ ? -gravity.z : gravity.z) + 0.5f
                )
            );

            target.target.localPosition = Vector3.Lerp(
                target.target.localPosition,
                targetPosition,
                Time.deltaTime * target.speed * speedMultiplier
            );
        }
    }
}