using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerScene : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button ButtonStartGame;
    [SerializeField] private Button ButtonRecords;
    [SerializeField] private Button ButtonTiter;
    [SerializeField] private Button ButtonSetings;


    [SerializeField] private Button[] ButtonsForManeMenu;

    [Header("Index Scenes")]
    [SerializeField] private int IndexScenegame = 1;

    [Header("Panels in menu")]
    [SerializeField] private GameObject PanelSetings;
    [SerializeField] private GameObject PanelRecords;
    [SerializeField] private GameObject PanelTiter;
    [SerializeField] private GameObject PanelManeMenu;

    [Header("Keys to PlayerPrefs")]
    [SerializeField] private string KeyMusicSoundValue = "musicvalue";
    [SerializeField] private string KeyMyNickname = "mynickname";

    [Header("SoundPlayer")]
    [SerializeField] private AudioSource MusicPlayer;

    [Header("Test authentication setings")]
    [SerializeField] private string SetMyNicknameTest = "It_is_me";

    public string MyNickname { get; private set; }




    private float MusicSoundValue;

    private void Start()
    {
        AuthenticationPlayer();
        initializationButtonOnClic();
        SetMusicSaundValue();
    }

    private void initializationButtonOnClic()
    {
        ButtonStartGame.onClick.AddListener(ClikButtonStartGame);
        ButtonRecords.onClick.AddListener(ClikButtonRecord);
        ButtonTiter.onClick.AddListener(ClikButtonTiter);
        ButtonSetings.onClick.AddListener(ClikButtonSetings);


        for(int i = 0; i < ButtonsForManeMenu.Length; i++ )
        {
            ButtonsForManeMenu[i].onClick.AddListener(ClikButtonForManeMenu);
        }
    }

    private void SetMusicSaundValue()
    {
        if (PlayerPrefs.HasKey(KeyMusicSoundValue))
        {
            MusicSoundValue = PlayerPrefs.GetFloat(KeyMusicSoundValue);
            MusicPlayer.volume = MusicSoundValue;
        }
        else
        {
            MusicSoundValue = 1f;
            PlayerPrefs.SetFloat(KeyMusicSoundValue, MusicSoundValue);
            MusicPlayer.volume = MusicSoundValue;
        }
    }

    private void ClikButtonStartGame()
    {
        SceneManager.LoadScene(IndexScenegame);
    }
    
    private void ClikButtonSetings()
    {
        PanelManeMenu.SetActive(false);
        PanelSetings.SetActive(true);
    }
    
    private void ClikButtonTiter()
    {
        PanelManeMenu.SetActive(false);
        PanelTiter.SetActive(true);
    }
    
    
    private void ClikButtonRecord()
    {
        PanelManeMenu.SetActive(false);
        PanelRecords.SetActive(true);
    }
    
    private void ClikButtonForManeMenu()
    {
        PanelSetings.SetActive(false);
        PanelRecords.SetActive(false);
        PanelTiter.SetActive(false);
        PanelManeMenu.SetActive(true);
    }

    private void AuthenticationPlayer()
    {
        MyNickname = TestAuthentication();
    }

    private string TestAuthentication()
    {
        PlayerPrefs.SetString(KeyMyNickname, SetMyNicknameTest);
        return SetMyNicknameTest;
    }


}
