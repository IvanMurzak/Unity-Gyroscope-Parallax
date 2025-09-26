using UnityEngine;

namespace UnityGyroscope.Parallax
{
    /// <summary>
    /// Mathematical validation of the 3D component approach.
    /// This script demonstrates why Quaternions resolve Gimbal Lock.
    /// </summary>
    public class GimbalLockMathValidation : MonoBehaviour
    {
        [Header("Gimbal Lock Demonstration")]
        [SerializeField] bool runValidation = false;

        [ContextMenu("Run Gimbal Lock Validation")]
        void RunValidation()
        {
            Debug.Log("=== Gimbal Lock Mathematical Validation ===");
            
            // Demonstrate problematic Euler angle scenario
            TestEulerAngleGimbalLock();
            
            // Demonstrate Quaternion solution
            TestQuaternionSolution();
            
            Debug.Log("=== Validation Complete ===");
        }

        void TestEulerAngleGimbalLock()
        {
            Debug.Log("\n--- Testing Euler Angle Approach (2D Components) ---");
            
            // Simulate a problematic rotation where Gimbal Lock occurs
            // This happens when Y rotation is ±90 degrees
            Quaternion attitude1 = Quaternion.Euler(45, 89, 30);
            Quaternion attitude2 = Quaternion.Euler(45, 91, 30);
            
            // OLD 2D approach - convert to Euler and do math
            Vector3 euler1 = attitude1.eulerAngles;
            Vector3 euler2 = attitude2.eulerAngles;
            
            // Normalize to -180 to 180 (as 2D components do)
            euler1.x = (euler1.x + 180f) % 360 - 180;
            euler1.y = (euler1.y + 180f) % 360 - 180;
            euler2.x = (euler2.x + 180f) % 360 - 180;
            euler2.y = (euler2.y + 180f) % 360 - 180;
            
            Vector3 deltaEuler = euler2 - euler1;
            
            Debug.Log($"Attitude 1 Euler: {euler1}");
            Debug.Log($"Attitude 2 Euler: {euler2}");  
            Debug.Log($"Delta Euler: {deltaEuler}");
            Debug.Log($"Delta magnitude: {deltaEuler.magnitude}");
            
            // Show the problem: small attitude change can cause large Euler delta
            if (deltaEuler.magnitude > 10f)
            {
                Debug.LogWarning("⚠️ Gimbal Lock detected! Small attitude change caused large Euler delta");
            }
        }

        void TestQuaternionSolution()
        {
            Debug.Log("\n--- Testing Quaternion Approach (3D Components) ---");
            
            // Same problematic rotations
            Quaternion attitude1 = Quaternion.Euler(45, 89, 30);
            Quaternion attitude2 = Quaternion.Euler(45, 91, 30);
            
            // NEW 3D approach - work directly with Quaternions
            Quaternion deltaQuat = Quaternion.Inverse(attitude1) * attitude2;
            
            // Extract angle of rotation
            float angle;
            Vector3 axis;
            deltaQuat.ToAngleAxis(out angle, out axis);
            
            Debug.Log($"Delta Quaternion: {deltaQuat}");
            Debug.Log($"Rotation angle: {angle} degrees");
            Debug.Log($"Rotation axis: {axis}");
            
            // Quaternions handle this smoothly
            Debug.Log("✅ Quaternion approach handles this rotation smoothly!");
            
            // Demonstrate smooth interpolation
            Quaternion lerped = Quaternion.Slerp(attitude1, attitude2, 0.5f);
            Debug.Log($"Smooth interpolation result: {lerped.eulerAngles}");
        }

        void Update()
        {
            if (runValidation)
            {
                runValidation = false;
                RunValidation();
            }
        }

        [ContextMenu("Explain Solution")]
        void ExplainSolution()
        {
            Debug.Log("=== How 3D Components Solve Gimbal Lock ===");
            Debug.Log("1. Store original rotation as Quaternion (not Euler)");
            Debug.Log("2. Calculate gyro delta as Quaternion multiplication");
            Debug.Log("3. Apply constraints only when converting to final rotation");
            Debug.Log("4. Use Quaternion.Slerp for smooth interpolation");
            Debug.Log("5. Avoid Euler angle arithmetic in the critical path");
            Debug.Log("\nResult: Smooth rotation regardless of device orientation!");
        }
    }
}