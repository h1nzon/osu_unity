using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public GameObject approachcircle, hitcircle, hitcircleoverlay;
    private GameObject CircleAccuracy, CircleBody, CircleBackGround;
    private SpriteRenderer AccuracyColor, BodyColor, BackGroundColor;
    private float targetScale = 0.85f;
    private Vector3 Accuracy, AccuracyChange;
    private bool ScaleCircle, fail;

    // Start is called before the first frame update
    void Start()
    {
        Accuracy = new Vector3(1f, 1f, 1f);
        AccuracyChange = new Vector3(0.1f * Time.deltaTime, 0.1f * Time.deltaTime, 0.1f * Time.deltaTime);
        Spawn(0f, 0f);
    }

    public void Spawn(float x, float y){
        CircleAccuracy = Instantiate(approachcircle, new Vector3(x, y, -0.1f), Quaternion.identity);
        CircleBody = Instantiate(hitcircle, new Vector3(x, y, -0.2f), Quaternion.identity);
        CircleBackGround = Instantiate(hitcircleoverlay, new Vector3(x, y, 0), Quaternion.identity);
        ScaleCircle = true;
    }

    void Update()
    {
        // if(CircleMain.transform.localScale == new Vector3(1f, 1f, 1f)){
        //     CircleMain.transform.localScale -= AccuracyChange;
        // }

        if(Input.GetKeyDown("z") || Input.GetKeyDown("x") || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)){

        }
        
        if(ScaleCircle){
            CircleAccuracy.transform.localScale -= AccuracyChange;
            if(CircleAccuracy.transform.localScale.x < targetScale){
                ScaleCircle = false;
                Destroy(CircleAccuracy);
                Destroy(CircleBody);
                Destroy(CircleBackGround);
            }
        }
    }
}
