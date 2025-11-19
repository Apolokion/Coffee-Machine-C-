using System;

public class Kaffeemaschine
{
    private static double maxWasser = 2.5;
    private static double maxBohnen = 2.5;
    
    public double wasser { get; private set; }
    public double bohnen { get; private set; }
    public double gesamtMengeKaffeProduziert { get; private set; }
    
    public Kaffeemaschine()
    {
        wasser = 0;
        bohnen = 0;
        gesamtMengeKaffeProduziert = 0;
    }
    
    public double wasserAuffuellen(double menge)
    {
        double freierPlatz = maxWasser - wasser;
        double tatsaechlicheMenge = Math.Min(menge, freierPlatz);
        wasser += tatsaechlicheMenge;
        return tatsaechlicheMenge;
    }
    
    public double bohnenAuffuellen(double menge)
    {
        double freierPlatz = maxBohnen - bohnen;
        double tatsaechlicheMenge = Math.Min(menge, freierPlatz);
        bohnen += tatsaechlicheMenge;
        return tatsaechlicheMenge;
    }
    
    public bool macheKaffee(double menge, double verhaeltnisWasserBohnen)
    {
        // Berechne benötigte Mengen an Wasser und Bohnen
        double wasserMenge = menge * verhaeltnisWasserBohnen / (verhaeltnisWasserBohnen + 1);
        double bohnenMenge = menge / (verhaeltnisWasserBohnen + 1);
        
        // Prüfe, ob genügend Wasser und Bohnen vorhanden sind
        if (wasser >= wasserMenge && bohnen >= bohnenMenge)
        {
            wasser -= wasserMenge;
            bohnen -= bohnenMenge;
            gesamtMengeKaffeProduziert += menge;
            return true;
        }
        
        return false;
    }
    
    public void StatusAnzeigen()
    {
        Console.WriteLine($"Aktueller Status:");
        Console.WriteLine($"- Wasser: {wasser:F2} kg / {maxWasser} kg");
        Console.WriteLine($"- Bohnen: {bohnen:F2} kg / {maxBohnen} kg");
        Console.WriteLine($"- Gesamt produzierter Kaffee: {gesamtMengeKaffeProduziert:F2} kg");
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Kaffeemaschine maschine = new Kaffeemaschine();
        
        Console.WriteLine("Willkommen bei der Kaffeemaschine!");
        Console.WriteLine("==================================");
        
        while (true)
        {
            maschine.StatusAnzeigen();
            
            Console.WriteLine("Was möchten Sie tun?");
            Console.WriteLine("1 - Wasser auffüllen");
            Console.WriteLine("2 - Bohnen auffüllen");
            Console.WriteLine("3 - Kaffee machen");
            Console.WriteLine("4 - Beenden");
            Console.Write("Ihre Wahl: ");
            
            string eingabe = Console.ReadLine();
            
            switch (eingabe)
            {
                case "1":
                    Console.Write("Wie viel Wasser möchten Sie auffüllen (in kg)? ");
                    if (double.TryParse(Console.ReadLine(), out double wasserMenge))
                    {
                        double aufgefuellt = maschine.wasserAuffuellen(wasserMenge);
                        Console.WriteLine($"Erfolgreich {aufgefuellt:F2} kg Wasser aufgefüllt.");
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Eingabe!");
                    }
                    break;
                    
                case "2":
                    Console.Write("Wie viele Bohnen möchten Sie auffüllen (in kg)? ");
                    if (double.TryParse(Console.ReadLine(), out double bohnenMenge))
                    {
                        double aufgefuellt = maschine.bohnenAuffuellen(bohnenMenge);
                        Console.WriteLine($"Erfolgreich {aufgefuellt:F2} kg Bohnen aufgefüllt.");
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Eingabe!");
                    }
                    break;
                    
                case "3":
                    Console.Write("Wie viel Kaffee möchten Sie machen (in kg)? ");
                    if (!double.TryParse(Console.ReadLine(), out double kaffeeMenge))
                    {
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                    }
                    
                    Console.Write("Verhältnis Wasser zu Bohnen (z.B. 2 für doppelt so viel Wasser): ");
                    if (!double.TryParse(Console.ReadLine(), out double verhaeltnis))
                    {
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                    }
                    
                    bool erfolg = maschine.macheKaffee(kaffeeMenge, verhaeltnis);
                    if (erfolg)
                    {
                        Console.WriteLine($"Kaffee erfolgreich produziert! ({kaffeeMenge:F2} kg)");
                    }
                    else
                    {
                        Console.WriteLine("Nicht genügend Wasser oder Bohnen für die Kaffeezubereitung!");
                    }
                    break;
                    
                case "4":
                    Console.WriteLine("Auf Wiedersehen!");
                    return;
                    
                default:
                    Console.WriteLine("Ungültige Auswahl! Bitte 1-4 eingeben.");
                    break;
            }
            
            Console.WriteLine("\nDrücken Sie eine Taste um fortzufahren...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}