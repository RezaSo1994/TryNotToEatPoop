using UnityEngine;
using UnityEngine.UI;

public class UserManige : MonoBehaviour
{
    public static UserManige _instance;

    public Text[] _text;

    [System.Serializable]
    public class User

    {
        public string userName;
        public int Score;
    }
    public User[] _users;


    [System.Serializable]
    public class SendScore
    {
        public string mode;
        public int getScore;
        public SendScore(string moed, int getScore)
        {
            this.mode = moed;
            this.getScore = getScore;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    public int GetScore(string Sort)
    {
        int backScore = 0;
        for (int i = 0; i < _users.Length; i++)
        {
            if (_users[i].userName == Sort)
            {
                backScore = _users[i].Score;
            }
        }
        return backScore;
    }

    [ContextMenu("test")]
    public void Setup()
    {
        for (int i = 0; i < _users.Length ; i++)
        {
            int Score = GetScore(_users[i].userName);
            SendScore _sendScore = new SendScore(_users[i].userName, Score);
            _text[i].text = "Score  \t\t  ".ToString() + "   " + PlayerController._instanse._scorePlayer[i].ToString();
        }
 
    }
}
