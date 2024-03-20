using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    private NavMeshAgent agent;
    private double freezeSeconds = 3.0;
    private double frozenTime = 0.0;
    public GameObject deathAnime;
    public GameObject deathOverlay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FreezeMonster();
            Destroy(other.gameObject);
            deathOverlay.GetComponent<Animator>().SetBool("Death", true);
            deathAnime.GetComponent<Animator>().SetBool("Death", true);
            StartCoroutine(ExitGameCoroutine());
            //LOAD ANIMATION()

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
