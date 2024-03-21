using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerAwarenessController
{
    float _playerAwarenessDistance = 6;
    float agentsRangeOfMovement = 1000f;


    // Update is called once per frame
    public bool GetNewPositionFrom(Transform centrePoint, Vector2 enemyToPlayerVector, bool isAgentDone,out Vector3 randomPosition)
    {
        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance) //is monster close to player?
        {
            randomPosition = Vector3.zero;
            return false; //chase the target's position (in this case - the player)
        }
        else
        {
            
            if (isAgentDone) //done with path
            {
                createRandomPoint(centrePoint.position, out randomPosition);
                Debug.DrawRay(randomPosition, Vector2.up, Color.red, 1.0f); //so you can see with gizmos
                
                //Debug.Log("getting random position is" + randomPosition);
                return true;
            }
            randomPosition = Vector3.zero;
            return false; //wander aimlessly
        }
    }

    public bool createRandomPoint(Vector3 center, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * agentsRangeOfMovement; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, agentsRangeOfMovement, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
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

