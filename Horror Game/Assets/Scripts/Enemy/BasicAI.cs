using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.SceneManagement;

public class BasicAI : MonoBehaviour
{

    [SerializeField] float FOV;
    [SerializeField] float SightDistance;
    [SerializeField] float range = 2;


    private float pFOV;

    [SerializeField] float walkSpeed = 4;
    [SerializeField] float runSpeed = 7;

    private Vector3 lastPosSaw;

    private float count = 0;

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
        HidingScript.spots.Add(this);
    }


    private void Update()
    {


        player = FindObjectOfType<MovementScript>().transform;

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        RaycastHit hit = new RaycastHit();
        if (Vector3.Distance(transform.position, player.position) < SightDistance)
        {
            Debug.DrawLine(transform.position, player.position, Color.red);
            if(Vector3.Angle(transform.forward, player.position - transform.position) < pFOV)
            {
                Debug.DrawLine(transform.position, player.position, Color.blue);
                Ray ray = new Ray(transform.position, player.position - transform.position);
             

                if(Physics.Raycast(ray, out hit))
                {
                  
                    if (hit.transform == player && HidingScript.hiding == false)
                    {
                        lastPosSaw = player.position;
                        agent.SetDestination(lastPosSaw);
                        Debug.DrawLine(transform.position, player.position, Color.green);
                        spotted = true;
                    }
                   
                }
               
            }
           
        }
     

  
        
        if(spotted)
        {

            if(Vector3.Distance(player.position, transform.position) < range)
            {
                Death();
            }

            pFOV = 360;
            if(agent.remainingDistance <= agent.stoppingDistance + 1)
            {
                if (hit.transform != player || HidingScript.hiding)
                {
                    spotted = false;
                    pFOV = FOV;
                    agent.SetDestination(nodes[Random.Range(0, nodes.Length)].position);
                }
            }
            agent.acceleration = 28;
            agent.SetDestination(lastPosSaw);
            agent.speed = runSpeed;
        }
        else
        {
            pFOV = FOV;
            agent.acceleration = 8;
            agent.speed = walkSpeed;
            if(agent.destination != null)
            {
                if(agent.remainingDistance <= agent.stoppingDistance + 1)
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

    void Death()
    {
        
        Application.LoadLevel(Application.loadedLevel);
        
    }

}
