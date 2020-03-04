using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D rb2d;

    void Start(){
        GameControl.Instance.AddScrollingObject(this);

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(GameControl.Instance.scrollSpeed, 0);
    }
    //private void Update() {
    //    transform.Translate(GameControl.Instance.scrollSpeed *0.01f, 0, 0);
    //}
    public void GameOver(){
        rb2d.velocity = Vector2.zero;
    }
}