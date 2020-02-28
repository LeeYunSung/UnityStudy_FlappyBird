using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour{
    [SerializeField] private int columnPoolSize = 5;
    [SerializeField] private Column columnPrefabs;

    private const float COLUMN_MIN = -2f;
    private const float COLUMN_MAX = 2f;
    const float XPOSITION = 20f;
 
    public Queue<Column> pooledObjects;
    
    void Start(){
        pooledObjects = new Queue<Column>();
        float spawnXPosition = 0;
        for (int i = 0; i < columnPoolSize; i++){
            Column pooledObject = SpawnColumn();
            spawnXPosition += 5f;
            pooledObject.transform.position = new Vector2(spawnXPosition, pooledObject.transform.position.y);
        }
    }
    public Column SpawnColumn(){
        Column pooledObject;
        pooledObject = (pooledObjects.Count > 0) ? pooledObjects.Dequeue() : MakeColumn();

        float spawnYPosition = UnityEngine.Random.Range(COLUMN_MIN, COLUMN_MAX);
        pooledObject.transform.position = new Vector2(XPOSITION, spawnYPosition);

        return pooledObject;
    }
    public void Despawn(Column column){
        pooledObjects.Enqueue(column);
    }
    public Column MakeColumn(){
        return Instantiate(columnPrefabs);
    }
}