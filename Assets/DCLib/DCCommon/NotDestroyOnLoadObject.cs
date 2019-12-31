using UnityEngine;

namespace DC
{
    public class NotDestroyOnLoadObject : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}