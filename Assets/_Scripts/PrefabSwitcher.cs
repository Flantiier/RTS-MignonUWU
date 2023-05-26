using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PrefabSwitcher : MonoBehaviour
{
#if UNITY_EDITOR

    #region Variables
    [SerializeField] private Transform prefabParent;
    [SerializeField] private PrefabSwicth[] prefabs;
    #endregion

    #region Methods
    [ContextMenu("Switch all")]
    private void Switch()
    {
        Transform[] temp = new Transform[prefabParent.childCount];
        for (int i = 0; i < temp.Length; i++)
            temp[i] = prefabParent.GetChild(i);

        for (int i = 0; i < temp.Length; i++)
        {
            Transform child = temp[i];
            GameObject obj = child.gameObject;

            for (int j = 0; j < prefabs.Length; j++)
            {
                if (prefabs[j].prefab.name != obj.name)
                    continue;
                else
                {
                    GameObject instance = PrefabUtility.InstantiatePrefab(prefabs[j].switchPrefab, prefabParent).GameObject();
                    instance.transform.position = child.transform.position;
                    instance.transform.rotation = child.transform.rotation;
                }
            }
        }

        for (int i = 0;i < temp.Length; i++)
            DestroyImmediate(temp[i].gameObject);
    }
    #endregion

    [System.Serializable]
    public struct PrefabSwicth
    {
        public GameObject prefab;
        public GameObject switchPrefab;
    }

#endif
}
