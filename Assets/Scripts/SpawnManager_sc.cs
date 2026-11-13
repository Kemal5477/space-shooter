using UnityEngine;
using System.Collections;

public class SpawnManager_sc : MonoBehaviour
{
   [SerializeField] GameObject enemyContainer;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject tripleShotBonusPrefab;
    [SerializeField] Vector2 xSpawnRange=new Vector2(-9.54f,9.54f);
    [SerializeField] Vector2 timeRange=new Vector2(3.0f,9.0f);
    [SerializeField] int bonusWaitTime=10;


    [SerializeField] float spawnY=7.35f;

     bool stopSpawning=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnBonusRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPlayerDeath(){
        stopSpawning=true;
    }
    IEnumerator SpawnEnemyRoutine(){
        while(stopSpawning==false){
            float randomX=Random.Range(xSpawnRange.x,xSpawnRange.y);
            Vector3 position=new Vector3(randomX,spawnY,0);
            GameObject newEnemy = Instantiate(enemyPrefab,position,Quaternion.identity);
            newEnemy.transform.parent=enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(timeRange.x,timeRange.y));
        }
    }
    IEnumerator SpawnBonusRoutine(){
      while(stopSpawning==false){
        float randomX=Random.Range(xSpawnRange.x,xSpawnRange.y);
            Vector3 position=new Vector3(randomX,spawnY,0);
            Instantiate(tripleShotBonusPrefab,position,Quaternion.identity);
        yield return new WaitForSeconds(bonusWaitTime);
    }

}
}
