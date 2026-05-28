using UnityEngine;

namespace UnityGyroscope.Parallax
{
    /// <summary>
    /// Comparison demo script showing the difference between 2D and 3D components.
    /// This script can be used to set up a side-by-side comparison in a demo scene.
    /// </summary>
    public class GimbalLockComparisonDemo : MonoBehaviour
    {
        [Header("Comparison Setup")]
        [SerializeField] GameObject cube2D;
        [SerializeField] GameObject cube3D;
        [SerializeField] bool autoSetupComponents = true;

        [Header("Test Settings")]
        [SerializeField] Vector3 maxOffset = new Vector3(30, 30, 30);
        [SerializeField] float speed = 5f;

        void Start()
        {
            if (autoSetupComponents)
                SetupComparison();
        }

        void SetupComparison()
        {
            if (cube2D == null || cube3D == null)
            {
                Debug.LogWarning("Please assign cube2D and cube3D GameObjects to see the comparison.");
                return;
            }

            // Setup 2D component (prone to Gimbal Lock)
            var rotator2D = cube2D.GetComponent<GyroRotator2DAttitude>() ?? cube2D.AddComponent<GyroRotator2DAttitude>();
            var target2D = new GyroRotator2D.GyroTarget
            {
                target = cube2D.transform,
                maxOffset = new Vector2(maxOffset.x, maxOffset.y),
                speed = speed,
                inverseX = true,
                inverseY = true
            };

            // Use reflection to set the targets list since it's private
            var targets2DField = typeof(GyroRotator2D).GetField("targets", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (targets2DField != null)
            {
                var targetsList2D = new System.Collections.Generic.List<GyroRotator2D.GyroTarget> { target2D };
                targets2DField.SetValue(rotator2D, targetsList2D);
            }

            // Setup 3D component (Gimbal Lock free)
            var rotator3D = cube3D.GetComponent<GyroRotator3DAttitude>() ?? cube3D.AddComponent<GyroRotator3DAttitude>();
            var target3D = new GyroRotator3D.GyroTarget
            {
                target = cube3D.transform,
                maxOffset = maxOffset,
                speed = speed,
                inverseX = true,
                inverseY = true,
                inverseZ = false
            };

            var targets3DField = typeof(GyroRotator3D).GetField("targets", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (targets3DField != null)
            {
                var targetsList3D = new System.Collections.Generic.List<GyroRotator3D.GyroTarget> { target3D };
                targets3DField.SetValue(rotator3D, targetsList3D);
            }

            // Position cubes for comparison
            cube2D.transform.position = new Vector3(-2, 0, 0);
            cube3D.transform.position = new Vector3(2, 0, 0);

            // Add labels
            CreateLabel(cube2D.transform, "2D Component\n(Gimbal Lock)", new Vector3(0, 2, 0));
            CreateLabel(cube3D.transform, "3D Component\n(Gimbal Lock Free)", new Vector3(0, 2, 0));

            Debug.Log("=== Gimbal Lock Comparison Demo ===");
            Debug.Log("Left Cube: 2D Component (may experience Gimbal Lock)");
            Debug.Log("Right Cube: 3D Component (Gimbal Lock resistant)");
            Debug.Log("Test by rotating your device to extreme angles - the 3D component should remain smooth.");
        }

        void CreateLabel(Transform parent, string text, Vector3 offset)
        {
            GameObject labelObject = new GameObject("Label");
            labelObject.transform.SetParent(parent);
            labelObject.transform.localPosition = offset;

            // In a real Unity project, you'd use TextMesh or TextMeshPro here
            // For this demo, we'll just log the setup
            labelObject.name = $"Label_{text.Replace("\n", "_").Replace(" ", "_")}";
        }

        [ContextMenu("Explain Gimbal Lock")]
        void ExplainGimbalLock()
        {
            Debug.Log("=== What is Gimbal Lock? ===");
            Debug.Log("Gimbal Lock occurs when using Euler angles and two rotation axes align,");
            Debug.Log("causing loss of one degree of freedom and unpredictable rotation behavior.");
            Debug.Log("");
            Debug.Log("In gyroscope applications, this manifests as:");
            Debug.Log("- Incorrect starting positions based on phone orientation");
            Debug.Log("- Jerky or unpredictable rotation at certain angles");
            Debug.Log("- Objects 'snapping' to unexpected orientations");
            Debug.Log("");
            Debug.Log("The 3D components solve this by using Quaternions instead of Euler angles!");
        }
    }
}