public struct GameTime
{
    public int seconds;
    public int minuets;

    //
    public override string ToString()
    {
        return $"{minuets}:{seconds}";
    }
}

public struct GameScore
{
    public int score;
    public int kills;
    public int coins;
    public int xp;

    //
    public override string ToString()
    {
        return $"Score:{score}|kills{kills}|coins{coins}|xp{xp}";
    }
}