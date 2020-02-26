using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Subject 주체
public class GameControl : MonoBehaviour
{
    public static GameControl Instance { private set; get; }
    public GameObject gameOverText;
    public Text scoreText;
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;

    private int score = 0;
    [SerializeField] private Bird bird;

    List<ScrollingObject> scrollingObjectList = new List<ScrollingObject>();
    public void AddScrollingObject(ScrollingObject scrollingObject){
        if (!scrollingObjectList.Contains(scrollingObject)){
            scrollingObjectList.Add(scrollingObject);
        }
    }
    public void RemoveScrollingObject(ScrollingObject scrollingObject){
        if (scrollingObjectList.Contains(scrollingObject)){
            scrollingObjectList.Remove(scrollingObject);
        }
    }
    public void NotifiyScrollingObjectList(){
        foreach(ScrollingObject scrollingObject in scrollingObjectList){
            scrollingObject.GameOver();
        }
    }
    void Awake(){
        if (Instance == null){
            Instance = this;
        }
        else if (Instance != this){
            Destroy(gameObject);
        }
    }
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            if (gameOver == true){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            bird.JumpBird();
        }
    }
    public void BirdScored() {
        if (gameOver){
            return;
        }
        score += 10;
        scoreText.text = "Score: " + score.ToString();
    }
    public void BirdDied(){
        gameOverText.SetActive(true);
        gameOver = true;
        NotifiyScrollingObjectList();
    }
}