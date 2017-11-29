using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionWander : FSMAction {

	private Animation monsterAnim;
	private MonsterWandering monsterWander;
	private GameObject monsters;
	private GameObject players;

	private string finishEvent;

	public actionWander (FSMState owner) : base (owner)
	{
	}

	public void Init (string finishEvent)
	{
		monsters = GameObject.FindGameObjectWithTag ("Monsters");
		players = GameObject.FindGameObjectWithTag ("Players");
		monsterAnim = GameObject.FindGameObjectWithTag("MonstersAnim").GetComponent<Animation> ();
		monsterWander = monsters.GetComponent<MonsterWandering> ();

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
