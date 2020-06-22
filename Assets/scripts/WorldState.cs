using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    public List<GameObject> worldState = new List<GameObject>();

    public List<RollerAgent> agentlist = new List<RollerAgent>();

    private void Start() {
        //agentCount = 0;
    }

    public bool assign(RollerAgent agent){
        //print("Agente counter :"+ agentCount.ToString());
            //print("agent_assigned");
        agentlist.Add(agent);
        return true;

    }

    public void Eat(){
        this.GetComponent<FoodGenerator>().NewFood();
        
    }


    public void requestClear(){
        this.GetComponent<FoodGenerator>().CleanCreate();
    }

}
