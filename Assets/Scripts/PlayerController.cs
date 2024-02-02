using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using System.Collections.Generic;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _instanse;
    public const string BLOCK = "block";
    public const string Prison = "prison";
    public const string JUMP = "Jump";
    public const string RESETCONT = "ResetCount";
    private Rigidbody2D rb;
    Transform tr;
    internal Vector2 _posplayer;
    private bool _isTimer = false;
    internal bool _cage;
    public delegate void InitDel();
    public event InitDel ev_Init;
    [SerializeField] private GameObject Tutorial;
    [SerializeField] private float height;

    [SerializeField] private Button _jumpBtn, _pauseBtn;

    public List<int> _scorePlayer = new List<int>();

    private int _prison = 0;
    [SerializeField] private Text prisonTxt;
    private Animator _anim;
    private float _currentSpeed;
    [SerializeField] private Animator _animback;

    [SerializeField] private Image _rieviveImg;
    [SerializeField] private Button _rievivebtn;
    [SerializeField] private GameObject _rievive;
    private bool _isRevive = true;
    private float _timerievive;
    private GameObject _badCol;
    internal int _starterSkin = 6;

    public int _cunterJump = 0;

    [SerializeField] internal ImitationPlayer[] _skinOther;

    private int _counterRievive = 0;
    [Serializable]
    public class DataAll
    {
        public int skin;
        [SpineSlot]
        public string[] slotproperty;
    }
    public DataAll[] _data;
    [System.Serializable]
    public class PlayerScore
    {
        public int id;
        public int score
        {
            get
            {
                return (PlayerPrefs.GetInt("PlayerScore" + id.ToString(), 0));
            }
            set
            {
                PlayerPrefs.SetInt("PlayerScore" + id.ToString(), value);
            }
        }
    }

    public void Imitation(string animState)
    {
        for (int i = 0; i < _skinOther.Length; i++)
        {
            if (_skinOther[i].gameObject.activeSelf)
                _skinOther[i].Imitation(animState);
        }
    }

    public PlayerScore[] _playerData;

    private float score, _dis;
    [SerializeField] private Text[] _highScore, _score;
    private SkeletonAnimation skin;
    private SkeletonRenderer skeltonRenderer;

    [SerializeField] private Text _tipeTxt , _tipeTxt2;
    [Space(10)]
    [SerializeField] private string[] _tipe;

    public int lastTipe
    {
        get
        {
            return PlayerPrefs.GetInt("lastTipe", 0);
        }
        set
        {
            PlayerPrefs.SetInt("lastTipe", value);
        }
    }
    public int FirstTutorial
    {
        get
        {
            return PlayerPrefs.GetInt("FirstTutorial", 0);
        }
        set
        {
            PlayerPrefs.SetInt("FirstTutorial", value);
        }
    }

    internal bool _isJump, _isDie;
    private void Awake()
    {
        _isDie = false;
        _instanse = this;

        _rievivebtn.onClick.AddListener(() =>
        {
            //  AdsShow(); Show Ads
            _isDeActiveDie = false;
            Sc_AdVideo.instance.ShowVideo();
            // Success Ads
            // SuccessRievive();
        });
        _scorePlayer = new List<int>();
        for (int d = 0; d < _playerData.Length; d++)
        {
            _scorePlayer.Add(_playerData[d].score);
        }

        _isRevive = false;
        tr = transform;
        _posplayer = tr.position;
        rb = GetComponent<Rigidbody2D>();
        skin = GetComponent<SkeletonAnimation>();
        skeltonRenderer = GetComponent<SkeletonRenderer>();
        _anim = GetComponent<Animator>();
        skin.loop = true;
        skin.skeleton.SetSkin("3");
        skin.AnimationName = "idle";

        _rievive.SetActive(false);
        _jumpBtn.onClick.AddListener(() => { JumpAllController(); });
        _pauseBtn.onClick.AddListener(() => { });
        ResetSkin();
        for (int i = 0; i < _highScore.Length; i++)
            _highScore[i].text = _playerData[0].score.ToString();
    }
    [ContextMenu("Delet")]
    public void Delet()
    {
        print("Delet");
        PlayerPrefs.DeleteAll();
    }
    private void Start()
    {
        Imitation("idle");
        prisonTxt.text = _prison.ToString() + "/3";
        Time.timeScale = 1;
        for (int i = 0; i < _highScore.Length; i++)
            _highScore[i].text = _playerData[0].score.ToString();

    }


    public void FixedUpdate()
    {
        if (_isTimer)
        {
            score += Time.deltaTime * 10;
            string value = score.ToString("00000");
            if (_prison == 3)
            {
                _dis += Time.deltaTime * 10;
                ChunkManeger.Instance._speed += ((_dis / (ChunkManeger.Instance._speedChunk * 1000)) * (float)0.01f);
                ChunkManeger.Instance._speed = Mathf.Clamp(ChunkManeger.Instance._speed, 0, ChunkManeger.Instance._maxSpeed);
            }
            for (int i = 0; i < _score.Length; i++)
            {
                _score[i].text = value;

            }
        }
        if (_isRevive)
        {
            _timerievive += Time.deltaTime * 0.3f;
            _rieviveImg.fillAmount = _timerievive;
            if (_rieviveImg.fillAmount == 1)
            {
                if (!_isRevive)
                    return;
                _rievive.SetActive(false);
                _isDeActiveDie = false;
                Die();
                _isRevive = false;
            }
        }
    }

    public void JumpAllController()
    {
        if (!_cage)
        {
            if (_cunterJump == 0)
            {
                Jump();
            }
            else
            {
                _skinOther[_cunterJump - 1].Imitation("jump");
            }

            var activeChar = _prison;
            if (activeChar != 0)
            {
                _cunterJump = (_cunterJump == activeChar) ? 0 : ++_cunterJump;
            }
        }
    }

    public void SetUpHUD()
    {
        //if(FirstTutorial == 0)
        //{
        Tutorial.SetActive(true);
        Invoke("DeActiveTutorial", 5);
        //}
        skin.loop = true;
        skin.AnimationName = "run";
        Imitation("run");
        _isTimer = true;
        if (ev_Init != null)
            ev_Init.Invoke();
    }

    public void DeActiveTutorial()
    {
        Tutorial.SetActive(false);
        FirstTutorial = 1;
    }
    public void Run()
    {
        if (!_isDie && !_cage)
        {
            skin.loop = true;
            skin.AnimationName = "run";
            _isJump = false;
            //Imitation("run");
            _isJump = false;
            tr.position = new Vector3(_posplayer.x, _posplayer.y);
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(BLOCK))
        {
            //  Die();
            Rievive(col.gameObject);

            Imitation("die");
        }
        if (col.CompareTag(RESETCONT))
        {
            ResetCount();
        }
        if (col.CompareTag(Prison))
        {
            col.gameObject.GetComponent<CageManager>()._isActive = true;
            _cage = true;
            CageHit();

        }
    }
    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag(RESETCONT))
        {
            ResetCount();
        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag(BLOCK))
        {
            print("OnCollisionEnter2D: " + col);
        }
        if (col.collider.CompareTag(RESETCONT))
        {

            ResetCount();
        }
        if (col.gameObject.CompareTag(Prison))
        {
            _cage = true;
            col.gameObject.GetComponent<CageManager>()._isActive = true;
            CageHit();

        }
    }
    public void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.CompareTag(RESETCONT))
        {
            ResetCount();
        }
    }
    private bool _isResetData = true;
    public void ResetCount()
    {
        if (_isResetData)
        {
            _isResetData = false;
            Invoke("ISActiveReset", 1.5f);
            if (!_isJump)
            {
                _cunterJump = 0;
                for (int i = 0; i < _skinOther.Length; i++)
                {
                    _skinOther[i].DeActiveJump();
                }
            }
            else
            {
                print("is Jumppppping");
            }
        }
    }
    private void ISActiveReset()
    {
        _isResetData = true;

    }
    public void CageHit()
    {
        //  print("CageHit");
        _isTimer = false;
        skin.loop = false;
        _isJump = true;
        skin.AnimationName = "win";
        Imitation("win");
        _animback.speed = 0;
        _currentSpeed = ChunkManeger.Instance._speed;
        ChunkManeger.Instance._isActiveAll = false;
        //   ChunkManeger.Instance._speed = 0;
    }
    public void EndCage()
    {
        //  print("EndCage");
        _cunterJump = 0;
        _cage = false;
        _isTimer = true;
        _isJump = false;
        //  CageSkin(_prison);
        _prison += 1;
        prisonTxt.text = _prison.ToString() + "/3";
        _skinOther[_prison - 1].gameObject.SetActive(true);

        skin.loop = true;
        _animback.speed = 1;
        skin.AnimationName = "run";
        Imitation("run");
        ChunkManeger.Instance._isActiveAll = true;
        // ChunkManeger.Instance._speed= _currentSpeed;
        _isJump = false;
    }


    public void Rievive(GameObject obj)
    {
        if (_counterRievive <= 2)
        {
            _badCol = obj;
            _rievive.SetActive(true);
            _timerievive = 0;
            _isRevive = true;

            var lastTipe2 = UnityEngine.Random.Range(0, _tipe.Length);
            while (lastTipe == lastTipe2)
            {
                lastTipe2 = UnityEngine.Random.Range(0, _tipe.Length);
            }
            lastTipe = lastTipe2;
           // print("lastTipe2 :" + _tipe[lastTipe2]);
            _tipeTxt.text = _tipe[lastTipe2].ToString();
            _tipeTxt2.text = _tipe[lastTipe2].ToString();
            ChunkManeger.Instance._isActiveAll = false;
            _animback.speed = 0;

            _isDie = true;
            skin.loop = false;
            skin.AnimationName = "die";
            _isTimer = false;

        }
        else
        {
            _isDeActiveDie = false;
            Die();
        }
        //  Imitation("die");
    }


    public void Die()
    {
        if (!_isDeActiveDie)
        {
            _rievive.SetActive(false);
            _isDie = true;
            skin.loop = false;
            _isTimer = false;
            skin.AnimationName = "die";
            Imitation("die");
            //  print("BLOCK");
            ChunkManeger.Instance._speed = 0;
            UIManager._instance.GameOver(true);
            AudioController.__instance__.Lose();
            AudioController.__instance__.StopMusic();
            print("GameOver");
            _scorePlayer.Add((int)score);

            // ss = Array.Sort(_scorePlayer.ToArray());
            _scorePlayer.Sort();
            _scorePlayer.Reverse();
            _animback.speed = 0;

            Array.Sort(_scorePlayer.ToArray(), (x, y) => y.CompareTo(x));
            if (_scorePlayer.Count > 10)
            {
                for (int i = 10; i < _scorePlayer.Count; i++)
                {
                    _scorePlayer.RemoveAt(i);
                }
            }
            for (int h = 0; h < 10; h++)
            {
                _playerData[h].score = _scorePlayer[h];
            }


            for (int p = 0; p < _highScore.Length; p++)
                _highScore[p].text = _playerData[0].score.ToString();



            for (int k = 0; k < _score.Length; k++)
            {
                _score[k].text = ((int)score).ToString();

            }
        }
    }
    private bool _isJumpSound;
    public void Jump()
    {
        if (!_isJump && !_isDie)
        {
            // rb.AddForce(Vector2.up * height, ForceMode2D.Impulse);
            skin.loop = false;
            skin.AnimationName = "jump";
            // Imitation("jump");
            _isJump = true;
            if (!_isJumpSound)
            {
                _isJumpSound = true;
                AudioController.__instance__.Jump();
                Invoke("ActiveSound", 0.4f);
            }
            _anim.SetTrigger(JUMP);
            Invoke("Run", 0.5f);
        }
    }
    public void ActiveSound()
    {
        _isJumpSound = false;
    }
    public void CageSkin(int cage)
    {
        for (int j = 0; j < _data[cage].slotproperty.Length; j++)
        {
            var str = _data[cage].slotproperty[j].ToString();
            if (str[str.Length - 1] == '2')
            {
                str = str.Remove(str.Length - 1);
            }
            //  skin.skeleton.SetAttachment(_data[cage].slotproperty[j], str + "-" + _data[cage].skin.ToString());
            skin.skeleton.SetAttachment(_data[cage].slotproperty[j], str + "-" + GameController._instance.cage[cage]._index);
            // print($"========= > >>>>>  {cage}    :::::: {GameController._instance.cage[cage]._index} ");

        }
    }
    public int Cage = 2;
    [ContextMenu("Text")]
    public void Test()
    {
        CageSkin(Cage);
    }
    [ContextMenu("Reset")]
    public void ResetSkin()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            for (int j = 0; j < _data[i].slotproperty.Length; j++)
            {
                // skin.skeleton.SetAttachment(_data[i].slotproperty[j], _data[i].slotproperty[j] + _data[i].skin);
                var str = _data[i].slotproperty[j].ToString();
                if (str[str.Length - 1] == '2')
                {
                    str = str.Remove(str.Length - 1);
                }
                skin.skeleton.SetAttachment(_data[i].slotproperty[j], str + "-" + _starterSkin.ToString());
            }
        }
        skin.skeleton.SetAttachment("body-up", "body-up-" + _starterSkin.ToString());

        skin.skeleton.SetAttachment("body-down", "body-down-" + _starterSkin.ToString());
    }
    public void RieviveEnd()
    {
        _rievive.SetActive(true); ;
    }
    public void AdsShow()
    {

    }
    internal bool _isDeActiveDie = false;
    public void SuccessRievive()
    {
       // _isDeActiveDie = true;
        ;
        _counterRievive += 1;
        _rievive.SetActive(false);
        _timerievive = 0;
        _isRevive = false;
        _cunterJump = 0;
        ChunkManeger.Instance._isActiveAll = true;
        _animback.speed = 1;

        _isJump = false;
        skin.loop = true;
        skin.AnimationName = "run";
        Imitation("run");
        _isTimer = true;

        _isDie = false;
        _badCol.SetActive(false);
        for (int i = 0; i < _skinOther.Length; i++)
        {
            _skinOther[i].DeActiveJump();
        }

    }
}
