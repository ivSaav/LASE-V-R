using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSetup : MonoBehaviour
{
    public Camera cameraA, cameraB;
    public Material cameraMatA, cameraMatB;

    private void Start()
    {
        SetupCamera(cameraA, cameraMatA);
        SetupCamera(cameraB, cameraMatB);
    }

    private static void SetupCamera(Camera cam, Material mat)
    {
        if (cam.targetTexture != null)
        {
            cam.targetTexture.Release();
        }

        cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mat.mainTexture = cam.targetTexture;
    }
}
