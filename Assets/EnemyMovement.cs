using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float _playerAwarenessDistance;
    [SerializeField] float agentRangeOfMovement;
    [SerializeField] Transform centrePoint;



    private NavMeshAgent agent;
    private double freezeSeconds = 3.0;
    private double frozenTime = 0.0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FreezeMonster();
            Destroy(other.gameObject);

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
        SceneManager.LoadScene("Scenes/Menu"); 
    }

    void Start()
    {
		// target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        //This code makes the monster chase the player
        if(agent.enabled)
        {

            Vector2 enemyToPlayerVector = target.position - agent.nextPosition;

            if(enemyToPlayerVector.magnitude <= _playerAwarenessDistance) //is monster close to player?
            {
                agent.SetDestination(target.position); //chase the target's position (in this case - the player)
            }
            else
            {
                Vector3 randomPosition;
                if(agent.remainingDistance <= agent.stoppingDistance) //done with path
                {
                    RandomPoint(centrePoint.position, out randomPosition);
                    Debug.Log("enemyToRandomVector, agent is going to ... " +randomPosition);
                    Debug.DrawRay(randomPosition, Vector2.up, Color.red, 1.0f); //so you can see with gizmos
                    agent.SetDestination(randomPosition); //wander aimlessly
                    
                }
            }
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

    
    bool RandomPoint(Vector3 center, out Vector3 result)
    {
 
        Vector3 randomPoint = center + Random.insideUnitSphere * agentRangeOfMovement; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, agentRangeOfMovement,  NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
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
