# Unity-Package-Template
UPM (Unity Package Manager) ready GitHub repository for Unity
New Unity packages system is very easy to use and make your project much more cleaner.
The repository helps you to create your own Unity package with dependecies.

# How to use
- "Use this template" green button at top right corner of GitHub page
- Clone your new repository
- Add all your stuff to <code>Assets/_PackageRoot directory</code>
- Update <code>Assets/_PackageRoot/package.json</code> to yours
- (on Windows) execute <code>gitSubTreePushToUPM.bat</code>
- (on Mac) execute <code>gitSubTreePushToUPM.makefile</code>

- (optional) Create release from UPM branch on GitHub web page for support different versions

![alt text](https://neogeek.dev/images/creating-custom-packages-for-unity-2018.3--git-release.png)


# How to import your package to Unity project
You may use one of the variants

## Variant 1
- Select "Add package from git URL"
- Paste URL to your GitHub repository with simple modification:
- <code>https://github.com/USER/REPO.git#upm</code> 
Dont forget to replace **USER** and **REPO** to yours

![alt text](https://neogeek.dev/images/creating-custom-packages-for-unity-2018.3--package-manager.png)

### **Or** you may use special version if you create one  
<code>https://github.com/USER/REPO.git#v1.0.0</code>
Dont forget to replace **USER** and **REPO** to yours

## Variant 2
Modify manifest.json file. Change <code>"your.own.package"</code> to the name of your package.
Dont forget to replace **USER** and **REPO** to yours.
<pre><code>{
    "dependencies": {
        "your.own.package": "https://github.com/USER/REPO.git#upm"
    }
}
</code></pre>

### **Or** you may use special version if you create one
Dont forget to replace **USER** and **REPO** to yours.
<pre><code>{
    "dependencies": {
        "your.own.package": "https://github.com/USER/REPO.git#v1.0.0"
    }
}
</code></pre>
