using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoidsAi : MonoBehaviour
{

    public float boidSpeed = 6f;    //boid's speed
    public float boidSeperation;    //distance kept from detected boids
    public float randomRotation;    //rotational value for boidWandering();
    public float maxRandomRange;    //maximum range for radom timeTrigger variable
    public float minRandomRange;    //minimum range for random timeTrigger variable
    public float timer;             //timer for boidWandering();
    public float timeTrigger;       //set time for random rotation for boidWandering();
    public bool boidHitting;
    public RayCaster rayCaster;     //raycast hit variable
    bool randomTimeSet;             //flag for armed time trigger
    Quaternion randomQRotation;     //rotational Quaternion for boidWandering();
    Transform target;               //raycast hit target

    void Start()
    {
        boidStartDirection();       //set boid's direction upon spawning
    }
    void Update()
    {
        boidMoving();       //make boids move constantly
        checkBoids();       //check for boids
        faceMouse();        //enable boids to seek mouse
    }
    void boidStartDirection()       //establishes boid's initial direction once spawned
    {
        float randomRotation = Random.Range(0, 359);        //generate random number for rotational value
        this.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);     //set boid's initial rotation as the random rotation variable
    }
    void boidMoving()       //moves boid by applying velocity
    {
        Vector3 boidVelocity = this.transform.right * (boidSpeed / 2);      //calculate velocity from boid's speed variable
        GetComponent<Rigidbody2D>().AddForce(boidVelocity);     //apply force to boid using boids velocity
    }
    void checkBoids()       //checks if boids have been detected
    {
        if (rayCaster.hitBoid)      //if the raycaster has hit an object
        {
            if (rayCaster.c != null)
            {
                target = rayCaster.target1;
                rayCaster.boidsList.Clear();
                boidSeeking();      //begin seeking detected boid
            }
        }
        else   //if raycaster has not hit anything
        {
            target = null;          //clear target
            boidWandering();        //enable boid to wander
        }
    }
    void boidSeeking()      //if boid has been detected{
    { 
        if (!boidHitting)       //if the boids collider is not hitting another boid run seeking algorithm
        {
            randomTimeSet = false;      //prevents boidWandering() from executing random rotation while seeking other boids
            /*Vector3 targetDistance = (target.position - transform.position).normalized * 0.5f + target.position;*/     //Attempt to find point in between player and target *UPDATE: DOESN'T SEEM TO CHANGE ANYTHING!!*
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target.rotation, 10f * Time.deltaTime);         // match boid allignmen to detected boid
            this.transform.position = Vector3.Slerp(this.transform.position, target.position, 0.5f * Time.deltaTime);            //move towards distance variable position
        }
    }
    void boidWandering()        //rotate boid randomly after randomly generated period of time
    {     
        timer++;        //increment timer
        if (!randomTimeSet)     //if timer trigger has not been armed
        {
            timer = 0;      //timer counter
            timeTrigger = Random.Range(minRandomRange, maxRandomRange);     //create random turn trigger time
            randomRotation = Random.Range(0, 360);      //create random rotation
            randomTimeSet = true;       //random time has been set
        }
        if (randomTimeSet)      //if timer is armed
        {
            if (timer >= timeTrigger)       //if the timer has reached the turn trigger timer
            {
                timer = 0f;     //reset time counter
                randomQRotation = Quaternion.Euler(0f, 0f, randomRotation);     //set randomRotation
                randomTimeSet = false;      //reset timer
            }
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, randomQRotation, 5 * Time.deltaTime);       //apply random rotation to boid
        }
    }
    void faceMouse()
    {
        if (Input.GetMouseButton(0))
        {
            if(!EventSystem.current.IsPointerOverGameObject ())
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
                transform.right = direction;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "boid")
        {
            boidHitting = true;
            Vector3 tempboidPosition = (this.transform.position - other.gameObject.transform.position).normalized * 1f + other.gameObject.transform.position;
            this.transform.position = Vector3.SlerpUnclamped(this.transform.position, tempboidPosition, 0.2f * Time.deltaTime);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "boid")
        {
            boidHitting = false;
        }
    }
}


//PROTOTYPING//
//private void RotateAwayFrom()
//{
//    Vector3 direction = transform.position - target.position;
//    direction.x -= 270;
//    direction.y -= 270;
//    Quaternion awayDirection = Quaternion.Euler(direction);
//    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, awayDirection, 15 * Time.deltaTime);
//}
//PROTOTYPING//