using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;

    [SerializeField] private GameObject _menu, _gameover, _hud, _leaderBoard ,_pause;
    [SerializeField] private Button _retryLoseBtn, _retrypauseBtn, _menuPauseBtn, _menuLoseBtn, _pauseBtn, _PlayHud, _PlayHud2;
    [SerializeField]private Animator _anim;
    [SerializeField] private Text[] _coinValue;
    public bool retry
    {
        get
        {
            return (PlayerPrefs.GetInt("retry", 0) == 0) ? false : true;
        }
        set
        {
            int val = (value) ? 1 : 0;
            PlayerPrefs.SetInt("retry", val);
        }
    }

    private void Awake()
    {
        _instance = this;
        _retryLoseBtn.onClick.AddListener(()=> { Retry(); });
        _retrypauseBtn.onClick.AddListener(()=> { Retry(); });
        _menuLoseBtn.onClick.AddListener(()=> { Application.LoadLevel(Application.loadedLevel); });
        _menuPauseBtn.onClick.AddListener(()=> { Application.LoadLevel(Application.loadedLevel); });
        _pauseBtn.onClick.AddListener(()=> { Time.timeScale = 0; _pause.SetActive(true); });
        _PlayHud.onClick.AddListener(()=> { Time.timeScale = 1  ; _pause.SetActive(false); });
        _PlayHud2.onClick.AddListener(()=> { Time.timeScale = 1  ; _pause.SetActive(false); });
    }
    private void Start()
    {
        
        if(retry)
        {
            Menu(false);
            retry = false;
            // 
            Invoke("SetRetry",0.2f);
        }
        else
        {
            Menu(true);
        }
    }
    public void SetRetry()
    {
        HUD(true);
    }
    public void Menu(bool isActive)
    {
        if(isActive)
        {
            _menu.SetActive(true);
        }
        else
        {
            _menu.SetActive(false);
        }
    }
    public void GameOver(bool isActive)
    {
        if (isActive)
        {
            _gameover.SetActive(true);
            _hud.SetActive(false);
            _menu.SetActive(false);

        }
        else
        {
            _gameover.SetActive(false);
            HUD(true);
        }
    }
    public void HUD(bool isActive)
    {
        if (isActive)
        {
            _hud.SetActive(true);
            PlayerController._instanse.SetUpHUD();
            Menu(false);
            GameController._instance.StartGame();
            AudioController.__instance__.GamePlay();
            _anim.enabled = true;
        }
        else
        {
            _hud.SetActive(false);
            Menu(true);
        }
    }
    public void LeaderBoard(bool isActive)
    {
        if (isActive)
        {
            _leaderBoard.SetActive(true);
            Menu(false);
            UserManige._instance.Setup();
        }
        else
        {
            _leaderBoard.SetActive(false);
            if(!_gameover.activeSelf)
            {
                Menu(true);
            }
           //
        }
    }
    [ContextMenu("Retey")]
    public void Retry()
    {
        retry = true;
        Application.LoadLevel(Application.loadedLevel);
    }
}
