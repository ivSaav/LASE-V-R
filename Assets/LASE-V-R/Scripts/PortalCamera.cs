using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PortalCamera : MonoBehaviour {

    [SerializeField] private Portal[] portals = new Portal[2];
    
    [SerializeField] private Camera portalCamera;

    [SerializeField] private int maxRecursion = 5;

    private Camera mainCamera;
    
    private RenderTexture[] portalTextures = new RenderTexture[2];

    private void Awake() {
        mainCamera = GetComponent<Camera>();

        portalTextures[0] = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        portalTextures[1] = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
    }

    // Start is called before the first frame update
    void Start() {
        portals[0].renderer.material.mainTexture = portalTextures[0];
        portals[1].renderer.material.mainTexture = portalTextures[1];
    }
    void Update() {
        for (var i = 0; i < portals.Length; i++) {
            if (portals[i].renderer.isVisible) {
                portalCamera.targetTexture = portalTextures[i];
                for (var iteration = maxRecursion - 1; iteration >= 0; iteration--) {
                    RenderCamera(portals[i], portals[1 - i], iteration);
                }
            }
        }
    }

    private void RenderCamera(Portal inPortal, Portal outPortal, int recursionDepth) {

        Transform inPortalTransform = inPortal.transform;
        Transform outPortalTransform = outPortal.transform;
        Transform mainCameraTransform = mainCamera.transform;
        Transform portalCameraTransform = portalCamera.transform;
        portalCameraTransform.position = mainCameraTransform.position;
        portalCameraTransform.rotation = mainCameraTransform.rotation;

        for (var i = 0; i < recursionDepth; i++) {
            // Calculate Relative Position of Portal Camera
            Vector3 relativePos = inPortalTransform.InverseTransformPoint(portalCameraTransform.position);
            relativePos = Quaternion.Euler(0, 180, 0) * relativePos;
            portalCameraTransform.position = outPortalTransform.TransformPoint(relativePos);
            
            // Calculate Relative Rotation of Portal Camera
            Quaternion relativeRotation = Quaternion.Inverse(inPortalTransform.rotation) * portalCameraTransform.rotation;
            relativeRotation = Quaternion.Euler(0, 180, 0) * relativeRotation;
            portalCameraTransform.rotation = outPortalTransform.rotation * relativeRotation;
        }

        Plane outView = new Plane(-outPortalTransform.forward, outPortalTransform.position);

        Vector4 clipPlaneWorld = new Vector4(outView.normal.x, outView.normal.y, outView.normal.z, outView.distance);
        Vector4 clipPlaneCamera = Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * clipPlaneWorld;

        portalCamera.projectionMatrix = mainCamera.CalculateObliqueMatrix(clipPlaneCamera);
        
        portalCamera.Render();

    }
}
