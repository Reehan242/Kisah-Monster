using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private FixedJoystick _joystick;

    [SerializeField] private Button DRight;
    [SerializeField] private Button DRightUp;
    [SerializeField] private Button DRightDown;
    [SerializeField] private Button DLeft;
    [SerializeField] private Button DLeftUp;
    [SerializeField] private Button DLeftDown;
    [SerializeField] private Button DUp;
    [SerializeField] private Button DDown;
    

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _playerSprite;
    [SerializeField] private AudioSource walk_sound ;
    private interaksi Interact;
    private DoorTrigger doorTrigger;
    public GameObject panelDialog;
    public GameObject talk;
    public GameObject canvasJoystick;
    private static string ObjectTag;
    [SerializeField] private float _moveSpeed;
    private Vector3 movement;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        talk.SetActive(false);
        // Menambahkan EventTrigger untuk mendeteksi PointerDown dan PointerUp
        AddEventTrigger(DRight, () => movement.x = 1f, () => movement.x = 0f);
        AddEventTrigger(DRightUp, () => { movement.x = 0.8f; movement.z = 0.8f; }, () => { movement.x = 0f; movement.z = 0f; });
        AddEventTrigger(DRightDown, () => { movement.x = 0.8f; movement.z = -0.8f; }, () => { movement.x = 0f; movement.z = 0f; });
        AddEventTrigger(DLeft, () => movement.x = -1f, () => movement.x = 0f);
        AddEventTrigger(DLeftUp, () => { movement.x = -0.8f; movement.z = 0.8f; }, () => { movement.x = 0f; movement.z = 0f; });
        AddEventTrigger(DLeftDown, () => { movement.x = -0.8f; movement.z = -0.8f; }, () => { movement.x = 0f; movement.z = 0f; });
        AddEventTrigger(DUp, () => movement.z = 1f, () => movement.z = 0f);
        AddEventTrigger(DDown, () => movement.z = -1f, () => movement.z = 0f);
    }
    private bool _isRunning = false;
    private void FixedUpdate()
    {
        // Update velocity berdasarkan input
        Vector3 move = new Vector3(movement.x * _moveSpeed, _rb.velocity.y, movement.z * _moveSpeed);
        _rb.velocity = move;
        // Ubah arah sprite hanya untuk pergerakan horizontal
        if (movement.x < 0) // Gerakan ke kiri
        {
            _playerSprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0) // Gerakan ke kanan
        {
            _playerSprite.transform.localScale = new Vector3(1, 1, 1);
        }
        //Hentikan suara langkah jika tidak bergerak
        if (movement == Vector3.zero && walk_sound != null && walk_sound.isPlaying)
        {
            walk_sound.Stop();
        }
        else if (movement != Vector3.zero && walk_sound != null && !walk_sound.isPlaying)
        {
            walk_sound.Play();
        }
        bool isRunning = movement != Vector3.zero;
        if (_isRunning != isRunning)
        {
            _animator.SetBool("isRunning", isRunning);
            _isRunning = isRunning;
        }
    }
    private void AddEventTrigger(Button button, System.Action onPointerDown, System.Action onPointerUp)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }
        // PointerDown event
        var pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDownEntry.callback.AddListener((e) => onPointerDown());
        trigger.triggers.Add(pointerDownEntry);
        // PointerUp event
        var pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUpEntry.callback.AddListener((e) => onPointerUp());
        trigger.triggers.Add(pointerUpEntry);
    }

    public static void changeTag(string tag)
    {
        ObjectTag = tag;
    }

    public void Interaksi()
    {
        AudioSetup.instance.playSfx("sfx_button");
        panelDialog.SetActive(true);
        talk.SetActive(false);
        canvasJoystick.SetActive(false);
        Interact.run_Interact(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out interaksi interaksi))
        {
            talk.SetActive(true);
            Interact = interaksi;
            Debug.Log("enter coll");
        }
        if (other.TryGetComponent(out DoorTrigger doorTrigger))
        {
            PlayerData playerData = SaveLoadManager.Instance.LoadGame();
            if(playerData.penjagakedai == false)
            {
                panelDialog.SetActive(true);
                talk.SetActive(false);
                canvasJoystick.SetActive(false);
            }
            this.doorTrigger = doorTrigger;
            this.doorTrigger.door_interact();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "npc" || other.gameObject.tag == "item")
        {
            talk.SetActive(false);
            Debug.Log("Exit coll");
        }
    }  
}
