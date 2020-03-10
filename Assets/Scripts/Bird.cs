using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private const float UPFORCE = 110f;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer render;

    private const string flapTriggerName = "Flap";
    private const string hurtTriggerName = "Hurt";
    private const string dieTriggerName = "Die";

    public List<GameObject> birdLife;
    int lifeListIndex = 2;

    private bool isInvincibility = false;

    void Start(){
        render = GetComponent<SpriteRenderer>();
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
        if (!isInvincibility && collision.transform.GetComponent<Collider2D>() != null){
            birdLife[lifeListIndex].SetActive(false);
            if (lifeListIndex == 0){
                anim.SetTrigger(dieTriggerName);
                GameControl.Instance.BirdDied();
                return;
            }
            anim.SetTrigger(hurtTriggerName);
            lifeListIndex--;
            StartCoroutine(FlickeringBird());
        }
    }
    IEnumerator FlickeringBird() {
        SuperBirdOn();
        for (int i = 0; i < 4; i++){
            render.color = new Color(1f, 1f, 1f, .5f);
            yield return new WaitForSecondsRealtime(0.5f);
            render.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSecondsRealtime(0.5f);
        }
        SuperBirdOff();
    }
    public void SuperBirdOn(){
        isInvincibility = true;
        Column.TriggerOn();
    }
    public void SuperBirdOff(){
        Column.TriggerOff();
        isInvincibility = false;
    }
}