
namespace PokeExe1.Pokemon
{
	public class Squirtle
	{
		public string Name { get; private set; } = null!;
		public string Type { get; } = "Agua";
		public string[] Atacks { get; private set; } = null!;

		public Squirtle(string name, string[] atacks)
		{
			Name = name;
			Atacks = atacks;
		}
		public void Atack()
		{
			Random random = new Random();
			int randomIndex = random.Next(Atacks.Length);
			Console.WriteLine($"{Name} usou {Atacks[randomIndex]}");
		}
	}
}
