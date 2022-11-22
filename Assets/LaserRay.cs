using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserRay : MonoBehaviour {

    private LineRenderer lineRenderer;

    [SerializeField] private Transform laserStartPoint;

    [SerializeField] private int maxBounces = 10;

    private RaycastHit hit;
    private LaserReceiver laserReceiver;

    // Start is called before the first frame update
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        var ray = new Ray(laserStartPoint.position, -laserStartPoint.right);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, laserStartPoint.position);

        for (var i = 0; i < maxBounces; i++)
        {
            // check collision
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000f))
            {
                // add new position to line renderer
                lineRenderer.positionCount += 1;
                    
                // render position at line hit point
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                
                if (hit.collider.CompareTag("Reflectable"))
                {
                    // direction of ray is the angle between the vector and the hit object
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                }
                else if (hit.collider.CompareTag("LaserReceiver")) {
                    laserReceiver = hit.collider.gameObject.GetComponent<LaserReceiver>();
                    laserReceiver.Invoke("Activate", 0);
                    break;
                }
                else {
                    break;
                }
            }
            else
            {              
                // add new position to line renderer
                lineRenderer.positionCount += 1;

                // line renderer position has the same origin and direction as the ray
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * 1000f);
                break;
            }
        }
    }
}