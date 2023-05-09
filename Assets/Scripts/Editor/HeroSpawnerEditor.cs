using Hero;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(HeroSpawner))]
    public class HeroSpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(HeroSpawner spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(spawner.transform.position, 0.4f);
        }
    }
}