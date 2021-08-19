using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class CustomerManager : MonoBehaviour
{
    private GameObject customer;
    private Vector3 doorPos;

    [SerializeField]
    GameObject[] animals;
    [SerializeField]
    InventoryManager moneyManager;

    private void Start()
    {
        GameObject doorLeft = GameObject.FindWithTag("DoorLeft");
        GameObject doorRight = GameObject.FindWithTag("DoorRight");
        doorPos = new Vector3((doorLeft.transform.position.x + doorRight.transform.position.x) / 2, 0, (doorLeft.transform.position.z + doorRight.transform.position.z) / 2);
    }


    public void SpawnCustomer()
    {
        customer = animals[Random.Range(0, 2)];
        GameObject newCustomer = Instantiate(customer, doorPos, Quaternion.identity);
        moneyManager.ChangeMoney(10);
    }

}
