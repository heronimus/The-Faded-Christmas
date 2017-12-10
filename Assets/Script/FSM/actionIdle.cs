﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionIdle : FSMAction {

	private Animation monsterAnim;
	private MonsterWandering monsterWander;
	private GameObject monsters;
	private GameObject players;

	private string finishEvent;
	private float timer;


	public actionIdle (FSMState owner) : base (owner)
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
		monsterAnim.CrossFade("idle");
		monsterWander.StopWandering ();
		monsterAnim.wrapMode= WrapMode.Loop;
		monsterAnim.CrossFade("idle_action");
	}

	public override void OnUpdate ()
	{
		if (Vector3.Distance (monsters.transform.position, players.transform.position) <= 40) {
			finishEvent = "ToWander";
			Finish ();
			return;
		}
		timer += Time.deltaTime;
		if (timer >= 30f) {
			timer = 0;
			finishEvent = "ToSleep";
			Finish ();
			return;
		}
		Debug.Log ("Dist :"+Vector3.Distance (monsters.transform.position, players.transform.position));
	}

	public override void OnExit ()
	{
		monsterAnim.wrapMode= WrapMode.Loop;
		monsterAnim.CrossFade("idle");
	}

	public void Finish ()
	{
		if (!string.IsNullOrEmpty (finishEvent)) {
			GetOwner ().SendEvent (finishEvent);
		}
	}
}