using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TrailLight : MonoBehaviour {
    [SerializeField] private Material onMaterial;

    [SerializeField] private Material offMaterial;

    [SerializeField]
    private PressButton button;

    [SerializeField] private Light light;
    
    private Renderer trailLightRenderer;
    // Start is called before the first frame update
    void Start() {
        trailLightRenderer = GetComponent<Renderer>();
        
        button.onEnableEvent.AddListener(ChangeOnMaterial);
        button.onDisableEvent.AddListener(ChangeOffMaterial);
        light.enabled = false;
    }

    private void ChangeOnMaterial() {
        trailLightRenderer.material = onMaterial;
        light.enabled = true;
    }
    
    private void ChangeOffMaterial() {
        trailLightRenderer.material = offMaterial;
        light.enabled = false;
    }
}
