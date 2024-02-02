using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donate : MonoBehaviour
{

    public string[] _path;

    public void GoToLink(int index)
    {
        Application.OpenURL(_path[index]);
    }


}
