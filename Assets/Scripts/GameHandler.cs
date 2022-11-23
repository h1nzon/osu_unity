using System;
using System.Globalization;
using System.IO;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject Circle;
    private string Path;
    private string Line;
    private string[] LineParams;
    private int CountLine = 1;
    private float timer;
    private float x, y, z = -9, delay;
    private bool isSpawn = true;
    void Start()
    {
        Path = Application.dataPath + "/StreamingAssets/1.txt";
        ReadLine();
    }

    void ReadLine(){
        StreamReader sr = new StreamReader(Path);
        sr = new StreamReader(Path);
        int i = 0;
        while(true)
        {
            if (sr.ReadLine() == "[HitObjects]")
            break;
        }
        while(i != CountLine){
            Line = sr.ReadLine();
            i++;
        }
        CountLine++;
        LineParams = Line.Split(",");
        x = (float)Convert.ToDecimal(LineParams[0], CultureInfo.GetCultureInfo("en-US")) / 80;
        y = (float)Convert.ToDecimal(LineParams[1], CultureInfo.GetCultureInfo("en-US")) / 80;
        delay = (float)Convert.ToDecimal(LineParams[2], CultureInfo.GetCultureInfo("en-US"));
        isSpawn = false;
    }

    // void SpawnCircle(float x, float y, float delay){
    //     while(delay > timer){
    //         GameObject CircleObject = Instantiate(Circle, new Vector2(x, y), Quaternion.identity);
    //         CircleObject.GetComponent<Circle>().Spawn(CircleObject);
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time * 1000;
        if(!isSpawn){
            if(timer > delay){
                GameObject CircleObject = Instantiate(Circle, new Vector3(x - 5f, y - 2.5f, z), Quaternion.identity);
                z += 3;
                CircleObject.GetComponent<Circle>().Spawn(CircleObject);
                isSpawn = true;
                ReadLine();
            }
        }
        Debug.Log(timer);
    }
}
