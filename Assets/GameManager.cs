using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("===Monster Setting===")]
    [SerializeField] GameObject[] MonsterPrefabs;
    [SerializeField] Vector3 SpawnPoints;
    [SerializeField] Transform MonsterParent;

    [SerializeField] Transform[] MovePoints;

    List<MonsterController> SpawnedMonsters = new List<MonsterController>();

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
                if (!i.Equals(this)) Destroy(i.gameObject);
            }

            DontDestroyOnLoad(this);
        }
    }

    public void DeleteMonster(MonsterController monster)
    {
        SpawnedMonsters.Remove(monster);
    }

    public void AllAttack()
    {
        foreach (MonsterController monster in SpawnedMonsters)
        {
            monster.Hit(10f);
        }
    }

    public Vector3 GetSpawnPoint()
    {
        return MovePoints[0].localPosition;
    }
    public Vector3[] GetMovePoints()
    {
        Vector3[] points = new Vector3[MovePoints.Length];
        for (int i=0; i <MovePoints.Length; i++)
        {
            points[i] = MovePoints[i].localPosition;
        }
        return points;
    }

    void SpawnMonster()
    {
        StopCoroutine("CoSpawnMonster");
        StartCoroutine("CoSpawnMonster");
    }

    IEnumerator CoSpawnMonster()
    {
        foreach (GameObject o in MonsterPrefabs)
        {
            GameObject monster = Instantiate(o, MonsterParent);
            SpawnedMonsters.Add(monster.GetComponent<MonsterController>());
            SpawnedMonsters[SpawnedMonsters.Count-1].MonsterMoving();

            yield return new WaitForSeconds(2.5f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnMonster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
