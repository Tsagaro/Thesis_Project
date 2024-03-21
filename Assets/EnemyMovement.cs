using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static PlayerAwarenessController;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float _playerAwarenessDistance;
    [SerializeField] float agentRangeOfMovement;
    [SerializeField] Transform centrePoint;



    private NavMeshAgent agent;
    private double freezeSeconds = 3.0;
    private double frozenTime = 0.0;
    public GameObject deathAnime;
    public GameObject deathOverlay;

    private PlayerAwarenessController controller;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FreezeMonster();
            Destroy(other.gameObject);
            deathOverlay.GetComponent<Animator>().SetBool("Death", true);
            deathAnime.GetComponent<Animator>().SetBool("Death", true);
            StartCoroutine(ExitGameCoroutine());

        }
        else if (other.gameObject.CompareTag("projectile"))
        {
            FreezeMonster();
        }
    }

    private void FreezeMonster()
    {
        agent.enabled = false;
    }   

    IEnumerator ExitGameCoroutine()
    {
        yield return new WaitForSeconds(3f);
        deathOverlay.GetComponent<Animator>().SetBool("Death", false);
        deathAnime.GetComponent<Animator>().SetBool("Death", false);
        SceneManager.LoadScene("Scenes/Menu"); 
    }

    void Start()
    {
		// target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //agent.SetDestination(target.position);
        //controller = new PlayerAwarenessController();
    }

    void Update()
    {
        if(target == null) return;
        //This code makes the monster chase the player
        if(agent.enabled)
        {


            Vector2 enemyToPlayerVector = target.position - agent.nextPosition;

            if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance) //is monster close to player?
            {
                agent.SetDestination(target.position); //chase the target's position (in this case - the player)
            }
            else
            {

                if (agent.remainingDistance <= agent.stoppingDistance) //done with path
                {
                    Vector3 randomPosition;
                    createRandomPoint(centrePoint.position, out randomPosition);
                    Debug.DrawRay(randomPosition, Vector2.up, Color.red, 1.0f); //so you can see with gizmos
                    agent.SetDestination(randomPosition);
                }
            }

           
            //bool wandering = controller.GetNewPositionFrom(centrePoint, enemyToPlayerVector, isAgentDone, out randomPosition);
            //Debug.Log("agent done being a thirsty b* " + isAgentDone + ", enemyToPlayerVector= " + enemyToPlayerVector.magnitude + " getting random position is: " + randomPosition + " [machinie is wandering] " + wandering);
            
        }
        else 
        {
            frozenTime += Time.deltaTime;
            if(frozenTime > freezeSeconds)
            {
                agent.enabled = true;
                frozenTime = 0.0;
            }
        }

    }


    public bool createRandomPoint(Vector3 center, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * agentRangeOfMovement; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, agentRangeOfMovement, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

}
