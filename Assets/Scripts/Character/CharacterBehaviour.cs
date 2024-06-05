using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pepe.pathfinding;

namespace pepe.pathfinding.character
{
    public class CharacterBehaviour : MonoBehaviour
    {
        Vector3 _worldMousePosition;

        public delegate void _UpdateCharacterTargetPosition(Vector3 postion);
        public static event _UpdateCharacterTargetPosition UpdateCharacterTargetPosition;


        [SerializeField] LayerMask floorLayer;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                UpdateTargetPosition();
            }


        }

        private void UpdateTargetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, floorLayer))
            {
                if (hit.collider.CompareTag("Floor"))
                {
                    _worldMousePosition = hit.point;
                    if (UpdateCharacterTargetPosition != null)
                        UpdateCharacterTargetPosition(_worldMousePosition);
                }
            }



        }

        private void OnDrawGizmos()
        {
            if (_worldMousePosition != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(_worldMousePosition, 0.2f);
            }
        }




    }

}
