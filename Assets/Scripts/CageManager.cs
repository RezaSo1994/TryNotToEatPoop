using Spine.Unity;
using UnityEngine;

public class CageManager : MonoBehaviour
{

    [SerializeField] private Animator _animCage;
    [SerializeField] private GameObject _char;
    [SerializeField] private Transform target;
    [SerializeField] private SkeletonAnimation skin;
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _ps;
    public bool _isActive;
    internal int _index;
    public void Start()
    {
        skin.AnimationName = "idle";
        _ps.gameObject.SetActive(false);
    }
    public void Update()
    {

        if (_isActive)
        {
            Active();
            _animCage.enabled = true;
            Invoke("Delay", 0.2f);
        }
    }
    public void Active()
    {
        _animCage.SetTrigger("NoIdle");
        _char.transform.position = Vector3.MoveTowards(_char.transform.position, target.position, _speed * Time.deltaTime);
        if (Mathf.Abs(_char.transform.position.x - target.position.x) < 0.3f)
        {
            PlayerController._instanse.EndCage();
            _isActive = false;
            skin.gameObject.SetActive(false);
            _ps.gameObject.SetActive(true);
            _ps.Play();
        }
    }
    private void Delay()
    {
        skin.AnimationName = "run";
    }
    public void Init(int _index)
    {
       this._index =_index;
      skin.skeleton.SetSkin(this._index.ToString());
    }

}
