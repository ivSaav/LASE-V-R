using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleRotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 25*Time.deltaTime, 0);
    }
}
