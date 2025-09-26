using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityGyroscope.Parallax;

namespace Extensions.Unity.Gyroscope.Parallax.Tests
{
    public class Gyro3DComponentsTest
    {
        private GameObject testObject;

        [SetUp]
        public void SetUp()
        {
            testObject = new GameObject("TestObject");
        }

        [TearDown]
        public void TearDown()
        {
            if (testObject != null)
                Object.DestroyImmediate(testObject);
        }

        [Test]
        public void GyroRotator3DAttitude_CanBeAddedToGameObject()
        {
            // Test that the component can be added without errors
            var component = testObject.AddComponent<GyroRotator3DAttitude>();
            Assert.IsNotNull(component);
        }

        [Test]
        public void GyroRotator3DGravity_CanBeAddedToGameObject()
        {
            var component = testObject.AddComponent<GyroRotator3DGravity>();
            Assert.IsNotNull(component);
        }

        [Test]
        public void GyroMover3DAttitude_CanBeAddedToGameObject()
        {
            var component = testObject.AddComponent<GyroMover3DAttitude>();
            Assert.IsNotNull(component);
        }

        [Test]
        public void GyroMover3DGravity_CanBeAddedToGameObject()
        {
            var component = testObject.AddComponent<GyroMover3DGravity>();
            Assert.IsNotNull(component);
        }

        [Test]
        public void GyroRotator3D_TargetStoresOriginalRotation()
        {
            var targetObj = new GameObject("Target");
            var originalRotation = Quaternion.Euler(45, 30, 15);
            targetObj.transform.localRotation = originalRotation;

            var component = testObject.AddComponent<GyroRotator3DAttitude>();
            var target = new GyroRotator3D.GyroTarget 
            { 
                target = targetObj.transform 
            };

            // Manually set the original rotation (simulating OnEnable)
            target.OriginalLocalRotation = targetObj.transform.localRotation;

            Assert.AreEqual(originalRotation, target.OriginalLocalRotation);

            Object.DestroyImmediate(targetObj);
        }

        [Test]
        public void GyroMover3D_TargetStoresOriginalPosition()
        {
            var targetObj = new GameObject("Target");
            var originalPosition = new Vector3(10, 20, 30);
            targetObj.transform.localPosition = originalPosition;

            var component = testObject.AddComponent<GyroMover3DAttitude>();
            var target = new GyroMover3D.GyroTarget 
            { 
                target = targetObj.transform 
            };

            // Manually set the original position (simulating OnEnable)
            target.OriginalLocalPosition = targetObj.transform.localPosition;

            Assert.AreEqual(originalPosition, target.OriginalLocalPosition);

            Object.DestroyImmediate(targetObj);
        }
    }
}