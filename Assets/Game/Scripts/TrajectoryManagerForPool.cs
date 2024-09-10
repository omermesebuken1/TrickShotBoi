using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TrajectoryManagerForPool : MonoBehaviour
{
    Camera cam;

    Touch touch;


    public Ball ball;

    public TrajectoryForPool trajectory;

    [SerializeField] float PushForce = 4f;
    bool isDraging;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;


   

    public bool touchOn;


    private bool touchEnabled;

    private void Start()
    {
        touchOn = true;
        cam = Camera.main;

    }

    private void Update()
    {

        UITouchChecker();

        MoveBallWithTouch();
        



    }


    

    private void MoveBallWithTouch()
    {
        if (touchOn)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                Vector2 pos = cam.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began && touchEnabled)
                {

                    
                    TouchOnDragStart();

                }

                else if (touch.phase == TouchPhase.Moved && touchEnabled)
                {

                    TouchOnDrag();

                }

                else if (touch.phase == TouchPhase.Ended && touchEnabled)
                {

                    TouchOnDragEnd();
                    

                }






            }

        }
    }

    


    #region DragTouch
    private void TouchOnDragStart()
    {

        startPoint = cam.ScreenToWorldPoint(touch.position);
        ball.rb.velocity = new Vector2(0, 0);
        ball.rb.angularVelocity = 0;
        
    }
    private void TouchOnDrag()
    {


        ball.rb.velocity = new Vector2(0, 0);
        ball.rb.angularVelocity = 0;
        ball.rb.isKinematic = true;

        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = distance * direction * PushForce;
        
        trajectory.UpdateDots(ball.pos, force);
        trajectory.Show();
    }
    private void TouchOnDragEnd()
    {
       // print(force);
        ball.rb.isKinematic = false;
        ball.Push(force);
        trajectory.Hide();
        startPoint = new Vector2(0,0);
        endPoint = new Vector2(0,0);
        distance = 0;
        force = new Vector2(0,0);

    }
    #endregion




    private void UITouchChecker()
    {
        //Exit if touch is over UI element.
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;

            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                
                touchEnabled = false;
                
                return;
            }
            
            else
            {
                touchEnabled = true;
                
            }
        }
    }
}
