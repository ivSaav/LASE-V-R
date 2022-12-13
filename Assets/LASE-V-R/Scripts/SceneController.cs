using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class SceneController : MonoBehaviour {

    [SerializeField] private string nextScene;

    [SerializeField] private LaserReceiver receiver;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (receiver.IsActive())
            {
                SceneManager.LoadScene(nextScene);
            }
        }
        
    }
}
