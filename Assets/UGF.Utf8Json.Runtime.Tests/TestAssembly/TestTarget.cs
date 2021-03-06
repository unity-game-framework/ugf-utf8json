﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Utf8Json.Runtime.Tests.TestAssembly
{
    [Serializable]
    public class TestTarget
    {
        public string Name { get; set; } = "TestTarget";
        public bool BoolValue { get; set; } = true;
        public float FloatValue { get; set; } = 50.5F;
        public int IntValue { get; set; } = 50;
        public Vector2 Vector2 { get; set; } = Vector2.one;
        public Bounds Bounds { get; set; } = new Bounds(Vector3.one, Vector3.one);
        public HideFlags Flags { get; set; } = HideFlags.DontSave;
        public int ReadOnly { get; } = 10;
        public int[] ArrayInt { get; set; }
        public List<int> ListInt { get; set; }
        public TestTarget2[] ArrayTarget { get; set; }
        public List<TestTarget2> ListTarget { get; set; }
        public Keyframe[] ArrayFrames { get; set; }
    }

    [Serializable]
    public class TestTarget2
    {
        public string Name { get; set; } = "TestTarget2";
        public bool BoolValue { get; set; } = true;
        public float FloatValue { get; set; } = 50.5F;
        public int IntValue { get; set; } = 50;
        public Vector2 Vector2 { get; set; } = Vector2.one;
        public Bounds Bounds { get; set; } = new Bounds(Vector3.one, Vector3.one);
        public HideFlags Flags { get; set; } = HideFlags.DontSave;
    }
}
