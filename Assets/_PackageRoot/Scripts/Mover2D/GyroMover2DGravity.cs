using Cysharp.Threading.Tasks;
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
	    protected override void ApplyTransform(GyroTarget target, Vector2 offsetMultiplier)
	    {
            var maxOffsetX = target.maxOffset.x;
            var maxOffsetY = target.maxOffset.y;

            target.target.localPosition = Vector3.Lerp
            (
                target.target.localPosition,
                new Vector3
                (
                    target.OriginalLocalPosition.x + Mathf.Lerp
                    (
                        -maxOffsetX * offsetMultiplier.x,
                        maxOffsetX * offsetMultiplier.x,
                        gravity.x + 0.5f
                    ),
                    target.OriginalLocalPosition.y + Mathf.Lerp
                    (
                        -maxOffsetY * offsetMultiplier.y,
                        maxOffsetY * offsetMultiplier.y,
                        gravity.y + 0.5f
                    ),
                    target.OriginalLocalPosition.z
                ),
                Time.deltaTime * target.speed
            );
        }
    }
}