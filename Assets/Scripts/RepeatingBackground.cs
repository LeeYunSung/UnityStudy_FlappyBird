using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private float groundHorizontalLength;

    void Start(){
        BoxCollider2D groundCollider = GetComponent<BoxCollider2D>();
        groundHorizontalLength = groundCollider.size.x;
    }
    void Update(){
        if(transform.position.x <-groundHorizontalLength){
            RepositionBackground();
        }
    }
    private void RepositionBackground(){
        Vector2 groundOffset = new Vector2(groundHorizontalLength* 2f, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
