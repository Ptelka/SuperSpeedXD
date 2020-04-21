using System;
using UnityEngine;

public class GameController: MonoBehaviour {
    public Road road;
    public Car car;
    public Speedometer speedometer;

    public GameObject left;
    public GameObject right;
    public GameObject tooSlowText;

    public GameObject[] backgrounds;
    
    private Parallax parallax;
    private Lines lines;


    void Start()
    {
        lines = new Lines(left, right, road.perspectiveFactor, road.size.y);
        parallax = new Parallax(backgrounds);
    }

    private void Update()
    {
        if (Screen.currentResolution.height != 240 || Screen.currentResolution.height != 320)
        {
            Screen.SetResolution(320, 240, false);
        }
        var distance = car.GetDistance();

        tooSlowText.SetActive(car.GetVelocity() < car.minVelocity + 30);

        road.distance = distance;
        lines.Update(road.curvature, distance);
        parallax.Update(road.curvature);
        car.curvature = road.curvature;
        speedometer.SetSpeed(car.GetVelocity());
    }
}