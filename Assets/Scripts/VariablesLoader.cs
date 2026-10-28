using System.IO;
using UnityEngine;
[System.Serializable]
public class PermanentData
{
	public bool folderPicked;
	public string folderPath;

	//La idea es, que al principio del todo ponga el tutorial, que cuando lo completes se ponga como true.
	//Y que si lo quiere volver a hacer, igual q con lo de folder, ponerlo en false, y reiniciar la ventana.
	public bool tutorialPlayed;
	public string language;
	public string movementType;
	public string movementHold;
	public bool freeCamera;
	public int roomNumber;


	public static PermanentData GenerateFirstInstance()
	{
		return new PermanentData
		{
			folderPicked = false,
			folderPath = Application.persistentDataPath, //Default is the folder whereever you get other permanent jsons stored
			tutorialPlayed = false,
			language = "español",
			movementType = "relative",
			movementHold = "not hold",
			freeCamera = true,
			roomNumber = 0
		};
	} 
}
public class VariablesLoader
{
	private static string parentDir = Directory.GetParent(Application.persistentDataPath).FullName;
	public static string folderPath = Path.Combine(parentDir, "Proyectum Delta");
	public static string rootToRead = Path.Combine(parentDir, "Proyectum Delta");

	public static string filePath = Path.Combine(rootToRead, "PermanentInfo\\VariablesLoader.json");
	
	public static PermanentData Data;

	public static void LoadOrCreate()
	{
		Debug.Log("parentDir = " + parentDir + ", rootToRead = " + rootToRead + ", filePath = " + filePath);


		/* Directory.CreateDirectory(folderPath);*/

		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);
			Data = JsonUtility.FromJson<PermanentData>(json);
			Debug.Log("Archivo de variables permanentes cargado correctamente. \n " + json);

			folderPath = Data.folderPath;
			filePath = Path.Combine(rootToRead, "PermanentInfo\\VariablesLoader.json");

			Debug.Log("Usando folderPath del JSON: " + folderPath);
		}
		else
		{
			Data = PermanentData.GenerateFirstInstance();
			Save(); // Guardar los valores por defecto
			Debug.Log("Archivo de variables permanentes creado por primera vez.");
		}

	}

	public static void Save()
	{
		string json = JsonUtility.ToJson(Data, true);
		File.WriteAllText(filePath, json);
		Debug.Log("Archivo de variables permanentes guardado.");
	}

/*	public static bool IsEnglish()
	{
		if (Data.language.Equals("español"))
			return false;
		else
			return true;
	}

	public static void ChangeAccordingToLanguage(TextMeshProUGUI TMP, string english, string español)
	{
		if (IsEnglish())
			TMP.text = english;
		else
			TMP.text = español;
	} */

	
}
