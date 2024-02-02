using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{

    public GameObject chunk;
    public bool isActive;

    private Transform tr;
    public float moveSpeed = 1.5f;
    public bool IsActive { get => isActive; }
    public List<GameObject> block = new List<GameObject>();

    public void Awake()
    {
        tr = transform;
        var blo = GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < blo.Length; i++)
        {
            if(blo[i].tag == "block")
                block.Add(blo[i].gameObject);
        }
        // var child = 
    }

    public void Update()
    {
        if (ChunkManeger.Instance._isActiveAll)
        {
            if (!isActive)
                return;

            tr.position -= Vector3.right * ChunkManeger.Instance._speed * Time.deltaTime;
            if (tr.position.x < ChunkManeger.Instance.killPos.position.x)
            {
                Hide();
                ChunkManeger.Instance.Spawn();
            }
        }
    }

    public void Show(Vector3 pos)
    {
        if (tr != null)
            tr.position = pos;

        isActive = true;
        chunk.SetActive(true);
    }
    public void Hide()
    {
        isActive = false;
        chunk.SetActive(false);
        for (int i = 0; i < block.Count; i++)
        {
            block[i].gameObject.SetActive(true);
        }
    }








}
