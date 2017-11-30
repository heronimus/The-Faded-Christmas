using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionChase : FSMAction {

	private Animation monsterAnim;
	private MonsterWandering monsterWander;
	private GameObject monsters;
	private GameObject players;

	private string finishEvent;
	private float timer;


	public actionChase (FSMState owner) : base (owner)
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

	}

	public override void OnUpdate ()
	{
		timer += Time.deltaTime;

		if (Vector3.Distance (monsters.transform.position, players.transform.position) >= 90) {

			monsterAnim.wrapMode= WrapMode.Loop;
			monsterAnim.CrossFade("idle_action");

			timer += Time.deltaTime;
			if (timer >= 10f) {
				timer = 0;
				Finish ();
				return;
			}
			Debug.Log ("Time : " + timer);
		} else {
			timer = 0;
			monsters.transform.position = Vector3.MoveTowards (monsters.transform.position, players.transform.position, .03f);
			monsters.transform.LookAt (players.transform);
		}

		Debug.Log ("Dist :"+Vector3.Distance (monsters.transform.position, players.transform.position));
	}

	public override void OnExit ()
	{
		monsterAnim.wrapMode= WrapMode.Loop;
		monsterAnim.CrossFade("rage");
	}

	public void Finish ()
	{
		if (!string.IsNullOrEmpty (finishEvent)) {
			GetOwner ().SendEvent (finishEvent);
		}
	}

	private IEnumerator Countdown(int time){
		while(time>0){
			Debug.Log(time--);
			yield return new WaitForSeconds(1);
		}
		Debug.Log("Countdown Complete!");
	}
}
