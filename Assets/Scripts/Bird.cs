using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float upForce = 200f;
    private Rigidbody2D rb2d;
    private Animator anim;

    private const string flapTriggerName = "Flap";
    private const string dieTriggerName = "Die";

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void JumpBird(){
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(new Vector2(0, upForce));
        anim.SetTrigger(flapTriggerName);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        rb2d.velocity = Vector2.zero;
        anim.SetTrigger(dieTriggerName);
        GameControl.Instance.BirdDied();
    }
}