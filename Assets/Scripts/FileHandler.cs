using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileHandler : MonoBehaviour
{
    private string[,] map = { { "a", "b", "c" }, { "b", "b", "b" }, { "d", "c", "a" } };

    public string[,] FileTo2DArray()
    {
        return map;
    }
}
