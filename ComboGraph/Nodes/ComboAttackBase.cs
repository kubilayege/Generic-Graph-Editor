using UnityEngine;

public abstract class ComboAttackBase : ComboNodeBase
{
    [Header("References")]
    public AnimationClip animationClip;
    public DamageData.DamageType DamageType;
    public ParticleGameObjectPoolItem MuzzleFlashFX;
    public Ailments ailments;

    [Header("Properties")]
    public StatType statType;
    public float multiplier = 1f;
    public float slowedTimeScale = 0.1f;
    public float slowTimeDuration = 0.1f;

    public override void Init()
    {
        base.Init();
        if (caster == null)
            return;
        
        caster = caster.Clone();
    }
}