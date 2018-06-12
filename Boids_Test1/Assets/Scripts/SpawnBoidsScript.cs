using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBoidsScript : MonoBehaviour{
    public Slider spawnSlider;
    public GameManager gameManager;
    public GameObject boids;
    public GameObject boidBait;
    public float spawnLimit;
    public List<GameObject> boidArray;
    public bool okFlag;
    Ray ray;
    RaycastHit hit;
    
    void Start(){
        boidArray = new List<GameObject>();    //create new gameobject list for boids to be stored in
        okFlag = false;     //set flag to false to prevent Swarm Controller from reading empty boid object list
        spawnLimit = spawnSlider.value;       //set starting spawn limit
    }
    void Update(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);      //gathers raycast position from the mouse position looking through the camera     
        if (Physics.Raycast(ray, out hit))        //if the raycast has hit something
        {
            if (gameManager.spawnBoids)        //if SpawnBoid button has been pressed and the flag is true
            {
                if (boidArray.Count < spawnSlider.value)      //if the gameobject list count is less than the spawn limit
                {
                    GameObject obj = Instantiate(boids, new Vector3(hit.point.x, hit.point.y, -2.0f), Quaternion.identity) as GameObject;    //instantiate a boid on raycast hitpoint coordinates (-2.0f accounts for camera's z distance)
                    boidArray.Add(obj);    //add instantiated boid to gameobject list
                    okFlag = true;      //okFlag enables gameobject list to be scanned in SwarmController class
                }
            }
            if (gameManager.boidMagnet)
            {
                boidBait.SetActive(true);
                boidBait.transform.position = new Vector3(hit.point.x, hit.point.y, -2.0f);
            }
            else
            {
                boidBait.SetActive(false);
            }
        }       
        if (gameManager.reset)       //if Reset button has been pressed
        {
            foreach (GameObject m in boidArray)
            {
                DestroyObject(m);       //destroy gameobject list
            }
            boidArray.Clear();      //clear gameobject list (just to be extra safe)
            boidArray = new List<GameObject>();     //create new gameobject list
            okFlag = false;     //reset okFlag to prevent Swarm Controller from reading empty gameobject list
        }
        if (gameManager.quit)
        {
            foreach (GameObject m in boidArray)
            {
                DestroyObject(m);       //destroy gameobject list
            }
            boidArray.Clear();      //clear gameobject list (just to be extra safe)
        }
    }
}
