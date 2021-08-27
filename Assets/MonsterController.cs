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

    [SerializeField] Material material;
    AnimationClip[] clips;

    Animator animator;
    Vector3 SpawnPoint;
    Vector3[] MovePoints;

    Sequence moveSequence;

    [SerializeField]

    IEnumerator HitAni()
    {
        material.DOKill();

        material.DOColor(new Color32(255, 0, 0, 1), 0.15f);
        yield return new WaitForSeconds(0.15f);

        material.DOColor(new Color32(255, 255, 255, 1), 0.15f);
        yield return new WaitForSeconds(0.15f);

        material.DOColor(new Color32(255, 0, 0, 1), 0.15f);
        yield return new WaitForSeconds(0.15f);

        material.DOColor(new Color32(255, 255, 255, 1), 0.15f);
        yield return new WaitForSeconds(0.15f);
    }

    IEnumerator DieAni()
    {
        animator.Play("Die", 0, 0);

        var duration = System.Array.Find(clips, x => x.name.Contains("Die")).length;

        // Fade Out Delay
        yield return new WaitForSeconds(duration + 0.25f);

        GameManager.instance.DeleteMonster(this);

        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void Hit(float damage)
    {
        if (hp <= 0) return;
        hp -= damage;

        StopCoroutine("HitAni");
        StartCoroutine("HitAni");

        if (hp <= 0) {
            moveSequence.Kill();

            StopCoroutine("DieAni");
            StartCoroutine("DieAni");
        }
    }

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

        for (int i = 1; i < MovePoints.Length; i++)
        {
            moveSequence.Append(
                transform.DOLocalMove(MovePoints[i], speed).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        transform.Rotate(new Vector3(0, -90f, 0));
                    })
                );
        }
        moveSequence.Append(transform.DOLocalMove(SpawnPoint, speed).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.Rotate(new Vector3(0, -90f, 0));
            })
        );

        moveSequence.SetLoops(-1);
        moveSequence.Play(); 
    }

    void Initialize()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (moveSequence == null) moveSequence = DOTween.Sequence();
        if (clips == null) clips = animator.runtimeAnimatorController.animationClips;

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
