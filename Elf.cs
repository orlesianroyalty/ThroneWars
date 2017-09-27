using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///A class for the orc race which describes behavior for their abilities and stats.
///Michelle Brannan 4/18/17
/// </summary>
public class Elf : MonoBehaviour, CombatClass
{

    public const float MOVEMENT_SPEED = 1.5f;
    public const float DAMAGE_MULTIPLIER = 1f;
    public const int MAXHP = 80;
    public const int MAXMP = 60;


    //Cooldowns
    public const float ABILITY1_COOLDOWN = 0.2f;
    public const float ABILITY2_COOLDOWN = 5f;
    public const float ABILITY3_COOLDOWN = 10f;
    public const float ABILITY4_COOLDOWN = 15f;

    //MP Cost
    public const float ABILITY1_COST = 0f;
    public const float ABILITY2_COST = 3f;
    public const float ABILITY3_COST = 8f;
    public const float ABILITY4_COST = 10f;

    public GameObject fireball;

    //Speed of  player
    public float movementSpeed = MOVEMENT_SPEED;

    //Damage multiplier for the character. Will be changed depending on active abilities.
    public float dmgMult = DAMAGE_MULTIPLIER;

    //Maximum amount of health and MP a character has.
    public int maxHP = MAXHP;
    public int maxMP = MAXMP;

    //Character's current amount of health and MP.
    public int hp = MAXHP;
    public int mp = MAXMP;

    //Cooldowns for orc abilities
    public float fireballCool = 0.2f;
    public float iceCool = 5f;
    public float healCool = 10f;
    public float necromancyCool = 15f;

    //MP cost for orc abilities
    public float fireballMP = 0f;
    public float iceMP = 3f;
    public float healMP = 8f;
    public float necromancyMP = 10f;

    //Prefabs and other necessary items
    public bool isEnemy;
    public GameObject boxcol;
    public GameObject boxcol2;
    private PlayerControl pc;
    private Rigidbody2D rb;
    private Transform trans;
    private float activatedAbilityTime = 0f;
    private bool activatedAbility2 = false;
    public int weapondmg = 0;

    // Use this for initialization
    void Start()
    {
        pc = GetComponent<PlayerControl>();
        rb = GetComponent<Rigidbody2D>();
        trans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activatedAbility2 && Time.time > activatedAbilityTime + fireballCool)
        {
            dmgMult = DAMAGE_MULTIPLIER;
        }
    }

    //Accessor methods
    public float getHP()
    {
        return hp;
    }

    public float getMP()
    {
        return mp;
    }

    public float getMaxMP()
    {
        return maxMP;
    }

    public float getMovementSpeed()
    {
        return movementSpeed;
    }

    public float getMaxHP()
    {
        return maxHP;
    }


    public float getAbility1Cooldown()
    {
        return fireballCool;
    }

    public float getAbility2Cooldown()
    {
        return iceCool;
    }

    public float getAbility3Cooldown()
    {
        return healCool;
    }

    public float getAbility4Cooldown()
    {
        return necromancyCool;
    }

    public float getAbility1Cost()
    {
        return fireballMP;
    }

    public float getAbility2Cost()
    {
        return iceMP;
    }

    public float getAbility3Cost()
    {
        return healMP;
    }

    public float getAbility4Cost()
    {
        return necromancyMP;
    }

    //Ability methods
    //Fireball
    public void ability1()
    {
        GameObject clone = fireball;
        clone.GetComponent<BoxColl2D>().enemy = isEnemy;
        clone.GetComponent<BoxColl2D>().mult = dmgMult;
        clone = Instantiate(clone, clone.transform.position, Quaternion.identity);

        //do animation
        int direction = pc.facingDirection;
        int dir = pc.facingDirection;

        clone = Instantiate(clone, clone.transform.position, Quaternion.identity);

        //0 down, 1 right, 2 up, 3 left
        if (dir == 0)
        {
            clone.transform.position = new Vector3(trans.localPosition.x, trans.localPosition.y - 1f, 0);
        }
        if (dir == 1)
        {
            clone.transform.position = new Vector3(trans.position.x + 1f, trans.position.y, 0);
        }
        if (dir == 2)
        {
            clone.transform.position = new Vector3(trans.position.x, trans.position.y + 1f, 0);
        }
        if (dir == 3)
        {
            clone.transform.position = new Vector3(trans.position.x - 1f, trans.position.y, 0);
        }

        clone.transform.Rotate(0f, 0f, pc.facingDirection * 90f);

    }


    //Ice Dagger
    public void ability2()
    {
        GameObject clone = boxcol;
        clone.GetComponent<BoxColl2D>().enemy = isEnemy;
        clone.GetComponent<BoxColl2D>().mult = dmgMult;
        clone = Instantiate(clone, clone.transform.position, Quaternion.identity);

        //do animation
        int direction = pc.facingDirection;

        switch (direction)
        {
            case 0:
                clone.transform.position = new Vector3(trans.localPosition.x, trans.localPosition.y - .3f, 0);
                break;
            case 1:
                clone.transform.position = new Vector3(trans.position.x + .2f, trans.position.y, 0);
                break;
            case 2:
                clone.transform.position = new Vector3(trans.position.x, trans.position.y + .3f, 0);
                break;
            case 3:
                clone.transform.position = new Vector3(trans.position.x - .2f, trans.position.y, 0);
                break;
        }

    }

    //Healing
    public void ability3()
    {
        timer();
        return;
    }


    // Had to do this since Unity gave compile errors otherwise
    public IEnumerator timer()
    {
        int loopCount = 0;

        while (loopCount <= 5)
        {
            if (hp != maxHP)
            {
                if (hp + 5 > maxHP)
                {
                    hp = maxHP;
                    break;
                }
                hp += 5;
                yield return new WaitForSecondsRealtime(1);
                loopCount++;
            }
            if (hp == maxHP)
                break;
        }
        yield break;
    }

    //Necromancy
    public void ability4()
    {
// saved for after enemy movement is done
    }


    public void takeDamage(int dealt)
    {
        hp -= dealt;
        if (hp < 0)
        {
            hp = 1;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public int weaponDamage()
    {
        return weapondmg;
    }

}