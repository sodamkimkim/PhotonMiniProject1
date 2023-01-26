using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        if(_other.gameObject.CompareTag("Flag"))
        {
        }
    }
} // end of class
