using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayForPoolMode : MonoBehaviour
{
    

    [SerializeField] private GameObject[] holeChecker;
    [SerializeField] private GameObject Ball;
    [SerializeField] private Transform BallStarPoint;



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


    [HideInInspector] public bool holePlaced;
    [HideInInspector] public int holeWall;
   

    [HideInInspector] public bool poolModeGoal;
    private float scaleDif;


    private float HoleAnchorX = 7.3f;
    private float minHoleAnchorY = 2;
    [HideInInspector] public float HoleAnchorY; 
    private float specialNum = 0.43f;
    private float heightDiff;
    private float minHeight = 9.23f;
    private float anchorDiff;



    private void Start()
    {

        Ball.transform.position = BallStarPoint.position;

        groundHole.SetActive(false);
        UpHole.SetActive(false);
        RightHole.SetActive(false);
        LeftHole.SetActive(false);
        
        HeightDifferenceFormula();
    }


    private void Update()
    {
        
        HolePlacer();
        GoalChecker();

    }



    

    

    private void HolePlacer()
    {

        if (!holePlaced)
        {


            if (holeWall == 1) // ground Hole
            {
                groundHole.SetActive(true);
                groundHole.GetComponent<CameraAnchor>().anchorOffset.x = Random.Range(-HoleAnchorX, HoleAnchorX);
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
                UpHole.GetComponent<CameraAnchor>().anchorOffset.x = Random.Range(-HoleAnchorX, HoleAnchorX);
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
                LeftHole.GetComponent<CameraAnchor>().anchorOffset.y = Random.Range(-HoleAnchorY, HoleAnchorY);
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
                RightHole.GetComponent<CameraAnchor>().anchorOffset.y = Random.Range(-HoleAnchorY, HoleAnchorY);
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

            if (holeChecker[i].GetComponent<Hole>().poolGoal == true)
            {
        
                Ball.GetComponent<TrailRenderer>().time = 0;
                Ball.transform.position = BallStarPoint.position;
                Ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Ball.GetComponent<Rigidbody2D>().angularVelocity = 0;
                DoGoalEffect();
                poolModeGoal = true;
                holeWall = Random.Range(1, 5);
                holePlaced = false;
                holeChecker[i].GetComponent<Hole>().poolGoal = false;
                updateScore = true;
                Ball.GetComponent<TrailRenderer>().time = 0.3f;


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

    public void ResetBall()
    {

                Ball.GetComponent<TrailRenderer>().time = 0;
                Ball.transform.position = BallStarPoint.position;
                Ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Ball.GetComponent<Rigidbody2D>().angularVelocity = 0;
                Ball.GetComponent<TrailRenderer>().time = 0.3f;

    }

    private void HeightDifferenceFormula()
    {
        
        heightDiff = FindObjectOfType<ViewportHandler>()._height - minHeight;
        anchorDiff = heightDiff * specialNum;
        HoleAnchorY = minHoleAnchorY + anchorDiff;
        //print("HoleAnchorY: " +HoleAnchorY);


    }

}
