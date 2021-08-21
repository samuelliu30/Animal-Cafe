using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour
{
    private NavMeshAgent agent;
    [Space(10)]
    public Animator[] animator;
    private GameObject destination;

    private bool arrived = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        ChangeAnimation("Walk");

    }

    // Update is called once per frame
    void Update()
    {
        if (!arrived)
        {
            agent.SetDestination(FindTable());
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    ChangeAnimation("Idle A");
                    arrived = true;
                }
            }
        }
    }

    private Vector3 FindTable()
    {
        GameObject[] table = GameObject.FindGameObjectsWithTag("Furniture");
        destination =  table[Random.Range(0, table.Length)];
        agent.stoppingDistance = destination.GetComponent<Renderer>().bounds.size.z / 2;
        Debug.Log(agent.stoppingDistance);

        return destination.transform.position;
    }

    void Eat()
    {

    }

    private void ChangeAnimation(string animation)
    {
        for (int i = 0; i < animator.Length; i++)
        {
            animator[i].SetTrigger(animation);
            //animator[i].SetBool(animation, true);
        }
    }

}
