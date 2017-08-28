using UnityEngine;

[CreateAssetMenu(menuName = "FeatureCreep/Level")]
public class Level : ScriptableObject
{

    public int levelNum;
    public GameObject[] emitters;
    public int featureCount;
    public int nextLevelNum;

}
