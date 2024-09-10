using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBallChecker : MonoBehaviour
{
    [HideInInspector] public bool isBasketBall;

    private float timer;

    private bool startTimer;



    private void OnTriggerEnter2D(Collider2D other) {

            if (other.gameObject.CompareTag("Ball"))
            {

                if(other.GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    isBasketBall = true;
                }

                
                
            }

    }


    private void OnTriggerExit2D(Collider2D other) {

            if (other.gameObject.CompareTag("Ball"))
            {

                isBasketBall = false;
                
            }

    }

    private void Update() 
    {
        if(startTimer)
        {

            timer += Time.deltaTime;

            if(timer > 0.5f)
            {
                timer = 0;
                isBasketBall = false;
                startTimer = false;
            }
            



        }
        

    }



    

}
