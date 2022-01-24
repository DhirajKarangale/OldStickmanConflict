[System.Serializable]
public class GameData
{
    public float[] playerSpwanPos = new float[2];
    public float[,] weaponsPosition = new float[2, 2];
    public string pickedWeaponName;
    public float currHealth;
    public int coin;
    public byte key;
    public byte palakCount;
    public byte bomb;
}