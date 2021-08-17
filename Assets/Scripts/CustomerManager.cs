using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private GameObject customer;

    [SerializeField]
    GameObject cat;

    private void Start()
    {
        customer = cat;
    }
    private Vector3 FindDoorPosition()
    {
        GameObject door = GameObject.FindWithTag("Door");
        return door.transform.position;
    }

    public void SpawnCustomer()
    {
        GameObject newCustomer = Instantiate(customer, FindDoorPosition(), Quaternion.identity);
    }

}
