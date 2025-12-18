using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_score : MonoBehaviour
{
    public static int memory = 0;
    public static int chaos = 0;

    public int set_memory = 0;
    public int set_chaos = 0;

    // // Update is called once per frame
    void Start()
    {
        memory = set_memory;
        chaos = set_chaos;
    }
}
