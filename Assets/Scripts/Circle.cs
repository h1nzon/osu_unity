using UnityEngine;

public class Circle : MonoBehaviour
{
    public GameObject CircleAccuracy, CircleBody, CircleBackGround, CircleCombo;
    [HideInInspector]
    public SpriteRenderer approachcircle, hitcircle, hitcircleoverlay, circlecombo;
    private GameObject CircleObject, GameControl;
    private float targetScale = 1f;
    private Vector3 AccuracyChange;
    private Color AccuracyColor, BodyColor, BgColor;
    bool ScaleAccuracy;
    public GameHandler gameHandler;

    private void Awake() {
        approachcircle = CircleAccuracy.GetComponent<SpriteRenderer>();
        hitcircle = CircleBody.GetComponent<SpriteRenderer>();
        hitcircleoverlay = CircleBackGround.GetComponent<SpriteRenderer>();
        circlecombo = CircleCombo.GetComponent<SpriteRenderer>();
        AccuracyChange = new Vector3(1f, 1f, 1f);
        GameControl = GameObject.Find("GameControl");
        gameHandler = GameControl.GetComponent<GameHandler>();
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
                gameHandler.NeedCircle++;
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
