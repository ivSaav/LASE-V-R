using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour {

    [SerializeField]
    private float startOfDay = 360;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update() {
        float timeOfDay = DateTime.Now.Hour * 60 + DateTime.Now.Minute - startOfDay;
        timeOfDay = timeOfDay < 0 ? timeOfDay + 1440 : timeOfDay;
        
        transform.eulerAngles = new Vector3(Mathf.Lerp(0, 360, timeOfDay / 1440), 90, 0);
    }
}