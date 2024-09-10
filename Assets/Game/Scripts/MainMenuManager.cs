using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject LoaderCanvas;
    [SerializeField] private GameObject PlayButton;

    [SerializeField] private GameObject SettingsButton;
    [SerializeField] private GameObject Ball;
    [SerializeField] private TextMeshProUGUI ErrorMessageArea;

    [SerializeField] private GameObject MidHud;
    [SerializeField] private GameObject Trajectory;

    public TMP_InputField nameInput;

    [SerializeField] private AudioClip ButtonClick;
    [SerializeField] private AudioClip NicknameEnter;
    [SerializeField] private AudioClip MainmenuEnter;


    private void Start()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(MainmenuEnter);
            }
        }
        SoundVibrationCheck();
        PlayButton.SetActive(false);
        SettingsButton.SetActive(false);
        Ball.SetActive(false);
        LoaderCanvas.SetActive(false);
        MidHud.SetActive(false);
        Trajectory.SetActive(false);
        ErrorMessageArea.text = "";

        PlayerHasName();
    }

    private void Update()
    {

        playerNameUpdated();
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

    private void PlayerHasName()
    {

        if (PlayerPrefs.HasKey("PlayerName"))
        {
            print("Hoşgeldin " + PlayerPrefs.GetString("PlayerName"));
            PlayButton.SetActive(true);

            SettingsButton.SetActive(true);
            Ball.SetActive(true);
            Trajectory.SetActive(true);
        }
        else
        {

            MidHud.SetActive(true);

        }

    }

    public void submitName()
    {
        if (nameInput.text == null)
        {
            ErrorMessageArea.text = "Nickname can't be empty.";
        }
        else if (nameInput.text.Length > 15)
        {
            ErrorMessageArea.text = "Nickname too long.";
        }
        else if (nameInput.text.Length < 3)
        {
            ErrorMessageArea.text = "Nickname too short.";
        }

        else if (nameInput.text != null)
        {
            PlayerPrefs.SetString("PlayerName", nameInput.text);
            FindObjectOfType<PlayfabManager>().SubmitNameButton();

            if (PlayerPrefs.HasKey("Sound"))
            {
                if (PlayerPrefs.GetInt("Sound") == 1)
                {
                    GetComponent<AudioSource>().PlayOneShot(NicknameEnter);
                }
            }
        }


    }


    private void playerNameUpdated()
    {
        if (FindObjectOfType<PlayfabManager>().nameUpdated)
        {
            PlayButton.SetActive(true);

            SettingsButton.SetActive(true);
            Ball.SetActive(true);
            Trajectory.SetActive(true);
            MidHud.SetActive(false);
            FindObjectOfType<PlayfabManager>().nameUpdated = false;
        }
    }

    private void SoundVibrationCheck()
    {
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
        }


        if (!PlayerPrefs.HasKey("Vibration"))
        {
            PlayerPrefs.SetInt("Vibration", 1);
        }


    }

}
