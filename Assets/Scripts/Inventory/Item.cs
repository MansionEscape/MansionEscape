using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int level;
    public string objective;
    public Sprite icon;
}
