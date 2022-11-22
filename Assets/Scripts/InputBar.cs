using System;
using UnityEngine;

public class InputBar : MonoBehaviour
{
    [HideInInspector]
    public GameObject zFirst, zSecond, zThird, xFirst, xSecond, xThird;
    [HideInInspector]
    public Sprite[] numbers;
    private SpriteRenderer ZfirstNumberSprite, ZsecondNumberSprite, 
    ZthirdNumberSprite, XfirstNumberSprite, XsecondNumberSprite, XthirdNumberSprite;
    private int zCount, xCount;
    private int digitCountZ, digitCountX;
    void Start()
    {
        ZfirstNumberSprite = zFirst.GetComponent<SpriteRenderer>();
        ZsecondNumberSprite = zSecond.GetComponent<SpriteRenderer>();
        ZthirdNumberSprite = zThird.GetComponent<SpriteRenderer>();
        XfirstNumberSprite = xFirst.GetComponent<SpriteRenderer>();
        XsecondNumberSprite = xSecond.GetComponent<SpriteRenderer>();
        XthirdNumberSprite = xThird.GetComponent<SpriteRenderer>();
    }

    void SpriteRenderer(){
        // Rendering sprites in objects of 0 to 9
        if(zCount > 0 && 10 > zCount){
            ZfirstNumberSprite.sprite = numbers[zCount];
        }
        // of 10 to 99
        if(zCount > 9 && 100 > zCount){
            zFirst.transform.localPosition= new Vector2(-0.1f, 0.4f);
            int firstNumber = zCount / 10;
            int secondNumber = zCount % 10;
            ZfirstNumberSprite.sprite = numbers[firstNumber];
            ZsecondNumberSprite.sprite = numbers[secondNumber];
        }
        // of 100 to 999
        if(zCount > 99){
            zFirst.transform.localPosition = new Vector2(-0.17f, 0.4f);
            zSecond.transform.localPosition = new Vector2(0.04f, 0.4f);
            int firstNumber = zCount / 100;
            int secondNumber = zCount / 10 % 10;
            int thirdNumber = zCount % 10;
            ZfirstNumberSprite.sprite = numbers[firstNumber];
            ZsecondNumberSprite.sprite = numbers[secondNumber];
            ZthirdNumberSprite.sprite = numbers[thirdNumber];
        }

        if(xCount > 0 && 10 > xCount){
            XfirstNumberSprite.sprite = numbers[xCount];
        }
        if(xCount > 9 && 100 > xCount){
            xFirst.transform.localPosition= new Vector2(-0.1f, -0.5f);
            int firstNumber = xCount / 10;
            int secondNumber = xCount % 10;
            XfirstNumberSprite.sprite = numbers[firstNumber];
            XsecondNumberSprite.sprite = numbers[secondNumber];
        }
        if(xCount > 99){
            xFirst.transform.localPosition = new Vector2(-0.17f, -0.5f);
            xSecond.transform.localPosition = new Vector2(0.04f, -0.5f);
            int firstNumber = xCount / 100;
            int secondNumber = xCount / 10 % 10;
            int thirdNumber = xCount % 10;
            XfirstNumberSprite.sprite = numbers[firstNumber];
            XsecondNumberSprite.sprite = numbers[secondNumber];
            XthirdNumberSprite.sprite = numbers[thirdNumber];
        }
    }

    void Update()
    {
        // Check input and get count numbers in variable
        if (Input.GetKeyDown("z") || Input.GetMouseButtonDown(0)){
            zCount++;
            digitCountZ = (int)Math.Log10(zCount) + 1;
            SpriteRenderer();
        }
        if (Input.GetKeyDown("x") || Input.GetMouseButtonDown(1)){
            xCount++;
            digitCountX = (int)Math.Log10(xCount) + 1;
            SpriteRenderer();
        }
    }
}
