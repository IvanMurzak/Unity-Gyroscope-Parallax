# [Unity Gyroscope Parallax](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax)

[![OpenUPM](https://img.shields.io/npm/v/extensions.unity.gyroscope.parallax?label=OpenUPM&registry_uri=https://package.openupm.com&labelColor=333A41 'OpenUPM package')](https://openupm.com/packages/extensions.unity.gyroscope.parallax/)
[![Unity Asset Store](https://img.shields.io/badge/Asset%20Store-View-blue?logo=unity&labelColor=333A41 'Asset Store')](https://u3d.as/3E42)
[![Unity Editor](https://img.shields.io/badge/Editor-X?style=flat&logo=unity&labelColor=333A41&color=49BC5C 'Unity Editor supported')](https://unity.com/releases/editor/archive)
[![Unity Runtime](https://img.shields.io/badge/Runtime-X?style=flat&logo=unity&labelColor=333A41&color=49BC5C 'Unity Runtime supported')](https://unity.com/releases/editor/archive)
[![r](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg 'Tests Passed')](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/actions/workflows/release.yml)

[![Stars](https://img.shields.io/github/stars/IvanMurzak/Unity-Gyroscope-Parallax 'Stars')](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/stargazers)
[![License](https://img.shields.io/github/license/IvanMurzak/Unity-Gyroscope-Parallax?label=License)](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/blob/main/LICENSE)
[![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)

![Unity Parallax Effect](https://github.com/IvanMurza/Unity-Gyroscope-Parallax/blob/main/docs/img/unity-chan-parallax.gif)

Unity Parallax based on gyroscope components. Supported fake gyroscope for simulation in Unity Editor. Alternative version to [Unity-Mouse-Parallax](https://github.com/IvanMurzak/Unity-Mouse-Parallax).

## Features

- ✔️ Add customizable parallax effect based on sensor data
- ✔️ Control object movement & rotation based on gyroscope
- ✔️ Supports both *New* and *Legacy* `Input System`
- ✔️ Embedded gyroscope simulator right into Unity Editor

### Stability status

| Unity Version | Editor test | Player test | Build test |
|---------------|-------------|-------------|------------|
| 2019.4.40f1    | ![2019.4.40f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2019-4-40f1-editmode) | ![2019.4.40f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2019-4-40f1-playmode) | ![2019.4.40f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2019-4-40f1-standalone) |
| 2020.3.48f1   | ![2020.3.48f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2020-3-48f1-editmode) | ![2020.3.48f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2020-3-48f1-playmode) | ![2020.3.48f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2020-3-48f1-standalone) |
| 2021.3.45f1   | ![2021.3.45f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2021-3-45f1-editmode) | ![2021.3.45f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2021-3-45f1-playmode) | ![2021.3.45f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2021-3-45f1-standalone) |
| 2022.3.62f1   | ![2022.3.62f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2022-3-62f1-editmode) | ![2022.3.62f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2022-3-62f1-playmode) | ![2022.3.62f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-2022-3-62f1-standalone) |
| 6000.0.58f1   | ![6000.0.58f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-6000-0-58f1-editmode) | ![6000.0.58f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-6000-0-58f1-playmode) | ![6000.0.58f1](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/workflows/release/badge.svg?job=test-unity-6000-0-58f1-standalone) |

---

## Installation

### Option 1 - Installer

- **[⬇️ Download Installer](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax/releases/download/1.4.1/Gyroscope-Parallax-Installer.unitypackage)**
- **📂 Import installer into Unity project**
  > - You may use double click on the file - Unity will open it
  > - OR: You may open Unity Editor first, then click on `Assets/Import Package/Custom Package`, then choose the file

### Option 2 - OpenUPM-CLI

- [⬇️ Install OpenUPM-CLI](https://github.com/openupm/openupm-cli#installation)
- 📟 Open command line in Unity project folder

```bash
openupm add extensions.unity.gyroscope.parallax
```

### Option 3 - Asset Store

- **[▶️ Open Asset Store](https://u3d.as/3E42)**

---

# Usage

- Add needed `Gyro...` component to any GameObject
- Link Targets to list of targets
- Press 'Play' button in Unity Editor
- Find `Fake Gyroscope Manager` in `DonDestroyOnLoad` scene (appears in *Play Mode*)
  ![image](https://user-images.githubusercontent.com/9135028/166464685-b6197e8a-547d-47ab-9039-824ce29f3ca5.png)
- Change XY values of `Gravity` and `Attitude` properties to simulate gyroscope in Unity Editor

## GyroMover2D

Moves list of objects using gyroscope.

![image](https://user-images.githubusercontent.com/9135028/166463235-50702210-3b09-417d-9b9a-547fce73ba15.png) ![image](https://user-images.githubusercontent.com/9135028/166465109-33274de8-84e3-44e4-a8ab-b7c1f3ea2380.png)

![Unity_WTZrJSE6qY](https://user-images.githubusercontent.com/9135028/166468223-2992f1a9-8ead-454e-bc3a-5adaab832868.gif)

## GyroRotator2D

Rotates list of objects using gyroscope.

![image](https://user-images.githubusercontent.com/9135028/176648393-cde4e34d-1c7c-4a58-9935-a5ff6081d2e7.png) ![image](https://user-images.githubusercontent.com/9135028/166465157-5f1325f3-8109-4a35-bd91-87082aa36cf9.png)

![Unity_CeUGRyFD5v](https://user-images.githubusercontent.com/9135028/166467361-485a1e2b-f799-4700-ada8-3982e06f2245.gif)
