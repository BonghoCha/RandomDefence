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

    bool init = false;

    [Header("===Monster Properties===")]
    [SerializeField] Type type;

    [SerializeField] float hp;
    [SerializeField] float power;
    [SerializeField] float shield;
    [SerializeField] float speed;

    Animator animator;
    Vector3 SpawnPoint;
    Vector3[] MovePoints;

    public void MonsterMoving()
    {
        if (!init)
        {
            init = true;

            Initialize();
        }

        // 몬스터 움직임
        animator.Play("Run", 0, 0);

        Quaternion targetRotation = transform.localRotation;

        Sequence sequence = DOTween.Sequence();
        for (int i=1; i < MovePoints.Length; i++)
        {
            sequence.Append(
                transform.DOLocalMove(MovePoints[i], speed).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        transform.Rotate(new Vector3(0, -90f, 0));
                    })
                );
        }
        sequence.Append(transform.DOLocalMove(SpawnPoint, speed).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.Rotate(new Vector3(0, -90f, 0));
            })
        );

        sequence.SetLoops(-1);
        sequence.Play(); 
    }

    void Initialize()
    {
        if (animator == null) animator = GetComponent<Animator>();

        SpawnPoint = GameManager.instance.GetSpawnPoint();
        MovePoints = GameManager.instance.GetMovePoints();

        transform.localPosition = SpawnPoint;

        gameObject.SetActive(true);

        if (type.Equals(Type.Slime)) MonsterMoving();
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
