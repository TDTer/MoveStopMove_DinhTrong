using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public const string ANIM_RUN = "run";
    public const string ANIM_IDLE = "idle";
    public const string ANIM_DIE = "die";
    public const string ANIM_DANCE = "dance";
    public const string ANIM_ATTACK = "attack";
    public const string ANIM_WIN = "win";

    public const string TAG_CHARACTER = "Character";
    public const string TAG_OBSTACLE = "Obstacle";


}

public enum WeaponType
{
    // None,
    Wp_Arrow = PoolType.Wp_Arrow,
    Wp_Axe_0 = PoolType.Wp_Axe_0,
    Wp_Axe_1 = PoolType.Wp_Axe_1,
    Wp_Boomerang = PoolType.Wp_Boomerang,
    Wp_Candy_0 = PoolType.Wp_Candy_0,
    Wp_Candy_1 = PoolType.Wp_Candy_1,
    Wp_Candy_2 = PoolType.Wp_Candy_2,
    Wp_Candy_3 = PoolType.Wp_Candy_3,
    Wp_Hammer = PoolType.Wp_Hammer,
    Wp_Knife = PoolType.Wp_Knife,
    Wp_Uzi = PoolType.Wp_Uzi,
    Wp_Z = PoolType.Wp_Z,
}

public enum BulletType
{
    // None,
    Bullet_Arrow = PoolType.Bullet_Arrow,
    Bullet_Axe_0 = PoolType.Bullet_Axe_0,
    Bullet_Axe_1 = PoolType.Bullet_Axe_1,
    Bullet_Boomerang = PoolType.Bullet_Boomerang,
    Bullet_Candy_0 = PoolType.Bullet_Candy_0,
    Bullet_Candy_1 = PoolType.Bullet_Candy_1,
    Bullet_Candy_2 = PoolType.Bullet_Candy_2,
    Bullet_Candy_3 = PoolType.Bullet_Candy_3,
    Bullet_Hammer = PoolType.Bullet_Hammer,
    Bullet_Knife = PoolType.Bullet_Knife,
    Bullet_Uzi = PoolType.Bullet_Uzi,
    Bullet_Z = PoolType.Bullet_Z,
}


public enum PantType
{
    Pant_Batman = 0,
    Pant_chambi = 1,
    Pant_comy = 2,
    Pant_dabao = 3,
    Pant_onion = 4,
    Pant_pokemon = 5,
    Pant_rainbow = 6,
    Pant_Skull = 7,
    Pant_vantim = 8,
}