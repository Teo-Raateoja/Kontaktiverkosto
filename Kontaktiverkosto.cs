class Verkosto
{
    private Dictionary<int, HashSet<int>> verkko = new Dictionary<int, HashSet<int>>();

    public void LisääKontakti(int id1, int id2)
    {
        if (!verkko.ContainsKey(id1))
        {
            verkko[id1] = new HashSet<int>();
        }
        if (!verkko.ContainsKey(id2))
        {
            verkko[id2] = new HashSet<int>();
        }
        if (!verkko[id1].Contains(id2))
        {
            verkko[id1].Add(id2);
            verkko[id2].Add(id1);
            Console.WriteLine($"Kontakti lisätty: {id1} ↔ {id2}");
        }
        else
        {
            Console.WriteLine($"Kontakti {id1} ↔ {id2} on jo olemassa.");
        }
    }

    public void PoistaKontakti(int id1, int id2)
    {
        if (verkko.ContainsKey(id1) && verkko[id1].Contains(id2))
        {
            verkko[id1].Remove(id2);
            verkko[id2].Remove(id1);
            Console.WriteLine($"Kontakti poistettu: {id1} ↔ {id2}");
        }
        else
        {
            Console.WriteLine($"kontaktia {id1} ↔ {id2} ei löytynyt.");
        }
    }

    public void NäytäTuntemat(int id)
    {
        if (verkko.ContainsKey(id))
        {
            var tuntemat = verkko[id];
            if (tuntemat.Count > 0)
            {
                Console.WriteLine($"Osallistuja {id} tuntee: {string.Join(", ", tuntemat)}");
            }
            else
            {
                Console.WriteLine($"Osallistuja {id} ei tunne ketään.");
            }
        }
        else
        {
            Console.WriteLine($"Osallistuja {id} ei ole verkostossa.");
        }
    }

    public void EtsiVerkostoitumisreitit(int id)
    {
        if (verkko.ContainsKey(id))
        {
            var suoratKontaktit = verkko[id];
            var kaverinKaveri = new HashSet<int>();

            foreach (var kontakt in suoratKontaktit)
            {
                if (verkko.ContainsKey(kontakt))
                {
                    kaverinKaveri.UnionWith(verkko[kontakt]);
                }
            }

            kaverinKaveri.ExceptWith(suoratKontaktit);
            kaverinKaveri.Remove(id);

            if (kaverinKaveri.Count > 0)
            {
                Console.WriteLine($"Yhden kontaktin päässä olevat henkilöt osallistujasta {id}: {string.Join(", ", kaverinKaveri)}");
            }
            else
            {
                Console.WriteLine($"Osallistuja {id} ei tunne kenenkään kautta muita.");
            }
        }
        else
        {
            Console.WriteLine($"Osallistuja {id} ei ole verkostossa.");
        }
    }
}
