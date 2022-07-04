using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Gyroscope = UnityGyroscope.Manager.Gyroscope;

namespace UnityGyroscope.Parallax
{ 
    public abstract class GyroRotator2D : MonoBehaviour
    {
                                    public          float               speedMultiplier = 1;
                                    public          Vector2             offsetMultiplier = Vector2.one;
        [SerializeField, Required]                  List<GyroTarget>    targets = new List<GyroTarget>();


        protected virtual async UniTask OnEnable()
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
        protected abstract void OnUpdatePrepeare();
        protected abstract void ApplyTransform(GyroTarget target, Vector2 offsetMultiplier, float toX, float toY, float toZ);
        protected abstract float CalcToX(GyroTarget target, Vector2 offsetMultiplier);
        protected abstract float CalcToY(GyroTarget target, Vector2 offsetMultiplier);
        protected abstract float CalcToZ(GyroTarget target, Vector2 offsetMultiplier);

        protected virtual void Update()
        {
            if (!Gyroscope.Instance.HasGyroscope)
                return;

            OnUpdatePrepeare();

            foreach (var target in targets)
            {
                if (target != null)
                {
                    var toX = CalcToX(target, offsetMultiplier);
                    var toY = CalcToY(target, offsetMultiplier);
                    var toZ = CalcToZ(target, offsetMultiplier);

                    if (target.axes == Axes.XY) ApplyTransform(target, offsetMultiplier, toX, toY, toZ);
                    if (target.axes == Axes.XZ) ApplyTransform(target, offsetMultiplier, toX, toZ, toY);
                    if (target.axes == Axes.YZ) ApplyTransform(target, offsetMultiplier, toZ, toX, toY);
                    if (target.axes == Axes.YX) ApplyTransform(target, offsetMultiplier, toY, toX, toZ);
                    if (target.axes == Axes.ZX) ApplyTransform(target, offsetMultiplier, toY, toZ, toX);
                    if (target.axes == Axes.ZY) ApplyTransform(target, offsetMultiplier, toZ, toY, toX);
                }
            }
        }

        [Serializable]
        public class GyroTarget
	    {
            public Transform    target;
            public float        speed       = 1;
            public Vector2      maxOffset   = new Vector2(100, 100);
            public Axes         axes        = Axes.XY;

            public Quaternion   OriginalLocalRotation { get; set; }
	    }
    }
}