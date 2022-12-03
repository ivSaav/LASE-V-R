using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal pairPortal;
    public MeshRenderer plane;

    private Camera _playerCam, _portalCam;
    private RenderTexture _viewTexture;

    private void Awake()
    {
        _playerCam = Camera.main;
        _portalCam = GetComponentInChildren<Camera>();
        _portalCam.enabled = false;
    }

    private void CreateViewTexture()
    {
        if (_viewTexture != null && _viewTexture.width == Screen.width && _viewTexture.height == Screen.height) return;

        if (_viewTexture != null)
        {
            _viewTexture.Release();
        }

        _viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
        _portalCam.targetTexture = _viewTexture;
        pairPortal.plane.material.mainTexture = _viewTexture;
    }

    public void Render()
    {
        plane.enabled = false;
        CreateViewTexture();

        // Ensure that the camera position and rotation relative to the portal are the same as for the main camera
        // relative to the pair portal
        Matrix4x4 m = transform.localToWorldMatrix * pairPortal.transform.worldToLocalMatrix *
                _playerCam.transform.localToWorldMatrix;
        _portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        _portalCam.Render();

        plane.enabled = true;
    }
}
