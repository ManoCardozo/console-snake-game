namespace SnakeGameConsole.Models
{
    public interface IFood
    {
        public string Representation { get; }

        public void Apply(Game game);
    }
}
