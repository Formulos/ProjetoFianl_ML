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
    public GameObject spawnZone;

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
        

        hunger = 10;
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

        float xmin = spawnZone.transform.position.x - spawnZone.transform.lossyScale.x*3;
        float xmax = spawnZone.transform.position.x + spawnZone.transform.lossyScale.x*3;

        float zmin = spawnZone.transform.position.z - spawnZone.transform.lossyScale.z*3;
        float zmax = spawnZone.transform.position.z + spawnZone.transform.lossyScale.z*3;

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
/*         for(int i=0;i<foodQuantity;i++){
            //print(worldState[10].transform.localPosition);
            Vector3 foodPosition = worldState[i].transform.localPosition;
            sensor.AddObservation(foodPosition.x);
            sensor.AddObservation(foodPosition.z);
            sensor.AddObservation(Vector3.Distance(foodPosition, this.transform.localPosition));

        }  */
    }

    public float speed = 2f;
    public override void OnActionReceived(float[] action)
        {
            
            //Vector3 controlSignal = Vector3.zero;
            //controlSignal.x = action[0];
            //controlSignal.z = action[1];
            //rBody.AddForce(controlSignal * speed);

            Vector3 dirToGo = Vector3.zero;
            //var rotateDir = Vector3.zero;

            //print("action 0 :" + action[0]);
            //print("action 1 :" + action[1]);

            int forwardAxis = 0;
            int rightAxis = 0;

            float threshold = 0.25f;

            if(action[0] > threshold){
                forwardAxis = 1;
            } else if(action[0] < -threshold){
                forwardAxis = -1;
            }

            if(action[1] > threshold){
                rightAxis = 1;
            } else if(action[1] < -threshold){
                rightAxis = -1;
            }   

            dirToGo.x = forwardAxis;
            dirToGo.z = rightAxis;

            rBody.AddForce(dirToGo * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            //transform.Rotate(rotateDir, Time.fixedDeltaTime * speed);



/*         if (rBody.velocity.sqrMagnitude > 150f) // slow it down
        {
            rBody.velocity *= 0.95f;
        } */
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
            Destroy(collision.gameObject);
            world.updateState(id);
            AddReward(1f);
            //starving = 0;
            hunger--;
            if(hunger == 0){
                SetReward(2f);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                EndEpisode();
            }
            //print("debug post pos: "+(worldState[id].transform.localPosition).ToString());
        }

    }
    
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0f;
        actionsOut[1] = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[0] = -1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            actionsOut[1] = 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            actionsOut[0] = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            actionsOut[1] = -1f;
        }
    }
}