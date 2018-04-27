﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBehavior : MonoBehaviour
{

    public enum GameState
    {
        MAIN_MENU,
        GAME_COUNTDOWN_TO_START,
        GAME_PLAY,
        GAME_OVER
    }

    public GameState s;
    public float countDown = 3.0f;
	public int score;
	private GameObject basketballPrefab;
	private GameObject mostRecentBasketball;
	public GameObject mainMenu;
	public GameObject timeDisplay;
	public GameObject scoreDisplay;
	public GameObject countdownToStartDisplay;

	public bool isInTimedGame = true;

    // Use this for initialization
    void Start()
    {
        s = GameState.MAIN_MENU;
        Debug.Log("start");
		score = 0;
		basketballPrefab = (GameObject)Resources.Load ("Prefabs/nba_basketball", typeof (GameObject));
		respawnBasketball ();
    }

    // Update is called once per frame
    void Update()
    {
		//updates time
		GameStateMachine();
		GameLogic ();
    }

	public void GameLogic(){
		//these if and else statements just update the menus based on the game state
		//display only the start menu, hide the score and countdown displays
		if (s == GameState.MAIN_MENU) {
			mainMenu.SetActive (true);
		} 
		else {
			mainMenu.SetActive (false);
		}
		//display the countdown menu
		if (s == GameState.GAME_COUNTDOWN_TO_START) {
			countdownToStartDisplay.SetActive (true);
		} else {
			countdownToStartDisplay.SetActive (false);
		}
		//display the score and gameplay countdowns
		if (s == GameState.GAME_PLAY) {
			timeDisplay.SetActive (true);
			scoreDisplay.SetActive (true);
		} else {
			timeDisplay.SetActive (false);
			scoreDisplay.SetActive (false);
		}

	}
		
	public void GameStateMachine(){
		//start button pressed
		if (OVRInput.Get(OVRInput.Button.Start) && (s == GameState.MAIN_MENU))
		{
			Debug.Log("mode chosen");
			s = GameState.GAME_COUNTDOWN_TO_START;
		}
		else if (s == GameState.GAME_COUNTDOWN_TO_START)
		{
			Debug.Log ("countdown to start");
			countDown = countDown - Time.deltaTime;
			if (countDown <= 0.0f)
			{
				s = GameState.GAME_PLAY;
				countDown = 10.0f;
			}
		}
		else if (s == GameState.GAME_PLAY)
		{
			Debug.Log("game play");
			countDown -= Time.deltaTime;
			if (countDown <= 0.0f)
			{
				s = GameState.GAME_OVER;
				countDown = 3.0f;
			}
		}
		else if (s == GameState.GAME_OVER)
		{
			Debug.Log ("game over");
			countDown -= Time.deltaTime;
			if (countDown <= 0.0f)
			{
				s = GameState.MAIN_MENU;
				countDown = 3.0f;
			}
		}
	}

	public void incrementScore() {
		print ("Score!");
		score += 1;
	}

	private void respawnBasketball() {
		mostRecentBasketball = Instantiate (basketballPrefab, new Vector3 (0, 0.25f, 0.25f), Quaternion.identity);
	}

	public void respawnBasketball(GameObject basketballThrown) {
		if (basketballThrown != mostRecentBasketball)
			return;
		respawnBasketball ();
	}
}