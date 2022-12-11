using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour {

    private float offSet = 144;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        float timeOfDay = DateTime.Now.Hour * 60 + DateTime.Now.Minute - offSet;
        
        transform.rotation = Quaternion.Euler(timeOfDay / 1440  * 360 - 90, 90, 0);
    }
}
