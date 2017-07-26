#if ENABLE_UNET
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


namespace UnityEditor
{
    [CustomEditor(typeof(NetworkTransformChild), true)]
    [CanEditMultipleObjects]
    public class NetworkTransformChildEditor : Editor
    {
        private static string[] axisOptions = {"None", "X", "Y (Top-Down 2D)", "Z (Side-on 2D)", "XY (FPS)", "XZ", "YZ", "XYZ (full 3D)"};

        bool m_Initialized = false;
        NetworkTransformChild sync;

        SerializedProperty m_Target;
        SerializedProperty m_MovementThreshold;

        SerializedProperty m_InterpolateRotation;
        SerializedProperty m_InterpolateMovement;
        SerializedProperty m_RotationSyncCompression;

        protected GUIContent m_MovementThresholdLabel;

        protected GUIContent m_InterpolateRotationLabel;
        protected GUIContent m_InterpolateMovementLabel;
        protected GUIContent m_RotationSyncCompressionLabel;

        SerializedProperty m_NetworkSendIntervalProperty;
        GUIContent m_NetworkSendIntervalLabel;

        public void Init()
        {
            if (m_Initialized)
                return;

            m_Initialized = true;
            sync = target as NetworkTransformChild;

            m_Target = serializedObject.FindProperty("m_Target");
            if (sync.GetComponent<NetworkTransform>() == null)
            {
                if (LogFilter.logError) { Debug.LogError("NetworkTransformChild must be on the root object with the NetworkTransform, not on the child node"); }
                m_Target.objectReferenceValue = null;
            }

            m_MovementThreshold = serializedObject.FindProperty("m_MovementThreshold");

            m_InterpolateRotation = serializedObject.FindProperty("m_InterpolateRotation");
            m_InterpolateMovement = serializedObject.FindProperty("m_InterpolateMovement");
            m_RotationSyncCompression = serializedObject.FindProperty("m_RotationSyncCompression");

            m_NetworkSendIntervalProperty = serializedObject.FindProperty("m_SendInterval");
            m_NetworkSendIntervalLabel = new GUIContent("Network Send Rate (seconds)", "Number of network updates per second");
            EditorGUI.indentLevel += 1;
            m_MovementThresholdLabel = new GUIContent("Movement Threshold");

            m_InterpolateRotationLabel = new GUIContent("Interpolate Rotation Factor");
            m_InterpolateMovementLabel = new GUIContent("Interpolate Movement Factor");
            m_RotationSyncCompressionLabel = new GUIContent("Compress Rotation");
            EditorGUI.indentLevel -= 1;
        }

        protected void ShowControls()
        {
            if (m_Target == null)
            {
                m_Initialized = false;
            }
            Init();

            serializedObject.Update();

            int sendRate = 0;
            if (m_NetworkSendIntervalProperty.floatValue != 0)
            {
                sendRate = (int)(1 / m_NetworkSendIntervalProperty.floatValue);
            }
            int newSendRate = EditorGUILayout.IntSlider(m_NetworkSendIntervalLabel, sendRate, 0, 30);
            if (newSendRate != sendRate)
            {
                if (newSendRate == 0)
                {
                    m_NetworkSendIntervalProperty.floatValue = 0;
                }
                else
                {
                    m_NetworkSendIntervalProperty.floatValue = 1.0f / newSendRate;
                }
            }
            if (EditorGUILayout.PropertyField(m_Target))
            {
                if (sync.GetComponent<NetworkTransform>() == null)
                {
                    if (LogFilter.logError) { Debug.LogError("NetworkTransformChild must be on the root object with the NetworkTransform, not on the child node"); }
                    m_Target.objectReferenceValue = null;
                }
            }

            EditorGUILayout.PropertyField(m_MovementThreshold, m_MovementThresholdLabel);
            if (m_MovementThreshold.floatValue < 0)
            {
                m_MovementThreshold.floatValue = 0;
                EditorUtility.SetDirty(sync);
            }
            EditorGUILayout.PropertyField(m_InterpolateMovement, m_InterpolateMovementLabel);

            EditorGUILayout.PropertyField(m_InterpolateRotation, m_InterpolateRotationLabel);

            int newRotation = EditorGUILayout.Popup(
                    "Rotation Axis",
                    (int)sync.syncRotationAxis,
                    axisOptions);
            if ((NetworkTransform.AxisSyncMode)newRotation != sync.syncRotationAxis)
            {
                sync.syncRotationAxis = (NetworkTransform.AxisSyncMode)newRotation;
                EditorUtility.SetDirty(sync);
            }

            EditorGUILayout.PropertyField(m_RotationSyncCompression, m_RotationSyncCompressionLabel);

            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            ShowControls();
        }
    }
}
#endif //ENABLE_UNET
