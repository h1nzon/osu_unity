using System;
using System.Globalization;
using System.IO;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject Circle;
    public AudioClip HitSound;
    private string Path;
    private string Line;
    private string[] LineParams;
    private int CountLine = 1;
    private float timer;
    private float x, y, z = -9, delay;
    private bool isSpawn = true;
    AudioSource audio;
    

    Camera cam;


    void Start()
    {
        audio = GetComponent<AudioSource>();
        cam = Camera.main;
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

        if(Input.GetKeyDown("z") || Input.GetKeyDown("x") || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100)){
                Debug.Log(hit.transform.name);
                Debug.Log(hit.transform.GetComponent<Transform>().localScale.x);
                hit.transform.GetComponent<Transform>().position = new Vector3(-20f, 0f);
                audio.PlayOneShot(HitSound);
                // Destroy(hit.transform);
            }
        }
    }
}
