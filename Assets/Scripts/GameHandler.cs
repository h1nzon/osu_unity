using System;
using System.Globalization;
using System.IO;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Circle prefab
    public GameObject Circle;
    
    // Audio
    public AudioClip HitSound;
    public AudioSource audio;

    // Map reader
    private string Path;
    private string Line;
    private string[] LineParams;
    private int CountLine = 1;

    // Spawn objects
    private float timer;
    private float x, y, z = -9, delay;
    private bool isSpawn = true;
    Camera cam;


    private void Awake() {
        Path = Application.dataPath + "/StreamingAssets/1.txt";
        cam = Camera.main;
    }
    
    void Start()
    {
        ReadLine();
    }

    // Read one string and spawn object
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

    void Update()
    {
        // Spawn objects at the right time
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
        // Click event clicking on the circle
        if(Input.GetKeyDown("z") || Input.GetKeyDown("x") || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100)){
                Debug.Log(hit.transform.name);
                Debug.Log(hit.transform.GetComponent<Transform>().localScale.x);
                hit.transform.GetComponent<Transform>().position = new Vector3(-20f, 0f);
                audio.PlayOneShot(HitSound);
            }
        }
    }
}
