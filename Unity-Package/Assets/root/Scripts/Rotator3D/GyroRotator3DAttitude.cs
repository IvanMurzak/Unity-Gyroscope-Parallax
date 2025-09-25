using System.Collections;
using UnityEngine;
using Gyroscope = UnityGyroscope.Manager.Gyroscope;

namespace UnityGyroscope.Parallax
{
    public class GyroRotator3DAttitude : GyroRotator3D
    {
        Quaternion gyroRotation;
        Quaternion originGyroRotation;

        protected override void OnEnable()
        {
            base.OnEnable();

            StartCoroutine(InitializeAfterFrame());
        }

        IEnumerator InitializeAfterFrame()
        {
            yield return null;
            originGyroRotation = Gyroscope.Instance.Attitude.Value;
        }

        protected override void OnDisable()
        {
            if (!Gyroscope.Instance.HasGyroscope)
                return;

            originGyroRotation = Gyroscope.Instance.Attitude.Value;

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

        protected override void OnUpdatePrepare()
        {
            // Calculate the delta rotation from the origin quaternion
            // This gives us the relative rotation change since initialization
            gyroRotation = Quaternion.Inverse(originGyroRotation) * Gyroscope.Instance.Attitude.Value;
        }

        protected override void ApplyTransform(GyroTarget target, Vector2 offsetMultiplier)
        {
            // Extract euler angles only for applying constraints
            Vector3 gyroEuler = gyroRotation.eulerAngles;
            
            // Normalize angles to -180 to 180 range
            gyroEuler.x = (gyroEuler.x + 180f) % 360 - 180;
            gyroEuler.y = (gyroEuler.y + 180f) % 360 - 180;
            gyroEuler.z = (gyroEuler.z + 180f) % 360 - 180;

            // Apply offset constraints and inversion
            float constrainedX = Mathf.Clamp(
                (target.inverseX ? -gyroEuler.x : gyroEuler.x), 
                -target.maxOffset.x * offsetMultiplier.x, 
                target.maxOffset.x * offsetMultiplier.x
            );
            
            float constrainedY = Mathf.Clamp(
                (target.inverseY ? -gyroEuler.y : gyroEuler.y), 
                -target.maxOffset.y * offsetMultiplier.y, 
                target.maxOffset.y * offsetMultiplier.y
            );
            
            float constrainedZ = Mathf.Clamp(
                (target.inverseZ ? -gyroEuler.z : gyroEuler.z), 
                -target.maxOffset.z, 
                target.maxOffset.z
            );

            // Create the constrained rotation as a quaternion
            Quaternion constrainedRotation = Quaternion.Euler(constrainedX, constrainedY, constrainedZ);
            
            // Apply the rotation to the original rotation
            Quaternion targetRotation = target.OriginalLocalRotation * constrainedRotation;

            // Smoothly interpolate to the target rotation
            target.target.localRotation = Quaternion.Slerp(
                target.target.localRotation,
                targetRotation,
                Time.deltaTime * target.speed * speedMultiplier
            );
        }
    }
}