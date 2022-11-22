using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public GameObject CircleAccuracy, CircleBody, CircleBackGround;
    [HideInInspector]
    public SpriteRenderer approachcircle, hitcircle, hitcircleoverlay;
    private GameObject CircleObject;
    private float targetScale = 0.85f;
    private Vector3 Accuracy, AccuracyChange;
    bool ScaleAccuracy;
    
    private void Awake() {
        approachcircle = CircleAccuracy.GetComponent<SpriteRenderer>();
        hitcircle = CircleBody.GetComponent<SpriteRenderer>();
        hitcircleoverlay = CircleBackGround.GetComponent<SpriteRenderer>();
        AccuracyChange = new Vector3(0.5f, 0.5f, 0.5f);
    }

    // Function spawn object from GameHandler
    public void Spawn(GameObject Circle){
        CircleObject = Circle;
        this.enabled = true;
        ScaleAccuracy = true;
    }

    void Update()
    {
        // Animation object and delete;
        if(ScaleAccuracy){
            CircleAccuracy.transform.localScale -= AccuracyChange * Time.deltaTime;
            if(CircleAccuracy.transform.localScale.x < targetScale){
                ScaleAccuracy = false;
                Destroy(CircleObject);
            }
        }
    }
}
