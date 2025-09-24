using UnityEngine;
using UnityGyroscope.Manager;

public class GyroscopeRotator : MonoBehaviour
{
    FakeGyroscopeManager fakeGyroscopeManager;

    void Update()
    {
        if (fakeGyroscopeManager == null)
            fakeGyroscopeManager = FindObjectOfType<FakeGyroscopeManager>();

        if (fakeGyroscopeManager == null)
            return;

        fakeGyroscopeManager.settings.gravity = new Vector3(
            x: Mathf.Sin(Time.time) / 2f,
            y: Mathf.Cos(Time.time) / 2f,
            z: 1
        );
        fakeGyroscopeManager.settings.attitude = new Vector3(
            x: Mathf.Sin(Time.time) / 2f,
            y: Mathf.Cos(Time.time) / 2f,
            z: 1
        );
    }
}
