using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class LaserPointerHandler : MonoBehaviour
{
    [SerializeField] private SteamVR_LaserPointer laserPointer;

    private void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.CompareTag("Button"))
        {
            e.target.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {

    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {

    }
}
