using UnityEngine;

public class Circle : MonoBehaviour
{
    public GameObject CircleAccuracy, CircleBody, CircleBackGround;
    [HideInInspector]
    public SpriteRenderer approachcircle, hitcircle, hitcircleoverlay;
    private GameObject CircleObject;
    private float targetScale = 1f;
    private Vector3 AccuracyChange;
    private Color AccuracyColor, BodyColor, BgColor;
    bool ScaleAccuracy;

    Camera cam;

    private void Awake() {
        approachcircle = CircleAccuracy.GetComponent<SpriteRenderer>();
        hitcircle = CircleBody.GetComponent<SpriteRenderer>();
        hitcircleoverlay = CircleBackGround.GetComponent<SpriteRenderer>();
        AccuracyColor = approachcircle.color;
        BodyColor = hitcircle.color;
        BgColor = hitcircleoverlay.color;
        AccuracyChange = new Vector3(1f, 1f, 1f);
        cam = Camera.main;
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
                this.enabled = false;
            }
        }
        
        // else{
        //     if(AccuracyColor.a < 100){
        //         AccuracyColor.a += 20f * Time.deltaTime;
        //         BodyColor.a += 20f * Time.deltaTime;
        //         BgColor.a += 20f * Time.deltaTime;
        //     }
        //     else{
        //         Destroy(CircleObject);
        //         this.enabled = false;
        //     }
        // }
    }
}
