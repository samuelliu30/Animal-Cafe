using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;
using CodeMonkey.Utils;


public class AnimalController : MonoBehaviour
{
    private RecipeManager recipeManager;

    private NavMeshAgent agent;
    [Space(10)]
    public Animator animator;
    private GameObject destination;
    [SerializeField] float destBias = 2f;
    public GameObject DialoguePop;
    private GameObject messageBox;
    private Vector3 offSet;

    private bool arrived = false;
    private bool talking;
    private bool leaving;

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
        recipeManager = GameObject.Find("RecipeManager").GetComponent<RecipeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (arrived == false && destination != null)
        //{
        //    if (!agent.pathPending)
        //    {
        //        if (agent.remainingDistance <= agent.stoppingDistance)
        //        {
        //            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
        //            {
        //                // Done
        //                arrived = true;
        //                ChangeAnimation(0);
        //                this.GetComponent<NavMeshAgent>().enabled = false;
        //                SitOn();
        //            }
        //        }
        //    }

        //}

        if (leaving == false && arrived == false && destination != null)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                arrived = true;
                ChangeAnimation(0);
                this.GetComponent<NavMeshAgent>().enabled = false;
                SitOn();
            }
        }

        if (leaving == true && arrived == false && destination != null)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                arrived = true;
                ChangeAnimation(0);
                this.GetComponent<NavMeshAgent>().enabled = false;
                Debug.Log("Bye Bye Suckers");
                Destroy(this.gameObject);
            }
        }

        if (talking)
        {
            if (messageBox)
            {
                messageBox.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + offSet);

            }
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

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Leave();
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

        // Do something like talking or ordering
        Talk();
        //Leave();
    }

    private void Talk()
    {
        Camera cam = Camera.main;
        GameObject canvas = GameObject.Find("Canvas");
        //float top = this.GetComponent<MeshFilter>().mesh.bounds.size.y;
        float top = 5f;
        offSet = new Vector3(0f, top, 0f);

        Vector3 finalPos = new Vector3(transform.position.x, transform.position.y + top, transform.position.z);
        messageBox = Instantiate(DialoguePop, cam.WorldToScreenPoint(finalPos), Quaternion.identity, canvas.transform);
        Sprite cuck = recipeManager.recipeIconDict["coffee"];
        messageBox.transform.Find("Image").GetComponent<Image>().sprite = cuck;

        messageBox.transform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            Order();
            Destroy(messageBox);
        };

        talking = true;
    }

    private void Leave()
    {
        arrived = false;
        GameObject doorLeft = GameObject.FindWithTag("DoorLeft");
        GameObject doorRight = GameObject.FindWithTag("DoorRight");
        Vector3 doorPos = new Vector3((doorLeft.transform.position.x + doorRight.transform.position.x) / 2, 0, (doorLeft.transform.position.z + doorRight.transform.position.z) / 2);
        this.GetComponent<NavMeshAgent>().enabled = true;
        ChangeAnimation(1);

        agent.SetDestination(doorPos);
        leaving = true;
    }

    private void Order()
    {
        StartCoroutine(ExecuteAfterTime(3));
    }

}
