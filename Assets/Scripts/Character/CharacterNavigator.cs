using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pepe.pathfinding.character
{
    public class CharacterNavigator : MonoBehaviour
    {
        [SerializeField] NavigationMesh _navigatorMesh;
        Queue<Vector3> path = new Queue<Vector3>();
        [SerializeField] float radius;
        Vector3 currentTarget;


        public delegate void _UpdateCharacterMoverPostion(Vector3 postion);
        public static event _UpdateCharacterMoverPostion UpdateCharacterMoverPostion;



        private void OnEnable()
        {
            CharacterBehaviour.UpdateCharacterTargetPosition += Navigate;
        }

        private void OnDisable()
        {
            CharacterBehaviour.UpdateCharacterTargetPosition -= Navigate;
        }

        public void Navigate(Vector3 position)
        {
            Vector3 source = _navigatorMesh.GetClosestVertexToPosition(transform.position);
            Vector3 sink = _navigatorMesh.GetClosestVertexToPosition(position);
            path = new Queue<Vector3>(_navigatorMesh.GetPath(source, sink));
            path.Enqueue(position);
            changeCurrentTarget(path.Dequeue());

        }


        private void Update()
        {
            if (path.Count > 0)
            {
                if (Vector3.Distance(currentTarget, transform.position) < radius)
                    changeCurrentTarget(path.Dequeue());

            }

        }

        private void changeCurrentTarget(Vector3 newTarget)
        {
            currentTarget = newTarget;
            if (UpdateCharacterMoverPostion != null)
                UpdateCharacterMoverPostion(currentTarget);
        }


    }
}