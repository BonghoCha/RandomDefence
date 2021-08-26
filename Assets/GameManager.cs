using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("===Monster Setting===")]
    [SerializeField] GameObject[] MonsterPrefabs;
    [SerializeField] Vector3 SpawnPoints;

    [SerializeField] Transform[] MovePoints;

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // 중복된 인스턴스 제거
            GameManager[] inst = FindObjectsOfType<GameManager>();
            foreach (GameManager i in inst)
            {
                Destroy(i.gameObject);
            }

            DontDestroyOnLoad(this);
        }
    }

    public Vector3 GetSpawnPoint()
    {
        return MovePoints[0].localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
