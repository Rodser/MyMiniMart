using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class Tool
    {
        [MenuItem("Tool/Clear Prefs")]
        public static void ClearPref()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}