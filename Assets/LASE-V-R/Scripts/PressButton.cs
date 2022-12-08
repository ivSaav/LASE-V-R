using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PressButton : MonoBehaviour {
    [SerializeField] private Material onMaterial;

    [SerializeField] private Material offMaterial;

    [SerializeField] private string interactionLayer;

    [SerializeField] private Light light;
    private int interactionLayerID;

    public UnityEvent onEnableEvent = new UnityEvent();
    public UnityEvent onDisableEvent = new UnityEvent();

    private new Renderer buttonRenderer;

    private bool engaged = false;
    // Start is called before the first frame update
    void Start() {
        buttonRenderer = GetComponent<Renderer>();

        interactionLayerID = LayerMask.NameToLayer(interactionLayer);

        onEnableEvent.AddListener(ChangeMaterial);
        onDisableEvent.AddListener(ChangeMaterial);
        light.enabled = false;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer != interactionLayerID) return;

        if (!engaged) {
            engaged = true;
            light.enabled = engaged;
            onEnableEvent.Invoke();
        }
        
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.layer != interactionLayerID) return;
        
        if (engaged) {
            engaged = false;
            light.enabled = engaged;
            onDisableEvent.Invoke();
        }
        
    }

    private void ChangeMaterial() {
        buttonRenderer.material = engaged ? onMaterial : offMaterial;
    }
}