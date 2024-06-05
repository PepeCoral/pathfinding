using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pepe.pathfinding.character
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] float walkingSpeed;
        [SerializeField] float radius;

        Vector3 _postionToMove;


        private void OnEnable()
        {
            CharacterNavigator.UpdateCharacterMoverPostion += MoveToPosition;
        }
        private void OnDisable()
        {
            CharacterNavigator.UpdateCharacterMoverPostion -= MoveToPosition;
        }
        private void MoveToPosition(Vector3 position)
        {
            //Corrected Position makes sure the Y value is the same height as the character
            Vector3 correctedPosition = new Vector3(position.x, transform.position.y, position.z);
            _postionToMove = correctedPosition;

        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, _postionToMove) > radius)
            {
                transform.LookAt(_postionToMove);
                transform.Translate(Vector3.forward * walkingSpeed);
            }
        }
    }
}