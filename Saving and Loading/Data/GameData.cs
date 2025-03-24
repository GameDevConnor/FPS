[System.Serializable]
public class GameData
{
    public int talismansCollected;

    // When we start a new game, the values in this constructor will be the initial values to start with
    public GameData()
    {
        talismansCollected = 0;
    }
}
