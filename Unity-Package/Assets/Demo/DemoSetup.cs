using UnityEngine;

public class DemoSetup : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
}
