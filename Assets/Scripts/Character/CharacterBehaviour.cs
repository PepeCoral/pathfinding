using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace pepecoral.pathfinding.character
{
    public class CharacterBehaviour : MonoBehaviour
    {
        Vector3 _worldMousePosition;

        public delegate void _OnPositionOrdered(Vector3 postion);
        public static event _OnPositionOrdered OnPositionOrdered;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MovePlayerToPosition();
            }
        }

        private void MovePlayerToPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                _worldMousePosition = hit.point;
            }

            OnPositionOrdered(_worldMousePosition);

        }

        private void OnDrawGizmos()
        {
            if (_worldMousePosition!=null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(_worldMousePosition, 0.5f);
            }
        }
    }

}
