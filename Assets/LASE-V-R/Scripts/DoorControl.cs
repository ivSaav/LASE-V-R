using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private LaserReceiver laserReceiver;

    private new Renderer renderer;
    private new Collider collider;

    private void Start()
    {
        renderer = transform.gameObject.GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.enabled = !laserReceiver.IsActive();
        collider.enabled = !laserReceiver.IsActive();
    }
}
