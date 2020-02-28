using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Bird>() != null){
            GameControl.Instance.BirdScored();
            StartCoroutine(Exchange());
        }
    }
    IEnumerator Exchange(){
        if (GameControl.Instance.gameOver != true){
            GameControl.Instance.GetComponent<ColumnPool>().SpawnColumn();
            yield return new WaitForSeconds(5f);
            GameControl.Instance.GetComponent<ColumnPool>().Despawn(this);
        }
    }
}