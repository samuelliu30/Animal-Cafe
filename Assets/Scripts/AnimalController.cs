using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AnimalController : MonoBehaviour
{
    private NavMeshAgent agent;
    [Space(10)]
    public Animator animator;
    private GameObject destination;
    [SerializeField] float destBias = 2f;
    [SerializeField] GameObject messageBox;

    private bool arrived = false;
    private bool talking;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

#nullable enable
        Vector3? dest = FindTable();
        if(dest != null)
        {
            agent.SetDestination((Vector3)dest);
            ChangeAnimation(1);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (arrived == false && destination != null)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // Done
                        arrived = true;
                        ChangeAnimation(0);
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
        ChangeAnimation(4);

    }

    private void ChangeAnimation(int animation)
    {
        this.animator.SetInteger("animation", animation);
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
        /*
        talking = true;

        Camera cam = Camera.main;
        GameObject canvas = GameObject.Find("Canvas");
        //float top = this.GetComponent<MeshFilter>().mesh.bounds.size.y;
        float top = 1f;
        Vector3 finalPos = new Vector3(transform.position.x, transform.position.y + top, transform.position.z);
        messageBox = Instantiate(messageBox, cam.WorldToScreenPoint(finalPos), Quaternion.identity, canvas.transform);
        */
    }

}
