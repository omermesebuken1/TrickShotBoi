using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

using distriqt.plugins.vibration;

public class Pota : MonoBehaviour
{
    [SerializeField] private GameObject checker1;
    [SerializeField] private GameObject checker2;

    [SerializeField] private GameObject BasketParticleEffect;
    [SerializeField] private GameObject BasketCreatedEffect;
    [SerializeField] private GameObject BasketDiedEffect;
    [SerializeField] private AudioClip BasketballNetSound;
    public bool Basket;

    private float delayTimer;

    private bool startTimer;


    private bool effectCasted;

    private FeedbackGenerator _impactGenerator;

    private void Start()
    {

        Instantiate(BasketCreatedEffect, transform.position, transform.rotation * Quaternion.Euler(new Vector3(-90, 0, 0)));

    }

    void Update()
    {

        if (checker1.GetComponent<BasketBallChecker>().isBasketBall && checker2.GetComponent<BasketBallChecker2>().isBasketBall2)
        {
            startTimer = true;

        }



        if (startTimer)
        {

            delayTimer += Time.deltaTime;
            FindObjectOfType<BasketBallBall>().WriteDistance();

            if (delayTimer > 0.1f)
            {
                if (!effectCasted)
                {
                    if (PlayerPrefs.HasKey("Sound"))
                    {
                        if (PlayerPrefs.GetInt("Sound") == 1)
                        {
                            GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
                            GetComponent<AudioSource>().PlayOneShot(BasketballNetSound);
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

                    Instantiate(BasketParticleEffect, transform.position, transform.rotation * Quaternion.Euler(new Vector3(-90, 0, 0)));
                    Instantiate(BasketDiedEffect, transform.position, transform.rotation * Quaternion.Euler(new Vector3(-90, 0, 0)));
                    effectCasted = true;
                }

            }

            if (delayTimer > 0.3f)
            {
                delayTimer = 0;
                Basket = true;
                startTimer = false;

            }

        }

    }

    private void DestroyEvent()
    {

        Destroy(this.gameObject);
    }
}
