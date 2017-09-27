using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///A class for the orc race which describes behavior for their abilities and stats.
///Michelle Brannan 4/18/17
/// </summary>
public class Orc : MonoBehaviour, CombatClass {

	public const float MOVEMENT_SPEED = 1.5f;
	public const float DAMAGE_MULTIPLIER = 1f;
	public const int MAXHP = 125;
	public const int MAXMP = 40;


	//Cooldowns
	public const float ABILITY1_COOLDOWN = 0.2f;
	public const float ABILITY2_COOLDOWN = 4f;
	public const float ABILITY3_COOLDOWN = 6f;
	public const float ABILITY4_COOLDOWN =  13f;

	//MP Cost
	public const float ABILITY1_COST = 0f;
	public const float ABILITY2_COST = 5f;
	public const float ABILITY3_COST = 8f;
	public const float ABILITY4_COST = 11f;

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
	public float swingCool = 0.2f;
	public float warcryCool = 4f;
	public float axepirouetteCool = 6f;
	public float earthshatterCool = 13f;

	//MP cost for orc abilities
	public float swingMP = 0f;
	public float warcryMP = 5f;
	public float axepirouetteMP = 8f;
	public float earthshatterMP = 11f;

	//Sound Effects
	public AudioClip warcryAudio;
	public AudioClip earthshatterAudio;
	public AudioClip axeswing2Audio;
	private AudioSource source;

	//Prefabs and other necessary items
	public bool isEnemy;
	public GameObject boxcol;
	public GameObject boxcol2;
	public GameObject warcryAnim;
	public GameObject earthshatterAnim;
	public GameObject axeswingAnim;
	private PlayerControl pc;
	private Rigidbody2D rb;
	private Transform t;
	private float activatedAbilityTime = 0f;
	private bool activatedAbility2 = false;
	public int weapondmg = 0;

	// Use this for initialization
	void Start () {
		pc = GetComponent<PlayerControl> ();
		rb = GetComponent<Rigidbody2D>();
		t = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (activatedAbility2 && Time.time > activatedAbilityTime + warcryCool) {
			dmgMult = DAMAGE_MULTIPLIER;
		}
	}

	//Accessor methods
	public float getHP() {
		return hp;
	}

	public float getMP() {
		return mp;
	}

	public float getMaxMP() {
		return maxMP;
	}

	public float getMovementSpeed() {
		return movementSpeed;
	}

	public float getMaxHP() {
		return maxHP;
	}
		

	public float getAbility1Cooldown() {
		return swingCool;
	}	

	public float getAbility2Cooldown() {
		return warcryCool;
	}	

	public float getAbility3Cooldown() {
		return axepirouetteCool;
	}

	public float getAbility4Cooldown() {
		return earthshatterCool;
	}

	public float getAbility1Cost() {
		return swingMP;
	}

	public float getAbility2Cost() {
		return warcryMP;
	}

	public float getAbility3Cost() {
		return axepirouetteMP;
	}

	public float getAbility4Cost() {
		return earthshatterMP;
	}

	//Ability methods
	//Orc ability1 is Swing in which a two handed weapon is swung in an arc dealing 25 damage to an enemy.
	public void ability1() {
		GameObject clone = boxcol;
		clone.GetComponent<BoxColl2D> ().enemy = isEnemy;
		clone.GetComponent<BoxColl2D> ().mult = dmgMult;
		clone = Instantiate (clone, clone.transform.position, Quaternion.identity);

		//do animation
		GameObject go = axeswingAnim;
		go = Instantiate (go, go.transform.position, Quaternion.identity);
		Destroy (go, 1.5f);
		int direction = pc.facingDirection;

		switch (direction) {
		case 0:
			clone.transform.position = new Vector3 (t.localPosition.x, t.localPosition.y - .3f, 0);
			break;
		case 1:
			clone.transform.position = new Vector3 (t.position.x + .2f, t.position.y, 0);
			break;
		case 2:
			clone.transform.position = new Vector3 (t.position.x, transform.position.y + .3f, 0);
			break;
		case 3:
			clone.transform.position = new Vector3 (t.position.x - .2f, t.position.y, 0);
			break;
		}

 	}


	//Orc ability2 is Warcry in which an orc will execute a warcry that will increase damage multiplier by 20% for 7s.
	public void ability2() {
		//do animation
		GameObject go = warcryAnim;
		go = Instantiate(go, go.transform.position, Quaternion.identity);
		Destroy (go, 1.5f);
		source = GetComponent<AudioSource>();
		source.PlayOneShot (warcryAudio, 1f);
		activatedAbilityTime = Time.time;
		dmgMult += .20f;
		activatedAbility2 = true;

	}

	//Orc ability3 is AxePirouette in which an orc will swing a two handed weapon 360 degrees dealing 15 damage per enemy.
	public void ability3() {
		//do animation
		GameObject go = axeswingAnim;
		go = Instantiate (go, go.transform.position, Quaternion.identity);
		Destroy (go, 1.5f);
		source = GetComponent<AudioSource>();
		source.PlayOneShot (axeswing2Audio, 1f);
		weapondmg = (int)(15f * dmgMult);
		Vector2 playerpos = new Vector2 (t.position.x, t.position.y);
		Collider2D[] cols = Physics2D.OverlapCircleAll (playerpos, 1f, 1);
		Debug.Log (cols.Length);
		foreach (Collider2D c in cols) {
			if (isEnemy && c.gameObject.tag == "Player") {
				c.gameObject.GetComponent<CombatClass> ().takeDamage (weapondmg);
				Debug.Log ("col damage to player");
			}
			if (!isEnemy && c.gameObject.tag == "Enemy") {
				c.gameObject.GetComponent<CombatClass> ().takeDamage (weapondmg);
				Debug.Log ("col damage to enemy");
			}
		}
	}

	//Orc ability4 is Earthshatter in which an orc will jump and slam a two handed weapon on the ground causing a minor earthquake
	// dealing 50 damage. 
	public void ability4() {
		//do animation
		GameObject go = earthshatterAnim;
		go = Instantiate (go, go.transform.position, Quaternion.identity);
		Destroy (go, 1.5f);
		source = GetComponent<AudioSource>();
		source.PlayOneShot (earthshatterAudio, 1f);
		GameObject clone = boxcol2;
		clone.GetComponent<BoxColl2D2> ().enemy = isEnemy;
		clone.GetComponent<BoxColl2D2> ().mult = dmgMult;
		clone = Instantiate (clone, clone.transform.position, Quaternion.identity);

		int direction = pc.facingDirection;
		switch (direction) {
		case 0:
			clone.transform.position = new Vector3 (t.localPosition.x, t.localPosition.y - .3f, 0);
			break;
		case 1:
			clone.transform.position = new Vector3 (t.position.x + .2f, t.position.y, 0);
			break;
		case 2:
			clone.transform.position = new Vector3 (t.position.x, transform.position.y + .3f, 0);
			break;
		case 3:
			clone.transform.position = new Vector3 (t.position.x - .2f, t.position.y, 0);
			break;
		}


	}
	

	public void takeDamage(int dealt)  {
		hp -= dealt;
		if (hp < 0) {
			hp = 1;
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		}
	}

	public int weaponDamage() {
		return weapondmg;
	}
		
}
