// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class MainCamera : MonoBehaviour
// {
//     private Portal[] _portals;
//     
//     private void Awake()
//     {
//         _portals = FindObjectsOfType<Portal>();
//     }
//
//     private void OnPreCull()
//     {
//         foreach (Portal portal in _portals)
//         {
//             portal.Render();
//         }
//     }
// }
