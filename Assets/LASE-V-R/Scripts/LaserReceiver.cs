using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceiver : MonoBehaviour {
    [SerializeField] private Texture textureEnabled, textureDisabled;

    private bool activated;
    private GameObject laserReceiverBase;
    private new Renderer renderer;
    private Coroutine _disableCoroutine;

    private const float DisableDelay = 0.03f;

    private void Start() {
        activated = false;
        laserReceiverBase = transform.Find("Base").gameObject;
        renderer = laserReceiverBase.GetComponent<Renderer>();

        foreach (LaserRay ray in FindObjectsOfType<LaserRay>())
        {
            ray.OnLaserHit += OnLaserHit;
        }
    }

    private void OnLaserHit(LaserRay laser, Ray ray, RaycastHit raycastHit) {
        if (raycastHit.collider.gameObject != gameObject) return;
        StopDisable();
        Activate();
    }

    private void Update() {
        renderer.material.SetTexture("_MainTex", activated ? textureEnabled : textureDisabled);
        
        if (IsActive())
        {
            if (_disableCoroutine == null)
            {
                _disableCoroutine = StartCoroutine(nameof(Disable));
            }
        }
        else
        {
            StopDisable();
        }
    }

    private void Activate() {
        activated = true;
    }

    public bool IsActive()
    {
        return activated;
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(DisableDelay);
        activated = false;
    }

    private void StopDisable()
    {
        if (_disableCoroutine == null) return;
        StopCoroutine(nameof(Disable));
        _disableCoroutine = null;
    }
}
