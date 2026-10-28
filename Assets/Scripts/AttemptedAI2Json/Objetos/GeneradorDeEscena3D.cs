using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GeneradorDeEscena3D : MonoBehaviour
{

    public static string[] mainObjects;
    public static string[] customObjects;
    public static List<GameObject> customList;
    public GameObject[] npc;

    public static bool escenaInicialCargada = false;


    public System.Collections.IEnumerator EsperarCargaYUsar()
    {
        while (!CustomObjectsLoader.isLoadingComplete)
            yield return null;

        Debug.Log("Loading custom objects list on start for generador de escenas 3d");
        customList = CustomObjectsLoader.prefabCache;
        // Cargar todos los GameObject (prefabs) de la carpeta Resources/Objetos3D/
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Objetos3D");
        GameObject[] customPrefabs = customList.ToArray();

        mainObjects = prefabs.Select(p => p.name).ToArray();
        customObjects = customPrefabs.Select(p => p.name).ToArray();


        Debug.Log(Path.Combine(VariablesLoader.rootToRead, "escena " + Map2D.x + "_" + Map2D.y + ".json") + "Existe? " + File.Exists(Path.Combine(VariablesLoader.rootToRead, "escena " + Map2D.x + "_" + Map2D.y + ".json")));
        if (File.Exists(Path.Combine(VariablesLoader.rootToRead, "escena " + Map2D.x + "_" + Map2D.y + ".json")))
        {
            Debug.Log("VOY A GENERAR LA ESCENA 0_0");
            CrearEscena(0, 0);
        }

        escenaInicialCargada = true;

    }

    public void Comenzar()
    {

        // Buscar el fichero más reciente
        string directorio = VariablesLoader.rootToRead;
        string[] archivos = Directory.GetFiles(directorio, "historial_ia_*.json");

        if (archivos.Length == 0)
        {
            Debug.LogWarning("No se encontraron ficheros de escena en: " + directorio);
            GestorHistorialIA.GuardarRespuestaForzada(@"{
    ""objects"": [
        {
            ""type"": ""npc"",
            ""position"": {
                ""x"": -2.0,
                ""y"": 0.0,
                ""z"": -2.5
            },
            ""scale"": 1.0,
            ""rotation"": ""north"",
            ""dialog"": ""El jabalí es fuerte y territorial. Sus colmillos lo hacen peligroso y su comportamiento impredecible exige precaución. Su caza enseña la importancia de conocer el comportamiento animal y actuar con responsabilidad.""
        }
    ]
}");
            return;
        }

        // Ordenamos por fecha de creaci�n y tomamos el �ltimo
        string ruta = archivos.OrderByDescending(File.GetCreationTime).First();

        Debug.Log("Cargando fichero: " + ruta);

        string json = File.ReadAllText(ruta);
        Escena3D escena = JsonUtility.FromJson<Escena3D>(json);

        if (escena == null || escena.objects == null)
        {
            Debug.LogError("Error: El JSON está vacío o mal formado: " + ruta);
            return;
        }

        foreach (Objeto3D objeto in escena.objects)
        {
            Debug.Log($"Creando objeto: {objeto.type} en {objeto.position}");
            CrearObjeto(objeto);
        }

        SaveScene();
        Debug.Log("Escena generada con " + escena.objects.Count + " objects.");
    }

    public static void CrearEscena(int x, int y)
    {
        string ruta = Path.Combine(VariablesLoader.rootToRead, "escena " + x + "_" + y + ".json");

        Debug.Log("Cargando fichero: " + ruta);

        string json = File.ReadAllText(ruta);
        Escena3D escena = JsonUtility.FromJson<Escena3D>(json);

        foreach (Objeto3D objeto in escena.objects)
        {
            Debug.Log($"Creando objeto: {objeto.type} en {objeto.position}");
            CrearObjeto(objeto);
        }

        Debug.Log("Escena generada con " + escena.objects.Count + " objects.");
    }

    public static void CrearObjeto(Objeto3D objeto)
    {
        GameObject go = null;

        if (mainObjects.Contains(objeto.type.ToLower()))
        {
            GameObject prefab = Resources.Load<GameObject>("Objetos3D/" + objeto.type.ToLower());

            if (prefab == null)
            {
                Debug.LogWarning("Prefab no encontrado para: " + objeto.type);
                return;
            }

            go = Instantiate(prefab);
            if (objeto.type.ToLower().Equals("npc"))
            {
                NpcDialog dialogoScript = go.GetComponent<NpcDialog>();
                if (dialogoScript != null && !string.IsNullOrEmpty(objeto.dialog))
                {
                    dialogoScript.textoDialogo = objeto.dialog;
                }
            }
        }

        //Aqui creo que se podria cambiar a minus objeto.type, habria que cambiar tamb la lista customObjects a minus.
        else if (customObjects.Contains(objeto.type))
        {
            GameObject prefab = null;

            foreach (GameObject customGO in customList)
            {
                if (customGO.name.Equals(objeto.type))
                {
                    prefab = customGO;
                    break;
                }
            }

            if (prefab == null)
            {
                Debug.LogWarning("Prefab no encontrado para: " + objeto.type);
                return;
            }

            Rigidbody rb = prefab.GetComponent<Rigidbody>();
            if(rb != null && !objeto.physics)
            {
                rb.useGravity = false;
                rb.isKinematic = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                
                PhysicsVariable pv = prefab.AddComponent<PhysicsVariable>();
                pv.SetValue(false);
            }
            else if(rb != null && objeto.physics)
            {
                PhysicsVariable pv = prefab.AddComponent<PhysicsVariable>();
                pv.SetValue(true);
            }
            go = Instantiate(prefab);
            go.SetActive(true);
        }

        else
        {
            Debug.LogWarning("type de objeto no reconocido: " + objeto.type);
            return;
        }

        go.transform.position = objeto.position.ToVector3();

        go.transform.localScale = new Vector3(objeto.scale, objeto.scale, objeto.scale);

        go.transform.rotation = objeto.rotation;
        
    }

    public static void SaveScene()///
    {
        GameObject[] todos = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        Escena3D copy = new Escena3D();

        foreach (GameObject obj in todos)
        {
            if (obj.name.Contains("Clone"))
            {
                Objeto3D aux = Transform2Objeto3D(obj);
                copy.objects.Add(aux);
            }
        }

        string json = JsonUtility.ToJson(copy, true);
        string ruta = Path.Combine(Application.persistentDataPath, "escena " + Map2D.x + "_" + Map2D.y + ".json");
        File.WriteAllText(ruta, json);
        Debug.Log("Escena guardada en: " + ruta + "En la habitacion: " + "escena " + Map2D.x + "_" + Map2D.y + ".json");
    }

    public static Objeto3D Transform2Objeto3D(GameObject obj)
    {
        Objeto3D aux = new Objeto3D();
        aux.type = obj.name.Replace("(Clone)", "").Trim();

        aux.position = new Vector3Data
        (
            obj.transform.position.x,
            obj.transform.position.y,
            obj.transform.position.z
        );

        aux.scale = obj.transform.localScale.x;

        //aqui habia un problema al ser primero: la rotacion esta mal calculada pq los quaternion son entre -1 y 1
        // segundo: si el objeto tiene fisicas se movera y rotara por tanto no sirve con comprobar cuatro casos especificos.

        aux.rotation = obj.transform.rotation;
            


        if (obj.name.Contains("npc"))
        {
            aux.dialog = obj.GetComponent<NpcDialog>().textoDialogo;
        }

        PhysicsVariable pv = obj.GetComponent<PhysicsVariable>();
		if (pv != null) { aux.physics = pv.GetValue(); }
		else { aux.physics = true; }

        return aux;
    }

    public static void CleanScene()
    {
        GameObject[] todos = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in todos)
        {
            if (obj.name.Contains("Clone"))
                Destroy(obj);
        }
    }
}
