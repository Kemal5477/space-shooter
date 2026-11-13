using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed=5.0f;
    Vector2 moveInput ;
    Vector2 xBounds=new(-9.23f,9.23f);
    Vector2 yBounds=new(-3.8f,-2.0f);

    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject tripleLaserPrefab;
    [SerializeField] Transform firePoint;

    [SerializeField] float fireRate = 0.15f; // İki ateş arasındaki bekleme süresi (saniye)
    float nextFireTime = 0f;

     [SerializeField] int currentLives=3;

     [SerializeField] bool isTripleShot=false;
      [SerializeField] int bonusDisableWaitTime=5;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(Vector3.right*moveSpeed*Time.deltaTime);
        Vector3 direction=new Vector3(moveInput.x,moveInput.y,0f);
        this.transform.Translate(direction * moveSpeed * Time.deltaTime,Space.World);
        
        ClampPosition();
    }

   public void OnMove(InputValue value){
    moveInput=value.Get<Vector2>();
    Debug.Log(moveInput);
   }

   void  ClampPosition(){
    Vector3 p=this.transform.position;
    p.x=Mathf.Clamp(p.x, xBounds.x,xBounds.y);
    p.y=Mathf.Clamp(p.y, yBounds.x,yBounds.y);
    this.transform.position=p;
   }

   public void OnFire()
{if (Time.time >= nextFireTime)
        {
            if(isTripleShot){
               Instantiate(tripleLaserPrefab, this.transform.position, Quaternion.identity);
            }
            else{
                Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

            }
            nextFireTime = Time.time + fireRate;//bir sonraki ateş edilebilir zaman
        }
       
}
public void TakeDamage(){
    currentLives--;

    if(currentLives == 0){
        SpawnManager_sc sm;
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager_sc>();
        if(sm==null){
            Debug.LogError("PlayerController::TakeDamage sm is NULL");
        }
        else
    {
        sm.OnPlayerDeath();
    }
        
        Destroy(this.gameObject);
    }
}
public void EnableTripleShot(){
    isTripleShot=true;
    StartCoroutine(DisableTripleShotRoutine());

    // bu durum geçici olmalı ve x saniye sonra iptal edilmeli
}
IEnumerator DisableTripleShotRoutine(){

    yield return new WaitForSeconds(bonusDisableWaitTime);
    isTripleShot=false;
}
}