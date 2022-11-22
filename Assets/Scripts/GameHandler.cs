using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject Circle, CircleObject;
    private bool spawnOneMore = true;
    void Start()
    {
        GameObject CircleObject = Instantiate(Circle, new Vector2(0, 0), Quaternion.identity);
        CircleObject.GetComponent<Circle>().Spawn(CircleObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnOneMore && Time.time > 2f){
            CircleObject = Instantiate(Circle, new Vector2(0.2f, 0), Quaternion.identity);
            CircleObject.GetComponent<Circle>().Spawn(CircleObject);
            spawnOneMore = false;
        }
    }
}
