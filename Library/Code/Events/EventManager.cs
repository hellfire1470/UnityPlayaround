using System.Collections.Generic;

public static class EventManager<T>
{
	private class IdentInstancePair
	{
		public string Identifier;
		public T Instance;
	}

	private class Trigger
	{
		public string triggerIdentifier;
		public string triggerMethod;

		public object instance;
		public string method;
		public object[] parameters;
		public bool forwardParams = false;
	}

	private static List<T> _listAll = new List<T>();
	private static List<IdentInstancePair> _listFor = new List<IdentInstancePair>();
	private static List<Trigger> _listWhen = new List<Trigger>();


	public static void Register(T instance, string identifier = null)
	{
		if(_listAll.Contains(instance)){
			throw new System.Exception ("instance already exists");
		}
		_listAll.Add(instance);
		if(identifier != null)
		{
			if(_listFor.Find(iInstancePair => iInstancePair.Identifier == identifier) != null)
			{
				throw new System.Exception("identifier already exists");
			}
			_listFor.Add(new IdentInstancePair
				{
					Identifier = identifier,
					Instance = instance
				});
		}
	}

	public static List<T> List
	{
		get{
			return _listAll;
		}
	}

	public static void Unregister(T instance)
	{
		_listAll.Remove(instance);
		_listFor.RemoveAll(identInstancePair => EqualityComparer<T>.Default.Equals(identInstancePair.Instance, instance));
		_listWhen.RemoveAll(trigger => EqualityComparer<object>.Default.Equals(trigger.instance, instance));
	}

	public static void Execute(string method, object[] parameters = null)
	{
		foreach (T instance in _listAll)
		{
			instance.GetType().GetMethod(method).Invoke(instance, parameters);
		}

		foreach (Trigger trigger in _listWhen) {
			if (trigger.triggerIdentifier != null) {
				List<IdentInstancePair> pairList = _listFor.FindAll (m => m.Identifier == trigger.triggerIdentifier);
				foreach (IdentInstancePair pair in pairList) {
					if (trigger.forwardParams) {
						_InvokeTrigger (trigger, parameters);
					} else {
						_InvokeTrigger (trigger);
					}
				}
			} else {
				if (trigger.forwardParams) {
					_InvokeTrigger (trigger, parameters);
				} else {
					_InvokeTrigger (trigger);
				}
			}
		}
	}

	public static void ExecuteFor(string identifier, string method, object[] parameters = null)
	{
		T instance = _listFor.Find(iInstancePair => iInstancePair.Identifier == identifier).Instance;
		instance.GetType().GetMethod(method).Invoke(instance, parameters);


		foreach (Trigger trigger in _listWhen) {
			if (trigger.triggerIdentifier != null) {
				if (trigger.triggerIdentifier == identifier) {
					if (trigger.triggerMethod == method) {
						if (trigger.forwardParams) {
							_InvokeTrigger (trigger, parameters);
						} else {
							_InvokeTrigger (trigger);
						}
					}
				}
			} else {
				if (trigger.forwardParams) {
					_InvokeTrigger (trigger, parameters);
				} else {
					_InvokeTrigger (trigger);
				}
			}
		}
	}

	private static void _InvokeTrigger(Trigger trigger){
		trigger.instance.GetType ().GetMethod (trigger.method).Invoke (trigger.instance, trigger.parameters);
	}
	private static void _InvokeTrigger(Trigger trigger, object[] parameters){
		trigger.instance.GetType ().GetMethod (trigger.method).Invoke (trigger.instance, parameters);
	}

	public static void RegisterTrigger<C>(string triggerIdentifier, string triggerMethod, C instance, string method, object[] parameters = null) {
		Trigger trigger = new Trigger {
			triggerIdentifier = triggerIdentifier,
			triggerMethod = triggerMethod,
			instance = instance,
			method = method,
			parameters = parameters
		};
		_listWhen.Add (trigger);
	}

	public static void RegisterTrigger<C>(string triggerMethod, C instance, string method, object[] parameters = null){
		Trigger trigger = new Trigger {
			triggerMethod = triggerMethod,
			instance = instance,
			method = method,
			parameters = parameters
		};
		_listWhen.Add (trigger);
	}

	public static void RegisterTrigger<C>(string triggerMethod, C instance, string method, bool forwardParams){
		Trigger trigger = new Trigger {
			triggerMethod = triggerMethod,
			instance = instance,
			method = method,
			forwardParams = forwardParams
		};
		_listWhen.Add (trigger);
	}

	public static void RegisterTrigger<C>(string triggerIdentifier, string triggerMethod, C instance, string method, bool forwardParams) {
		Trigger trigger = new Trigger {
			triggerIdentifier = triggerIdentifier,
			triggerMethod = triggerMethod,
			instance = instance,
			method = method,
			forwardParams = forwardParams
		};
		_listWhen.Add (trigger);
	}
}