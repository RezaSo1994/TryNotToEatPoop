using UnityEngine;
using Spine.Unity;
public class BlockControler : MonoBehaviour
{
    public const string BLOCK = "block";

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag(BLOCK))
        {
                PlayerController._instanse.Rievive(collision.gameObject);
        }
    }


}
