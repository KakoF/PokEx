namespace PokeExe1.Pokemon
{
	public class Bulbassauro
	{
		public string Name { get; private set; } = null!;
		public string Type { get; } = "Planta";
		public string[] Atacks { get; private set; } = null!;

		public Bulbassauro(string name, string[] atacks)
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
