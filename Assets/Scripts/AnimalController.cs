using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AnimalController : MonoBehaviour
{
    private NavMeshAgent agent;
    [Space(10)]
    public Animator[] animator;
    private GameObject destination;
    [SerializeField] float destBias = 0.5f;
    [SerializeField] GameObject messageBox;

    private bool arrived = false;
    private bool talking;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        ChangeAnimation("Walk");

#nullable enable
        Vector3? dest = FindTable();
        if(dest != null)
        {
            agent.SetDestination((Vector3)dest);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(arrived == false && destination != null)
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

        if (talking)
        {
            messageBox.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
        }

    }

#nullable enable
    private Vector3? FindTable()
    {
        GameObject[] chairs = GameObject.FindGameObjectsWithTag("AvailableChair");
        if(chairs.Length > 0)
        {
            destination = chairs[Random.Range(0, chairs.Length)];
            agent.stoppingDistance = destination.GetComponent<Renderer>().bounds.size.z / 2 + destBias;
            destination.gameObject.tag = "TakenChair";

            return destination.transform.position;
        }
        return null;

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
        Talk();
    }

    private void Talk()
    {
        talking = true;

        Camera cam = Camera.main;
        GameObject canvas = GameObject.Find("Canvas");
        //float top = this.GetComponent<MeshFilter>().mesh.bounds.size.y;
        float top = 1f;
        Vector3 finalPos = new Vector3(transform.position.x, transform.position.y + top, transform.position.z);
        messageBox = Instantiate(messageBox, cam.WorldToScreenPoint(finalPos), Quaternion.identity, canvas.transform);
    }

}
