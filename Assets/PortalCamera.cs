using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal, pairPortal;

    private void LateUpdate()
    {
        var playerOffset = playerCamera.position - pairPortal.position;
        transform.position = portal.position + playerOffset;

        var angle = Quaternion.Angle(portal.rotation, pairPortal.rotation);

        var rotationDifference = Quaternion.AngleAxis(angle, Vector3.up);
        var newDirection = rotationDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
    }
}
