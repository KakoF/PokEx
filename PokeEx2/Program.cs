
using PokeExe2.Arena;
using PokeExe2.Pokemon;

var bulbassauro = new Bulbassauro("Bulba", ["Chicote de vinhas", "Raio solar", "Bomba de Semente"]);
var charmander = new Charmander("Charmozin", ["Lança-chamas", "Ataque de Chamas", "Rajada de Chamas", "Scratch", "Growl", "Ember", "Smokescreen", "Dragon Rage"]);
var squirtle = new Squirtle("Tartaruga Pika", ["Bolha", "Investida", "Tackle", "Tail Whip", "Water Gun", "Withdraw", "Bite", "Rapid Spin", "Protect", "Water Pulse"]);
var pikachu = new Squirtle("Pikachu", ["Ataque Rápido", "Raio do Trovão", "Thunderstorm", "Choque do Trovão"]);

Arena.Battle(pikachu, charmander);
