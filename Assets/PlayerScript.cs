using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    private float health; //unused health from UML
    public PlayerControls pcs;
    private InputAction move, fire, exit;

    Renderer r;
    bool rFlip;

    private void Awake()
    {
        pcs = new PlayerControls();
    }

    private void OnEnable()
    {
        move = pcs.Player.Move;
        move.Enable();

        fire = pcs.Player.Fire;
        fire.Enable();
        fire.performed+= Fire;

        exit = pcs.Player.Exit;
        exit.Enable();
        exit.performed += Exit;
        
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        exit.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        Vector2 myInput = move.ReadValue<Vector2>();
        transform.Translate(new Vector3(myInput.x, 0, myInput.y) * Time.deltaTime * speed);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired");
        if (rFlip)
        {
            r.material.color = Color.blue;
            rFlip = false;
        } else
        {
            r.material.color = Color.yellow;
            rFlip = true;
        }
    }

    private void Exit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}
