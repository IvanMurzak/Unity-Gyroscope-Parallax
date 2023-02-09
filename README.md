# Unity-Gyroscope-Parallax
![npm](https://img.shields.io/npm/v/extensions.unity.gyroscope.parallax) [![openupm](https://img.shields.io/npm/v/extensions.unity.gyroscope.parallax?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/extensions.unity.gyroscope.parallax/) ![License](https://img.shields.io/github/license/IvanMurzak/Unity-Gyroscope-Parallax) [![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)

Unity Parallax based on gyroscope components. Supported fake gyroscope for simulation in Unity Editor. Alternative version to [Unity-Mouse-Parallax](https://github.com/IvanMurzak/Unity-Mouse-Parallax).

### Features
- ✔️ support legacy Input System
- ✔️ support new Input System
- ✔️ ability to simulate gyroscope in Unity Editor
- ✔️ move/rotate objects based on gyroscope
- ✔️ ability to add custom controllers

![unity-gyroscope-parallax-small](https://user-images.githubusercontent.com/9135028/177197269-a11dd87c-3f6b-400d-bf91-01a9b051cb1b.gif)

# GyroMover2D
Moves list of objects using gyroscope.

![image](https://user-images.githubusercontent.com/9135028/166463235-50702210-3b09-417d-9b9a-547fce73ba15.png) ![image](https://user-images.githubusercontent.com/9135028/166465109-33274de8-84e3-44e4-a8ab-b7c1f3ea2380.png)

![Unity_WTZrJSE6qY](https://user-images.githubusercontent.com/9135028/166468223-2992f1a9-8ead-454e-bc3a-5adaab832868.gif)


# GyroRotator2D
Rotates list of objects using gyroscope.

![image](https://user-images.githubusercontent.com/9135028/176648393-cde4e34d-1c7c-4a58-9935-a5ff6081d2e7.png)
 ![image](https://user-images.githubusercontent.com/9135028/166465157-5f1325f3-8109-4a35-bd91-87082aa36cf9.png)

![Unity_CeUGRyFD5v](https://user-images.githubusercontent.com/9135028/166467361-485a1e2b-f799-4700-ada8-3982e06f2245.gif)

# How to install - Option 1 (RECOMMENDED)
- [Install OpenUPM-CLI](https://github.com/openupm/openupm-cli#installation)
- Open command line in Unity project folder
- `openupm add extensions.unity.gyroscope.parallax`

# How to install - Option 2
- Add this code to <code>/Packages/manifest.json</code>
```json
{
  "dependencies": {
    "extensions.unity.gyroscope.parallax": "1.4.1",
  },
  "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "extensions.unity",
        "com.cysharp",
        "com.neuecc"
      ]
    }
  ]
}
```

# How to use
- add needed `Gyro...2D` component to any GameObject
- link Targets to list of targets
- press 'Play' button in Unity Editor
- find `Fake Gyroscope Manager` in `DonDestroyOnLoad` scene (appears in Play Mode)
- ![image](https://user-images.githubusercontent.com/9135028/166464685-b6197e8a-547d-47ab-9039-824ce29f3ca5.png)
- change XY values of `Gravity` and `Attitude` properties to simulate gyroscope in Unity Editor
