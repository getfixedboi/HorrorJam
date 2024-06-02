using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Variables")]
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _gravity;
   
    private Vector3 _velocity;

    [HideInInspector]
    public Vector3 MoveInput;

    [HideInInspector]
    public CharacterController Character;

    private void Awake()
    {
        Character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (PauseMenu.IsPaused || DialogueSystem.IsDialogue)
        {
          
            return;
        }
        float axis = Input.GetAxis("Horizontal");
        float axis2 = Input.GetAxis("Vertical");
        MoveInput = new Vector3(axis, 0f, axis2);
        MoveInput.Normalize();
        Vector3 vector2 = transform.right * axis + transform.forward * axis2;
        vector2 = Vector3.ClampMagnitude(vector2, 1f);
        Character.Move(vector2 * _speed * Time.deltaTime);
        _velocity.y = _velocity.y + _gravity * Time.deltaTime;
        Character.Move(_velocity * Time.deltaTime);
    }
}
