using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoOnMainThread : MonoBehaviour {

	public static Queue<System.Action> ExecuteOnMainThread = new Queue<System.Action> ();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		while (ExecuteOnMainThread.Count > 0) {
			ExecuteOnMainThread.Dequeue ().Invoke ();
		}
	}
}
