using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.GetComponent<Bird>() != null){
            GameControl.Instance.BirdScored();
            StartCoroutine(DoPooling());
        }
    }
    IEnumerator DoPooling(){
        yield return new WaitForSeconds(10f);
        if (GameControl.Instance.gameOver != true){
            GameControl.Instance.GetComponent<ColumnPool>().Pooling();
        }
    }
}