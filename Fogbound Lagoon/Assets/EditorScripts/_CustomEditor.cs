using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FBLStage.Utils;
using System;

namespace FBLStage.Editor
{
    //https://forum.unity.com/threads/customeditor-attribute-doesnt-allow-multiple-instances.1119790/

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class _CustomEditor : CustomEditor
    {
        public _CustomEditor(Type inspectedType) : base(inspectedType) { }
        public _CustomEditor(Type inspectedType, bool editorForChildClasses) : base(inspectedType, editorForChildClasses) { }

    }
}