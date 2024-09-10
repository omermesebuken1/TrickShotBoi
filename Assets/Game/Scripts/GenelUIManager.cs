using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenelUIManager : MonoBehaviour
{
    [SerializeField] private GameObject LoaderCanvas;
    [SerializeField] private AudioClip GameSceneEnter;
    [SerializeField] private AudioClip ButtonClick;

    private void Start() {
        LoaderCanvas.SetActive(false);
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(GameSceneEnter);
            }
        }
    }


    public void GoToScene(string sceneName)
    {
        //ingamecanvas.SetActive(false);
        LevelManager.Instance.LoadScene(sceneName);
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(ButtonClick);
            }
        }

    }
}
