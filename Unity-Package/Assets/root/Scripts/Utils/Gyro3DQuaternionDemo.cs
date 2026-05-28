using UnityEngine;

namespace UnityGyroscope.Parallax
{
    /// <summary>
    /// Demonstration script that shows the key differences between 2D and 3D gyroscope components.
    /// 
    /// The main improvement in 3D components:
    /// - Uses Quaternion operations throughout to avoid Gimbal Lock
    /// - Stores and works with original rotation as Quaternion
    /// - Only converts to Euler when necessary for constraint application
    /// - Uses Quaternion.Slerp for smooth interpolation
    /// 
    /// This resolves the Gimbal Lock issue that occurs in 2D components when using Euler angles.
    /// </summary>
    public class Gyro3DQuaternionDemo : MonoBehaviour
    {
        [Header("Demonstration of Quaternion vs Euler Approach")]
        [SerializeField] bool showDifference = true;
        
        void Start()
        {
            if (showDifference)
            {
                Debug.Log("=== Gyroscope Parallax 3D Components ===");
                Debug.Log("Key improvements over 2D components:");
                Debug.Log("1. Uses Quaternion arithmetic to avoid Gimbal Lock");
                Debug.Log("2. Stores original rotation as Quaternion");
                Debug.Log("3. Only converts to Euler for constraint application");
                Debug.Log("4. Uses Quaternion.Slerp for smooth interpolation");
                Debug.Log("5. Supports full 3D rotation including Z-axis");
            }
        }

        /// <summary>
        /// Example of how the 3D components avoid Gimbal Lock by using Quaternions
        /// </summary>
        public void ExplainQuaternionApproach()
        {
            // Old 2D approach (Gimbal Lock prone):
            // 1. Convert Quaternion to Euler angles
            // 2. Do math with Euler angles  
            // 3. Apply constraints to Euler angles
            // 4. Convert back to Quaternion with Quaternion.Euler()
            // 5. This can cause Gimbal Lock when certain angles align
            
            // New 3D approach (Gimbal Lock free):
            // 1. Work directly with Quaternions when possible
            // 2. Store original rotation as Quaternion
            // 3. Calculate delta rotation as Quaternion
            // 4. Only convert to Euler for constraint application
            // 5. Apply final rotation using Quaternion.Slerp
            
            Debug.Log("3D Components use Quaternion operations to avoid Gimbal Lock!");
        }
    }
}