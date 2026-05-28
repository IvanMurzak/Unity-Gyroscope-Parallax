using System.Collections;
using UnityEngine;
using Gyroscope = UnityGyroscope.Manager.Gyroscope;

namespace UnityGyroscope.Parallax
{
    public class GyroMover3DAttitude : GyroMover3D
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

        private float RoundInRange(float min, float max, float value)
            => Mathf.Max(min, Mathf.Min(max, value));

        protected override void OnUpdatePrepare()
        {
            // Calculate the delta rotation from the origin quaternion
            gyroRotation = Quaternion.Inverse(originGyroRotation) * Gyroscope.Instance.Attitude.Value;
        }

        protected override void ApplyTransform(GyroTarget target, Vector2 offsetMultiplier)
        {
            // Extract euler angles for position calculation
            Vector3 gyroEuler = gyroRotation.eulerAngles;
            
            // Normalize angles to -180 to 180 range
            gyroEuler.x = (gyroEuler.x + 180f) % 360 - 180;
            gyroEuler.y = (gyroEuler.y + 180f) % 360 - 180;
            gyroEuler.z = (gyroEuler.z + 180f) % 360 - 180;

            var maxOffsetX = Mathf.Abs(target.maxOffset.x);
            var maxOffsetY = Mathf.Abs(target.maxOffset.y);
            var maxOffsetZ = Mathf.Abs(target.maxOffset.z);

            Vector3 targetPosition = new Vector3(
                target.OriginalLocalPosition.x + RoundInRange(
                    -maxOffsetX * offsetMultiplier.x,
                    maxOffsetX * offsetMultiplier.x,
                    target.inverseX ? -gyroEuler.x : gyroEuler.x
                ),
                target.OriginalLocalPosition.y + RoundInRange(
                    -maxOffsetY * offsetMultiplier.y,
                    maxOffsetY * offsetMultiplier.y,
                    target.inverseY ? -gyroEuler.y : gyroEuler.y
                ),
                target.OriginalLocalPosition.z + RoundInRange(
                    -maxOffsetZ,
                    maxOffsetZ,
                    target.inverseZ ? -gyroEuler.z : gyroEuler.z
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