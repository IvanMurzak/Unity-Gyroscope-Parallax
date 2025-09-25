using UnityEngine;
using Gyroscope = UnityGyroscope.Manager.Gyroscope;

namespace UnityGyroscope.Parallax
{
    public class GyroRotator3DGravity : GyroRotator3D
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
            // Convert gravity vector to rotation angles with constraints
            float rotationX = Mathf.Lerp(
                -target.maxOffset.x * offsetMultiplier.x,
                target.maxOffset.x * offsetMultiplier.x,
                (target.inverseX ? -gravity.x : gravity.x) + 0.5f
            );
            
            float rotationY = Mathf.Lerp(
                -target.maxOffset.y * offsetMultiplier.y,
                target.maxOffset.y * offsetMultiplier.y,
                (target.inverseY ? -gravity.y : gravity.y) + 0.5f
            );
            
            float rotationZ = Mathf.Lerp(
                -target.maxOffset.z,
                target.maxOffset.z,
                (target.inverseZ ? -gravity.z : gravity.z) + 0.5f
            );

            // Create target rotation as quaternion
            Quaternion gravityRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
            Quaternion targetRotation = target.OriginalLocalRotation * gravityRotation;

            // Apply smooth rotation using Slerp
            target.target.localRotation = Quaternion.Slerp(
                target.target.localRotation,
                targetRotation,
                Time.deltaTime * target.speed * speedMultiplier
            );
        }
    }
}