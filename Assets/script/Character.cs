using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {
	public int x,y;
	public int Targetx,Targety;
	bool isNextLevel = true;
	static int startpos = 0;
	static int steps = 0;
	public Text textarea;
	public Text gameovertext;
	public GameObject _gameOver;
	public GameObject _restart;
	public GameObject _congrats;
	private bool gameover;
	private bool restart;

	[SerializeField]
	AstartPathFinding astartpathfind;
	[SerializeField]
	IsometricManger isometricmanger;
	[SerializeField]
	NotifyMe notifMe;

	int index = 0;
	[SerializeField]
	bool inAnimation;
	[SerializeField]
	SquareGrid mygrid;

	// Use this for initialization
	void Start () {
		_gameOver.SetActive(false);
		_restart.SetActive(false);
		_congrats.SetActive(false);

		gameovertext.text = "";

		if(Application.loadedLevelName == "Level01"){
			notifMe.Notification("Be careful where you walk");
		}

		if(Application.loadedLevelName == "Level03"){
			if(startpos == 1){
				Transform startPoint = isometricmanger.unitys[(1 * mygrid.GridSize + 0)].gameObject.transform;
				x = 1;
				y = 0;
				LeanTween.move(gameObject, startPoint, .01f);
				CheckTrigger();
			}
			if(startpos == 2)
			{
				Transform startPoint = isometricmanger.unitys[(2 * mygrid.GridSize + 4)].gameObject.transform;
				x = 2;
				y = 4;
				LeanTween.move(gameObject, startPoint, .01f);
				CheckTrigger();
			}
		}
	}

//	void Awake() {
//		DontDestroyOnLoad(_gameOver);
//		DontDestroyOnLoad(_restart);
//		DontDestroyOnLoad(_congrats);
//	}

	public List<Node> path;

	// Update is called once per frame
	void Update () {
		if(!gameover)
		{
			textarea.text = "Steps: "+steps;
			if(Input.GetMouseButtonDown(0)){
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(ray,out hit, 100f)){
					Debug.Log("Hit");
					if(hit.collider !=null){

						if(hit.collider.GetComponent<Unitys>().walkable && !inAnimation){
							string[] Row_Column = hit.collider.name.Split('_');
							Targetx = int.Parse(Row_Column[0]);
							Targety = int.Parse(Row_Column[1]);
							//Debug.Log(Targetx+","+Targety);
							astartpathfind.FindPath(x,y,Targetx,Targety);
							Movement();
						}
					}
				}

			}
		}

		if(restart){
			if(Input.GetMouseButtonDown(0)){
				//SceneManager.LoadScene(
				SceneManager.LoadScene("Level01");
				steps = 0;
			}
		}
	}

	void Movement(){
		for (int i = 0; i < path.Count; i++)
		{
			//Debug.Log(path[i].x + "," + path[i].y);
		}

		if (path != null) {
			if (index < path.Count)
			{
				Transform target = isometricmanger.unitys[(path[index].x * mygrid.GridSize + path[index].y)].gameObject.transform;

				x = path[index].x;
				y = path[index].y;

				inAnimation =true;
				LeanTween.move(gameObject, target, .16f).setOnComplete(delegate ()
					{
					//	index++;
					//	Movement();
						inAnimation =false;
						steps++;
						LoadNextLevel();
					});

				CheckTrigger();
					
			}
			else {
				inAnimation = false;
				LeanTween.cancelAll();
				index = 0;
			}

		}
	}

	void CheckTrigger(){
		if(isometricmanger.unitys[x*mygrid.GridSize+y].trigger){

			MoveTrap(isometricmanger.unitys[x*mygrid.GridSize+y].trap);

			Transform currentStanding = isometricmanger.unitys[x*mygrid.GridSize+y].gameObject.transform;
			float moveStart =currentStanding.localPosition.y-.2f;
			float moveEnd =moveStart+.2f;

			LeanTween.moveY(currentStanding.gameObject,moveStart,.3f).setDelay(.1f).setOnComplete(delegate() {
				LeanTween.moveY(currentStanding.gameObject,moveEnd,.3f);
				inAnimation = false;
			});;
		}
	}
		
	void MoveTrap(GameObject _gameobject){
		switch(_gameobject.name)
		{
			case "Box1":
			RollRock(_gameobject,9,14);
			break;
			case "Box2":
			RollRock(_gameobject,4,9);
			break;
			case "Box3":
			RollRock(_gameobject,22,21);
			break;
			case "Box4":
			RollRock(_gameobject,18,23);
			notifMe.Notification("No way to go out");
			GameOver();
			break;
			case "Box5":
			RollRock(_gameobject,3,4);
			notifMe.Notification("You blocked the door");
			GameOver();
			break;
			case "Box6":
			RollRock(_gameobject,2,7);
			break;
			case "Box7":
			RollBlock(_gameobject,20,21);
			break;
			case "Box8":
			RollRock(_gameobject,17,12);
			notifMe.Notification("You woke up the Dragon");
			GameOver();
			break;
			case "Grass1":
			RollGrass(_gameobject,12,11);
			break;
			case "Grass2":
			RollGrass(_gameobject,7,12);
			break;
			case "NextStop":
			ifBlockNextLevel();
			break;
		}
	}

	void RollRock(GameObject _gameobject,int gridTo, int gridFrom){

		Transform target = isometricmanger.unitys[gridTo].gameObject.transform;
		isometricmanger.unitys[gridTo].walkable = false;
		isometricmanger.unitys[gridFrom].walkable = true;

		LeanTween.move(_gameobject, target, .16f);
		
	}
	void RollGrass(GameObject _gameobject,int gridTo, int gridFrom){
		Transform target = isometricmanger.unitys[gridTo].gameObject.transform;
		isometricmanger.unitys[gridTo].walkable = false;
		isometricmanger.unitys[gridFrom].walkable = true;
		LeanTween.move(_gameobject, target, .16f);
	}

	void RollBlock(GameObject _gameobject,int gridTo, int gridFrom){
		Transform target = isometricmanger.unitys[gridTo].gameObject.transform;
		isometricmanger.unitys[gridTo].walkable = false;
		isometricmanger.unitys[gridFrom].walkable = true;
		LeanTween.move(_gameobject, target, .16f);
	}

	void ifBlockNextLevel(){
		isNextLevel = isometricmanger.unitys[(path[index].x * mygrid.GridSize + path[index].y)].dontAllowNext;

		if(isNextLevel){
			notifMe.Notification("Door is blocked");
		}
		else{
			notifMe.Notification("Door is open");
		}
	}

	void LoadNextLevel(){
		if(isometricmanger.unitys[(path[index].x * mygrid.GridSize + path[index].y)].name == "1_4" && Application.loadedLevelName == "Level01")
		{
			SceneManager.LoadScene("Level02");
		}
		if(isometricmanger.unitys[(path[index].x * mygrid.GridSize + path[index].y)].name == "0_2" && Application.loadedLevelName == "Level01" && !isNextLevel)
		{
			startpos = 0;
			SceneManager.LoadScene("Level03");
		}
		if(isometricmanger.unitys[(path[index].x * mygrid.GridSize + path[index].y)].name == "0_4" && Application.loadedLevelName == "Level02")
		{
			startpos = 1;
			SceneManager.LoadScene("Level03");
		}
		if(isometricmanger.unitys[(path[index].x * mygrid.GridSize + path[index].y)].name == "4_2" && Application.loadedLevelName == "Level02")
		{
			startpos = 2;
			SceneManager.LoadScene("Level03");
		}
		if(isometricmanger.unitys[(path[index].x * mygrid.GridSize + path[index].y)].name == "0_3" && Application.loadedLevelName == "Level03")
		{
			GameWon();
		}

	}
		
	void GameOver(){
//		Debug.Log("wee" + gameovertext.text);
//		gameovertext.text = "Game Over";
		_gameOver.SetActive(true);
		gameover = true;
		StartCoroutine(DelayRestart());
	}

	void GameWon(){
		//		Debug.Log("wee" + gameovertext.text);
		//		gameovertext.text = "Game Over";
		_congrats.SetActive(true);
		gameover = true;
		StartCoroutine(DelayRestart());
	}

	IEnumerator DelayRestart()
	{
		//This is a coroutine
		//DoSomething();

		yield return new WaitForSeconds(2);
		_restart.SetActive(true);
		//gameovertext.text = "Click to Restart";
		restart = true;
	}

}

