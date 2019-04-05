using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveableFrame : MonoBehaviour, IDragHandler {


	[SerializeField] GameObject _objectToMove;


	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		
		_objectToMove.transform.position = eventData.position;
	}
	#endregion

}
