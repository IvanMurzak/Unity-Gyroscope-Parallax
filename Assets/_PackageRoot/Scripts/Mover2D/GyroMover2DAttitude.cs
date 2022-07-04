using Cysharp.Threading.Tasks;
using UnityEngine;
using Gyroscope = UnityGyroscope.Manager.Gyroscope;

namespace UnityGyroscope.Parallax
{
    public class GyroMover2DAttitude : GyroMover2D
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


        protected override async UniTask OnEnable()
        {
            await base.OnEnable();

            await UniTask.DelayFrame(1);
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

        private float RoundInRange(float min, float max, float value) => Mathf.Max(min, Mathf.Min(max, value));
        private float SmoothPower(float min, float max, float value) => 1f - (Mathf.Min(Mathf.Abs(value - min), Mathf.Abs(value - max)) / ((max - min) / 2f));

	    protected override void OnUpdatePrepeare()
	    {
            gyroEuler = Correct(Gyroscope.Instance.Attitude.Value) - originGyroEulerAngles;
        }
	    protected override void ApplyTransform(GyroTarget target, Vector2 offsetMultiplier)
	    {
            var maxOffsetX = Mathf.Abs(target.maxOffset.x);
            var maxOffsetY = Mathf.Abs(target.maxOffset.y);

            target.target.localPosition = Vector3.Lerp
            (
                target.target.localPosition,
                new Vector3
                (
                    target.OriginalLocalPosition.x + RoundInRange
                    (
                        -maxOffsetX * offsetMultiplier.x,
                        maxOffsetX * offsetMultiplier.x,
                        gyroEuler.x
                    ),
                    target.OriginalLocalPosition.y + RoundInRange
                    (
                        -maxOffsetY * offsetMultiplier.y,
                        maxOffsetY * offsetMultiplier.y,
                        gyroEuler.y
                    ),
                    target.OriginalLocalPosition.z
                ),
                Time.deltaTime * target.speed
            );
        }
    }
}