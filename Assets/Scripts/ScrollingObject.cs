using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public static List<ScrollingObject> scrollingObjectList = new List<ScrollingObject>();
    public const float SCROLLINGSPEED = -1.5f;
    public const float WEIGHT = 5;
    private static float speed = SCROLLINGSPEED;

    void Start(){
        scrollingObjectList.Add(this);
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(speed, 0);
    }
    public static void GameOver(){
        foreach (ScrollingObject scrolling in scrollingObjectList){          
            scrolling.rb2d.velocity = Vector2.zero;
        }
    }
    public static void SpeedUp() {
        speed = SCROLLINGSPEED * WEIGHT;
        ChangeVelocity();
    }
    public static void SpeedDown(){
        speed = SCROLLINGSPEED;
        ChangeVelocity();
    }
    private static void ChangeVelocity(){
        foreach (ScrollingObject scrolling in scrollingObjectList) {
            scrolling.rb2d.velocity = new Vector2(speed, 0);
        }
    }
    private void OnDestroy(){
        scrollingObjectList.Remove(this);
    }
}