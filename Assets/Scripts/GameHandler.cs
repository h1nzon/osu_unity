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
    [HideInInspector]
    public int NeedCircle;
    private List<GameObject> CircleList = new List<GameObject>();
    private int CountCircle = 0;
    private bool NewCombo;
    private int Combo = 1;
    bool StartGame = false;

    //Number assets
    [SerializeField]
    private List<Sprite> Numbers;

    // Audio
    [SerializeField]
    private AudioClip HitSound, MapSound;
    [SerializeField]
    private AudioSource audio;

    // Map reader
    private string path;
    private string Line;
    private string[] LineParams;
    private int CountLine = 1;

    // Spawn objects
    private float timer;
    private float x, y, z = 0, delay;
    Camera cam;

    private void Awake() {
        cam = Camera.main;
        audio.clip = MapSound;
    }

    // Request for Windows and WebGl
    IEnumerator GetRequest (string file_name) {
    var uri = string.Concat (Application.streamingAssetsPath + "/", file_name);
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
        Application.targetFrameRate = 0;
        QualitySettings.vSyncCount = 0;
        StartCoroutine(GetRequest ("1.osu"));
        audio.Play();
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
        if(Line != null){
            CountLine++;
            LineParams = Line.Split(",");
            NewCombo = LineParams[3] == "2";
            x = float.Parse(LineParams[0]) / 80;
            y = float.Parse(LineParams[1]) / 80;
            delay = float.Parse(LineParams[2]);
        }
        else
        {
            StartGame = false;
        }   
    }

    void Update()
    {
        if(StartGame){
            timer = audio.time * 1000;
           // Spawn objects at the right time
            if(audio.time * 1000 > delay){
                CircleList.Add(Instantiate(CirclePrefab, new Vector3(x - 3f, y - 2.5f, z), Quaternion.identity));
                CircleList[CountCircle].name = "Circle_" + CountCircle;
                CircleList[CountCircle].GetComponent<Circle>().Spawn(CircleList[CountCircle]);
                SpriteRenderer circlecombo = CircleList[CountCircle].GetComponent<Circle>().CircleCombo.GetComponent<SpriteRenderer>();
                if(NewCombo){
                    Combo = 1;
                }
                circlecombo.sprite = Numbers[Combo];
                z += 0.1f;
                Combo++;
                CountCircle++;
                ReadLine(path);
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
