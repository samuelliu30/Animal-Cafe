using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    private bool arrived = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!arrived)
        {
            arrived = agent.SetDestination(FindTable());
        }
    }

    private Vector3 FindTable()
    {
        GameObject[] table = GameObject.FindGameObjectsWithTag("Furniture");
        return table[Random.Range(0, table.Length)].transform.position;
    }

    void Eat()
    {

    }
}
