using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    [SerializeField] private LaserReceiver laserReceiver;

    [SerializeField] private Material onMaterial, offMaterial;

    private Renderer bulbRenderer;

    private Light lightBulb;
    // Start is called before the first frame update
    void Start()
    {
        lightBulb = GetComponentInChildren<Light>();
        lightBulb.enabled = false;

        bulbRenderer = transform.Find("SciFi_WallLightBulb").GetComponent<Renderer>();
        bulbRenderer.material = onMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        lightBulb.enabled = laserReceiver.IsActive();
        bulbRenderer.material = lightBulb.enabled ? onMaterial : offMaterial;
    }
}
