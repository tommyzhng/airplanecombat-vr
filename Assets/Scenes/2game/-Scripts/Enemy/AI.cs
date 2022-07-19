using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject player;

    //Waypoints
    public List<Transform> waypoints = new List<Transform>();
    private Transform curWaypoint;
    private Transform nextWaypoint;
    private int waypointIndex = 0;
    private float lerpStep;

    Vector3 currWp;
    Vector3 nextWp;

    [Range(0, 1)]
    public float rollStep = 0f;

    float direction;
    float targetDirection;

    bool offGround = false;

    void Update()
	{
        if (player.transform.position.y > 8 && offGround == false)
        {
            offGround = true;
        }
        if (offGround == true)
        {
            CalculateWaypoint();
        }
        
    }
    void CalculateWaypoint()
    {
        //Moving towards waypoints
        curWaypoint = waypoints[waypointIndex];
        if (waypointIndex != 8)         //If waypointindex isnt the last waypoint, calculate vector3 for next waypoint
        {
            nextWaypoint = waypoints[waypointIndex + 1];
            currWp = Vector3.MoveTowards(transform.position, curWaypoint.position, (30 * Time.deltaTime));
            nextWp = Vector3.MoveTowards(transform.position, nextWaypoint.position, (30 * Time.deltaTime));

            transform.position = Vector3.Lerp(currWp, nextWp, lerpStep);            //Smoothly lerp between two waypoints, adding to lerpStep below
        }
        else                            //Regular Move towards final waypoint
        {
            transform.position = Vector3.MoveTowards(transform.position, curWaypoint.position, (30 * Time.deltaTime));
        }
        //Angle (yaw and pitch)
        transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(curWaypoint.position - transform.position),
                Quaternion.LookRotation(nextWaypoint.position - transform.position), lerpStep);


        //Angle(Roll)

        //Add to lerp.
        float distanceToWaypoint = Vector3.Distance(transform.position, curWaypoint.position);      //Calculate distance to current waypoint

        if (distanceToWaypoint <= 100f)
        {
            lerpStep += 0.3f * Time.deltaTime;                                                      //Add to lerpStep to smoothly transfer waypoints
            if (lerpStep >= 1)                                                                      //If lerpStep = 1, then change waypoint and reset lerpStep
            {
                waypointIndex++;
                lerpStep = 0;
            }
            Debug.DrawRay(transform.position, nextWaypoint.position - transform.position, Color.green);
            direction = Vector3.Dot(Vector3.Cross(transform.forward, nextWaypoint.position - transform.position), transform.up);
        }
        else
        {
            Debug.DrawRay(transform.position, curWaypoint.position - transform.position, Color.green);
            targetDirection = Vector3.Dot(Vector3.Cross(transform.forward, nextWaypoint.position - transform.position), transform.up);
        }

        
        float halfDir = targetDirection / 2;
        //Debug.Log("begin: " + targetDirection + " half: " + halfDir + "cur: " + direction);

        if (direction > targetDirection)      //current Direction becomes greater than the beginning targetDirection target is on the left
        {
            if (direction <= halfDir)
            {
                if (rollStep <= 1)
                {
                    rollStep += 0.9f * Time.deltaTime;
                }
            }
            else
            {
                if(rollStep >= 0)
                {
                    rollStep -= 0.9f * Time.deltaTime;
                }
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 40), rollStep);
        }
        else if (direction < targetDirection)       //if current direction becomes smallar than beginning direction, target is on the right
        {
            if (direction >= halfDir)
            {
                if (rollStep <= 1)
                {
                    rollStep += 0.9f * Time.deltaTime;
                }
            }
            else
            {
                if (rollStep >= 0)
                {
                    rollStep -= 0.9f * Time.deltaTime;
                }
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -40), rollStep);
        }  
    }


}

