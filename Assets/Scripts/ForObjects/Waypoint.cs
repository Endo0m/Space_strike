using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField]
    private Transform targetTerminal;
    [SerializeField]
    private Transform secondTerminal;
    [SerializeField]
    private float speed = 5f;

    private bool isMoving = false;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            SetNextWaypoint();
        }
    }
    private void Update()
    {
        if (isMoving)
        {
            MoveToWaypoint();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleTerminal();
                SetNextWaypoint();
            }
        }
    }
    private void SetNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        isMoving = true;
    }
    private void MoveToWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

        if (transform.position == waypoints[currentWaypointIndex].position)
        {
            isMoving = false;
        }
    }
    private void ToggleTerminal()
    {
        if (targetTerminal == secondTerminal)
        {
            targetTerminal = secondTerminal;
            secondTerminal = targetTerminal;
        }
        else
        {
            targetTerminal = secondTerminal;
            secondTerminal = targetTerminal;
        }
    }
}
