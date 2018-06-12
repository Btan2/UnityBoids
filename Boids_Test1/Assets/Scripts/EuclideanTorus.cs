using UnityEngine;
using System.Collections;

public class EuclideanTorus : MonoBehaviour
{
    void Update(){
        // Teleport the game object to opposite Vector3 position
        if (transform.position.x > 9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        else if (transform.position.x < -9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        else if (transform.position.y > 5)
        {
            transform.position = new Vector3(transform.position.x, -5, 0);
        }
        else if (transform.position.y < -5)
        {
            transform.position = new Vector3(transform.position.x, 5, 0);
        }
    }
}