using Cysharp.Threading.Tasks;
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

        protected override async UniTask OnEnable()
        {
            await base.OnEnable();
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

        private float RoundInRange(float min, float max, float value) => Mathf.Max(min, Mathf.Min(max, value));

	    protected override void OnUpdatePrepeare()
	    {
            gravity = Correct(Gyroscope.Instance.Gravity.Value) - originGravity;
        }
        protected override void ApplyTransform(GyroTarget target, Vector2 powerMultiplier, Vector2 offsetMultiplier, float toX, float toY, float toZ)
        {
            target.target.localRotation = Quaternion.Lerp
            (
                target.target.localRotation,
                Quaternion.Euler(toX, toY, toZ),
                Time.deltaTime * target.speed
            );
        }

        protected override float CalcToX(GyroTarget target, Vector2 powerMultiplier, Vector2 offsetMultiplier)
        {
            var maxOffsetX = Mathf.Abs(target.maxOffset.x);
            return target.OriginalLocalRotation.x + RoundInRange
            (
                -maxOffsetX * offsetMultiplier.x,
                maxOffsetX * offsetMultiplier.x,
                gravity.x * target.power.x * powerMultiplier.x
            );
        }

        protected override float CalcToY(GyroTarget target, Vector2 powerMultiplier, Vector2 offsetMultiplier)
        {
            var maxOffsetY = Mathf.Abs(target.maxOffset.y);
            return target.OriginalLocalRotation.y + RoundInRange
            (
                -maxOffsetY * offsetMultiplier.y,
                maxOffsetY * offsetMultiplier.y,
                gravity.y * target.power.y * powerMultiplier.y
            );
        }

        protected override float CalcToZ(GyroTarget target, Vector2 powerMultiplier, Vector2 offsetMultiplier)
        {
            return target.OriginalLocalRotation.z;
        }
    }
}