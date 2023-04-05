using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public class CareGuide
{
    [FirestoreProperty]
    public string Water { get; set; } // represent as low, moderate, high

    [FirestoreProperty]
    public string Sunlight { get; set; }  // same as water

    [FirestoreProperty]
    public string Hardiness { get; set; }  // same as water

    [FirestoreProperty]
    public string Soil { get; set; } // same as water
}
