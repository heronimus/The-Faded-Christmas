using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionWander : FSMAction {

	private Animation monsterAnim;
	private MonsterWandering monsterWander;
	private GameObject monsters;
	private GameObject players;

	private string finishEvent;
	private float timer;

	public actionWander (FSMState owner) : base (owner)
	{
	}

	public void Init (GameObject monsters, GameObject players, Animation monsterAnim, MonsterWandering monsterWander, string finishEvent)
	{
		this.monsters = monsters;
		this.players = players;
		this.monsterAnim = monsterAnim;
		this.monsterWander = monsterWander;

		this.finishEvent = finishEvent;
	}

	public override void OnEnter ()
	{
		monsterAnim.wrapMode= WrapMode.Loop;
		monsterAnim.CrossFade("walk");
		monsterWander.StartWandering ();
	}

	public override void OnUpdate ()
	{
		if (Vector3.Distance (monsters.transform.position, players.transform.position) <= 25) {
			finishEvent = "ToChase";
			Finish ();
			return;
		}

		timer += Time.deltaTime;
		if (timer >= 30f) {
			timer = 0;
			finishEvent = "ToBack";
			Finish ();
			return;
		}

		Debug.Log ("Timer w : " + timer);
		Debug.Log (Vector3.Distance (monsters.transform.position, players.transform.position));
	}

	public override void OnExit ()
	{

	}

	public void Finish ()
	{
		if (!string.IsNullOrEmpty (finishEvent)) {
			GetOwner ().SendEvent (finishEvent);
		}
	}
}
