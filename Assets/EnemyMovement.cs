using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    private NavMeshAgent agent;
    public string restartScenePath = "Scenes/Menu";
    private double freezeSeconds = 3.0;
    private double frozenTime = 0.0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(restartScenePath); 
        }
        else if (other.gameObject.CompareTag("projectile"))
        {
            agent.enabled = false;
        }
    

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
        if(agent.enabled)
        {
            //chase the target's position (in this case - the player)
            agent.SetDestination(target.position);
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
}
