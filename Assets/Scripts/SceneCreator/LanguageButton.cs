/* using System.Windows.Forms;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI languageButton;
    [SerializeField] TutorialPopUp tutorialPopUp;
    [SerializeField] TextMeshProUGUI RelativeText;
    [SerializeField] TextMeshProUGUI HoldText;

    public void StartLanguage()
    {
        string language = VariablesLoader.Data.language;
        SetLanguage(language);

        

    }

    public void CycleLanguage()
    {
        if (VariablesLoader.Data.language.Equals("español"))
        {
            SetLanguage("english");
        }
        else
        {
            SetLanguage("español");
        }
    }

    

    public void SetLanguage(string language)
    {

        if (language.Equals("español"))
        {
            languageButton.text = "español";
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
            VariablesLoader.Data.language = "español";
            VariablesLoader.Save();

            //Tutorial...
            TutorialPopUp.dialogs = new string[] {
                "Bienvenido al tutorial, si quieres saltartelo, pulsa 'M'. Si quieres continuar, pulsa 'C'.",//0
                "Muevete con WASD ó las flechas, mueve la camara con el ratón.",//1
                "También tienes las teclas 'Q' y 'E' para bajar y subir.",//2
                "Abre el menu principal con el 'ESPACIO'.",//3
                "Pulse el botón de abajo a la izquierda para salir del menu, tiene un icono de una pantalla y una flecha hacia abajo.",//4
                "Vuelve a abrir el menu y carga la escena predeterminada, abre el menu principal con 'ESPACIO' y pulsa el boton de abajo a la derecha \"Crear Escena\" con el lápiz y +.",//5
                "Destruye objetos con la 'Z', apunta con el puntero central al objeto y pulsa Z.",//6
                "Deshaz tus eliminaciones con 'CTRL+Z'.",//7
                "Abre el menu con 'Espacio'. Crea puertas a habitaciones nuevas con los botones de arriba. Para esconderlos pulsalos de nuevo, ROOM x:0 y:0 representa la habitacion en la que estás.",//8
                "Viaja a la nueva habitación haciendo click izquierdo en cualquier puerta creada en la escena. Ten en cuenta que tu posiciónsigue la logica de un mapa. Norte y sur el eje Y, este y oeste el eje X",//9
                "Ve al menu principal y pulsa la pestaña OBJETOS, alli se ven los objetos que tienes cargados / disponibles.",//10
                "Puedes también buscar objetos en específico en el buscador. Regresa al menu pulsando la flecha de arriba a la izquierda, o con el ESC.",//11
                "Haz click en CONFIGURACIÓN, aquí hay configuraciones extras donde puedes cambiar la carpeta de objetos o las formas de movimiento.",//12
                "¡Hagamos lo más importante, creemos nuestra propia escena!, Primero, vuelve al menu principal y copia el Prompt de la IA.",//13
                "Ve a tu navegador y utiliza la IA de tu preferencia, pégalo, y añade el tema que quieres enseñar al final. Copia la respuesta entera y pegala en el campo de la izquierda.",//14
                "Asegurese de que se ha pegado todo el mensaje. Genial, ahora que está cargado, ya puedes pulsar el boton crear la escena.",//15
                "Observa tu nueva escena, haz click izquierdo sobre cualquier objeto para acceder a un menu donde puedes ajustar dicho objeto.",//16
                "Aquí podrás ajustar la posicion, el tamaño, y la rotación, pulsa los botones o utiliza las teclas asociadas.",//17
                "Well Done!" }; //18
            tutorialPopUp.Reload();

            if (VariablesLoader.Data.movementType.Equals("relative"))
                RelativeText.text = "Relativo";
            else
                RelativeText.text = "Absoluto";

            if (VariablesLoader.Data.movementHold.Equals("hold"))
                HoldText.text = "Mantener";
            else
                HoldText.text = "No mantener";

        }
        else if (language.Equals("english"))
        {
            languageButton.text = "english";
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
            VariablesLoader.Data.language = "english";
            VariablesLoader.Save();

            //Tutorial...
            TutorialPopUp.dialogs = new string[] {
                "This is a tutorial, if you wish to skip it, please press the 'M' key. Otherwise press the 'C' key.",//0
                "You can move with WASD or the arrow keys, and look with the mouse",//1
                "You can also move with Q and E to go down and up.",//2
                "Open the menu with SPACE",//3
                "Then press the down-left icon with the screen and an arrow down to hide this menu or press 'Esc'",//4
                "Open the menu again and create the default scene, press space and click on the bottom right icon \"Create Scene\" with a plus sign.",//5
                "Now try and delete an object, point to it and press Z",//6
                "Undo your deletion with CTRL+Z",//7
                "Create a door to a new room with any of the top icons on the menu. To hide them, click on them again, The ROOM 0|0 represents on which room you are",//8
                "Travel to the next room by left clicking on any door created. Please note that doors follow cardinal directions, and the room counter follows a map count. North & South for Y axis, East & West for X axis",//9
                "Go to the menu and click on the Objects tab, this menu displays all of the objects available / loaded.",//10
                "You can search anything by typing on the input box, now return to the menu by clicking on the top-left arrow or by pressing ESC.",//11
                "Go to the menu and click on the Configuration tab, this menu displays extra settings.",//12
                "Now let's do the most important thing, create our own scene! First, go back to the main menu and copy the AI prompt",//13
                "Go to your preffered AI, paste it, and ask for anything. Copy the entire response and paste it on the ForceJson field, and click it.",//14
                "Make sure everything was copied correctly. Great, now on the main menu, create the new scene.",//15
                "Observe your new scene, left click any objects to open the Click Menu.",//16
                "Here you can click on the buttons to adjust any object, or use the referenced keys. Well Done, you completed the tutorial!",//17
                "Well Done!" }; //18
            tutorialPopUp.Reload();

            if (VariablesLoader.Data.movementType.Equals("relative"))
                RelativeText.text = "Relative";
            else
                RelativeText.text = "Absolute";
            
            if (VariablesLoader.Data.movementHold.Equals("hold"))
                HoldText.text = "Hold";
            else
                HoldText.text = "Not Hold";
            
        }

    }

} */