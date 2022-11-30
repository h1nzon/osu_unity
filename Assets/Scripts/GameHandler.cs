using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class GameHandler : MonoBehaviour
{
    // Circle
    public GameObject CirclePrefab;
    private List<GameObject> CircleList = new List<GameObject>();
    private int CountCircle = 0;
    public int NeedCircle = 0;
    bool StartGame = false;
    private bool NewCombo;
    private int Combo = 1;

    //Number assets
    public List<Sprite> Numbers;

    // Audio
    public AudioClip HitSound, MapSound;
    public AudioSource audio;
    private bool audioPlay = false;

    // Map reader
    private string path;
    private string Line;
    private string[] LineParams;
    private int CountLine = 1;

    // Spawn objects
    private float timer;
    private float x, y, z = 0, delay;
    private bool isSpawn = true;
    Camera cam;

    private void Awake() {
        cam = Camera.main;
        audio.clip = MapSound;
    }

    // Request for Windows and WebGl
    IEnumerator GetRequest (string file_name) {
    var uri = string.Concat (Application.streamingAssetsPath + "/", file_name);
    Debug.Log(Application.streamingAssetsPath);
    using (var webRequest = UnityWebRequest.Get (uri)) {
            yield return webRequest.SendWebRequest ();
            // Windows request
            if (webRequest.isNetworkError) {
                Debug.LogError (webRequest.error);
                Debug.Log("Selection: Default Windows path.");
                path = Application.dataPath + "/StreamingAssets/1.osu";
            }
            else {
                // WebGL request
                Directory.CreateDirectory (Application.streamingAssetsPath);
                var savePath = Path.Combine (Application.streamingAssetsPath, file_name);
                Debug.Log("Selection: WebGL path.");
                path = savePath;
                Debug.Log(path);
                File.WriteAllText (savePath, webRequest.downloadHandler.text);
            }
            StartGame = true;

            ReadLine(path);
        }
    }

    void Start()
    {
        StartCoroutine(GetRequest ("1.osu"));
    }

    // Read one string and spawn object
    void ReadLine(string path){
        StreamReader sr = new StreamReader(path);
        sr = new StreamReader(path);
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
        NewCombo = LineParams[3] == "2";
        x = (float)Convert.ToDecimal(LineParams[0], CultureInfo.GetCultureInfo("en-US")) / 80;
        y = (float)Convert.ToDecimal(LineParams[1], CultureInfo.GetCultureInfo("en-US")) / 80;
        delay = (float)Convert.ToDecimal(LineParams[2], CultureInfo.GetCultureInfo("en-US"));
        isSpawn = false;
        if(!audioPlay){
            audio.Play();
        }
        audioPlay = true;
    }

    void Update()
    {
        if(StartGame){
            timer = audio.time * 1000;
           // Spawn objects at the right time
            if(!isSpawn){
                if(audio.time * 1000 > delay){
                    CircleList.Add(Instantiate(CirclePrefab, new Vector3(x - 3f, y - 2.5f, z), Quaternion.identity));
                    CircleList[CountCircle].name = "Circle_" + CountCircle;
                    z += 0.1f;
                    CircleList[CountCircle].GetComponent<Circle>().Spawn(CircleList[CountCircle]);
                    SpriteRenderer circlecombo = CircleList[CountCircle].GetComponent<Circle>().CircleCombo.GetComponent<SpriteRenderer>();
                    if(NewCombo){
                        Combo = 1;
                    }
                    circlecombo.sprite = Numbers[Combo];
                    Combo++;
                    CountCircle++;
                    isSpawn = true;
                    ReadLine(path);
                }
            }
            // Click event clicking on the circle
            if(Input.GetKeyDown("z") || Input.GetKeyDown("x") || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)){
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, 100))
                {
                    if(hit.transform.name == "Circle_" + NeedCircle){
                        Destroy(CircleList[NeedCircle]);
                        audio.PlayOneShot(HitSound, 1f);
                        NeedCircle++;
                    }
                }
            } 
        }
    }
}
