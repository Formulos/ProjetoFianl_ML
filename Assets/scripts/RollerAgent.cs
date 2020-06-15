using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine.SceneManagement;

public class RollerAgent : Agent
{
    public WorldState world;
    public List<GameObject> worldState = new List<GameObject>();
    Rigidbody rBody;
    int hunger;
    private int foodQuantity;

    //float starving = 0;
    void Start () 
    {
        world = this.transform.parent.gameObject.GetComponent<WorldState>();
        foodQuantity = this.transform.parent.gameObject.GetComponent<FoodGenerator>().foodQuantity;
        print(foodQuantity);
        world.assign(this);
        //print("assigning agent");
        //world = this.transform.parent.gameObject.GetComponent<WorldState>();
        //world.assign(this);

    }

    //public Transform Target;
    public override void OnEpisodeBegin()
    {        
        

        hunger = 5;
        rBody = this.GetComponent<Rigidbody>();
        //starving = 0;
        //world.updateState(0);
        //if (this.transform.localPosition.y < 0)
        //{
        world.requestClear();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // If the Agent fell, zero its momentum
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;

        float xmin = this.transform.parent.position.x - this.transform.parent.transform.lossyScale.x*3;
        float xmax = this.transform.parent.position.x + this.transform.parent.lossyScale.x*3;

        float zmin = this.transform.parent.position.z - this.transform.parent.lossyScale.z*3;
        float zmax = this.transform.parent.position.z + this.transform.parent.lossyScale.z*3;

        Vector3 pos = new Vector3(Random.Range(xmin, xmax), 0.5f, Random.Range(zmin, zmax));

        this.transform.position = pos;
        //}
        world.requestState();

        // Move the target to a new spot
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //print("STATUS");
        //rBody = this.GetComponent<Rigidbody>();
        // Target and Agent positions
        //sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        //print("position: " + (this.transform.position).ToString());

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
        //print("x_velocity: " + (rBody.velocity.x).ToString());
        //print("z_velocity: " + (rBody.velocity.z).ToString());

        //sensor.AddObservation(this.starving);

        //Vector2 rpos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.z);
        for(int i=0;i<foodQuantity;i++){
            //print(worldState[10].transform.localPosition);
            Vector3 foodPosition = worldState[i].transform.localPosition;
            sensor.AddObservation(foodPosition);
            //print("Food_position: " + (foodPosition).ToString());
            //sensor.AddObservation(Vector2.Distance(rpos,foodPosition));
            //print("dists: "+ (Vector2.Distance(rpos,foodPosition)).ToString());
/*             for(int j=0;j<2;j++){
                //sensor.AddObservation(worldState[i,j]);
                if(worldState[i,2]==0){
                    print("i: "+i.ToString());
                    print("xpos: "+worldState[i,0].ToString());
                    print("zpos: "+worldState[i,1].ToString());
                }
                
            } */
        } 
    }

    public float speed = 10;
    public override void OnActionReceived(float[] action)
        {
            
            Vector3 controlSignal = Vector3.zero;
            controlSignal.x = action[0];
            controlSignal.z = action[1];
            rBody.AddForce(controlSignal * speed);
            if(this.transform.localPosition.y < -0.1f){
                AddReward(-1f);
                EndEpisode();
            }
        //penalidade de morrer de fome
        AddReward(-0.0005f);
        //print(starving);
        //print("penalt"+ (-0.0005f*starving).ToString());
        //starving +=0.5f;

        }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("food"))
        {
            int id = collision.gameObject.GetComponent<Food>().id;
            //print("debug pre pos: "+(other.transform.position).ToString());
            //print("debug pre pos: "+(worldState[id].transform.localPosition).ToString());
            world.updateState(id);
            Destroy(collision.gameObject);
            AddReward(1f);
            //starving = 0;
            hunger--;
            if(hunger == 0){
                //SetReward(2f);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                EndEpisode();
            }
            //print("debug post pos: "+(worldState[id].transform.localPosition).ToString());
        }

    }
    
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }
}