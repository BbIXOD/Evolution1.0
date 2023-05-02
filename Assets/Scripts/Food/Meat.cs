

public class Meat : AbstractFood
{
    private void OnDestroy()
    {
        var horn = player.GetComponent<MutationController>().parts[(int)PartsEnum.Horn] as Horn;

        horn?.AddPoints();
    }
}
