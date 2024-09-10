using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GamePlay : MonoBehaviour
{

    [SerializeField] private GameObject[] holeChecker;
    [SerializeField] public GameObject Ball;
    [SerializeField] public Transform BallStarPoint;



    [SerializeField] private GameObject groundWall;
    [SerializeField] private GameObject upWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;

    [SerializeField] private GameObject groundHole;
    [SerializeField] private GameObject UpHole;
    [SerializeField] private GameObject LeftHole;
    [SerializeField] private GameObject RightHole;

    [SerializeField] private GameObject Environment;

    [SerializeField] private GameObject groundHoleEffectPoint;
    [SerializeField] private GameObject UpHoleEffectPoint;
    [SerializeField] private GameObject LeftHoleEffectPoint;
    [SerializeField] private GameObject RightHoleEffectPoint;

    [SerializeField] private GameObject groundHoleChanger;
    [SerializeField] private GameObject UpHoleChanger;
    [SerializeField] private GameObject LeftHoleChanger;
    [SerializeField] private GameObject RightHoleChanger;



    [SerializeField] private GameObject GoalEffect;
    [SerializeField] private GameObject GoalParticleEffect;

    [HideInInspector] public bool updateScore;

    [HideInInspector] public bool holeModeGoal;


    [HideInInspector] public bool holePlaced;
    [HideInInspector] public int holeWall;


    private int movingHolesRandom;
    [SerializeField] private int movingHolesPossibilty;
    [SerializeField] private float SpeedOfMovingHoles;
    private bool moveHoles;
    private int directionOfMovingHoles;

    private int ScaleChangeRandom;
    [SerializeField] private int ScaleChangePossibilty;
    private bool ScaleChangeBool;


    private float scaleDif;
    private float HoleRealScale = 0.13379f;



    private void Start()
    {

        Ball.transform.position = BallStarPoint.position;

        groundHole.SetActive(false);
        UpHole.SetActive(false);
        RightHole.SetActive(false);
        LeftHole.SetActive(false);


        
        isHoleMoving();
        isScaleChanging();
        directionOfMovingHoles = 1;
    }


    private void Update()
    {

        HolePlacer();
        GoalChecker();
        
        MoveHoles();
        ScaleChange();
        scaleChangeAmountCalculator();


    }



    private void isHoleMoving()
    {
        movingHolesRandom = Random.Range(1, 101);

        if (movingHolesRandom <= movingHolesPossibilty)
        {
            moveHoles = true;
        }
        else
        {
            moveHoles = false;
        }
    }

    private void isScaleChanging()
    {
        ScaleChangeRandom = Random.Range(1, 101);

        if (ScaleChangeRandom <= ScaleChangePossibilty)
        {
            ScaleChangeBool = true;
        }
        else
        {
            ScaleChangeBool = false;
        }
    }

    private void HolePlacer()
    {

        if (!holePlaced)
        {


            if (holeWall == 1) // ground Hole
            {
                groundHole.SetActive(true);
                groundHole.GetComponent<CameraAnchor>().anchorOffset.x = Random.Range(-7.3f, 7.3f);
                groundWall.GetComponent<BoxCollider2D>().enabled = false;

                upWall.GetComponent<BoxCollider2D>().enabled = true;
                UpHole.GetComponent<Animator>().SetTrigger("CloseHole");

                leftWall.GetComponent<BoxCollider2D>().enabled = true;
                LeftHole.GetComponent<Animator>().SetTrigger("CloseHole");

                rightWall.GetComponent<BoxCollider2D>().enabled = true;
                RightHole.GetComponent<Animator>().SetTrigger("CloseHole");

                holePlaced = true;
            }

            if (holeWall == 2) // up Hole
            {
                UpHole.SetActive(true);
                UpHole.GetComponent<CameraAnchor>().anchorOffset.x = Random.Range(-7.3f, 7.3f);
                upWall.GetComponent<BoxCollider2D>().enabled = false;

                groundWall.GetComponent<BoxCollider2D>().enabled = true;
                groundHole.GetComponent<Animator>().SetTrigger("CloseHole");

                leftWall.GetComponent<BoxCollider2D>().enabled = true;
                LeftHole.GetComponent<Animator>().SetTrigger("CloseHole");

                rightWall.GetComponent<BoxCollider2D>().enabled = true;
                RightHole.GetComponent<Animator>().SetTrigger("CloseHole");

                holePlaced = true;
            }

            if (holeWall == 3) // left Hole
            {
                LeftHole.SetActive(true);
                LeftHole.GetComponent<CameraAnchor>().anchorOffset.y = Random.Range(-2f, 2f);
                leftWall.GetComponent<BoxCollider2D>().enabled = false;

                upWall.GetComponent<BoxCollider2D>().enabled = true;
                UpHole.GetComponent<Animator>().SetTrigger("CloseHole");

                groundWall.GetComponent<BoxCollider2D>().enabled = true;
                groundHole.GetComponent<Animator>().SetTrigger("CloseHole");

                rightWall.GetComponent<BoxCollider2D>().enabled = true;
                RightHole.GetComponent<Animator>().SetTrigger("CloseHole");

                holePlaced = true;
            }

            if (holeWall == 4) // right Hole
            {
                RightHole.SetActive(true);
                RightHole.GetComponent<CameraAnchor>().anchorOffset.y = Random.Range(-2f, 2f);
                rightWall.GetComponent<BoxCollider2D>().enabled = false;

                upWall.GetComponent<BoxCollider2D>().enabled = true;
                UpHole.GetComponent<Animator>().SetTrigger("CloseHole");

                leftWall.GetComponent<BoxCollider2D>().enabled = true;
                LeftHole.GetComponent<Animator>().SetTrigger("CloseHole");

                groundWall.GetComponent<BoxCollider2D>().enabled = true;
                groundHole.GetComponent<Animator>().SetTrigger("CloseHole");

                holePlaced = true;
            }

            if (holeWall == 5) // empty
            {

                groundHole.SetActive(false);
                UpHole.SetActive(false);
                RightHole.SetActive(false);
                LeftHole.SetActive(false);
    
                rightWall.GetComponent<BoxCollider2D>().enabled = true;
                upWall.GetComponent<BoxCollider2D>().enabled = true;
                leftWall.GetComponent<BoxCollider2D>().enabled = true;
                groundWall.GetComponent<BoxCollider2D>().enabled = true;
               
                holePlaced = true;
            }

        }

    }

    private void GoalChecker()
    {

        for (int i = 0; i < holeChecker.Length; i++)
        {

            if (holeChecker[i].GetComponent<Hole>().goal == true)
            {
                //Environment.GetComponent<Animator>().SetTrigger("GoalTrigger");
                holeModeGoal = true;
                Ball.GetComponent<TrailRenderer>().time = 0;
                Ball.transform.position = BallStarPoint.position;
                Ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Ball.GetComponent<Rigidbody2D>().angularVelocity = 0;
                DoGoalEffect();
                holeWall = Random.Range(1, 5);
                holePlaced = false;
                isHoleMoving();
                isScaleChanging();
                holeChecker[i].GetComponent<Hole>().goal = false;
                updateScore = true;
                Ball.GetComponent<TrailRenderer>().time = 0.3f;


            }

        }




    }

    

    private void MoveHoles()
    {
        if (moveHoles)
        {

            if (holeWall == 1) // ground Hole
            {

                if (groundHole.GetComponent<CameraAnchor>().anchorOffset.x < -6.85)
                {
                    directionOfMovingHoles = 1;
                }

                if (groundHole.GetComponent<CameraAnchor>().anchorOffset.x > 6.85)
                {
                    directionOfMovingHoles = -1;
                }

                groundHole.GetComponent<CameraAnchor>().anchorOffset.x += Time.deltaTime * directionOfMovingHoles * SpeedOfMovingHoles;

            }

            if (holeWall == 2) // up Hole
            {

                if (UpHole.GetComponent<CameraAnchor>().anchorOffset.x < -6.85)
                {
                    directionOfMovingHoles = 1;
                }

                if (UpHole.GetComponent<CameraAnchor>().anchorOffset.x > 6.85)
                {
                    directionOfMovingHoles = -1;
                }

                UpHole.GetComponent<CameraAnchor>().anchorOffset.x += Time.deltaTime * directionOfMovingHoles * SpeedOfMovingHoles;

            }

            if (holeWall == 3) // left Hole
            {

                if (LeftHole.GetComponent<CameraAnchor>().anchorOffset.y < -1.7)
                {
                    directionOfMovingHoles = 1;
                }

                if (LeftHole.GetComponent<CameraAnchor>().anchorOffset.y > 1.7)
                {
                    directionOfMovingHoles = -1;
                }

                LeftHole.GetComponent<CameraAnchor>().anchorOffset.y += Time.deltaTime * directionOfMovingHoles * SpeedOfMovingHoles;

            }

            if (holeWall == 4) // right Hole
            {

                if (RightHole.GetComponent<CameraAnchor>().anchorOffset.y < -1.7)
                {
                    directionOfMovingHoles = 1;
                }

                if (RightHole.GetComponent<CameraAnchor>().anchorOffset.y > 1.7)
                {
                    directionOfMovingHoles = -1;
                }

                RightHole.GetComponent<CameraAnchor>().anchorOffset.y += Time.deltaTime * directionOfMovingHoles * SpeedOfMovingHoles;

            }

        }
    }

    private void ScaleChange()
    {

        if (ScaleChangeBool)
        {

            if (holeWall == 1 && groundHole.GetComponent<CameraAnchor>().anchorOffset.x < 6.85 && groundHole.GetComponent<CameraAnchor>().anchorOffset.x > -6.85) // ground Hole
            {

                groundHole.GetComponent<Animator>().SetTrigger("ScaleChange");

            }


            if (holeWall == 2 && UpHole.GetComponent<CameraAnchor>().anchorOffset.x < 6.85 && UpHole.GetComponent<CameraAnchor>().anchorOffset.x > -6.85) // Up Hole
            {

                UpHole.GetComponent<Animator>().SetTrigger("ScaleChange");

            }

            if (holeWall == 3 && LeftHole.GetComponent<CameraAnchor>().anchorOffset.y < 1.7 && LeftHole.GetComponent<CameraAnchor>().anchorOffset.y > -1.7) // Left Hole
            {

                LeftHole.GetComponent<Animator>().SetTrigger("ScaleChange");

            }

            if (holeWall == 4 && RightHole.GetComponent<CameraAnchor>().anchorOffset.y < 1.7 && RightHole.GetComponent<CameraAnchor>().anchorOffset.y > -1.7) // Right Hole
            {

                RightHole.GetComponent<Animator>().SetTrigger("ScaleChange");

            }


        }

    }

    private void DoGoalEffect()
    {
        if (holeWall == 1)
        {

            GoalEffect.transform.localScale = GoalEffect.transform.localScale + new Vector3(scaleDif, scaleDif, scaleDif);
            Instantiate(GoalEffect, groundHoleEffectPoint.transform.position, groundHoleEffectPoint.transform.rotation);
            GoalEffect.transform.localScale = GoalEffect.transform.localScale - new Vector3(scaleDif, scaleDif, scaleDif);
            Instantiate(GoalParticleEffect, groundHoleEffectPoint.transform.position, groundHoleEffectPoint.transform.rotation * Quaternion.Euler(-90, 0, 0));

        }

        if (holeWall == 2)
        {
            GoalEffect.transform.localScale = GoalEffect.transform.localScale + new Vector3(scaleDif, scaleDif, scaleDif);
            Instantiate(GoalEffect, UpHoleEffectPoint.transform.position, UpHoleEffectPoint.transform.rotation);
            GoalEffect.transform.localScale = GoalEffect.transform.localScale - new Vector3(scaleDif, scaleDif, scaleDif);
            Instantiate(GoalParticleEffect, UpHoleEffectPoint.transform.position, UpHoleEffectPoint.transform.rotation * Quaternion.Euler(-90, 0, 0));
        }

        if (holeWall == 3)
        {
            GoalEffect.transform.localScale = GoalEffect.transform.localScale + new Vector3(scaleDif, scaleDif, scaleDif);
            Instantiate(GoalEffect, LeftHoleEffectPoint.transform.position, LeftHoleEffectPoint.transform.rotation);
            GoalEffect.transform.localScale = GoalEffect.transform.localScale - new Vector3(scaleDif, scaleDif, scaleDif);
            Instantiate(GoalParticleEffect, LeftHoleEffectPoint.transform.position, LeftHoleEffectPoint.transform.rotation * Quaternion.Euler(-90, 0, 0));
        }

        if (holeWall == 4)
        {

            GoalEffect.transform.localScale = GoalEffect.transform.localScale + new Vector3(scaleDif, scaleDif, scaleDif);
            Instantiate(GoalEffect, RightHoleEffectPoint.transform.position, RightHoleEffectPoint.transform.rotation);
            GoalEffect.transform.localScale = GoalEffect.transform.localScale - new Vector3(scaleDif, scaleDif, scaleDif);
            Instantiate(GoalParticleEffect, RightHoleEffectPoint.transform.position, RightHoleEffectPoint.transform.rotation * Quaternion.Euler(-90, 0, 0));
        }
    }

    private void scaleChangeAmountCalculator()
    {

        if (holeWall == 1)
        {
            scaleDif = groundHoleChanger.transform.localScale.x - HoleRealScale;
        }

        if (holeWall == 2)
        {
            scaleDif = UpHoleChanger.transform.localScale.x - HoleRealScale;
        }

        if (holeWall == 3)
        {
            scaleDif = LeftHoleChanger.transform.localScale.x - HoleRealScale;
        }

        if (holeWall == 4)
        {
            scaleDif = RightHoleChanger.transform.localScale.x - HoleRealScale;
        }


    }


}
