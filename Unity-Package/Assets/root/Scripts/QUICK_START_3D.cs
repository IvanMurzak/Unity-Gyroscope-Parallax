/*
=== UNITY GYROSCOPE PARALLAX 3D COMPONENTS ===
Solution to Gimbal Lock Issue

PROBLEM SOLVED:
The original 2D components suffered from "Gimbal Lock" causing:
- Incorrect starting positions when holding phone vertically vs horizontally  
- Jerky rotation at certain angles
- Loss of one degree of freedom during rotation

SOLUTION:
New 3D components use Quaternion arithmetic throughout to avoid Gimbal Lock

QUICK START GUIDE:
1. Replace your existing 2D components:
   - GyroRotator2DAttitude → GyroRotator3DAttitude
   - GyroRotator2DGravity  → GyroRotator3DGravity
   - GyroMover2DAttitude   → GyroMover3DAttitude
   - GyroMover2DGravity    → GyroMover3DGravity

2. Update your maxOffset settings:
   - Old: Vector2(x, y) 
   - New: Vector3(x, y, z) - now supports Z-axis!

3. Configure the new inverseZ property if needed

4. Enjoy smooth, consistent gyroscope behavior!

AVAILABLE COMPONENTS:
▶ GyroRotator3DAttitude  - Smooth attitude-based rotation using Quaternions
▶ GyroRotator3DGravity   - Gravity-based rotation with 3D support
▶ GyroMover3DAttitude    - Position movement based on device attitude
▶ GyroMover3DGravity     - Position movement based on gravity vector

TECHNICAL BENEFITS:
✅ No more Gimbal Lock issues
✅ Smooth rotation at all angles  
✅ Consistent behavior regardless of phone orientation
✅ Full 3D support including Z-axis
✅ Better interpolation using Quaternion.Slerp
✅ Same familiar API as 2D components

For detailed documentation, see:
- README_3D_Components.md
- GimbalLockComparisonDemo.cs for side-by-side comparison
- GimbalLockMathValidation.cs for mathematical proof

Happy parallaxing! 🎮📱
*/