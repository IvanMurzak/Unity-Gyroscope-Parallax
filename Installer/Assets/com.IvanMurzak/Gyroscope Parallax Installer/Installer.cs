/*
┌───────────────────────────────────────────────────────────────────────────────┐
│  Author: Ivan Murzak (https://github.com/IvanMurzak)                          │
│  Repository: GitHub (https://github.com/IvanMurzak/Unity-Gyroscope-Parallax)  │
│  Copyright (c) 2025 Ivan Murzak                                               │
│  Licensed under the Apache License, Version 2.0.                              │
│  See the LICENSE file in the project root for more information.               │
└───────────────────────────────────────────────────────────────────────────────┘
*/
#nullable enable
using UnityEditor;

namespace com.IvanMurzak.Unity.Gyroscope.Parallax.Installer
{
    [InitializeOnLoad]
    public static partial class Installer
    {
        public const string PackageId = "extensions.unity.gyroscope.parallax";
        public const string Version = "1.4.1";

        static Installer()
        {
#if !IVAN_MURZAK_INSTALLER_PROJECT
            AddScopedRegistryIfNeeded(ManifestPath);
#endif
        }
    }
}