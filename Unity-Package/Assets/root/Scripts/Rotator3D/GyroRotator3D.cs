using System;
using System.Collections.Generic;
using UnityEngine;
using Gyroscope = UnityGyroscope.Manager.Gyroscope;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace UnityGyroscope.Parallax
{
    public abstract class GyroRotator3D : MonoBehaviour
    {
        public float speedMultiplier = 1;
        public Vector2 offsetMultiplier = Vector2.one;

#if ODIN_INSPECTOR
        [Required]
#endif
        [SerializeField] List<GyroTarget> targets = new List<GyroTarget>();

        protected virtual void OnEnable()
        {
            if (!Gyroscope.Instance.HasGyroscope)
                return;

            foreach (var target in targets)
                target.OriginalLocalRotation = target.target.localRotation;

            Subscribe();
        }
        
        protected virtual void OnDisable()
        {
            if (!Gyroscope.Instance.HasGyroscope)
                return;

            Unsubscribe();

            foreach (var target in targets)
                target.target.localRotation = target.OriginalLocalRotation;
        }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();
        protected abstract void OnUpdatePrepare();
        protected abstract void ApplyTransform(GyroTarget target, Vector2 offsetMultiplier);

        protected virtual void Update()
        {
            if (!Gyroscope.Instance.HasGyroscope)
                return;

            OnUpdatePrepare();

            foreach (var target in targets)
            {
                if (target != null)
                    ApplyTransform(target, offsetMultiplier);
            }
        }

        [Serializable]
        public class GyroTarget
        {
            public Transform target;
            public bool inverseX = true;
            public bool inverseY = true;
            public bool inverseZ = false;
            public float speed = 1;
            public Vector3 maxOffset = new Vector3(30, 30, 30);
            public Axes axes = Axes.XY;

            public Quaternion OriginalLocalRotation { get; set; }
        }
    }
}