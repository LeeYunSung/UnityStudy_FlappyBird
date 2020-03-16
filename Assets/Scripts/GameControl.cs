using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour { 
    public static GameControl Instance { private set; get; }

    [SerializeField] private Text startText;
    [SerializeField] private Text pauseText;

    [SerializeField] private Bird bird;
    [SerializeField] private Booster booster;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

    [SerializeField] private GameObject gameOverView;

    public bool gameOver;
    public bool isPaused;

    private int score = 0;
    private const int TIME = 3;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
        StartCoroutine(PrintStartText(1f));
        highScoreText.text = "";
    }
    public void BirdScored() {
        score += 10;
        scoreText.text = "Score: " + score.ToString();
    }
    public void BirdDied() {
        SaveScore();
        pauseText.gameObject.SetActive(false);
        gameOverView.SetActive(true);
        gameOver = true;
        ScrollingObject.GameOver();
        booster.StopBoost();
    }
    IEnumerator PrintStartText(float waitTime){
        Time.timeScale = 0;
        isPaused = true;
        int time = TIME;
        while (time>0){
            startText.text = time.ToString();
            yield return new WaitForSecondsRealtime(waitTime);
            time--;
        }
        startText.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(true);
        Time.timeScale = 1;
        isPaused = false;
        gameOver = false;
    }
    public void BackgroundClick(){
        if (isPaused == false && gameOver == false){
            bird.JumpBird();
        }
    }
    public void RestartClick(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PauseClick(){
        isPaused = !isPaused;
        Time.timeScale = (isPaused) ? 0f : 1f;
        pauseText.text = (!isPaused) ? "II" : "►";
        if (!isPaused){
            startText.gameObject.SetActive(true);
            pauseText.gameObject.SetActive(false);
            StartCoroutine(PrintStartText(1f));
        }
    }
    public void SaveScore(){
        if (!PlayerPrefs.HasKey("Score")){
            highScoreText.text = score.ToString();
            PlayerPrefs.SetString("Score", score.ToString());
            PlayerPrefs.Save();
            return;
        }
        string scoreString = PlayerPrefs.GetString("Score") + '\n' + score.ToString();
        PlayerPrefs.SetString("Score", scoreString);
        PlayerPrefs.Save();

        string[] scores = PlayerPrefs.GetString("Score").Split('\n');
        int[] highScore = new int[scores.Length];
        for (int i = 0; i < highScore.Length; i++){
            highScore[i] = int.Parse(scores[i]);
        }
        for (int i = 0; i < scores.Length - 1; i++){
            for (int j = i + 1; j < scores.Length; j++){
                if (highScore[i] < highScore[j]){
                    int temp = highScore[i];
                    highScore[i] = highScore[j];
                    highScore[j] = temp;
                }
            }
        }
        for (int i = 0; i < scores.Length; i++) {
            if (i > 4) {
                break;
            }
            highScoreText.text += highScore[i] + "\n";
        }
    }
}