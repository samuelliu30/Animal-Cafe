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
    public List<GameObject> pooledAnimals;
    public int amountToPool;


    private void Start()
    {
        GameObject doorLeft = GameObject.FindWithTag("DoorLeft");
        GameObject doorRight = GameObject.FindWithTag("DoorRight");
        doorPos = new Vector3((doorLeft.transform.position.x + doorRight.transform.position.x) / 2, 0, (doorLeft.transform.position.z + doorRight.transform.position.z) / 2);

        //Initialize object pooler
        pooledAnimals = new List<GameObject>();
        for (int i = 0; i < animals.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(animals[i]);
            obj.SetActive(false);
            pooledAnimals.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager

        }
    }

    public GameObject GetPooledObject()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledAnimals.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledAnimals[i].activeInHierarchy)
            {
                return pooledAnimals[i];
            }
        }
        // otherwise, return null   
        return null;
    }

    public void SpawnCustomer()
    {
        customer = animals[Random.Range(0, animals.Length)];
        GameObject newCustomer = Instantiate(customer, doorPos, Quaternion.identity);
        moneyManager.ChangeMoney(10);
    }

}
