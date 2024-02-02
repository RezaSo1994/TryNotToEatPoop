using UnityEngine;
using System.Collections.Generic;
public class GameController : MonoBehaviour
{
    public static GameController _instance;

    [SerializeField] private ChunkManeger chunkManeger;
    [SerializeField] private ChunkController chunkStarter;
    [SerializeField] internal CageManager[] cage;

    private List<int> skinsData = new List<int>() { 1, 2, 3, 4, 5 ,6};
    public int lastSkin
    {
        get
        {
            return PlayerPrefs.GetInt("_lastSkin", 0);
        }
        set
        {
            PlayerPrefs.SetInt("_lastSkin", value);
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public void Start()
    {
        Init();
    }
    [ContextMenu("StartGame")]
    public void StartGame()
    {
        chunkManeger.Init();
        chunkStarter.Show(new Vector3(-50,0,0));
     

    }

    public void Init()
    {
        skinsData = new List<int>();
        skinsData = new List<int>() { 1, 2, 4, 6,6};

;        var randomSelectStarter = Random.Range(0,4);
        while(lastSkin == randomSelectStarter)
        {
            randomSelectStarter = Random.Range(0, 3);
        }
        lastSkin = randomSelectStarter;
        PlayerController._instanse._starterSkin = skinsData[randomSelectStarter];
        print("Player " + skinsData[randomSelectStarter]);



        skinsData.RemoveAt(randomSelectStarter);


        PlayerController._instanse.ResetSkin();
        for (int x = 0; x < PlayerController._instanse._skinOther.Length; x++)
        {
            PlayerController._instanse._skinOther[x].gameObject.SetActive(true);
        }
        for (int i = 0; i < cage.Length; i++)
        {
            var rand = Random.Range(0, skinsData.Count -1);
            cage[i].Init(skinsData[rand]);
           PlayerController._instanse._skinOther[i].Skin(skinsData[rand]);
              print("cage " + i  + "         " + skinsData[rand]);
            skinsData.RemoveAt(rand);

        }
        for (int y = 0; y < PlayerController._instanse._skinOther.Length; y++)
        {
            PlayerController._instanse._skinOther[y].gameObject.SetActive(false);
        }
    }


   //public void StaetGame()
   //{
   //    chunkManeger.Init();
   //    chunkStarter.enabled = false;
   //}

}
