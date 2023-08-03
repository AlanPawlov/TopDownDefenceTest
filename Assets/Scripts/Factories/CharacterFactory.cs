using Zenject;

public class CharacterFactory : BaseFactory<Character>
{
    public CharacterFactory(DiContainer diContainer) : base(diContainer)
    {
    }
}
