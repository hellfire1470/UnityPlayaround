using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Manager<T> {
	private static Dictionary<string, T> _managedObj = new Dictionary<string, T>();

	public static void Add(string key, T instance){
		_managedObj.Add (key, instance);
	}

	public static T Get(string key){
		if(_managedObj.ContainsKey(key)){
			return _managedObj [key];
		}
		throw new UnityException ("key not found");
	}

	public static void Remove(string key){
		if (_managedObj.ContainsKey (key)) {
			_managedObj.Remove (key);
		}
	}
}