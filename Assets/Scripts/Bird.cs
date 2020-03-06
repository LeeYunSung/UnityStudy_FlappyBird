using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    private const float UPFORCE = 110f;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer render;
    private new Collider2D collider;

    private const string flapTriggerName = "Flap";
    private const string hurtTriggerName = "Hurt";
    private const string dieTriggerName = "Die";

    public List<GameObject> birdLife;
    int lifeListIndex = 2;
    bool isInvincibility = false;

    void Start(){
        render = GetComponent<SpriteRenderer>();
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
        if (!isInvincibility && collision.gameObject.GetComponent<Column>() != null){
            birdLife[lifeListIndex].SetActive(false);
            if (lifeListIndex == 0){
                anim.SetTrigger(dieTriggerName);
                GameControl.Instance.BirdDied();
                return;
            }
            lifeListIndex--;
            anim.SetTrigger(hurtTriggerName);
            StartCoroutine(FlickeringBird(collision));
        }
    }
    IEnumerator FlickeringBird(Collision2D collision)
    {
        InvinibilityOn();
        //GameObject[] gameObjects;
        //gameObjects = collision.gameObject.GetComponentsInChildren<GameObject>();
        //foreach (GameObject column in gameObjects)
        //{
        //    column.transform.GetComponent<Collider2D>().enabled = false;
        //}
        for (int i = 0; i < 4; i++){
            render.color = new Color(1f, 1f, 1f, .5f);
            yield return new WaitForSecondsRealtime(0.5f);
            render.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSecondsRealtime(0.5f);
        }
        //foreach (GameObject column in gameObjects)
        //{
        //    column.transform.GetComponent<Collider2D>().enabled = true;
        //}
        InvinibilityOff();
    }
    public void InvinibilityOn(){
        collider.isTrigger = true;
        isInvincibility = true;
    }
    public void InvinibilityOff(){
        collider.isTrigger = false;
        isInvincibility = false;
    }
}