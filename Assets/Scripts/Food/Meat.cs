

public class Meat : AbstractFood
{
    private void OnDestroy()
    {
        var horn = player.GetComponent<MutationController>().partsList[(int)PartsEnum.Horn] as Horn;

        horn?.AddPoints();
    }
}
