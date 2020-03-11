using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private const float UPFORCE = 110f;
    private Rigidbody2D rb2d;
    private Animator anim;

    private const string flapTriggerName = "Flap";
    private const string hurtTriggerName = "Hurt";
    private const string dieTriggerName = "Die";
    private const string boostTriggerName = "Booster";

    public List<GameObject> birdLife;
    int lifeListIndex = 2;

    private float superTime = 0f;
    public static bool isBoost = false;

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update(){
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (transform.position.x < 0){
            transform.position = new Vector2(0, transform.position.y);
        }
        else if(pos.y > 1f){
            pos.y = 1f;
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
    }
    public void JumpBird(){
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(new Vector2(0, UPFORCE));
        anim.SetTrigger(flapTriggerName);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if (superTime == 0 && collision.transform.GetComponent<Collider2D>() != null){
            birdLife[lifeListIndex].SetActive(false);
            if (lifeListIndex == 0){
                anim.SetTrigger(dieTriggerName);
                GameControl.Instance.BirdDied();
                return;
            }
            anim.SetTrigger(hurtTriggerName);
            SuperTimeAdd(2f);
            lifeListIndex--;
        }
    }
    IEnumerator SuperMode() {
        if (isBoost){
            anim.SetTrigger(boostTriggerName);
        }
        while (superTime > 0){
            Column.TriggerOn();
            yield return new WaitForSecondsRealtime(1f);
            superTime -= 1;
        }
        Column.TriggerOff();
    }
    public void SuperTimeAdd(float time){
        superTime = time;
        StartCoroutine(SuperMode());
    }
}