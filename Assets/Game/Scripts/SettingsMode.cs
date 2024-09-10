using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMode : MonoBehaviour
{

    [SerializeField] private GameObject LoaderCanvas;
    [SerializeField] private Toggle SoundCheck;
    [SerializeField] private Toggle VibrationCheck;
    [SerializeField] private GameObject Backbutton;
    [SerializeField] private TextMeshProUGUI ErrorMessageArea;
    public TMP_InputField nameInput;

    [SerializeField] private AudioClip ButtonClick;
    [SerializeField] private AudioClip NicknameEnter;

    [SerializeField] private TextMeshProUGUI SensitivityValue;
    [SerializeField] private Slider SensitivitySlider;
   
    private void Start()
    {
        LoaderCanvas.SetActive(false);
        ErrorMessageArea.text = "";
        SensitivityCheck();
        if(PlayerPrefs.GetInt("Sound") == 1)
        {
            SoundCheck.isOn = true;
        }
        else
        {
            SoundCheck.isOn = false;
        }

        if(PlayerPrefs.GetInt("Vibration") == 1)
        {
            VibrationCheck.isOn = true;
        }
        else
        {
            VibrationCheck.isOn = false;
        }


    }

    
    private void Update()
    {
        SoundVibrationSettings();
        Sensitivity();
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

     public void submitName()
    {
        if(nameInput.text == null)
        {
            ErrorMessageArea.text = "Nickname can't be empty.";
            ErrorMessageArea.color = Color.red;
        }
        else if(nameInput.text.Length > 15)
        {
            ErrorMessageArea.text = "Nickname too long.";
            ErrorMessageArea.color = Color.red;
        }
        else if(nameInput.text.Length < 3)
        {
            ErrorMessageArea.text = "Nickname too short.";
            ErrorMessageArea.color = Color.red;
        }
        
        else if(nameInput.text != null)
        {
            ErrorMessageArea.text = "Nickname Changed.";
            ErrorMessageArea.color = Color.white;
            PlayerPrefs.SetString("PlayerName",nameInput.text);
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

    private void SoundVibrationSettings()
    {
        


        if(SoundCheck.isOn)
        {
            PlayerPrefs.SetInt("Sound",1);
        }
        else
        {
            PlayerPrefs.SetInt("Sound",0);
        }

        if(VibrationCheck.isOn)
        {
            PlayerPrefs.SetInt("Vibration",1);
        }
        else
        {
            PlayerPrefs.SetInt("Vibration",0);
        }

    }

    public void privacy()
    {
        Application.OpenURL("https://doc-hosting.flycricket.io/trick-shot-boi-privacy-policy/eed707f0-30d8-41ef-89a4-bc83de882904/privacy");
    }

    public void Terms()
    {
        Application.OpenURL("https://doc-hosting.flycricket.io/trick-shot-boi-terms-of-use/a68f9bec-8f3b-40d9-b0c2-52b952868fc9/terms");
    }


    private void Sensitivity()
    {

        SensitivityValue.text = SensitivitySlider.value.ToString("F1");
        PlayerPrefs.SetFloat("Sensitivity",SensitivitySlider.value);

    }


    private void SensitivityCheck()
    {

        if(PlayerPrefs.HasKey("Sensitivity"))
        {

             SensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");

        }
        else
        {
            PlayerPrefs.SetFloat("Sensitivity",3f);
        }


    }










}
