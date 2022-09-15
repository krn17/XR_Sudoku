using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JMRSDK.InputModule;
using DG.Tweening;

public class NumberField : MonoBehaviour, ISelectClickHandler
{
	Board board;
	int x1, y1, value;
	string identifier;
	public TextMeshPro numberText;

	public MeshRenderer pieceRenderer;
	public Material darkMaterial;
	public Vector2 startRotatioSpeedRange;

	bool disabled = true;

	private void Start() {
		Vector3 rotaion = transform.localRotation.eulerAngles;
		rotaion.y += 180;
		transform.DOLocalRotate(rotaion, 0f);
		float duration = Random.Range(startRotatioSpeedRange.x, startRotatioSpeedRange.y);
		rotaion.y -= 180;
		transform.DOLocalRotate(rotaion, duration).SetEase(Ease.InOutBack);
	}
	public void SetValues(int _x1, int _y1, int _value, string _identifier, Board _board) {
		x1 = _x1;
		y1 = _y1;
		value = _value;
		identifier = _identifier;
		board = _board;

		numberText.text = value != 0 ? value.ToString() : "";
		disabled = value == 0 ? false : true;

		if(value != 0) {

		}
		else {
			numberText.color = Color.cyan;
		}
	}

	public void ReceiveInput(int number) {
		value = number;
		numberText.text = value != 0 ? value.ToString() : "";

		// update riddle
		board.SetInputInRiddleGrid(x1, y1, value);
	}

	public int GetX() {
		return x1;
	}
	public int GetY() {
		return y1;
	}

	public void SetHint(int _value) {
		value = _value;
		numberText.text = value.ToString();
		numberText.color = Color.magenta;
		
		disabled = true;
	}

	public void SetDarkMaterial() {
		pieceRenderer.material = darkMaterial;
	}

	public void OnSelectClicked(SelectClickEventData eventData)
	{
		// activate number input
		if(!disabled) {
			NumberInput.instance.ActivateInputField(this);
		}
	}
}
