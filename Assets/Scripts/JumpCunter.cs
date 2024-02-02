using UnityEngine;
using UnityEngine.UI;

public class JumpCunter : MonoBehaviour
{
    private Text _txt;
    void Start()
    {
        _txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController._instanse != null)
            _txt.text = PlayerController._instanse._cunterJump.ToString();
    }
}
