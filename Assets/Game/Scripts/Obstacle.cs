using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

   
    [SerializeField] private LayerMask checkLayerMask;
    private float RandomScale;
   
    private float m_Red;
    private float m_Green;
    private float m_Blue;

    public bool sphereChecker;

    

    void Start()
    {

        

        RandomScale = Random.Range(0.15f, 0.25f);
        transform.localScale = new Vector3(RandomScale, RandomScale, RandomScale);

        float massFactor = 0.2f/RandomScale;

        GetComponent<Rigidbody2D>().mass = GetComponent<Rigidbody2D>().mass * massFactor;

        m_Red = Random.Range(110, 200) / 255.0f;
        m_Green = Random.Range(110, 200) / 255.0f;
        m_Blue = Random.Range(110, 200) / 255.0f;

       

        GetComponent<SpriteRenderer>().color = new Color(m_Red, m_Green, m_Blue);
        

        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)));


    }

    private void Update()
    {

        InsideChecker();
        MoveRandomly();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.transform.CompareTag("Obstacle"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)));
        }

    }

    private void InsideChecker()
    {
        if(sphereChecker)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position,0.3f, checkLayerMask,Mathf.Infinity,Mathf.Infinity);

        if(collider != null && collider.gameObject != this.gameObject)
        {
            Destroy(this.gameObject);
        }

        }
        
    }

    private void MoveRandomly()
    {

       float veloSpeed = Mathf.Sqrt(Mathf.Pow(GetComponent<Rigidbody2D>().velocity.x,2) + Mathf.Pow(GetComponent<Rigidbody2D>().velocity.y,2));

        if(veloSpeed < 0.3f)
        {

            GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)));

        }
    }

   
    



}
