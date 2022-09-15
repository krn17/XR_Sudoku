using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberInput : MonoBehaviour
{
	public static NumberInput instance;
	NumberField field;
	private void Awake() {
		instance = this;
	}

	private void Start() {
		gameObject.SetActive(false);
	}

	private void Update() {
		if(UIManager.Instance.pauseMenuOpen && gameObject.activeSelf) {
			gameObject.SetActive(false);
		}
	}

	public void ActivateInputField(NumberField _field) {
		gameObject.SetActive(true);
		field = _field;
	}

	public void InputClick(int number) {
		field.ReceiveInput(number);
		
		gameObject.SetActive(false);
	}
}
