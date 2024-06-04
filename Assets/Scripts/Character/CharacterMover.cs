using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pepecoral.pathfinding.character
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] float walkingSpeed;

        Vector3 _postionToMove;


        private void OnEnable()
        {
            CharacterBehaviour.OnPositionOrdered += MoveToPosition; 
        }
        private void OnDisable()
        {
            CharacterBehaviour.OnPositionOrdered -= MoveToPosition;
        }
        private void MoveToPosition(Vector3 position)
        {   
            //Corrected Position makes sure the Y value is the same height as the character
            Vector3 correctedPosition = new Vector3(position.x, transform.position.y, position.z);
            _postionToMove = correctedPosition;

        }

        private void FixedUpdate()
        {
            if (_postionToMove != null)
            { 
                transform.LookAt(_postionToMove);
                transform.position+= transform.forward*walkingSpeed;
            }
        }
    }
}