using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceiver : MonoBehaviour {

    [SerializeField] private Texture textureEnabled, textureDisabled;

    private bool activated;
    private GameObject laserReceiverBase;
    
    // Start is called before the first frame update
    void Start() {
        activated = false;
        laserReceiverBase = transform.Find("Base").gameObject;
    }

    // Update is called once per frame
    void Update() {
        laserReceiverBase.GetComponent<Renderer>().material.SetTexture("_MainTex", activated ? textureEnabled : textureDisabled);

        activated = false;
    }

    void Activate() {
        activated = true;
    }
}
