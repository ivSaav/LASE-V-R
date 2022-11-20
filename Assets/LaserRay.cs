using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : MonoBehaviour {

    private LineRenderer lineRenderer;

    [SerializeField] private Transform laserStartPoint;

    [SerializeField] private int maxBounces = 10;
    // Start is called before the first frame update
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, laserStartPoint.position);
    }

    // Update is called once per frame
    void Update() {
        RaycastLaser(transform.position, -transform.right);
    }

    void RaycastLaser(Vector3 position, Vector3 direction) {
        lineRenderer.SetPosition(0, laserStartPoint.position);

        for (var bounceIndex = 1; bounceIndex <= maxBounces; bounceIndex++) {
            if (Physics.Raycast(position, direction, out var laserHit)) {
                position = laserHit.point;
                direction = Vector3.Reflect(direction, laserHit.normal);
                
                lineRenderer.SetPosition(bounceIndex, laserHit.point);

                if (!laserHit.transform.gameObject.CompareTag("Reflectable")) {
                    for (var remainingIndex = bounceIndex + 1; remainingIndex <= maxBounces; remainingIndex++) {
                        lineRenderer.SetPosition(remainingIndex, laserHit.point);
                    }
                    break;
                }
            }
            else {
                for (var remainingIndex = bounceIndex + 1; remainingIndex <= maxBounces; remainingIndex++) {
                    lineRenderer.SetPosition(remainingIndex, position + direction * 1000);
                }
                break;
            }
        }
    }
}