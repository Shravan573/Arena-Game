using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour

{
    public Transform Player;
    public Vector3 cameraOffset;
    private void Start()
    {
        cameraOffset = transform.position - Player.transform.position;
    }
    private void LateUpdate()
    {
        Vector3 newPosition = Player.transform.position + cameraOffset;
        transform.position = newPosition;
    }
}
