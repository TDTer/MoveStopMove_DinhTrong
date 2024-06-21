using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Character : GameUnit
{
    public const float ATTACK_RANGE = 5f;
    public const float TIME_ON_DEATH = 1f;
    public const float MAX_SIZE = 2.5f;
    public const float MIN_SIZE = 1f;

    public Character target;
    [SerializeField] PantData pantData;
    [SerializeField] private Animator anim;
    [SerializeField] public Collider coll;
    [SerializeField] Transform rightHandPosition;
    [SerializeField] GameObject insightMasker;
    [SerializeField] GameObject rangeAttackOutline;
    [SerializeField] protected Renderer currentPant;

    private string animName;
    private int score;
    private Vector3 targetPoint;
    protected float size = 1;

    protected Weapon currentWeapon;

    public int Score => score;


    public bool IsDead { get; protected set; }

    public virtual void OnInit()
    {
        IsDead = false;
        score = 0;

        WearClothes();
    }

    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }

    public virtual void OnAttack()
    {
        target = GetTargetInRange();

        if (target != null && !target.IsDead)
        {
            targetPoint = target.TF.position;
            TF.LookAt(targetPoint + (TF.position.y - targetPoint.y) * Vector3.up);
            ChangeAnim(Constant.ANIM_ATTACK);
        }

    }

    public Character GetTargetInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.TF.position, ATTACK_RANGE);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(Constant.TAG_CHARACTER) && hitCollider != coll)
            {
                return hitCollider.GetComponent<Character>();
            }
        }

        return null;
    }

    public virtual void Throw()
    {
        currentWeapon.Throw(this, targetPoint, size);
    }

    public void OnHit(UnityAction hitAction)
    {
        if (!IsDead)
        {
            IsDead = true;
            hitAction.Invoke();
            OnDeath();
        }
    }
    public void ResetAnim()
    {
        animName = Constant.ANIM_IDLE;
        ChangeAnim(animName);
    }

    public virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    public virtual void OnDeath()
    {
        // Debug.Log("Is Checked");
        ChangeAnim(Constant.ANIM_DIE);
        LevelManager.Ins.CharecterDeath(this);
    }

    public virtual void OnMoveStop() { }


    public void ChangeWeapon(WeaponType weaponType)
    {
        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, rightHandPosition);
    }

    public void ChangePant(PantType pantType)
    {
        currentPant.material = pantData.GetPant(pantType);
    }

    public virtual void WearClothes()
    {

    }

    public virtual void TakeOffClothes()
    {
        SimplePool.Despawn(currentWeapon);
    }


    public void ActiveMaker()
    {
        insightMasker.SetActive(true);
    }

    public void DeactiveMaker()
    {
        insightMasker.SetActive(false);
    }

    public void AddScore(int amount = 1)
    {
        SetScore(score + amount);
    }

    public void SetScore(int score)
    {
        this.score = score > 0 ? score : 0;
        // indicator.SetScore(this.score);
        SetSize(1 + this.score * 0.1f);
    }

    protected virtual void SetSize(float size)
    {
        size = Mathf.Clamp(size, MIN_SIZE, MAX_SIZE);
        this.size = size;

        rangeAttackOutline.transform.parent = null;
        TF.localScale = size * Vector3.one;
        rangeAttackOutline.transform.parent = TF;

    }

    public void DespawnWeapon()
    {
        if (currentWeapon) SimplePool.Despawn(currentWeapon);
    }
}
