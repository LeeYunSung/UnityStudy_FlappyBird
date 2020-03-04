using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public static Bird Instance { private set; get; }

    private const float UPFORCE = 150f;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer render;
    private new Collider2D collider;

    private const string flapTriggerName = "Flap";
    private const string hurtTriggerName = "Hurt";
    private const string dieTriggerName = "Die";



    void Awake(){
        if (Instance == null){
            Instance = this;
        }
        render = GetComponent<SpriteRenderer>();
    }
    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

    }
    public void JumpBird(){
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(new Vector2(0, UPFORCE));
        anim.SetTrigger(flapTriggerName);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        LifePointControl.Instance.deleteLifePoint();
        if (GameControl.Instance.gameOver == true){
            anim.SetTrigger(dieTriggerName);
            return;
        }
        anim.SetTrigger(hurtTriggerName);
        StartCoroutine(FlickeringBird());
    }
    IEnumerator FlickeringBird(){
        collider.isTrigger = true;
        for (int i = 0; i < 4; i++)
        {
            render.color = new Color(1f, 1f, 1f, .5f);
            yield return new WaitForSeconds(.5f);
            render.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(.5f);
        }
        collider.isTrigger = false;
    }
}