using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Board))]
public class PieceSpawner : MonoBehaviour
{
	public Transform piecesStartPos;
	public Transform boardParent;
	public GameObject piecePrefab;
	public float offsetBwPieces = 0.2f;

	public Transform A1, A2, A3, B1, B2, B3, C1, C2, C3;
	private Board board;

	private void Start() {
		board = GetComponent<Board>();
		SpawnPieces();
	}

	private void SpawnPieces() {
		Vector3 currentPos = piecesStartPos.position;

		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				GameObject numberPiece = Instantiate(piecePrefab, currentPos, Quaternion.identity);
				NumberField numberField = numberPiece.GetComponent<NumberField>();
				numberField.SetValues(i, j, board.GetRiddleGrid()[i, j], i + "," + j, board);
				numberPiece.name =  i + "," + j;

				if(board.GetRiddleGrid()[i, j] == 0) {
					board.fieldList.Add(numberField);
				}
				// A1
				if(i < 3 && j < 3) {
					numberPiece.transform.SetParent(A1);
				}
				// A2
				else if(i < 3 && j > 2 && j < 6) {
					numberPiece.transform.SetParent(A2);
					numberField.SetDarkMaterial();
				}
				// A3
				else if(i < 3 && j > 5) {
					numberPiece.transform.SetParent(A3);
				}
				// B1
				else if( i > 2 && i < 6 && j < 3) {
					numberPiece.transform.SetParent(B1);
					numberField.SetDarkMaterial();
				}
				// B2
				else if( i > 2 && i < 6 && j > 2 && j < 6) {
					numberPiece.transform.SetParent(B2);
				}
				// B3
				else if(i > 2 && i < 6 && j > 5) {
					numberPiece.transform.SetParent(B3);
					numberField.SetDarkMaterial();
				}
				// C1
				else if( i > 5 && j < 3) {
					numberPiece.transform.SetParent(C1);
				}
				// C2
				else if(  i > 5 && j > 2 && j < 6) {
					numberPiece.transform.SetParent(C2);
					numberField.SetDarkMaterial();
				}
				// C3
				else if( i > 5 && j > 5) {
					numberPiece.transform.SetParent(C3);
				}


				currentPos.x += offsetBwPieces;
			}
			currentPos.y -= offsetBwPieces;
			currentPos.x = piecesStartPos.position.x;
		}
	}
}
