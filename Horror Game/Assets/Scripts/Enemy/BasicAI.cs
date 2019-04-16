using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BasicAI : MonoBehaviour
{

    [SerializeField] float FOV;
    [SerializeField] float SightDistance;
    [SerializeField] float range = 2;
    [SerializeField] float searchRange = 10;

    [SerializeField] private Transform searchObj;
    [SerializeField] private bool searching = false;
    private float pFOV;

    [SerializeField] Transform[] spawnNodes;

    private float waiter = 0;
    private List<HidingObject> nearbyObjects = new List<HidingObject>();

    [SerializeField] float walkSpeed = 4;
    [SerializeField] float runSpeed = 7;

    private Vector3 Waitdestination;
    private Vector3 lastPosSaw;

    private float count = 0;


    [SerializeField] Animator enemyAnimator;

    private bool spotted;

    private Transform player;

    [SerializeField] Transform[] nodes;

    public void Forget()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(nodes[Random.Range(0, nodes.Length)].position);
    }




    private void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;

        if (spawnNodes.Length > 0)
        {
            Random.seed = System.DateTime.Now.Millisecond;
            transform.position = spawnNodes[Random.Range(0, spawnNodes.Length)].position;
        }
    }

    private void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        if(agent.enabled == false)
        {
            agent.enabled = true;
        }

        if (waiter <= 0)
        {
            Random.seed = System.DateTime.Now.Millisecond;

            player = FindObjectOfType<MovementScript>().transform;

            
            Debug.DrawLine(transform.position, agent.destination, Color.black);

            RaycastHit hit = new RaycastHit();
            if (Vector3.Distance(transform.position, player.position) < SightDistance)
            {
                Debug.DrawLine(transform.position, player.position, Color.red);
                if (Vector3.Angle(transform.forward, player.position - transform.position) < pFOV)
                {
                    Debug.DrawLine(transform.position, player.position, Color.blue);
                    Ray ray = new Ray(transform.position, player.position - transform.position);


                    if (Physics.Raycast(ray, out hit))
                    {

                        if (hit.transform == player && HidingScript.hiding == false)
                        {
                            lastPosSaw = player.position;
                            agent.SetDestination(lastPosSaw);
                            Debug.DrawLine(transform.position, player.position, Color.green);
                            spotted = true;
                            searching = false;
                        }

                    }

                }

            }

            GetComponent<MeshRenderer>().material.color = Color.white;

            if (spotted)
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else if (agent.destination == lastPosSaw)
            {
                GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            else if (searching)
            {
                GetComponent<MeshRenderer>().material.color = Color.magenta;
            }

            if (searching == false)
            {
                if (spotted)
                {

                    enemyAnimator.SetBool("Walking", false);
                    enemyAnimator.SetBool("Running", true);


                    if (Vector3.Distance(player.position, transform.position) < range)
                    {
                        Death();
                    }

                    pFOV = 360;
                    if (agent.remainingDistance <= agent.stoppingDistance + 1)
                    {
                        if (hit.transform != player || HidingScript.hiding)
                        {
                            searching = true;
                            spotted = false;
                            pFOV = FOV;

                        }
                        else if (HidingScript.hiding)
                        {
                            searching = true;
                            spotted = false;
                            pFOV = FOV;
                        }
                    }
                    agent.acceleration = 28;
                    agent.SetDestination(lastPosSaw);
                    agent.speed = runSpeed;
                }
                else
                {

                    enemyAnimator.SetBool("Walking", true);
                    enemyAnimator.SetBool("Running", false);

                    pFOV = FOV;
                    agent.acceleration = 8;
                    agent.speed = walkSpeed;
                    if (agent.destination != null)
                    {
                        if (agent.remainingDistance <= agent.stoppingDistance + 1)
                        {
                            agent.SetDestination(nodes[Random.Range(0, nodes.Length)].position);
                        }
                    }
                    else
                    {
                        agent.SetDestination(nodes[Random.Range(0, nodes.Length)].position);
                    }
                }
            }
            else
            {
                pFOV = FOV;
                agent.acceleration = 8;
                agent.speed = walkSpeed;

                enemyAnimator.SetBool("Walking", true);
                enemyAnimator.SetBool("Running", false);


                if (searchObj == null)
                {
                    nearbyObjects.Clear();

                    foreach (HidingObject spot in HidingSpotManager.hidingObjects)
                    {
                        Debug.Log("Precounted");
                        if (Vector3.Distance(spot.transform.position, transform.position) <= searchRange)
                        {
                            Debug.Log("Counted!");
                            nearbyObjects.Add(spot);
                        }
                    }

                    HidingObject[] objs = nearbyObjects.ToArray();

                    Debug.Log(objs.Length + " :size");
                    if (objs.Length > 0)
                    {
                        searchObj = objs[Random.RandomRange(0, objs.Length)].transform;

                        Wait(2, searchObj.position, agent);
                    }
                    else
                    {
                        Wait(2, nodes[Random.Range(0, nodes.Length)].position, agent);
                    }
                    }
                else
                {

                    enemyAnimator.SetBool("Walking", true);
                    enemyAnimator.SetBool("Running", false);


                    Debug.DrawLine(transform.position, agent.destination, Color.magenta);
                    if (searchObj == HidingScript.hideObject && HidingScript.hiding)
                    {
                        Debug.Log("Correct hiding spot");
                        if (Vector3.Distance(transform.position, searchObj.position) < 2)
                        {
                            
                            searching = false;
                            Death();
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, searchObj.position) < 2)
                        {
                            searchObj = null;
                            searching = false;
                            Wait(2, nodes[Random.Range(0, nodes.Length)].position, agent);
                           
                        }
                    }
                }
            }
        }
        else
        {

            enemyAnimator.SetBool("Walking", false);
            enemyAnimator.SetBool("Running", false);


            waiter -= Time.deltaTime;

            if(waiter <= 0.05)
            {
                agent.destination = Waitdestination;
            }

        }
    }

    public void Wait(float time, Vector3 destination, NavMeshAgent agent)
    {
        waiter = time;
        Waitdestination = destination;
        agent.SetDestination(agent.transform.position);
    }

    void Death()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        
    }

}
