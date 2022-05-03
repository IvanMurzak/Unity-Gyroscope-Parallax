# Unity-Gyroscope-Parallax
![npm](https://img.shields.io/npm/v/extensions.unity.gyroscope.parallax) ![License](https://img.shields.io/github/license/IvanMurzak/Unity-Gyroscope-Parallax)

Unity Parallax based on gyroscope components.

# GyroMover2D
Moves list of objects using gyroscope.

![image](https://user-images.githubusercontent.com/9135028/166463235-50702210-3b09-417d-9b9a-547fce73ba15.png) ![image](https://user-images.githubusercontent.com/9135028/166465109-33274de8-84e3-44e4-a8ab-b7c1f3ea2380.png)


# GyroRotator2D
Rotates list of objects using gyroscope.

![image](https://user-images.githubusercontent.com/9135028/166463296-7b50aae8-9f43-442d-978a-b2b479170d94.png) ![image](https://user-images.githubusercontent.com/9135028/166465157-5f1325f3-8109-4a35-bd91-87082aa36cf9.png)


# How to install
- Add this code to <code>/Packages/manifest.json</code>
```json
{
  "dependencies": {
    "extensions.unity.gyroscope.parallax": "1.0.0",
  },
  "scopedRegistries": [
    {
      "name": "Unity Extensions",
      "url": "https://registry.npmjs.org",
      "scopes": [
        "extensions.unity"
      ]
    }
  ]
}
```

# How to use
- add needed `Gyro...` component to any GameObject
- link Targets to list of targets
- press 'Play' button in Unity Editor
- find `Fake Gyroscope Manager` in `DonDestroyOnLoad` scene (appears in Play Mode)
- ![image](https://user-images.githubusercontent.com/9135028/166464685-b6197e8a-547d-47ab-9039-824ce29f3ca5.png)
- change XY values of `Gravity` and `Attitude` properties to simulate gyroscope in Unity Editor