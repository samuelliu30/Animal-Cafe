using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    private bool store;

    private void Start()
    {
        store = false;
    }

    public bool IfStore()
    {
        return store;
    }

    public void StoreFurniture()
    {
        store = !store;
    }
}
