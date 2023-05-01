

public class PlayerEvo
{
    private int _evoPoints;
    public int EvoPoints { get => _evoPoints; set => _evoPoints = SetEvoPoints(value); }
    private int _evoPointsMax = 100;
    private const int LvlIncrement = 2;
    
    private readonly MutationController _controller;

    public PlayerEvo(MutationController controller)
    {
        _controller = controller;
    }
    
    private int SetEvoPoints(int points)
    {
        if (points < _evoPointsMax)
        {
            return points;
        }
        
        points = LvlUp(points);
        _controller.Mutation();

        return points;
    }
    
    private int LvlUp(int points)
    {
        points -= _evoPointsMax;
        _evoPointsMax *= LvlIncrement;
        
        return points;
    }
}
