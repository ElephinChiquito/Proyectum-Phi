/* using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

//Falta poner que se actualice Variables.Data.tutorialPlayed = true;

public class TutorialPopUp : MonoBehaviour
{
	[SerializeField] private GameObject tutorialCanvasReference; // arrástralo en el inspector
	[SerializeField] Image buttonCreate;
	[SerializeField] Image buttonView;
	[SerializeField] Image buttonDoor;
	[SerializeField] Image roomCounter;
	[SerializeField] Image objectsMenu;
	[SerializeField] Image auxMenu;
	[SerializeField] Image copyAIPrompt;
	[SerializeField] Image forceJson;
	[SerializeField] Image backAuxMenu;


	public static GameObject tutorialCanvas;
	[SerializeField] public TextMeshProUGUI tutorialText;
	public static int step = 0;
	private bool firstTimeCreatePushed = false;
	public void SetFirstTimeCreatePushedTrue(){ if(step == 5 || step == 15) firstTimeCreatePushed = true; }
	private static bool firstTimeViewPushed = false;
	public void SetFirstTimeViewPushedTrue() { if(step == 4) firstTimeViewPushed = true; }
	private static bool firstTimeZPushed = false;
	public static void SetFirstTimeZPushedTrue() { if(step == 6) firstTimeZPushed = true; }
	private static bool firstTimeCtrlZPushed = false;
	public static void SetFirstTimeCtrlZPushedTrue() { if(step == 7) firstTimeCtrlZPushed = true; }
	private static bool firstTimeDoorPushed = false;
	public void SetFirstTimeDoorPushedTrue() { if(step == 8) firstTimeDoorPushed = true; }
	public static bool firstTimeTravel = false;
	public static void SetFirstTimeTravelTrue() { if(step == 9) firstTimeTravel = true; }
	public bool firstTimeObjectsPushed = false;
	public void SetFirstTimeObjectsPushedTrue() { if(step == 10) firstTimeObjectsPushed = true; }
	public bool firstTimeBackAuxButton = false;
	public void SetFirstTimeBackAuxButtonTrue() { if(step == 11) firstTimeBackAuxButton = true; }
	public bool firstTimeConfigPushed = false;
	public void SetFirstTimeConfigPushedTrue() { if(step == 12) firstTimeConfigPushed = true; }
	public bool firstTimeCopyPushed = false;
	public void SetFirstTimeCopyPushedTrue() { if(step == 13) firstTimeCopyPushed = true; }
	public bool firstTimeForceJsonPushed = false;
	public void SetFirstTimeForceJsonPushedTrue() { if(step == 14) firstTimeForceJsonPushed = true; }
	public static bool firstTimeClickMenuOpen = false;
	public static void SetFirstTimeClickMenuOpenTrue() { if(step == 16) firstTimeClickMenuOpen = true; }
	public static bool firstTimeClickMenuClosed = false;
	public static void SetFirstTimeClickMenuClosedTrue() { if(step == 17) firstTimeClickMenuClosed = true; }


	public static string[] dialogs;

	void Awake()
	{
		// Copiamos la referencia serializada al campo estático
		tutorialCanvas = tutorialCanvasReference;
	}

	void Start()
	{
		tutorialText.text = dialogs[step];
	}

	public void EndTutorial()
	{
		Debug.Log("Tutorial skipped");
		tutorialCanvas.SetActive(false);
		VariablesLoader.Data.tutorialPlayed = true;
		VariablesLoader.Save();
	}

	public void RestartTutorial()
	{
		VariablesLoader.Data.tutorialPlayed = false;
		VariablesLoader.Save();

		step = 0;
		Wait(3);

		Reload();
		tutorialCanvas.SetActive(true);
	}
	
	public IEnumerator Wait(int x)
    {
        for (int i = 0; i < x; i++)
        	yield return null;
    }

	public void Reload()
	{
		tutorialText.text = dialogs[step];
	}

	public void ContinueTutorial()
	{
		switch (step)
		{
			case 0:
				if (Input.GetKeyDown(KeyCode.C))
				{
					NextDialog();
				}
				break;

			case 1:
				if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
				{
					NextDialog();
				}
				break;
			case 2:
				if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
				{
					NextDialog();
				}
				break;
			case 3:
				if (Input.GetKeyDown(KeyCode.Space))
				{
					NextDialog();
					StartCoroutine(MakeButtonNotice(buttonView, new Color(0.1647f, 0.2039f, 0.3137f), Color.red));
				}
				break;

			case 4:
				if (firstTimeViewPushed)
				{
					firstTimeViewPushed = false;
					NextDialog();
					StartCoroutine(MakeButtonNotice(buttonCreate, new Color(0.1647f, 0.2039f, 0.3137f), Color.red));
				}
				break;

			case 5:
				if (firstTimeCreatePushed)
				{
					firstTimeCreatePushed = false;
					firstTimeViewPushed = false;
					NextDialog();
				}
				break;
			case 6:
				if (firstTimeZPushed)
				{
					NextDialog();
				}
				break;
			case 7:
				if (firstTimeCtrlZPushed)
				{
					NextDialog();
					StartCoroutine(MakeButtonNotice(buttonDoor, new Color(0.1647f, 0.2039f, 0.3137f), Color.red));
					StartCoroutine(MakeButtonNotice(roomCounter, new Color(0.1647f, 0.2039f, 0.3137f), Color.red));
				}
				break;
			case 8:
				if (firstTimeDoorPushed)
				{
					NextDialog();
				}
				break;
			case 9:
				if (firstTimeTravel)
				{
					NextDialog();
					StartCoroutine(MakeButtonNotice(objectsMenu, new Color(0.1647f, 0.2039f, 0.3137f), Color.red));
				}
				break;
			case 10:
				if (firstTimeObjectsPushed)
				{
					NextDialog();
					StartCoroutine(MakeButtonNotice(backAuxMenu, new Color(0.1647f, 0.2039f, 0.3137f), Color.red));
				}
				break;
			case 11:
				if (firstTimeBackAuxButton)
				{
					NextDialog();
					StartCoroutine(MakeButtonNotice(auxMenu, new Color(0.1647f, 0.2039f, 0.3137f), Color.red));
				}
				break;
			case 12:
				if (firstTimeConfigPushed)
				{
					NextDialog();
					StartCoroutine(MakeButtonNotice(copyAIPrompt, new Color(0.1647f, 0.2039f, 0.3137f), Color.red));
				}
				break;
			case 13:
				if (firstTimeCopyPushed)
				{
					NextDialog();
					StartCoroutine(MakeButtonNotice(forceJson, new Color(0.1647f, 0.2039f, 0.3137f), Color.red));
				}
				break;
			case 14:
				if (firstTimeForceJsonPushed)
				{
					NextDialog();
					StartCoroutine(MakeButtonNotice(backAuxMenu, new Color(0.1647f, 0.2039f, 0.3137f), Color.red));
					StartCoroutine(MakeButtonNotice(buttonCreate, new Color(0.1647f, 0.2039f, 0.3137f), Color.red));
					firstTimeCreatePushed = false;
				}
				break;
			case 15:
				if (firstTimeCreatePushed)
				{
					NextDialog();
				}
				break;
			case 16:
				if (firstTimeClickMenuOpen)
				{
					NextDialog();
				}
				break;
			case 17:
				if (firstTimeClickMenuClosed)
				{
					NextDialog();
				}
				break;
			default:
				EndTutorial();
				break;
		}
	}

	public void NextDialog()
	{
		Debug.Log("Next step: " + (step + 1));
		step = step + 1;
		Reload();
	}

	private IEnumerator MakeButtonNotice(Image image, Color background, Color highlight)
	{
		int currentStep = step;
		while (step == currentStep)
		{
			if (image.gameObject.activeInHierarchy)
			{
				image.color = highlight;
				yield return new WaitForSecondsRealtime(0.66f);

				image.color = background;
				yield return new WaitForSecondsRealtime(0.66f);
			}
			else { yield return null; }
		}
		image.color = background;
	}
}
 */