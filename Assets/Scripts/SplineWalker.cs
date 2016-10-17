using System;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{

    public BezierSpline spline;
    public SplineWalkerMode mode;

    private bool goingForward = true;

    public float duration;
    public bool lookForward;

    private float progress;
    int slices = 4;

    Vector3[] points;
    GameObject bezier;

    void Start()
    {
        bezier = GameObject.Find("BezierSpline");
        points = bezier.GetComponent<BezierSpline>().points;
    }

    private void Update()
    {
        //Debug.Log(getClosestPointToCubicBezier(transform.position.x, transform.position.z, slices, points[0].x, points[0].z, points[1].x, points[1].z, points[2].x, points[2].z, points[3].x, points[3].z));

        if (goingForward)
        {

            progress += Time.deltaTime / duration;
            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else
        {
            progress -= Time.deltaTime / duration;
            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }
        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }

    }

    public double getClosestPointToCubicBezier(double fx, double fy, int slices, double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3)
    {
        double tick = 1d / (double)slices;
        double x;
        double y;
        double t;
        double best = 0;
        double bestDistance = Mathf.Infinity;
        double currentDistance = 0;
        for (int i = 0; i <= slices; i++)
        {
            t = i* tick;
            //Debug.Log("t: " + t);
            //B(t) = (1-t)**3 p0 + 3(1 - t)**2 t P1 + 3(1-t)t**2 P2 + t**3 P3
            x = (1 - t) * (1 - t) * (1 - t) * x0 + 3 * (1 - t) * (1 - t) * t * x1 + 3 * (1 - t) * t * t * x2 + t * t * t * x3;
            y = (1 - t) * (1 - t) * (1 - t) * y0 + 3 * (1 - t) * (1 - t) * t * y1 + 3 * (1 - t) * t * t * y2 + t * t * t * y3;

            //Debug.Log("x: "+ x + "y: " + y);

            //x = (1 - t) * (1 - t) * x0 + 2 * (1 - t) * t * x1 + t * t * x2; //quad.
            //y = (1 - t) * (1 - t) * y0 + 2 * (1 - t) * t * y1 + t * t * y2; //quad.

            currentDistance = Math.Sqrt((x - fx) * (x - fx) + (y - fy) * (y - fy));
            if (currentDistance < bestDistance)
            {
                bestDistance = currentDistance;
                best = t;
            }
        }
        return bestDistance;
    }
}
