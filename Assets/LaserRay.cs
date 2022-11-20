using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : MonoBehaviour {

    private LineRenderer lineRenderer;

    [SerializeField] private Transform laserStartPoint;

    [SerializeField] private int maxBounces = 10;

    private RaycastHit hit;

    private void Awake()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    // // Start is called before the first frame update
    // void Start() {
    //     lineRenderer = GetComponent<LineRenderer>();
    //     lineRenderer.SetPosition(0, laserStartPoint.position);
    // }

    // Update is called once per frame
    void Update()
    {

        var ray = new Ray(laserStartPoint.position, -laserStartPoint.right);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, laserStartPoint.position);

        for (var i = 0; i < maxBounces; i++)
        {
            // check collision
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
            {
                if (hit.collider.CompareTag("Reflectable"))
                {
                    // add new position to line renderer
                    lineRenderer.positionCount += 1;
                    
                    // render position at line hit point
                    lineRenderer.SetPosition(lineRenderer.positionCount-1, hit.point);

                    // direction of ray is the angle between the vector and the hit object
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                }
                else
                {
                    // TODO: handle non reflection hits
                    break;
                }
            }
            else
            {              
                // add new position to line renderer
                lineRenderer.positionCount += 1;

                // line renderer position has the same origin and direction as the ray
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * 100f);
                break;
            }
        }
        // RaycastLaser(transform.position, -transform.right);
    }

    void RaycastLaser(Vector3 position, Vector3 direction) {
        lineRenderer.SetPosition(0, laserStartPoint.position);

        var ray = new Ray(position, direction);
        for (var bounceIndex = 1; bounceIndex <= maxBounces; bounceIndex++) {
            if (Physics.Raycast(position, ray.direction, out var laserHit)) {
                
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