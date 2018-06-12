using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class GameManager : MonoBehaviour {

    public bool spawnBoids;
    public bool castRays;
    public bool boidMagnet;
    public bool reset;
    public bool quit;
    public DisplayTextScript display;
    public SwarmController drawRays;

    public void Update(){
        spawnBoids = false;
        boidMagnet = false;
        if (Input.GetButtonDown("DrawRays"))
        {
            if(castRays)
            {
                drawRays.draw = false;
                castRays = false;
            }
            else if (!castRays)
            {
                drawRays.draw = true;
                castRays = true;
            }
        }
        if (Input.GetButton("Fire2"))       //spawn boids
        {
            spawnBoids = true;
        }
        if (Input.GetButton("Fire1"))
        {
            boidMagnet = true;
        }
        if (Input.GetButton("Reset"))       //enable reset field
        {
            reset = true;
            display.timer = 0;
        }
        if (Input.GetButtonUp("Reset"))       //enable reset field
        {
            reset = false;
        }
        if (Input.GetButtonDown("Cancel"))      //quit game
        {
            quit = true;
            Application.Quit();
        }
    }
}
