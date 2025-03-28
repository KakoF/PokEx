using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeExe2.Pokemon
{
	public abstract class PokemonBase
	{
		public abstract string Type { get; }

		public string Name { get; private set; } = null!;

		public string[] Atacks { get; private set; } = null!;

		public PokemonBase(string name, string[] atacks)
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
