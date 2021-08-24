using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{

    private bool ifPlaceable = true;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Furniture") || collision.gameObject.CompareTag("AvailableChair") || collision.gameObject.CompareTag("TakenChair"))
        {
            ifPlaceable = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Furniture") || collision.gameObject.CompareTag("AvailableChair") || collision.gameObject.CompareTag("TakenChair"))
        {
            ifPlaceable = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Furniture") || collision.gameObject.CompareTag("AvailableChair")|| collision.gameObject.CompareTag("TakenChair"))
        {
            ifPlaceable = false;
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
