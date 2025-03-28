using PokeExe2.Pokemon;

namespace PokeExe2.Arena
{
	public static class Arena
	{
		public static void Battle(PokemonBase pokemon1, PokemonBase pokemon2)
		{
			pokemon1.Atack();
			pokemon2.Atack();
		}
	}
}
