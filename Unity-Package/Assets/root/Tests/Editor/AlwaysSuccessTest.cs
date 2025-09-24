using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Extensions.Unity.Gyroscope.Parallax.Tests
{
    public class Test
    {
        [SetUp]
        public void SetUp()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void AlwaysSuccessfulTest()
        {
            Assert.IsTrue(true);
        }

        [UnityTest]
        public IEnumerator AlwaysSuccessfulCoroutineTest()
        {
            yield return null;
            Assert.IsTrue(true);
        }
    }
}