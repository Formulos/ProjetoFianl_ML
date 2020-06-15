using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    public List<GameObject> worldState = new List<GameObject>();

    private RollerAgent[] agentlist = new RollerAgent[32];
    public int agentCount;

    private void Start() {
        //agentCount = 0;
    }

    public bool assign(RollerAgent agent){
        //print("Agente counter :"+ agentCount.ToString());
        if(agentCount <32){
            print("agent_assigned");
            agentlist[agentCount] = agent;
            agentCount+= 1;
            return true;
        }
        print("Agent overflow");
        return false;

    }

    public void updateState(int id){
        GameObject new_food = this.GetComponent<FoodGenerator>().NewFood(id);
        worldState[id] = new_food;
        
        for(int i=0;i<agentCount;i++){
            agentlist[i].worldState = worldState;
        }
    }

    public void requestState(){        
        for(int i=0;i<agentCount;i++){
            //print(agentlist[i]);
            agentlist[i].worldState = worldState;
        }
    }

    public void requestClear(){
        this.GetComponent<FoodGenerator>().CleanCreate();

        for(int i=0;i<agentCount;i++){
            //print(agentlist[i]);
            agentlist[i].worldState = worldState;
        }
    }

}
