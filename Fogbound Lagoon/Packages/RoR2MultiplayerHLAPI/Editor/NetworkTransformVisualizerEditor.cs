using UnityEngine.Networking;

namespace UnityEditor
{
    [CustomEditor(typeof(NetworkTransformVisualizer), true)]
    [CanEditMultipleObjects]

    public class NetworkTransformVisualizerEditor : NetworkBehaviourInspector
    {
        internal override bool hideScriptField
        {
            get
            {
                return true;
            }
        }
    }
}
