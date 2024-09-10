using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBallChecker2 : MonoBehaviour
{
   [HideInInspector] public bool isBasketBall2;

    private void OnTriggerEnter2D(Collider2D other) {

            if (other.gameObject.CompareTag("Ball"))
            {
                if(other.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    isBasketBall2 = true;
                }


                
                
            }

    }


    private void OnTriggerExit2D(Collider2D other) {

            if (other.gameObject.CompareTag("Ball"))
            {

                isBasketBall2 = false;
                
            }

    }

  
}
