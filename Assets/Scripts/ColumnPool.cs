using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour{
    [SerializeField] private Column columnPrefabs;
    [SerializeField] private int columnPoolSize = 5;
    [SerializeField] private GameObject columns;

    private const float COLUMN_MIN = -2f;
    private const float COLUMN_MAX = 2f;
    float xPosition;
 
    public Queue<Column> pooledObjects;//쓰고 반납한 Column들

    void Start(){
        pooledObjects = new Queue<Column>();
        float spawnXPosition = 0;
        for (int i = 0; i < columnPoolSize; i++){
            Column pooledObject = SpawnColumn(spawnXPosition);
            pooledObject.transform.position = new Vector2(spawnXPosition, pooledObject.transform.position.y);
            spawnXPosition += 5f;
        }
        xPosition = spawnXPosition - 5f;
    }
    public Column SpawnColumn(float x){
        Column pooledObject;
        pooledObject = (pooledObjects.Count > 0) ? pooledObjects.Dequeue() : MakeColumn();
        float spawnYPosition = UnityEngine.Random.Range(COLUMN_MIN, COLUMN_MAX);
        xPosition = x + 5f;
        pooledObject.transform.position = new Vector2(xPosition, spawnYPosition);
        pooledObject.Init();

        return pooledObject;
    }
    public void Despawn(Column column){
        pooledObjects.Enqueue(column);
    }
    public Column MakeColumn(){
        Column column = Instantiate(columnPrefabs);
        column.transform.parent = columns.transform;
        return column;
    }
}