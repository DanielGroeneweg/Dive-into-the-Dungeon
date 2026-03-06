using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(AttackBehaviour))]
[Serializable]
public class State
{
    [Range(0f, 1f)] public float healthPercentageThreshold;
    public MoveBehaviour MoveBehaviour;
    public AttackBehaviour AttackBehaviour;
}
public class BossController : MonoBehaviour
{
    [Tooltip("Should always have at least one state at value 1")]
    [SerializeField] private List<State> bossStages = new();
    [SerializeField] private Health health;

    private AttackBehaviour attackBehaviour;
    private MoveBehaviour moveBehaviour;
    private void FixedUpdate()
    {
        if (!attackBehaviour.CanAttack) moveBehaviour.DoMovement();

        else
        {
            moveBehaviour.StopMovement();
            attackBehaviour.DoAttack();
        }
    }
    private void CheckBossState(float min, float max, float current)
    {
        float percentage = current / max;
        foreach (State bossState in bossStages)
        {
            if (percentage >= bossState.healthPercentageThreshold)
            {
                attackBehaviour = bossState.AttackBehaviour;
                moveBehaviour = bossState.MoveBehaviour;
            }
        }
    }
    private void OnEnable()
    {
        StartCoroutine(Link());
    }
    private IEnumerator Link()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.LinkGameOverEvent(Disable);
        health.healthChanged += CheckBossState;
    }
    private void OnDisable()
    {
        GameManager.Instance.UnlinkGameOverEvent(Disable);
        health.healthChanged -= CheckBossState;
    }
    private void Disable(GameOverEventData data) { enabled = false; }
#if UNITY_EDITOR
    [SerializeField] private bool editing;
    private void OnValidate()
    {
        if (bossStages.Count < 2 || editing) return;

        bossStages.Sort((a, b) => b.healthPercentageThreshold.CompareTo(a.healthPercentageThreshold));

        // Sort list
        for (int i = bossStages.Count - 1; i > 0; i--)
        {
            State a = bossStages[i];
            State b = bossStages[i - 1];

            if (a.healthPercentageThreshold == b.healthPercentageThreshold) bossStages.RemoveAt(i);
        }
    }
#endif
}