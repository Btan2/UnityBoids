using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwarmController : MonoBehaviour {

    public SpawnBoidsScript activeBoids;
    public Vector3 newScale;
    public float spawnLimit;
    public float newSpeed;
    public bool draw;
    int i = 0;

    void Start(){
        newScale = new Vector3(0.4f, 0.4f, 0.4f);
        newSpeed = 4f;
        AdjustSpawnLimit(100);
    }
	void Update (){
        updateSwarmController();
    }
    public void AdjustSpawnLimit(float size){
        spawnLimit = size;
    }
    public void AdjustSize(float size){
        newScale = new Vector3(size, size, size);
    }
    public void AdjustSpeed(float speed){
        newSpeed = speed;
    }
    public void updateSwarmController(){
        i = 0;
        if (activeBoids.okFlag)
        {
            activeBoids.spawnLimit = spawnLimit;
            foreach (GameObject boid in activeBoids.boidArray)
            {
                if (i > activeBoids.boidArray.Count) //if indexer is larger than gameobject list then break
                {
                    break;
                }
                else
                {
                    activeBoids.boidArray[i].transform.localScale = newScale;
                    activeBoids.boidArray[i].GetComponent<BoidsAi>().boidSpeed = newSpeed;
                    //if (draw)
                    //{
                    //    activeBoids.boidArray[i].GetComponent<RayCaster>().drawRays = true;
                    //}
                    //else if (!draw)
                    //{
                    //    activeBoids.boidArray[i].GetComponent<RayCaster>().drawRays = false;
                    //}
                    i++;
                }
            }
        }
    }
}

