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
    [SerializeField] float destBias = 0.5f;

    private bool arrived = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        ChangeAnimation("Walk");
        agent.SetDestination(FindTable());

    }

    // Update is called once per frame
    void Update()
    {
        if(arrived == false)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // Done
                        arrived = true;
                        ChangeAnimation("Idle A");
                        this.GetComponent<NavMeshAgent>().enabled = false;
                        SitOn();
                    }
                }
            }

        }

    }

    private Vector3 FindTable()
    {
        GameObject[] charis = GameObject.FindGameObjectsWithTag("Chair");
        destination = charis[Random.Range(0, charis.Length)];
        agent.stoppingDistance = destination.GetComponent<Renderer>().bounds.size.z / 2 + destBias;

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

    private void SitOn()
    {
        Vector3 pos = destination.transform.position;
        this.transform.position = new Vector3(pos.x, destination.GetComponent<Renderer>().bounds.size.y / 2, pos.z);
        this.transform.rotation = destination.transform.rotation;
    }

}
