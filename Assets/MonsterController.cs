using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterController : MonoBehaviour
{
    enum Type
    {
        Slime,
        TurtleShell,
        Beholder,
        Chest
    }

    [Header("===Monster Properties===")]
    [SerializeField] Type type;

    [SerializeField] float hp;
    [SerializeField] float power;
    [SerializeField] float shield;
    [SerializeField] float speed;

    Vector3 SpawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPoint = GameManager.instance.GetSpawnPoint();

        transform.localPosition = SpawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
