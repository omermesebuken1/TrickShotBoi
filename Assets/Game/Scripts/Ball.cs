using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

using distriqt.plugins.vibration;

public class Ball : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    [HideInInspector] public TrailRenderer trail;

    [SerializeField] private GameObject colEffectLight;

    [SerializeField] private GameObject colEffectMedium;

    [SerializeField] private AudioClip BasketballHitLight;
    [SerializeField] private AudioClip BasketballHitHeavy;
    [SerializeField] private AudioClip BallHitLight;
    [SerializeField] private AudioClip BallHitHeavy;

    private FeedbackGenerator _selectGenerator;
    private FeedbackGenerator _impactGenerator;

    [HideInInspector] public Vector3 pos { get { return transform.position; } }


    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        trail = GetComponent<TrailRenderer>();

    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void Update()
    {




        if (rb.velocity == new Vector2(0, 0))
        {

            trail.enabled = false;
        }
        else
        {
            trail.enabled = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        //print(other.gameObject.name);

        //print("X: " + rb.velocity.x + " Y: " + rb.velocity.y);

        if (!other.gameObject.CompareTag("NoEffect"))
        {

            if (Mathf.Abs(rb.velocity.x) > 14 || Mathf.Abs(rb.velocity.y) > 12)
            {
                if (PlayerPrefs.HasKey("Sound"))
                {
                    if (PlayerPrefs.GetInt("Sound") == 1)
                    {
                        if (SceneManager.GetActiveScene().name == "BasketMode")
                        {
                            GetComponent<AudioSource>().PlayOneShot(BasketballHitHeavy);
                        }
                        else
                        {
                            GetComponent<AudioSource>().PlayOneShot(BallHitHeavy);
                        }
                    }
                }

                if (PlayerPrefs.HasKey("Vibration"))
                {
                    if (PlayerPrefs.GetInt("Vibration") == 1)
                    {
                        if (_impactGenerator == null)
                        {
                            _impactGenerator = Vibration.Instance.CreateFeedbackGenerator(FeedbackGeneratorType.IMPACT);
                            _impactGenerator.Prepare();
                        }
                        _impactGenerator.PerformFeedback();

                    }
                }

                Instantiate(colEffectMedium, transform.position, colEffectLight.transform.rotation);
            }

            if ((Mathf.Abs(rb.velocity.x) > 8 || Mathf.Abs(rb.velocity.y) > 6) && (Mathf.Abs(rb.velocity.x) < 14 || Mathf.Abs(rb.velocity.y) < 12))
            {
                if (PlayerPrefs.HasKey("Sound"))
                {
                    if (PlayerPrefs.GetInt("Sound") == 1)
                    {
                        if (SceneManager.GetActiveScene().name == "BasketMode")
                        {
                            GetComponent<AudioSource>().PlayOneShot(BasketballHitLight);
                        }
                        else
                        {
                            GetComponent<AudioSource>().PlayOneShot(BallHitLight);
                        }
                    }
                }

                if (PlayerPrefs.HasKey("Vibration"))
                {
                    if (PlayerPrefs.GetInt("Vibration") == 1)
                    {
                        if (_selectGenerator == null)
                        {
                            _selectGenerator = Vibration.Instance.CreateFeedbackGenerator(FeedbackGeneratorType.SELECTION);
                        }
                        _selectGenerator.PerformFeedback();

                    }
                }

                Instantiate(colEffectLight, transform.position, colEffectLight.transform.rotation);
            }



        }

        if ((Mathf.Abs(rb.velocity.x) > 2 || Mathf.Abs(rb.velocity.y) > 2) && (Mathf.Abs(rb.velocity.x) < 8 || Mathf.Abs(rb.velocity.y) < 6))
        {
            if (PlayerPrefs.HasKey("Sound"))
            {
                if (PlayerPrefs.GetInt("Sound") == 1)
                {
                    if (SceneManager.GetActiveScene().name == "BasketMode")
                    {
                        GetComponent<AudioSource>().pitch = Random.Range(0.7f, 1.3f);
                        GetComponent<AudioSource>().PlayOneShot(BasketballHitLight);
                    }
                    else
                    {
                        GetComponent<AudioSource>().pitch = Random.Range(0.7f, 1.3f);
                        GetComponent<AudioSource>().PlayOneShot(BallHitLight);
                    }
                }
            }

            if (PlayerPrefs.HasKey("Vibration"))
            {
                if (PlayerPrefs.GetInt("Vibration") == 1)
                {
                    if (_selectGenerator == null)
                    {
                        _selectGenerator = Vibration.Instance.CreateFeedbackGenerator(FeedbackGeneratorType.SELECTION);
                    }
                    _selectGenerator.PerformFeedback();

                }
            }



        }

    }


}
