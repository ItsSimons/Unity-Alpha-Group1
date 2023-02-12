using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;

[CreateAssetMenu(fileName = "prebab_brush" , menuName = "Brushes/Prefab brush")]
[CustomGridBrush(false , true , false , "prefab_brush")]
public class prefab_brush : GameObjectBrush
{

}
