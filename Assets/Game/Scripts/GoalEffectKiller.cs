using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEffectKiller : MonoBehaviour
{
   private float timer;
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 0.5f)
        {

                Destroy(this.gameObject);

        }
        
    }
}
