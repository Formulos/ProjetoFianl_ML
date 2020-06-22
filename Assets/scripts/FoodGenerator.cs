using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FoodGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject food_prefab;
    public GameObject spawnZone;
    public int foodQuantity;

    private GameObject[] gameObjects;
    void Start()
    {
        //print("gerando comida");
        float xmin = spawnZone.transform.position.x - spawnZone.transform.lossyScale.x*4;
        float xmax = spawnZone.transform.position.x + spawnZone.transform.lossyScale.x*4;

        float zmin = spawnZone.transform.position.z - spawnZone.transform.lossyScale.z*4;
        float zmax = spawnZone.transform.position.z + spawnZone.transform.lossyScale.z*4;

        
        for(int i =0;i<foodQuantity;i++){
            Vector3 pos = new Vector3(Random.Range(xmin, xmax), 0.5f, Random.Range(zmin, zmax));
            Instantiate(food_prefab,pos,Quaternion.identity,this.gameObject.transform);
        }
    }
    


    public void CleanCreate(){
        float xmin = spawnZone.transform.position.x - spawnZone.transform.lossyScale.x*4;
        float xmax = spawnZone.transform.position.x + spawnZone.transform.lossyScale.x*4;

        float zmin = spawnZone.transform.position.z - spawnZone.transform.lossyScale.z*4;
        float zmax = spawnZone.transform.position.z + spawnZone.transform.lossyScale.z*4;

    
        for (int i = 0; i < this.transform.childCount; i++)
            {
                Transform child = this.transform.GetChild(i);
                if (child.tag == "food")
                {
                    Destroy(child.gameObject);
                }
            }
                 
            
        for(int i =0;i<foodQuantity;i++){
            Vector3 pos = new Vector3(Random.Range(xmin, xmax), 0.5f, Random.Range(zmin, zmax));
            Instantiate(food_prefab,pos,Quaternion.identity,this.gameObject.transform);
        }
    }

        
    

        

    public void NewFood(){
        float xmin = spawnZone.transform.position.x - spawnZone.transform.lossyScale.x*4;
        float xmax = spawnZone.transform.position.x + spawnZone.transform.lossyScale.x*4;

        float zmin = spawnZone.transform.position.z - spawnZone.transform.lossyScale.z*4;
        float zmax = spawnZone.transform.position.z + spawnZone.transform.lossyScale.z*4;
        Vector3 pos = new Vector3(Random.Range(xmin, xmax), 0.5f, Random.Range(zmin, zmax));

        GameObject tmp = Instantiate(food_prefab,pos,Quaternion.identity,this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
