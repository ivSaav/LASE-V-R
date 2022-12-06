using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    [SerializeField] private Portal pairPortal;

    [SerializeField] private Renderer portalOutlineRenderer;
    [SerializeField] private Color portalOutlineColour;

    public new Renderer renderer { get; private set; }

    private Camera _playerCam, _portalCam;
    private RenderTexture _viewTexture;

    private LaserRay _teleportedLaser;
    private GameObject _laserStartPoint;
    private Coroutine _disableLaserCoroutine;

    private const float DisableDelay = 0.03f;

    private void Awake() {
        renderer = GetComponent<MeshRenderer>();
        _teleportedLaser = GetComponentInChildren<LaserRay>();
        _laserStartPoint = new GameObject();
    }

    private void Start() {
        portalOutlineRenderer.material.SetColor("_PortalOutlineColour", portalOutlineColour);

        foreach (LaserRay ray in FindObjectsOfType<LaserRay>())
        {
            ray.OnLaserHit += TeleportLaser;
        }
    }

    private void TeleportLaser(LaserRay laser, RaycastHit raycastHit)
    {
        if (laser == _teleportedLaser) return;
        pairPortal.StopDisableLaser();

        Transform pairTransform = pairPortal.transform;
        Transform laserTransform = _laserStartPoint.transform;

        Vector3 relativePos = transform.InverseTransformPoint(raycastHit.point);
        relativePos = Quaternion.Euler(0, 180, 0) * relativePos;
        laserTransform.position = pairTransform.TransformPoint(relativePos);

        Quaternion relativeRotation = Quaternion.Inverse(transform.rotation) * laser.transform.rotation;
        relativeRotation = Quaternion.Euler(0, 180, 0) * relativeRotation;
        laserTransform.rotation = pairTransform.rotation * relativeRotation;

        pairPortal._teleportedLaser.laserStartPoint = laserTransform;
    }

    private void Update()
    {
        if (!_teleportedLaser.IsLaserDisabled())
        {
            if (_disableLaserCoroutine == null)
            {
                _disableLaserCoroutine = StartCoroutine(nameof(DisableLaser));
            }
        }
        else
        {
            StopDisableLaser();
        }
    }

    private IEnumerator DisableLaser()
    {
        yield return new WaitForSeconds(DisableDelay);
        _teleportedLaser.DisableLaser();
    }

    private void StopDisableLaser()
    {
        if (_disableLaserCoroutine == null) return;
        StopCoroutine(nameof(DisableLaser));
        _disableLaserCoroutine = null;
    }
}
