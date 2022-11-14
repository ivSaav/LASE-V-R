using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Clock : MonoBehaviour
{   
    private Transform hour_transf;
    private Transform min_transf;
    private Transform sec_transf;
    // Start is called before the first frame update
    void Start()
    {
        hour_transf = transform.Find("Clock_Analog_A_Hour");
        min_transf = transform.Find("Clock_Analog_A_Minute");
        sec_transf = transform.Find("Clock_Analog_A_Second");
    }

    // Update is called once per frame
    void Update()
    {  
        var dateTime = DateTime.Now;
        hour_transf.eulerAngles = new Vector3(30 * dateTime.Hour, 0, 0);
        min_transf.eulerAngles = new Vector3(6 * dateTime.Minute, 0, 0);
        sec_transf.eulerAngles = new Vector3(6 * dateTime.Second, 0, 0);
    }
}
