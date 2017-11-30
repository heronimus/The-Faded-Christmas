using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMain : MonoBehaviour {


	public GameObject monsterObject;
	private GameObject player;

	private Animation monsterAnim;
	private MonsterWandering monsterWander;

	//FSM
	private FSM fsm;
	private FSMState idleState;
	private FSMState wanderState;
	private FSMState chaseState;
	private FSMState backState;
	private FSMState sleepState;
	private actionIdle idleAction;
	private actionWander wanderAction;
	private actionChase chaseAction;
	private actionBack backAction;
	private actionSleep sleepAction;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Players");
		monsterAnim = monsterObject.GetComponent<Animation> ();
		monsterWander = GetComponent<MonsterWandering> ();


		//FiniteStateMachine for Monsters
		fsm = new FSM ("Monsters-FSM-AI");
			//define State and Action
			idleState = fsm.AddState ("IdleState");
			idleAction = new actionIdle (idleState);
			wanderState = fsm.AddState ("WanderState");
			wanderAction = new actionWander (wanderState);
			chaseState = fsm.AddState ("ChaseState");
			chaseAction = new actionChase (chaseState);
			backState = fsm.AddState ("BackState");
			backAction = new actionBack (backState);
			sleepState = fsm.AddState ("SleepState");
			sleepAction = new actionSleep (sleepState);
			
			//Add Action to State
			idleState.AddAction (idleAction);
			wanderState.AddAction (wanderAction);
			chaseState.AddAction (chaseAction);
			backState.AddAction (backAction);
			sleepState.AddAction (sleepAction);

			
			//Add Transation
			idleState.AddTransition ("ToWander", wanderState);
			idleState.AddTransition ("ToSleep", sleepState);
			wanderState.AddTransition ("ToBack", backState);
			wanderState.AddTransition ("ToChase", chaseState);
			chaseState.AddTransition ("ToWander", wanderState);
			backState.AddTransition ("ToSleep", sleepState);
			sleepState.AddTransition ("ToIdle", idleState);

			//Init Action
			wanderAction.Init (gameObject,player,monsterAnim,monsterWander,"ToChase");
			idleAction.Init (gameObject,player,monsterAnim,monsterWander,"ToWander");
			chaseAction.Init (gameObject,player,monsterAnim,monsterWander,"ToWander");
			backAction.Init (gameObject,player,monsterAnim,monsterWander,"ToSleep");
			sleepAction.Init (gameObject,player,monsterAnim,monsterWander,"ToIdle");

			//Start
			fsm.Start ("SleepState");

	}
	
	// Update is called once per frame
	void Update () {
		fsm.Update ();
	}

}
