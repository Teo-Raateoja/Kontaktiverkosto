using System;
using System.Collections.Generic;

class Tapahtuma
{
    private Dictionary<int, Osallistuja> osallistujat = new Dictionary<int, Osallistuja>();
    private List<int> ilmoittautumisjarjestys = new List<int>();
    private HashSet<int> kirjautuneet = new HashSet<int>();
    private Queue<Osallistuja> puheenvuorot = new Queue<Osallistuja>();
    private const int MaksimiOsallistujat = 50;
    private int seuraavaId = 1;

    public void LisaaOsallistuja()
    {
        if (osallistujat.Count < MaksimiOsallistujat)
        {
            Console.Write("Anna osallistujan nimi: ");
            string nimi = Console.ReadLine();

            Console.Write("Anna osallistujan yhteystiedot: ");
            string yhteystiedot = Console.ReadLine();

            var osallistuja = new Osallistuja(seuraavaId, nimi, yhteystiedot);
            osallistujat[seuraavaId] = osallistuja;
            ilmoittautumisjarjestys.Add(seuraavaId);
            Console.WriteLine($"Osallistuja lisätty onnistuneesti. ID: {seuraavaId}\n");
            seuraavaId++;
        }
        else
        {
            Console.WriteLine("Tapahtuma on täynnä! Uusia osallistujia ei voi lisätä.\n");
        }
    }

    public void PoistaOsallistuja()
    {
        Console.Write("Anna poistettavan osallistujan ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (osallistujat.ContainsKey(id))
            {
                osallistujat.Remove(id);
                ilmoittautumisjarjestys.Remove(id);
                kirjautuneet.Remove(id);
                Console.WriteLine($"Osallistuja ID {id} poistettu onnistuneesti.\n");
            }
            else
            {
                Console.WriteLine("Virhe: Annetulla ID:llä ei löytynyt osallistujaa.\n");
            }
        }
        else
        {
            Console.WriteLine("Virheellinen syöte. Anna kelvollinen ID.\n");
        }
    }

    public void KirjaaOsallistuminen()
    {
        Console.Write("Anna kirjautuvan osallistujan ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (osallistujat.ContainsKey(id))
            {
                if (!kirjautuneet.Contains(id))
                {
                    kirjautuneet.Add(id);
                    Console.WriteLine($"Osallistuja ID {id} kirjattu sisään onnistuneesti.\n");
                }
                else
                {
                    Console.WriteLine("Tämä osallistuja on jo kirjautunut tapahtumaan.\n");
                }
            }
            else
            {
                Console.WriteLine("Virhe: Tämä osallistuja ei ole ilmoittautunut tapahtumaan.\n");
            }
        }
        else
        {
            Console.WriteLine("Virheellinen syöte. Anna kelvollinen ID.\n");
        }
    }

    public void ListaaKirjautuneet()
    {
        if (kirjautuneet.Count > 0)
        {
            Console.WriteLine("Tapahtumaan kirjautuneet osallistujat:");
            foreach (var id in kirjautuneet)
            {
                var osallistuja = osallistujat[id];
                Console.WriteLine($"ID: {osallistuja.Id}, Nimi: {osallistuja.Nimi}, Yhteystiedot: {osallistuja.Yhteystiedot}");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Tapahtumaan ei ole vielä kirjautuneita osallistujia.\n");
        }
    }

    public void LisaaPuheenvuoroon()
    {
        Console.Write("Anna puheenvuoroon ilmoittautuvan osallistujan ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (osallistujat.ContainsKey(id))
            {
                var osallistuja = osallistujat[id];
                puheenvuorot.Enqueue(osallistuja);
                Console.WriteLine($"Osallistuja ID {id} lisätty puheenvuorolistalle.\n");
            }
            else
            {
                Console.WriteLine("Virhe: Tämä osallistuja ei ole ilmoittautunut tapahtumaan.\n");
            }
        }
        else
        {
            Console.WriteLine("Virheellinen syöte. Anna kelvollinen ID.\n");
        }
    }

    public void PoistaPuheenvuorosta()
    {
        if (puheenvuorot.Count > 0)
        {
            var seuraava = puheenvuorot.Dequeue();
            Console.WriteLine($"Osallistuja ID {seuraava.Id}, Nimi: {seuraava.Nimi} on pitänyt puheensa ja poistettu vuorolistalta.\n");
        }
        else
        {
            Console.WriteLine("Puheenvuorolistalla ei ole osallistujia.\n");
        }
    }

    public void NäytaSeuraavaPuheenvuoro()
    {
        if (puheenvuorot.Count > 0)
        {
            var seuraava = puheenvuorot.Peek();
            Console.WriteLine($"Seuraava puheenvuoron pitäjä: ID: {seuraava.Id}, Nimi: {seuraava.Nimi}\n");
        }
        else
        {
            Console.WriteLine("Puheenvuorolistalla ei ole osallistujia.\n");
        }
    }
}

class Osallistuja
{
    public int Id { get; }
    public string Nimi { get; }
    public string Yhteystiedot { get; }

    public Osallistuja(int id, string nimi, string yhteystiedot)
    {
        Id = id;
        Nimi = nimi;
        Yhteystiedot = yhteystiedot;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Tapahtuma tapahtuma = new Tapahtuma();
        Verkosto verkosto = new Verkosto();
        bool jatka = true;


        while (jatka)
        {
            Console.WriteLine("Valitse toiminto:");
            Console.WriteLine("1. Lisää osallistuja");
            Console.WriteLine("2. Poista osallistuja ID:n perusteella");
            Console.WriteLine("3. Kirjaa osallistuminen");
            Console.WriteLine("4. Lisää puheenvuoroon");
            Console.WriteLine("5. Listaa kirjautuneet osallistujat");
            Console.WriteLine("6. Poista osallistuja puheenvuorosta");
            Console.WriteLine("7. Näytä seuraava puheenvuoro");
            Console.WriteLine("8. Lisää yhteys osallistujien välille");
            Console.WriteLine("9. Poista yhteys osallistujien väliltä");
            Console.WriteLine("10. Näytä osallistujan tuntemat henkilöt");
            Console.WriteLine("11. Etsi verkostoitumisreittejä");
            Console.WriteLine("12. Lopeta ohjelma");
            Console.Write("Valinta: ");
            string valinta = Console.ReadLine();

            switch (valinta)
            {
                case "1":
                    tapahtuma.LisaaOsallistuja();
                    break;
                case "2":
                    tapahtuma.PoistaOsallistuja();
                    break;
                case "3":
                    tapahtuma.KirjaaOsallistuminen();
                    break;
                case "4":
                    tapahtuma.LisaaPuheenvuoroon();
                    break;
                case "5":
                    tapahtuma.ListaaKirjautuneet();
                    break;
                case "6":
                    tapahtuma.PoistaPuheenvuorosta();
                    break;
                case "7":
                    tapahtuma.NäytaSeuraavaPuheenvuoro();
                    break;


                case "8":
                    Console.Write("Anna ensimmäisen osallistujan ID: ");
                    int id1 = int.Parse(Console.ReadLine());
                    Console.Write("Anna toisen osallistujan ID: ");
                    int id2 = int.Parse(Console.ReadLine());
                    verkosto.LisääKontakti(id1, id2);
                    break;
                case "9":
                    Console.Write("Anna ensimmäisen osallistujan ID: ");
                    id1 = int.Parse(Console.ReadLine());
                    Console.Write("Anna toisen osallistujan ID: ");
                    id2 = int.Parse(Console.ReadLine());
                    verkosto.PoistaKontakti(id1, id2);
                    break;
                case "10":
                    Console.Write("Anna osallistujan ID: ");
                    int id = int.Parse(Console.ReadLine());
                    verkosto.NäytäTuntemat(id);
                    break;
                case "11":
                    Console.Write("Anna osallistujan ID: ");
                    id = int.Parse(Console.ReadLine());
                    verkosto.EtsiVerkostoitumisreitit(id);
                    break;


                case "12":
                    jatka = false;
                    break;
                default:
                    Console.WriteLine("Virheellinen valinta. Yritä uudelleen.\n");
                    break;
            }
        }
    }
}
