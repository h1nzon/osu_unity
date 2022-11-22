using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawn : MonoBehaviour
{
    Circle circle;
    // Start is called before the first frame update
    void Start()
    {
        circle.Spawn(0f, 0f);
        circle.Spawn(50f, 50f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
