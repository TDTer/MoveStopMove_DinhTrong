using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public const float TIME_TO_RELOAD = 5f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Rigidbody rb;
    [SerializeField] bool isCanAttack;
    [SerializeField] PantType pantType;

    private bool isMoving = false;
    Character lastTarget;
    WeaponType weaponType;

    public Character Target => target;

    public int Coin => Score * 10;


    public override void OnInit()
    {
        OnSetFromData();
        base.OnInit();

        TF.rotation = Quaternion.Euler(Vector3.up * 180);
        SetSize(MIN_SIZE);
        ResetAnim();
        lastTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();

        if (Input.GetMouseButton(0) && JoyStick.direction != Vector3.zero)
        {
            rb.MovePosition(rb.position + JoyStick.direction * moveSpeed * Time.deltaTime);
            TF.position = rb.position;

            TF.forward = JoyStick.direction;

            ChangeAnim(Constant.ANIM_RUN);
            isMoving = true;
        }


        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
            OnMoveStop();
            OnAttack();
        }
    }

    private void CheckTarget()
    {
        target = GetTargetInRange();

        if (target != null)
        {
            if (target != lastTarget)
            {
                target.ActiveMaker();
                if (lastTarget != null)
                {
                    lastTarget.DeactiveMaker();
                }
                lastTarget = target;
            }
        }
        else
        {
            if (lastTarget != null)
            {
                lastTarget.DeactiveMaker();
                lastTarget = target;
            }
        }

    }
    public override void OnAttack()
    {
        base.OnAttack();
        if (target != null)
        {
            Throw();
            ResetAnim();
        }
    }

    public override void OnMoveStop()
    {
        base.OnMoveStop();
        rb.velocity = Vector3.zero;
        ChangeAnim(Constant.ANIM_IDLE);
    }

    internal void OnRevive()
    {
        ChangeAnim(Constant.ANIM_IDLE);
        IsDead = false;
    }

    internal void OnSetFromData()
    {
        weaponType = UserDataManager.Ins.userData.playerWeapon;
        pantType = UserDataManager.Ins.userData.playerPant;
    }

    public override void WearClothes()
    {
        base.WearClothes();

        ChangeWeapon(weaponType);
        ChangePant(pantType);
    }

    public void TryCloth(UIShop.ShopType shopType, Enum type)
    {
        switch (shopType)
        {
            case UIShop.ShopType.Pant:
                ChangePant((PantType)type);
                break;

            case UIShop.ShopType.Weapon:
                DespawnWeapon();
                ChangeWeapon((WeaponType)type);
                break;
            default:
                break;
        }

    }
}
