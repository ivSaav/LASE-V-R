using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private LaserReceiver laserReceiver;

    private Renderer renderer;
    private void Start()
    {
        renderer = transform.gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.enabled = !laserReceiver.IsActive();
    }
}
