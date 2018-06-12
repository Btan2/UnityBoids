using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{

    RaycastHit2D hit2;   //raycast hit variable
    RaycastHit2D hit3;   //raycast hit variable
    public RaycastHit2D hit;   //raycast hit variable
    public DrawLine line;
    public Vector3 c;
    public List<Transform> boidsList;
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public bool drawRays;
    public bool hitBoid;    //public hit bool so Ai script can check for raycast hit

    void Start()
    {
        Physics2D.queriesStartInColliders = false; //disable raycast colliding with the boid itself
        boidsList = new List<Transform>();
    }

    void Update()
    {
        Vector3 ray45 = Quaternion.AngleAxis(45, Vector3.forward) * transform.right;
        Vector3 ray135 = Quaternion.AngleAxis(-45, Vector3.forward) * transform.right;
        hit = Physics2D.Raycast(transform.position, transform.right, 2);    //cast a ray from boid (front)
        hit2 = Physics2D.Raycast(transform.position, ray45, 2);    //cast a ray from boid (front)
        hit3 = Physics2D.Raycast(transform.position, ray135, 2);    //cast a ray from boid (front)

        if (hit.collider != null)   //if the raycast has hit an object
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);   //draw a raycast line in debug
            //if (drawRays)
            //{
            //    line.drawLine(transform.position, hit.point, Color.red, 0.025f);        //physical raycast draw line
            //}
            if (hit.collider.tag == "boid")    //if the raycast has hit another boid
            {
                hitBoid = true;     //boid has been detected
                target1 = hit.collider.transform;     //set transform object to raycast hit object
            }
        }
        else if (hit2.collider != null)   //if the raycast has hit an object
        {
            Debug.DrawLine(transform.position, hit2.point, Color.red);
            if (hit2.collider.tag == "boid")    //if the raycast has hit another boid
            {
                hitBoid = true;     //boid has been detected
                target1 = hit2.collider.transform;     //set transform object to raycast hit object
            }
        }
        else if (hit3.collider != null)
        {
            Debug.DrawLine(transform.position, hit3.point, Color.red);
            if (hit3.collider.tag == "boid")    //if the raycast has hit another boid
            {
                hitBoid = true;     //boid has been detected
                target1 = hit3.collider.transform;    //set transform object to raycast hit object
            }
        }
        else
        {
            hitBoid = false;        //boid has not been detected
            Vector3 dir = transform.position + ray135 * 2;
            Debug.DrawLine(transform.position, dir, Color.green);     //draw a raycast line in debug
            dir = transform.position + ray45 * 2;
            Debug.DrawLine(transform.position, dir, Color.green);     //draw a raycast line in debug
            dir = transform.position + transform.right * 2;     //direction for raycast and draw line
            Debug.DrawLine(transform.position, dir, Color.green);     //draw a raycast line in debug
            //if (drawRays)
            //{
            //    line.drawLine(transform.position, dir, Color.green, 0.009f);       //physical raycast draw line
            //}
        }
    }
}



//hit2 = Physics2D.Raycast(transform.position, transform.up, 2);
//if (hit2.collider != null)   //if the raycast has hit an object
//{
//    Debug.DrawLine(transform.position, hit2.point, Color.red);   //draw raycast line from boid if the raycast has hit an object
//    if (hit2.collider.tag == "boid")    //if the raycast has hit another boid
//    {
//        targetDistance[1] = Vector3.Distance(this.transform.position, hit2.collider.transform.position);
//        targetLeft = hit2.collider.transform;    //set transform object to raycast hit object
//        print("Boid on port");
//    }
//}
//else
//{
//    Debug.DrawLine(transform.position, transform.position + transform.up * 2, Color.yellow);       //draw raycast line from boid
//}
//
//hit3 = Physics2D.Raycast(transform.position, -transform.up, 2);
//if (hit3.collider != null)   //if the raycast has hit an object
//{
//    Debug.DrawLine(transform.position, hit3.point, Color.red);   //draw raycast line from boid if the raycast has hit an object
//    if (hit3.collider.tag == "boid")    //if the raycast has hit another boid
//    {
//        targetDistance[2] = Vector3.Distance(this.transform.position, hit3.collider.transform.position);
//        targetRight = hit3.collider.transform;    //set transform object to raycast hit object
//        print("Boid on starboard");
//    }
//}
//else
//{
//    Debug.DrawLine(transform.position, transform.position + -transform.up, Color.yellow);      //draw raycast line from boid
//}
//Debug.DrawLine(transform.position, hit.point, Color.red);   //draw raycast line from boid if the raycast has hit an object
//Debug.DrawLine(transform.position, transform.position + transform.right* 2, Color.yellow);     //draw raycast line from boid

