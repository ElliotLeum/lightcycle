using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class LightTrail : MonoBehaviour
{
    public Transform emitter;
    public GameObject vehicle;
    public GameObject trailPrefab;
    public new BoxCollider collider;
    float timer = 0.0f;
    public float trailSpawnRate = 0.4f;
    public float trailGapSize = 0.2f;
    Vector3 position1;
    Vector3 up;

    void Start()
    {
        position1 = vehicle.transform.position;
        up = vehicle.transform.up;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > trailSpawnRate)
        {
            Vector3 posStart = position1;
            placeTrail(vehicle, posStart, up);
            position1 = FindPosition(vehicle);
            up = vehicle.transform.up;
            timer = 0.0f;
        }
    }

    public void placeTrail(GameObject player, Vector3 posStart, Vector3 upDir)
    {
        Vector3 position2 = player.transform.position;

        float distance = Vector3.Distance(position2, posStart);
        if(distance > 0)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.GetComponent<MeshRenderer>().enabled = false;
            cube.layer = 9;
            cube.transform.localPosition = position2;
            cube.transform.localScale = new Vector3(0.05f, 2.0f, distance);
            cube.transform.rotation = Quaternion.LookRotation(player.transform.position - posStart, upDir);
            Destroy(cube, 10);
        }
        
    }

    public Vector3 FindPosition(GameObject player)
    {
        Vector3 pos = player.transform.position;
        return pos;
    }
}