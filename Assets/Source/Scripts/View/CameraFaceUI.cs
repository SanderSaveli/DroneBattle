using UnityEngine;

namespace Sander.DroneBattle
{
    public class CameraFaceUI : MonoBehaviour
    {
        private void Update()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
