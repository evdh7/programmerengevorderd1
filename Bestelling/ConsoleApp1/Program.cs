using BestellingBL;

DateTime now = DateTime.Now;
Bestelling b = new Bestelling(now);

b.VoegProductToe(new Product(10, "stoel", 100), 5);
b.VoegProductToe(new Product(10, "stoel", 100), 2);

foreach (var x in b.Producten())
{
    Console.WriteLine($"(producten {x.Item1.Naam} aantal {x.Item2}");

}


