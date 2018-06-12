using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTextScript : MonoBehaviour {
    public float timer;
    public BoidsAi boidsValues;
    public SpawnBoidsScript boidcount;
    public SwarmController drawRay;
    public Text FPS;
    public Text boidCountDisplay;
    public Text boidSpeed;
    public Text spawnLimit;
    public Text maxTurnDelay;
    public Text minTurnDelay;
    public Text timeDisplay;
    public Text raycastDisplay;
    public Text boidSize;
    public Slider sizeSlider;
    public Slider speedSlider;
    public Slider spawnSlider;
    float frameRate;

    void Start(){
        boidCountDisplay.text = "Boids: 0";
        StartCoroutine(CalculateFPS());
    }
    void Update(){
        timer++; //game timer;
        boidCounter();
        drawRays();
        maxTurnDelay.text = "Max Turn Delay: " + (boidsValues.maxRandomRange / 100).ToString() + "s";
        minTurnDelay.text = "Min Turn Delay: " + (boidsValues.minRandomRange / 100).ToString() + "s";
        timeDisplay.text = "Time: " + (timer / 100).ToString();
        boidSize.text = "Boid Size: " + sizeSlider.value;
        boidSpeed.text = "Boid Speed: " + speedSlider.value;
        spawnLimit.text = "Spawn Limit: " + spawnSlider.value;
        FPS.text = "FPS: " + string.Format("{0:0.00}", frameRate);
    }
    private IEnumerator CalculateFPS(){
        while (true)
        {
            frameRate = 1 / Time.deltaTime;
            yield return new WaitForSeconds(1);
        }
    }
    public void drawRays(){
        if (drawRay.draw)
        {
            raycastDisplay.text = "Raycasting: On";
        }
        else if (!drawRay.draw)
        {
            raycastDisplay.text = "Raycasting: Off";
        }
    }
    public void boidCounter(){
        if (boidcount.boidArray.Count >= spawnSlider.maxValue)
        {
            boidCountDisplay.text = "Boids: MAX";
        }
        else
        {
            boidCountDisplay.text = "Boids: " + boidcount.boidArray.Count;
        }
    }
}
