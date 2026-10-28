using System;
using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GeneradorDeEscena3D generador;
    public FlyCamera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    /// <summary>
    /// Primero: PermanentData -> VariablesLoader
    /// Segundo: -> FolderPicker -> PermanentData
    /// Tercero: FolderPicker + folderP.DisplayText-> CustomObjectsLoader (Coroutine)
    /// Cuarto: CustomObjectsLoader.StartCallFromChatSample(this);
    /// Quinto.1: ChatSample
    /// Quinto.2: Generador de escena 3d
    /// </summary>
    private bool buttonPressedFolderPick = false;
    [SerializeField] public GameObject LoadingScreen;
    [SerializeField] public GameObject Crosshair;
    [SerializeField] public GameObject ErrorScreen;
    [SerializeField] public RoomSorroundings roomSorroundings;


/*     [SerializeField] public LanguageButton languageButton;
    [SerializeField] public ClickMenu clickMenu;*/
    void Start()
    {
        //Poner en activo las pantallas necesarias, y desactivadas.
        cam.transform.root.gameObject.SetActive(false);
        LoadingScreen.SetActive(true);
        Crosshair.SetActive(true);
        ErrorScreen.SetActive(false);

        VariablesLoader.LoadOrCreate();
        if (!Directory.Exists(VariablesLoader.rootToRead))
        {
            Debug.Log("La carpeta no existe: " + VariablesLoader.rootToRead);
            cam.transform.root.gameObject.SetActive(true);
            LoadingScreen.SetActive(false);
            ErrorScreen.SetActive(true);
            WaitForSeconds a = new WaitForSeconds(60);
            return;
        }
        cam.transform.root.gameObject.SetActive(true);


        if (VariablesLoader.rootToRead == null)
        {
            Debug.LogError("RootToRead is missing, this is due to there not being a Proyectum Delta folder un Persistant Data Path");
        }

        /* buttonPressedFolderPick = VariablesLoader.Data.folderPicked; */
        /* if (!buttonPressedFolderPick)
        {
            StartCoroutine(DoTutorial());
        }
        else
        {
            tutorialScreen.gameObject.SetActive(false);
            Step2();
        } */
        Step2();

        /* Esto va desp de que se cree VariablesLoader */
        /* if (!VariablesLoader.Data.tutorialPlayed)
            Tutorial.SetActive(true);
        else Tutorial.SetActive(false); */

        /* Esto va desp de que se cree VariablesLoader */
        /* languageButton.StartLanguage();
        clickMenu.StartClickMenu(); */
        roomSorroundings.setSorroundingRoom(VariablesLoader.Data.roomNumber);
        
    }

    private IEnumerator DoTutorial()
    {
        // Bucle que espera a que la variable cambie
        while (!buttonPressedFolderPick)
        {
            yield return null; // espera un frame
        }
        // Continúa después de la pulsación
        Debug.Log("Botón pulsado, continúa el tutorial");
        Step2();
    }

    public void OnFolderPickButtonPressed()
    {
        buttonPressedFolderPick = true;
    }

    void Step2()
    {
        FolderPicker.InitializeForGettingFolderPath();
        /* FolderPicker.DisplayPathOnText(); */
        cam.StartFlyCamera();

        StartCoroutine(LoadThenStartSystems());
    }

    private IEnumerator LoadThenStartSystems()
    {
        CustomObjectsLoader.loadingBar = GameObject.Find("LoadingBar").GetComponent<Slider>();
        CustomObjectsLoader.loadingText = GameObject.Find("LoadingText").GetComponent<TextMeshProUGUI>();
        yield return StartCoroutine(CustomObjectsLoader.LoadAllCustomObjectsCoroutine());

        // Esperamos explícitamente a que isLoadingComplete se active
        while (!CustomObjectsLoader.isLoadingComplete)
            yield return null;
        Debug.Log("Carga completa confirmada. Iniciando sistemas dependientes..." + string.Join(", ", CustomObjectsLoader.prefabCache));

        yield return StartCoroutine(generador.EsperarCargaYUsar());
    }
}
