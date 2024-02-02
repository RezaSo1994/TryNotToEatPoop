using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController __instance__;
    public AudioSource _music, _sound;
    [SerializeField]
    public bool _isMeut;
    [Space]
    [SerializeField]
    private AudioClip _tap, _Lose , _jump , _menu , _gamePlay;

    public Button _audioBtn , _audioBtnLose;
    [SerializeField]
    private Sprite _activeAudio, _deActiveAudio;

    public bool _SoundData
    {
        get
        {
            return (PlayerPrefs.GetInt("_SoundData", 0) == 0) ? false : true;
        }
        set
        {
            int val = (value) ? 1 : 0;
            PlayerPrefs.SetInt("_SoundData", val);
        }
    }

    private void Awake()
    {

        __instance__ = this;
        //MeutAudio();

    }
    private void Start()
    {
        AudioController.__instance__._isMeut = _SoundData;
        MeutAudio();
        AudioController.__instance__._music.clip = _menu;
        Music();
    }
    public void BtnMusic()
    {
        _SoundData = (_SoundData) ? false : true;
        AudioController.__instance__._isMeut = _SoundData;
        MeutAudio();
        Music();
    }
    private void MeutAudio()
    {
        AudioController.__instance__._isMeut = _SoundData;
        if (_SoundData)
        {
            _audioBtn.GetComponentsInChildren<Image>()[1].sprite = _activeAudio;
            _audioBtnLose.GetComponentsInChildren<Image>()[1].sprite = _activeAudio;
        }
        else
        {
            _audioBtn.GetComponentsInChildren<Image>()[1].sprite = _deActiveAudio;
            _audioBtnLose.GetComponentsInChildren<Image>()[1].sprite = _deActiveAudio;
        }
    }
    public void Music()
    {
        if (!_isMeut)
            _music.Play();
        else
        {
            _music.Stop();
        }
    }
    public void StopMusic()
    {
        if (!_isMeut)
            _music.Stop();
    }
    public void Tap()
    {
        if (!_isMeut)
        {
            _sound.PlayOneShot(_tap);
        }
    }
    public void Lose()
    {
        if (!_isMeut)
            _sound.PlayOneShot(_Lose);
    }
    public void Jump()
    {
        if (!_isMeut)
            _sound.PlayOneShot(_jump);
    }
    public void GamePlay()
    {
        if (!_isMeut)
        {
            AudioController.__instance__._music.clip = _gamePlay;
            _music.Play();
        }
    }
}
