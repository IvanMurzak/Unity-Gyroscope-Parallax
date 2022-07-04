using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Gyroscope = UnityGyroscope.Manager.Gyroscope;

namespace UnityGyroscope.Parallax
{ 
    public abstract class GyroMover2D : MonoBehaviour
    {
                                    public          float               speedMultiplier = 1;
                                    public          Vector2             offsetMultiplier = Vector2.one;
        [SerializeField, Required]                  List<GyroTarget>    targets = new List<GyroTarget>();


        protected virtual async UniTask OnEnable()
        {
            if (!Gyroscope.Instance.HasGyroscope)
                return;

            foreach (var target in targets)
                target.OriginalLocalPosition = target.target.localPosition;

            Subscribe();
        }
        protected virtual void OnDisable()
        {
            if (!Gyroscope.Instance.HasGyroscope)
                return;

            Unsubscribe();

            foreach (var target in targets)
                target.target.localPosition = target.OriginalLocalPosition;
        }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();
        protected abstract void OnUpdatePrepeare();
        protected abstract void ApplyTransform(GyroTarget target, Vector2 offsetMultiplier);

        protected virtual void Update()
        {
            if (!Gyroscope.Instance.HasGyroscope)
                return;

            OnUpdatePrepeare();

            foreach (var target in targets)
            {
                if (target != null)
                    ApplyTransform(target, offsetMultiplier);
            }
        }

        [Serializable]
        public class GyroTarget
	    {
            public Transform    target;
            public float        speed       = 1;
            public Vector2      maxOffset   = new Vector2(100, 100);

            public Vector3      OriginalLocalPosition { get; set; }
	    }
    }
}