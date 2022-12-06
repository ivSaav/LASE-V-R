using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceiver : MonoBehaviour {
    [SerializeField] private Texture textureEnabled, textureDisabled;

    private bool activated;
    private GameObject laserReceiverBase;
    private new Renderer renderer;

    private void Start() {
        activated = false;
        laserReceiverBase = transform.Find("Base").gameObject;
        renderer = laserReceiverBase.GetComponent<Renderer>();

        foreach (LaserRay ray in FindObjectsOfType<LaserRay>())
        {
            ray.OnLaserHit += (r, hit) => Activate();
        }
    }

    private void Update() {
        renderer.material.SetTexture("_MainTex", activated ? textureEnabled : textureDisabled);

        activated = false;
    }

    private void Activate() {
        activated = true;
    }

    public bool IsActive()
    {
        return activated;
    }
}
