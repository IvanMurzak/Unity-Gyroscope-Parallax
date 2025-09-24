/*
┌───────────────────────────────────────────────────────────────────────────────┐
│  Author: Ivan Murzak (https://github.com/IvanMurzak)                          │
│  Repository: GitHub (https://github.com/IvanMurzak/Unity-Gyroscope-Parallax)  │
│  Copyright (c) 2025 Ivan Murzak                                               │
│  Licensed under the Apache License, Version 2.0.                              │
│  See the LICENSE file in the project root for more information.               │
└───────────────────────────────────────────────────────────────────────────────┘
*/
using UnityEngine;
using UnityEditor;
using System.IO;

namespace com.IvanMurzak.Unity.Gyroscope.Parallax.Installer
{
    public static class PackageExporter
    {
        public static void ExportPackage()
        {
            var packagePath = "Assets/com.IvanMurzak/Gyroscope Parallax Installer";
            var outputPath = "build/Gyroscope-Parallax-Installer.unitypackage";

            // Ensure build directory exists
            var buildDir = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(buildDir))
            {
                Directory.CreateDirectory(buildDir);
            }

            // Export the package
            AssetDatabase.ExportPackage(packagePath, outputPath, ExportPackageOptions.Recurse);

            Debug.Log($"Package exported to: {outputPath}");
        }
    }
}