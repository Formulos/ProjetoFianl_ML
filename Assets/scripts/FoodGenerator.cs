using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject food_prefab;
    public WorldState world;
    public GameObject spawnZone;
    public int foodQuantity;

    void Awake()
    {
        print("iniciando cena");
        world = this.GetComponent<WorldState>();
        //print("gerando comida");
        float xmin = spawnZone.transform.position.x - spawnZone.transform.lossyScale.x*4;
        float xmax = spawnZone.transform.position.x + spawnZone.transform.lossyScale.x*4;

        float zmin = spawnZone.transform.position.z - spawnZone.transform.lossyScale.z*4;
        float zmax = spawnZone.transform.position.z + spawnZone.transform.lossyScale.z*4;

        
        for(int i =0;i<foodQuantity;i++){
            Vector3 pos = new Vector3(Random.Range(xmin, xmax), 0.5f, Random.Range(zmin, zmax));
            GameObject tmp = Instantiate(food_prefab,pos,Quaternion.identity,this.gameObject.transform);
            tmp.GetComponent<Food>().id = i;
            world.worldState.Add(tmp);
        }
    }
    
    public void CleanCreate(){
        float xmin = spawnZone.transform.position.x - spawnZone.transform.lossyScale.x*4;
        float xmax = spawnZone.transform.position.x + spawnZone.transform.lossyScale.x*4;

        float zmin = spawnZone.transform.position.z - spawnZone.transform.lossyScale.z*4;
        float zmax = spawnZone.transform.position.z + spawnZone.transform.lossyScale.z*4;

        for(int i =0;i<foodQuantity;i++){
            Destroy(world.worldState[i]);

        }
        world.worldState.Clear();
        for(int i =0;i<foodQuantity;i++){
            Vector3 pos = new Vector3(Random.Range(xmin, xmax), 0.5f, Random.Range(zmin, zmax));
            GameObject tmp = Instantiate(food_prefab,pos,Quaternion.identity,this.gameObject.transform);
            tmp.GetComponent<Food>().id = i;
            world.worldState.Add(tmp);
        }
    }

        
    
    
    
/*         Vector3 pos = this.transform.position + new Vector3(-20f,0.5f,20f);
        for(int i=0; i<8;i++){
            for(int j=0; j<8;j++){
                int id = j+i*8;
                Instantiate(food_prefab,pos,Quaternion.identity,this.gameObject.transform).GetComponent<Food>().id = id;
                world.worldState[id,0] = pos.x; //futuramente fazer posição ser relativa para varias instancias
                world.worldState[id,1] = pos.z;
                world.worldState[id,2] = 1f;
                pos.x+= 5f;            
            }
            pos.x = this.transform.position.x -20f;
            pos.z -= 5f;
        } */

        

    public GameObject NewFood(int id){
        float xmin = spawnZone.transform.position.x - spawnZone.transform.lossyScale.x*4;
        float xmax = spawnZone.transform.position.x + spawnZone.transform.lossyScale.x*4;

        float zmin = spawnZone.transform.position.z - spawnZone.transform.lossyScale.z*4;
        float zmax = spawnZone.transform.position.z + spawnZone.transform.lossyScale.z*4;
        Vector3 pos = new Vector3(Random.Range(xmin, xmax), 0.5f, Random.Range(zmin, zmax));

        GameObject tmp = Instantiate(food_prefab,pos,Quaternion.identity,this.gameObject.transform);
        tmp.GetComponent<Food>().id = id;
        return tmp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
