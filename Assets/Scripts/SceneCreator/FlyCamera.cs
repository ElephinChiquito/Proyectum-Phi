using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour
{
	[SerializeField] Camera cam;
	/* [SerializeField] TutorialPopUp tutorialPopUp; */
	[SerializeField] CharacterController playerController;
	public float movementSpeed = 10f;
	public float lookSpeed = 2f;
	public float boostMultiplier = 2f;
	private float verticalVelocity = 0f;
	public float gravity = 15f; // configurable desde el inspector
	[SerializeField] float jumpHeight = 2f;

	public Stack<Objeto3D> historyZ;

	private float yaw = 0f;
	private float pitch = 0f;

	// --- VARIABLES PARA EL SISTEMA DE AGARRE ---
	private Rigidbody grabbedObject = null;
	public float grabDistance = 3f;
	public float grabStrength = 300f;
	public float maxGrabDistance = 50f;
	private Vector3 holdOffset;


	//Voy a empezar a utilizar cosas distintas a Start, para poder no depender de unity en cuanto a el orden de ejecucion de los programas
	public void StartFlyCamera()
	{
		//mirar al frente
		transform.eulerAngles = new Vector3(pitch, yaw, 0f);


		//Esto son configuraciones para mejorar el rendimiento
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 1;

		cam.allowHDR = false;
		cam.allowMSAA = false;


		/* UnlockAllForCanvas(); */

		/* historyZ = new Stack<Objeto3D>(); */

		/* CycleFreeCamera();
		CycleFreeCamera(); */

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	/* public void LockAllForCanvas()
	{
		Lock();
		canvas.gameObject.SetActive(true);
		canvasAuxGO.SetActive(false);
		if (tutorialCanvas.activeInHierarchy)
		{
			for (int i = 0; i < tutorialCanvas.transform.childCount; i++)
			{
				RectTransform childGOTransform = tutorialCanvas.transform.GetChild(i).GetComponent<RectTransform>();
				childGOTransform.anchoredPosition = new Vector2(72, 16);
			}
			clippyText.SetActive(false);
		}
		else
		{
			clippyText.SetActive(true);
		}
	}

	public void UnlockAllForCanvas()
	{
		Unlock();//237 160
		canvas.gameObject.SetActive(false);
		canvasAuxGO.SetActive(false);
		if (tutorialCanvas.activeInHierarchy)
		{
			for (int i = 0; i < tutorialCanvas.transform.childCount; i++)
			{
				RectTransform childGOTransform = tutorialCanvas.transform.GetChild(i).GetComponent<RectTransform>();
				childGOTransform.anchoredPosition = new Vector2(237, 160);
			}
		}
	}

	public void AdjustTutorialPopUpAuxMenu()
	{
		if (tutorialCanvas.activeInHierarchy)
		{
			for (int i = 0; i < tutorialCanvas.transform.childCount; i++)
			{
				RectTransform childGOTransform = tutorialCanvas.transform.GetChild(i).GetComponent<RectTransform>();
				childGOTransform.anchoredPosition = new Vector2(237, -160);
			}
		}
	}
	public void AdjustTutorialPopUpAuxMenuOut()
	{
		LockAllForCanvas();
	} */

	/* public void LockAllForClickMenu()
	{
		Lock();
		clickMenuCanvas.gameObject.SetActive(true);
		if (tutorialCanvas.activeInHierarchy)
		{
			for (int i = 0; i < tutorialCanvas.transform.childCount; i++)
			{
				RectTransform childGOTransform = tutorialCanvas.transform.GetChild(i).GetComponent<RectTransform>();
				childGOTransform.anchoredPosition = new Vector2(-234.7f, -155.8f); // -234.7 -155.8
			}
		}
	} */
	/* public void UnlockAllForClickMenu()
	{
		Unlock();//237 160
		clickMenuCanvas.gameObject.SetActive(false);
		if (tutorialCanvas.activeInHierarchy)
		{
			for (int i = 0; i < tutorialCanvas.transform.childCount; i++)
			{
				RectTransform childGOTransform = tutorialCanvas.transform.GetChild(i).GetComponent<RectTransform>();
				childGOTransform.anchoredPosition = new Vector2(237, 160);
			}
		}

		//Guardar los cambios a la escena
		GeneradorDeEscena3D.SaveScene();
	} */

	/* public void Lock()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		locked = true;
	}
	public void Unlock()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		locked = false;
	} */


	void Update()
	{
		/* if (!VariablesLoader.Data.tutorialPlayed)
		{
			if (Input.GetKeyDown(KeyCode.M)) tutorialPopUp.EndTutorial();
			else tutorialPopUp.ContinueTutorial();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UnlockAllForCanvas();
			UnlockAllForClickMenu();
		} */

		//ClickMenu key shortcuts
		/* if (clickMenu.gameObject.activeInHierarchy)
		{
			//Movement
			if (clickMenu.holdMovementMode)
			{
				if (Input.GetKey(KeyCode.D)) clickMenu.MoveX(clickMenu.movementSensibility);
				if (Input.GetKey(KeyCode.A)) clickMenu.MoveX(-clickMenu.movementSensibility);
				if (Input.GetKey(KeyCode.E)) clickMenu.MoveY(clickMenu.movementSensibility);
				if (Input.GetKey(KeyCode.Q)) clickMenu.MoveY(-clickMenu.movementSensibility);
				if (Input.GetKey(KeyCode.W)) clickMenu.MoveZ(clickMenu.movementSensibility);
				if (Input.GetKey(KeyCode.S)) clickMenu.MoveZ(-clickMenu.movementSensibility);

				//Scale
				if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus)) clickMenu.Scale(0.15f);
				if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus)) clickMenu.Scale(-0.15f);

				//Rotation
				if (Input.GetKey(KeyCode.UpArrow)) clickMenu.RotateX(10);
				if (Input.GetKey(KeyCode.DownArrow)) clickMenu.RotateX(-10);
				if (Input.GetKey(KeyCode.RightArrow)) clickMenu.RotateY(-10);
				if (Input.GetKey(KeyCode.LeftArrow)) clickMenu.RotateY(10);
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.D)) clickMenu.MoveX(clickMenu.movementSensibility);
				if (Input.GetKeyDown(KeyCode.A)) clickMenu.MoveX(-clickMenu.movementSensibility);
				if (Input.GetKeyDown(KeyCode.E)) clickMenu.MoveY(clickMenu.movementSensibility);
				if (Input.GetKeyDown(KeyCode.Q)) clickMenu.MoveY(-clickMenu.movementSensibility);
				if (Input.GetKeyDown(KeyCode.W)) clickMenu.MoveZ(clickMenu.movementSensibility);
				if (Input.GetKeyDown(KeyCode.S)) clickMenu.MoveZ(-clickMenu.movementSensibility);

				//Scale
				if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus)) clickMenu.Scale(0.15f);
				if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) clickMenu.Scale(-0.15f);

				//Rotation
				if (Input.GetKeyDown(KeyCode.UpArrow)) clickMenu.RotateX(10);
				if (Input.GetKeyDown(KeyCode.DownArrow)) clickMenu.RotateX(-10);
				if (Input.GetKeyDown(KeyCode.RightArrow)) clickMenu.RotateY(-10);
				if (Input.GetKeyDown(KeyCode.LeftArrow)) clickMenu.RotateY(10);
			}


			//Additional
			if (Input.GetKeyDown(KeyCode.Z)) clickMenu.Delete();
		}
		if (locked) return;
 */

		/* if (Input.GetKeyDown(KeyCode.Space)) LockAllForCanvas(); */
		if (Input.GetMouseButtonDown(0)) CheckInteractionWithMouseLeft();
		if (Input.GetMouseButtonDown(1)) TryGrabObject();
		if (Input.GetMouseButton(1)) MoveObject();
		if (Input.GetMouseButtonUp(1)) ReleaseObject();

		/* if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Z)) UndoZ();
		else if (Input.GetKeyDown(KeyCode.Z)) DestroyWithZ();
 */


		/* if (VariablesLoader.Data.freeCamera)
		{
			FreeCameraCode();
		}
		else
		{
			// Movimiento con teclado sin E ni Q, y solo con movimiento en los ejes X / Z. 
			// Que la altura está ya predefinida 1.824083f
			NotFreeCameraMode();
		} */

		NotFreeCameraMode();
	}

	private void TryGrabObject()
	{
		if (grabbedObject != null) return;

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit, maxGrabDistance))
		{
			Rigidbody rb = hit.collider.attachedRigidbody;
			PhysicsVariable[] pvArray = hit.collider.GetComponentsInParent<PhysicsVariable>();
			PhysicsVariable pv = null;
			if(pvArray != null && pvArray.Length > 0){ 
				pv = pvArray[0];
				Debug.Log("pv for grabbed object = " + pv.GetValue()); 
			}
			if(pv != null && !pv.GetValue()){ return; } // No están activadas las fisicas
			if (rb != null && !rb.isKinematic)
			{
				grabbedObject = rb;

				holdOffset = hit.point - rb.position;

				grabbedObject.useGravity = false;
				grabbedObject.linearDamping = 10f; // estabilidad
			}
		}
	}

	public void MoveObject()
	{
		if (grabbedObject == null) return;

		Vector3 targetPos = cam.transform.position + cam.transform.forward * grabDistance - holdOffset;

		Vector3 force = (targetPos - grabbedObject.position) * grabStrength;
		grabbedObject.AddForce(force, ForceMode.Acceleration);
	}

	private void ReleaseObject()
	{
		if (grabbedObject == null) return;

		grabbedObject.useGravity = true;		
		grabbedObject.linearDamping = 0.05f;
		grabbedObject = null;
	}

	/* public void CycleFreeCamera()
	{
		if (VariablesLoader.Data.freeCamera == true)
		{
			VariablesLoader.Data.freeCamera = false;
			// Mantener la altura fija (por ejemplo, 1.824083f)
			Vector3 pos = transform.position;
			pos.y = 1.824083f;
			transform.position = pos;

			playerController.enabled = true;
		}
        else
        {
			VariablesLoader.Data.freeCamera = true;
			playerController.enabled = false;
        }

		VariablesLoader.Save();
	} */
	/* private void FreeCameraCode()
	{
		// Movimiento con teclado
		float moveForward = Input.GetAxis("Vertical");   // W/S o ↑/↓
		float moveRight = Input.GetAxis("Horizontal");   // A/D o ←/→
		float moveUp = 0f;

		if (Input.GetKey(KeyCode.E)) moveUp += 1f;       // E = subir
		if (Input.GetKey(KeyCode.Q)) moveUp -= 1f;       // Q = bajar

		float speed = movementSpeed;
		if (Input.GetKey(KeyCode.LeftShift)) speed *= boostMultiplier;

		Vector3 direction = new Vector3(moveRight, moveUp, moveForward);
		transform.Translate(direction * speed * Time.deltaTime);

		// Movimiento de cámara con ratón (siempre activo)
		yaw += lookSpeed * Input.GetAxis("Mouse X");
		pitch -= lookSpeed * Input.GetAxis("Mouse Y");
		pitch = Mathf.Clamp(pitch, -90f, 90f);

		transform.eulerAngles = new Vector3(pitch, yaw, 0f);
	} */

	private void NotFreeCameraMode()
	{
		// 🔹 Movimiento normal (solo XZ) con colisiones y gravedad
		float moveForward = Input.GetAxis("Vertical");
		float moveRight = Input.GetAxis("Horizontal");

		float speed = movementSpeed / 2;
		if (Input.GetKey(KeyCode.LeftShift)) speed *= boostMultiplier;

		// Calcula dirección según la cámara
		Vector3 forward = transform.forward;
		Vector3 right = transform.right;

		// Elimina movimiento vertical (solo XZ)
		forward.y = 0;
		right.y = 0;
		forward.Normalize();
		right.Normalize();

		Vector3 direction = forward * moveForward + right * moveRight;

		// 🔸 Gravedad manual
		if (playerController.isGrounded)
		{
			verticalVelocity = -0.5f;
			if (Input.GetKeyDown(KeyCode.Space))
			{
				verticalVelocity = Mathf.Sqrt(2f * gravity * jumpHeight);
			}
		}
		else
		{
			verticalVelocity -= gravity * 2 * Time.deltaTime;
		}

		// Combina movimiento horizontal + vertical
		Vector3 velocity = direction * speed;
		velocity.y = verticalVelocity;

		playerController.Move(velocity * Time.deltaTime);

		// Rotación libre del ratón
		yaw += lookSpeed * Input.GetAxis("Mouse X");
		pitch -= lookSpeed * Input.GetAxis("Mouse Y");
		pitch = Mathf.Clamp(pitch, -90f, 90f);
		transform.eulerAngles = new Vector3(pitch, yaw, 0f);
	}

	/* public void DestroyWithZ()
	{
		//Si se quisiese un objeto sin hitbox, hace falta hacer una hitbox, pero en una layer q no afecte al personaje
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000))
		{
			Debug.Log("Destruyendo: " + hit.collider.gameObject.transform.root.name);

			// LA HITBOX COLLIDER DEBE ESTAR EN EL HIJO, COMO SE SUELE HACER
			GameObject interact = hit.collider.gameObject;
			if (interact != null)
			{
				if (interact.CompareTag("Undestructible"))
					Debug.Log("Can not destroy this Undestructible object.");
				else
				{
					GameObject go = interact.transform.root.gameObject;
					SaveZ(go);

					Destroy(interact.transform.root.gameObject);
					Invoke(nameof(SaveScene), 0.01f);

					TutorialPopUp.SetFirstTimeZPushedTrue();
				}
			}
		}
	} */
	/* public void DestroyWithClickMenu(GameObject go)
	{
		Debug.Log("Destruyendo: " + go.transform.root.name);

		SaveZ(go);

		Destroy(go);
		Invoke(nameof(SaveScene), 0.01f);

		TutorialPopUp.SetFirstTimeZPushedTrue();
	} */

	/* public void SaveZ(GameObject go)
	{
		Debug.Log("Añadida al Stack: " + go.name);

		Objeto3D objeto3D = GeneradorDeEscena3D.Transform2Objeto3D(go);
		historyZ.Push(objeto3D);
	} */

	/* public void UndoZ()
	{
		Debug.Log("Deshaciendo destrucción");
		if (historyZ.Count > 0)
		{
			GeneradorDeEscena3D.CrearObjeto(historyZ.Pop());
			TutorialPopUp.SetFirstTimeCtrlZPushedTrue();
		}
		else
			Debug.Log("Stack Vacio.");
	} */

	/// <summary>
	/// Hago esto porque para utilizar Invoke y esperar un frame a que se haya borrado el objeto necesito una funcion local.
	/// </summary>
	public void SaveScene()
	{
		GeneradorDeEscena3D.SaveScene();
	}


	public void CheckInteractionWithMouseLeft()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1000))
		{
			Debug.Log("Has hecho clic en: " + hit.collider.gameObject.name);

			DoorBehaviour door = hit.collider.GetComponentInParent<DoorBehaviour>();
			if (door != null)
			{
				Debug.Log("Cambiando habitacion");
				door.Travel();
				/* TutorialPopUp.SetFirstTimeTravelTrue(); */
			}
			if (hit.collider.gameObject.tag != "Undestructible")
			{
				/* if (VariablesLoader.Data.freeCamera)
                {
					OpenClickMenu(hit.collider.gameObject.transform.root.gameObject);
					clickMenu.ShowNPCDialog(false);
                } */
			}
			// Si el objeto tiene un script "Interactuable", llama a su acción
			NpcDialog interact = hit.collider.GetComponentInParent<NpcDialog>();
			if (interact != null)
			{
				Debug.Log("Voy a hablar");
				interact.Interact();
				/* if (VariablesLoader.Data.freeCamera)
                {
					clickMenu.ShowNPCDialog(true);
					clickMenu.SetNPCDialogBox(interact.textoDialogo);
                } */
			}
		}
	}
	/* 
		[SerializeField] Canvas clickMenuCanvas;
		[SerializeField] ClickMenu clickMenu;

		public void OpenClickMenu(GameObject go)
		{
			LockAllForClickMenu();

			clickMenu.SelectGO(go);
		} */


}
