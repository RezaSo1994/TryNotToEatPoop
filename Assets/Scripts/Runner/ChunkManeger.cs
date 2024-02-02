using System.Collections.Generic;
using UnityEngine;

public class ChunkManeger : MonoBehaviour
{
    private static ChunkManeger _instanse;
    public static ChunkManeger Instance { get => _instanse; }

    public List<ChunkController> _chunks = new List<ChunkController>();

    private ChunkController chunkNow;
    private ChunkController lastChunk = null;
    [SerializeField] internal float _speed = 5;
    [SerializeField] internal float _maxSpeed = 35;
    public float _speedChunk = 5;   

    [SerializeField] private int counter = 0;
    [SerializeField] internal Transform bornPos, nextBornPos;
    [SerializeField] internal Transform killPos;

    private float lasttime;

    internal bool _isActiveAll = false;
    protected void Awake()
    {
        _instanse = this;

    }
    [ContextMenu("Init")]
    public void Init()
    {
        _isActiveAll = true;
        ResetAllChunk();
        counter = 0;
        lastChunk = null;
        Spawn();

    }

    public void Spawn()
    {
        if (_chunks.Count <= counter +3)
        {
            print("End");
            counter -= 5;
            chunkNow = _chunks[counter];
            chunkNow.Show((counter != 0) ? nextBornPos.position : bornPos.position);
            counter = ++counter;
            // counter = 0;
        }
        else if (_chunks.Count == counter - 2)
        {
            counter = ++counter;
            counter -= 5;
            print("End : " + counter);
        }
        else
        {
            chunkNow = _chunks[counter];
            chunkNow.Show((counter != 0) ? nextBornPos.position : bornPos.position);
            counter = ++counter;
        }
    }

    public void ResetAllChunk()
    {
        for (int j = 0; j < _chunks.Count; j++)
        {
            _chunks[j].Hide();
        }
    }

}
