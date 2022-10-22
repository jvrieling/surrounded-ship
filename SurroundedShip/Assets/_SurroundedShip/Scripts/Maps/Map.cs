using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int mapId;

    public List<RectTransform> waypoints = new List<RectTransform>();

    public RectTransform ship;

    public int currentIndex;
    public float moveTime;

    private int prevIndex;
    private float moveTimer;
    private bool moving;
    private Vector3 target, origin;

    private void Update()
    {
        if(moving && moveTimer < moveTime)
        {
            moveTimer += Time.deltaTime;

            ship.position = Vector3.Lerp(origin, target, moveTimer / moveTime);
        }
        else if (moving)
        {
            moving = false;
            ship.position = target;
        }
    }

    private void OnValidate()
    {
        if (currentIndex != prevIndex)
        {
            SetShipPosition(currentIndex);
            prevIndex = currentIndex;
        }
    }

    public void SetShipPosition(int index)
    {
        if (waypoints.Count < 1 || ship == null) return;

        if(index >= 0 && index <= waypoints.Count - 1)
        {
            origin = ship.position;
            target = waypoints[index].position;
            moving = true;
            moveTimer = 0;
        }
    }
}
