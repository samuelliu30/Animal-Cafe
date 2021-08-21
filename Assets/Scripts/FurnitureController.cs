using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{

    private bool ifPlaceable = true;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Furniture"))
        {
            ifPlaceable = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Furniture"))
        {
            ifPlaceable = true;
        }
    }

    public bool IfPlaceable
    {
        get
        {
            return ifPlaceable;
        }
    }

}
