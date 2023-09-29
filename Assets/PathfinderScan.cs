using Pathfinding;
using UnityEngine;

public  class PathfinderScan : MonoBehaviour
{
    void Start()
    {
        var pathfinder = GetComponentInParent<AstarPath>();
        pathfinder.Scan();
    }



}
