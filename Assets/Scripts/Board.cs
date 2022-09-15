using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty {
		DEBUG,
		EASY,
		MEDIUM,
		HARD,
		INSANE
	}
public class Board : MonoBehaviour
{
	
	int[,] solvedGrid = new int[9, 9];
	int[,] riddleGrid = new int[9, 9];
	int piecesToRemove = 35;
	string s;

	public Difficulty difficulty;
	public bool debugDifficulty = false;

	[HideInInspector]
	public List<NumberField> fieldList = new List<NumberField>();
	private int maxHints = 3;
	private int currHints;

	void Awake()
	{
		

		InitGrid(ref solvedGrid);

		ShuffleGrid(ref solvedGrid, 10);
		CreateRiddleGrid();
	}

	void InitGrid(ref int[,] grid) {
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				grid[i, j] = (i * 3 + i / 3 + j) % 9 + 1;
			}
		}
	}

	void CreateRiddleGrid() {
		// copy solved grid
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				riddleGrid[i, j] = solvedGrid[i, j];
			}
		}

		// set difficulty
		SetDifficulty();

		// remove from riddle grid
		for (int i = 0; i < piecesToRemove; i++)
		{
			int x1 = Random.Range(0, 9);
			int y1 = Random.Range(0, 9);

			// reroll untill we find one without 0
			while(riddleGrid[x1, y1] == 0) {
				x1 = Random.Range(0, 9);
				y1 = Random.Range(0, 9);
			}

			riddleGrid[x1, y1] = 0;
		}
		DebugGrid(ref riddleGrid);
	}

	void DebugGrid(ref int[,] grid) {
		s = "";
		int sep = 0;
		for (int i = 0; i < 9; i++)
		{
			s += "|";
			for (int j = 0; j < 9; j++)
			{
				s += grid[i, j].ToString();

				sep = j % 3;
				if(sep == 2) {
					s += "|";
				}
			}
			s += "\n";
		}
		Debug.Log(s);
	}

	void ShuffleGrid(ref int[,] grid, int shuffleAmt) {
		for (int i = 0; i < shuffleAmt; i++)
		{
			int val1 = Random.Range(1, 10);
			int val2 = Random.Range(1, 10);

			// mix grid cells
			MixTwoGridCells(ref grid, val1, val2);
		}
		DebugGrid(ref grid);
	}

	void MixTwoGridCells(ref int[,] grid, int val1, int val2) {
		int x1 = 0;
		int x2 = 0;
		int y1 = 0;
		int y2 = 0;

		for (int i = 0; i < 9; i+=3)
		{
			for (int k = 0; k < 9; k+=3)
			{
				for (int j = 0; j < 3; j++)
				{
					for (int l = 0; l < 3; l++)
					{
						if(grid[i+j, k+l] == val1) {
							x1 = i + j;
							y1 = k + l;
						}

						if(grid[i+j, k+l] == val2) {
							x2 = i + j;
							y2 = k + l;
						}
					}
				}
				grid[x1, y1] = val2;
				grid[x2, y2] = val1;
			}
		}
	}

	public int[,] GetRiddleGrid() {
		return riddleGrid;
	}

	public void SetInputInRiddleGrid(int x, int y, int value) {
		riddleGrid[x, y] = value;
	}

	public void CheckComplete() {
		if(CheckIfWon()) {
			Debug.Log("You Won!");
			UIManager.Instance.ShowWinPanel();
		}
		else {
			UIManager.Instance.ShowTryAgainText();
			Debug.Log("Try again");
		}
	}

	bool CheckIfWon() {
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				if(riddleGrid[i, j] != solvedGrid[i, j])
					return false;
			}
		}
		return true;
	}

	public void ShowHint() {
		if(fieldList.Count > 0 && currHints > 0) {
			int randIndex = Random.Range(0, fieldList.Count);
			currHints--;

			int cellX = fieldList[randIndex].GetX();
			int cellY = fieldList[randIndex].GetY();
			riddleGrid[cellX, cellY] = solvedGrid[cellX, cellY];

			fieldList[randIndex].SetHint(riddleGrid[cellX, cellY]);
			fieldList.RemoveAt(randIndex);
		}
		else {
			Debug.Log("No hints left");
		}
	}

	void SetDifficulty() {
		difficulty = (Difficulty)System.Enum.Parse( typeof(Difficulty), PlayerPrefs.GetString("game_difficulty", "EASY") );
		// Debug difficulty
		if(debugDifficulty && Application.isEditor) {
			difficulty = Difficulty.DEBUG;
		}
		switch(difficulty) {
			case Difficulty.DEBUG:
				piecesToRemove = 5;
				maxHints = 2;
				currHints = maxHints;
				break;
			case Difficulty.EASY:
				piecesToRemove = 35;
				maxHints = 4;
				currHints = maxHints;
				break;
			case Difficulty.MEDIUM:
				piecesToRemove = 40;
				maxHints = 6;
				currHints = maxHints;
				break;
			case Difficulty.HARD:
				piecesToRemove = 45;
				maxHints = 8;
				currHints = maxHints;
				break;
			case Difficulty.INSANE:
				piecesToRemove = 55;
				maxHints = 10;
				currHints = maxHints;
				break;
			default:
				piecesToRemove = 35;
				maxHints = 2;
				currHints = maxHints;
				break;
		}
	}

	public int GetCurrentHints() {
		return currHints;
	}
	public int GetMaxHints() {
		return maxHints;
	}
}
