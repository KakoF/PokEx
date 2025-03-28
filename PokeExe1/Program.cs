using PokeExe1.Arena;
using PokeExe1.Pokemon;

var bulbassauro = new Bulbassauro("Bulba", ["Chicote de vinhas", "Raio solar", "Bomba de Semente"]);
var charmander = new Charmander("Charmozin", ["Lança-chamas", "Ataque de Chamas", "Rajada de Chamas", "Scratch", "Growl", "Ember", "Smokescreen", "Dragon Rage"]);
var squirtle = new Squirtle("Tartaruga", ["Bolha", "Investida", "Tackle", "Tail Whip", "Water Gun", "Withdraw", "Bite", "Rapid Spin", "Protect", "Water Pulse"]);

Arena.Battle(squirtle, bulbassauro);