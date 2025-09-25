# Gyroscope Parallax 3D Components

This directory contains the new 3D gyroscope components that resolve the **Gimbal Lock** problem found in the 2D components by using Quaternion-based calculations instead of Euler angles.

## The Problem: Gimbal Lock

The original 2D components suffer from Gimbal Lock, which occurs when using Euler angles for rotations. This causes:
- Loss of one degree of freedom when certain angles align
- Unpredictable rotation behavior when the phone orientation matches specific angles
- Incorrect starting positions when holding the phone vertically vs. horizontally

## The Solution: Quaternion-Based 3D Components

The new 3D components resolve this by:
- **Using Quaternion arithmetic** throughout the calculation process
- **Storing original rotations as Quaternions** instead of Euler angles  
- **Only converting to Euler** when necessary for constraint application
- **Using Quaternion.Slerp** for smooth interpolation instead of Quaternion.Lerp
- **Supporting full 3D transformations** including Z-axis rotation/movement

## Available Components

### Rotator Components
- **`GyroRotator3DAttitude`** - Rotates objects based on device attitude using Quaternions
- **`GyroRotator3DGravity`** - Rotates objects based on gravity vector using Quaternions

### Mover Components  
- **`GyroMover3DAttitude`** - Moves objects based on device attitude
- **`GyroMover3DGravity`** - Moves objects based on gravity vector

## Usage

1. Add any of the 3D components to a GameObject instead of the 2D versions
2. Configure the **targets** list with the objects you want to affect
3. Adjust **maxOffset** values for X, Y, and Z axes (3D components support Z-axis)
4. Set **inverseX**, **inverseY**, **inverseZ** to control direction
5. Adjust **speed** and global **speedMultiplier** for responsiveness

## Key Improvements Over 2D Components

| Feature | 2D Components | 3D Components |
|---------|---------------|---------------|
| Gimbal Lock | ❌ Suffers from it | ✅ Resolved |
| Rotation Method | Euler angles | Quaternions |
| Interpolation | Quaternion.Lerp | Quaternion.Slerp |
| Z-axis Support | ❌ Limited | ✅ Full support |
| Phone Orientation | ❌ Inconsistent | ✅ Consistent |
| Smooth Rotation | ⚠️ Can be jerky | ✅ Always smooth |

## Migration from 2D to 3D

To migrate from existing 2D components:

1. **Replace the component**: Change `GyroRotator2DAttitude` → `GyroRotator3DAttitude`
2. **Update maxOffset**: Change from `Vector2` to `Vector3` (add Z value)
3. **Add inverseZ**: New boolean property for Z-axis control
4. **Test and adjust**: The behavior should be smoother and more consistent

## Technical Details

The 3D components avoid Gimbal Lock by:

```csharp
// OLD 2D approach (Gimbal Lock prone):
var euler = attitude.eulerAngles;
// Math operations on euler angles...
target.localRotation = Quaternion.Euler(toX, toY, toZ);

// NEW 3D approach (Gimbal Lock free):
gyroRotation = Quaternion.Inverse(originGyroRotation) * attitude;
// Quaternion operations...
target.localRotation = Quaternion.Slerp(current, target, time);
```

This ensures smooth, predictable rotation regardless of device orientation.