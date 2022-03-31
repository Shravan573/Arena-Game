using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPatrol))]
public class Fieldofview : Editor
{
    private void OnSceneGUI()
    {
        EnemyPatrol fov = (EnemyPatrol)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.sightrange);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.sightrange);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.sightrange);

        if (fov.playerinsight)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
