using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform targetToFollow;
    public Vector3 offset;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 DesiredPosition = targetToFollow.position + offset;
        Vector3 SmoothFollow = Vector3.Lerp(transform.position, DesiredPosition, duration);
        transform.position = SmoothFollow;
    }
}
