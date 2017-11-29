using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMain : MonoBehaviour {


	public GameObject monsterObject;
	public GameObject player;

	private Animation monsterAnim;
	private MonsterWandering monsterWander;

	//FSM
	private FSM fsm;
	private FSMState idleState;
	private FSMState wanderState;
	private FSMState chaseState;
	private actionIdle idleAction;
	private actionWander wanderAction;


	// Use this for initialization
	void Start () {
//		monsterAnim = monsterObject.GetComponent<Animation> ();
//		monsterWander = GetComponent<MonsterWandering> ();



		fsm = new FSM ("AITest FSM");
		idleState = fsm.AddState ("IdleState");
		wanderState = fsm.AddState ("WanderState");
		idleAction = new actionIdle (idleState);
		wanderAction = new actionWander (wanderState);

		idleState.AddAction (idleAction);
		wanderState.AddAction (wanderAction);

		idleState.AddTransition ("ToWander", wanderState);
		wanderState.AddTransition ("ToIdle", idleState);

		wanderAction.Init ("ToIdle");
		idleAction.Init ("ToWander");

		fsm.Start ("WanderState");
	}
	
	// Update is called once per frame
	void Update () {
		fsm.Update ();
	}

}
