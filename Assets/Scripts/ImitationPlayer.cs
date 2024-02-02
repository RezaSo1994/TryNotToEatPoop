using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImitationPlayer : MonoBehaviour
{
    internal SkeletonAnimation _skin;
    public const string BLOCK = "block";
    private Animator _anim;
    [SerializeField]internal bool _isJump, _isDie;
    private void Awake()
    {
        _skin = GetComponent<SkeletonAnimation>();
        _anim = GetComponent<Animator>();
    }

    public void Imitation(string animState)
    {
        if (gameObject.activeSelf)
        {
            //  print("Jump " + animState);

            if (animState == "idle" || animState == "run")
                _skin.loop = true;
            else
                _skin.loop = false;
            _skin.AnimationName = animState;
            if (animState == "jump" && !_isJump)
            {
                Invoke("Run", 0.5f);
                _isJump = true;
                _skin.AnimationName = "jump";
                _anim.SetTrigger("Jump");
                AudioController.__instance__.Jump();
            }
            // StartCoroutine(JumpOther(animState));
        }
        else
        {
            // print("animState");
        }

    }
    public void Run()
    {
        if (!PlayerController._instanse._isDie && !PlayerController._instanse._cage)
        {
            print("Run Imit");
            _skin.loop = true;
            _skin.AnimationName = "run";
            _isJump = false;
            transform.position = new Vector3(transform.position.x, PlayerController._instanse._posplayer.y);
        }
    }
    public void DeActiveJump()
    {
        _isJump = false;
    }
    public IEnumerator JumpOther(string animState)
    {
        if (_skin.gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.1f);
            _skin.AnimationName = animState;
        }
    }

    public void Skin(int index)
    {
        _skin.skeleton.SetSkin(index.ToString());
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(BLOCK))
        {
            PlayerController._instanse.Rievive(col.gameObject);
            _skin.loop = true;
            PlayerController._instanse.Imitation("die");
        }
    }
}
