using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float width = 30;
    public float height = 20;
    public Food foodPrefab;
    //[SerializeField] float spawnTimer;
    //float counter;

    public List<SteeringAgents> allBoids = new List<SteeringAgents>();

    void Awake()
    {
        if (instance == null)
        instance = this;

        else 
        Destroy(gameObject);
        ChangeFoodPosition();
    }
    void Update()
    {
        
    }

    public void ChangeFoodPosition()
    {
        float w = width / 2;
        float h = height / 2;

        Vector3 spawn = new Vector3(Random.Range(-w,w), Random.Range(-h,h),0);
        foodPrefab.gameObject.transform.position = spawn;
        
    }

    public void ShiftPositionOnBounds(Transform t)
    {
        Vector3 pos = t.position;
        float w = width / 2;
        float h = height / 2;

        if (pos.y > h) pos.y = -h;
        if (pos.y < -h) pos.y = h;
        if (pos.x > w) pos.x = -w;
        if (pos.x < -w) pos.x = w;

        t.position = pos;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.right * width + Vector3.up * height);
    }
}
