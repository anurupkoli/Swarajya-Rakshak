using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public int X;
    public int Z;
    void Start()
    {
        X = Mathf.RoundToInt(transform.position.x);
        Z = Mathf.RoundToInt(transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
