using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControlButton : MonoBehaviour
{
    [SerializeField] private PressButton button;

    private bool enabled = true;

    private new Renderer renderer;
    private new Collider collider;

    private void Start()
    {
        renderer = transform.gameObject.GetComponent<Renderer>();
        collider = GetComponent<Collider>();
        renderer.enabled = enabled;
        collider.enabled = enabled;

        button.onEnableEvent.AddListener(DisableForcefield);
        button.onDisableEvent.AddListener(EnableForcefield);
    }

    private void EnableForcefield() {
        enabled = true;
        renderer.enabled = enabled;
        collider.enabled = enabled;
    }
        
    private void DisableForcefield() {
        enabled = false;
        renderer.enabled = enabled;
        collider.enabled = enabled;
    }
}
