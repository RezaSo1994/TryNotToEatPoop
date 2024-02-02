using UnityEngine;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{
    public static HUDManager _instance;
    [SerializeField]
    private Text[] _conTxt, _scoreTxt;
    [SerializeField]
    private Image _mana;
    [SerializeField]
    internal int _maxCountMana = 5;
    internal int _currentMana = 0;
    internal int _score = 0;
    public int Coin
    {
        get
        {
            return (PlayerPrefs.GetInt("Coin", 0));
        }
        set
        {
            PlayerPrefs.SetInt("Coin", value);
           for (int i = 0; i < _conTxt.Length; i++)
           {
               _conTxt[i].text = value.ToString();
           }
        }
    }

    private float _addAmoun = 0.025f;

    private int _doubleJump = 0;
    [Space]
    [SerializeField]
    private Scrollbar _jumpValue;
    [SerializeField]
    private bool _isActive;
    [SerializeField]
    private float _speed , _speedEmptyMana;
    private float _valueNew;
    private int _dir;
    [SerializeField]
    private bool _isFull = false;
    private bool _isEmptyMana;

    public delegate void External();
    public event External _big;
    public event External _default;

    private void Awake()
    {
        _instance = this;
        // ResetData();
        for (int i = 0; i < _conTxt.Length; i++)
        {
            _conTxt[i].text = Coin.ToString();
        }
    }
    private void Init()
    {
        _speed = 1;
    }
    private void FixedUpdate()
    {
        if (_isActive)
        {
            if (_valueNew >= 1)
            {
                _dir = -1;
            }
            else if (_valueNew <= 0)
            {
                _dir = +1;
            }
            _valueNew += Time.deltaTime * _speed * _dir;
            _jumpValue.value = _valueNew;
        }
        if(_isEmptyMana )
        {
            _mana.fillAmount -= Time.deltaTime * _speedEmptyMana;
            if(_mana.fillAmount <= 0)
            {
                _isEmptyMana = _isFull = false;
                _isEmptyMana = false;
                _currentMana = 0;
                if (_default != null)
                    _default.Invoke();
            }
        }
    }
    public float GetValue()
    {
        return _valueNew;
    }
   // public void S()
   // {
   //     _isActive = false;
   // }
    public void ResetValue()
    {
       // _isActive = false;
        _valueNew = 0;
    }
    public void Iint()
    {
        _isFull = false;
       // _isActive = false;
        ResetData();
    }

    internal void ShowCoinValue(int count)
    {
        for (int i = 0; i < _conTxt.Length; i++)
        {
            _conTxt[i].text = count.ToString();
        }
    }
    internal void jumpValueHUDShow(bool isActive)
    {
        _jumpValue.gameObject.SetActive(isActive);
        _isActive = isActive;
    }
    internal void Score(int index)
    {
        _score += index;
        for (int i = 0; i < _scoreTxt.Length; i++)
        {


            _scoreTxt[i].text = _score.ToString();
        }
    }
    public void ManaUpData()
    {
        if (!_isFull)
        {
            _currentMana = (_currentMana <= _maxCountMana) ? ++_currentMana : 0;

            _mana.fillAmount = ((float)_currentMana / (float)_maxCountMana);
            if (_currentMana == _maxCountMana)
            {
                _isFull = true;
                //Invoke("ResetMana",1f);
                _isEmptyMana = true;
                if (_big != null)
                    _big.Invoke();
            }
        }
    }
    internal void DoubleJump()
    {
        print("Duble Jump");
        ResetMana();

    }
    private void ResetMana()
    {
        _currentMana = 0;
        _mana.fillAmount = 0;
        _isFull = false;
    }
    public void ResetData()
    {
        _isFull = false;
        _score = 0;
        _speed = 1;
        ResetMana();
        Score(0);
    }
    internal void AddSpeed()
    {
        _speed += _addAmoun;
    }


}
