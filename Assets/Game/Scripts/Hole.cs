using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    
    [HideInInspector] public bool goal;

    [HideInInspector] public bool poolGoal;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            goal = true;
        }

        if(other.gameObject.CompareTag("PoolBall"))
        {
            poolGoal = true;
        }
            
    }

}
