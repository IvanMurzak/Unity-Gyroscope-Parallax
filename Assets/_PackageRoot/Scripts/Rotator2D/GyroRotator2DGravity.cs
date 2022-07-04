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

	    protected override void OnUpdatePrepeare()
	    {
            gravity = (Correct(Gyroscope.Instance.Gravity.Value) - originGravity).normalized;
        }
        protected override void ApplyTransform(GyroTarget target, Vector2 offsetMultiplier, float toX, float toY, float toZ)
        {
            target.target.localRotation = Quaternion.Lerp
            (
                target.target.localRotation,
                Quaternion.Euler(toX, toY, toZ),
                Time.deltaTime * target.speed * speedMultiplier
            );
        }

        protected override float CalcToX(GyroTarget target, Vector2 offsetMultiplier)
        {
            return target.OriginalLocalRotation.x + Mathf.Lerp
            (
                -target.maxOffset.x * offsetMultiplier.x,
                target.maxOffset.x * offsetMultiplier.x,
                gravity.x + 0.5f
            );
        }

        protected override float CalcToY(GyroTarget target, Vector2 offsetMultiplier)
        {
            return target.OriginalLocalRotation.y + Mathf.Lerp
            (
                -target.maxOffset.y * offsetMultiplier.y,
                target.maxOffset.y * offsetMultiplier.y,
                gravity.y + 0.5f
            );
        }

        protected override float CalcToZ(GyroTarget target, Vector2 offsetMultiplier)
        {
            return target.OriginalLocalRotation.z;
        }
    }
}