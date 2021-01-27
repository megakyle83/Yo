using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointObject : MonoBehaviour
{
    public GameObject platform;
    public Transform start;
    public Transform end;
    public float speed;

    private Vector3 target;

    void Start()
    {
        target = end.position;
    }
    void Update()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, target, speed * Time.deltaTime);
        if(platform.transform.position == end.position)
        {
            target = start.position;
        }
        if(platform.transform.position == start.position)
        {
            target = end.position;
        }
    }
}
