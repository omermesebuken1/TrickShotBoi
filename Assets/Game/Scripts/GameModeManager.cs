using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    
    [SerializeField] private AudioClip ButtonClick;

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
