using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class ToDo
    {
        // Get und Set erlauben es, den Wert zu lesen und zu schreiben.
        public string Description { get; set; }
        // Get erlaubt es, den Wert nur zu lesen. DateTime ist auch eine Klasse wie diese. Sie enthält eine Zeit (inklusive Datum)
        public DateTime CreatedAt { get; }

        // Hier im Konstruktor wird die Klasse erstellt. Beim erstellen muss man eine Beschreibung mitgeben. Wann das To-do erstellt wurde wird auch gespeichert.
        public ToDo(string description)
        {
            Description = description;

            // DateTime.Now gibt die aktuelle Zeit zurück.
            CreatedAt = DateTime.Now;
        }

    }
}
