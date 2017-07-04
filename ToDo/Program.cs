using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    internal static class Program
    {
        private static readonly List<ToDo> ToDos = new List<ToDo>();

        // Diese Methode wird als erstes ausgeführt. string[] args sind dabei die Argumente, die beim ausführen mitgegeben werden können. Wir arbeiten nicht mit argumenten.
        public static void Main(string[] args)
        {
            // So genannte Main loop. Das Programm springt nur aus dieser Schlaufe, wenn die Applikation beendet wird.
            while (true)
            {
                ExecuteAction();
            }
        }

        // Diese Methode zeigt das Auswahlmenu an
        private static void ShowMenu()
        {
            // Console.Clear() löscht den Text im Fenster, sodass wir Platz für neuen haben
            Console.Clear();
            Console.WriteLine("Bitte wähle eine Aktion aus: ");
            Console.WriteLine("[0] To-dos anzeigen");
            Console.WriteLine("[1] To-do erfassen");
            Console.WriteLine("[2] To-do löschen");
            Console.WriteLine("[3] To-do bearbeiten");
            Console.WriteLine("[4] Applikation beenden");
        }

        // Diese Methode dient dazu, eine Zahl auszuwählen. Zusätzlich validiert diese die Eingabe.
        private static int InputSelect()
        {
            Console.Write("> ");
            // Console.ReadKey gibt das erste Zeichen zurück, welches auf der Tastatur gedrückt wurde. Der Catch Block wird nur erreicht, wenn ein Fehler auftritt (der Benutzer keine Zahl eingegeben hat)
            try
            {
                // hier lesen wir den getippten Text aus. Die Methode Convert.ToInt32 wirft einen Fehler wenn sich Buchstaben in der Zeichenfolge befinden. Der Fehler wird vom Catch Block abgefangen.
                var input = Convert.ToInt32(Console.ReadLine());

                // Liegt input im validen bereich?
                if (!(input >= 0 && input <= 4))
                {
                    // Hier werfen wir einen Fehler. Somit springt unser programm danach in den Catch Block.
                    throw new Exception("Validation Error");
                }

                return input;
            }
            catch (Exception)
            {
                Console.WriteLine("\r\nBitte gebe eine valide Zahl ein. (Zwischen 0 und 4)");

                // Danach wird die funktion selbst aufgerufen. Dies führt dazu, das der User seine Eingeabe widerholen kann.
                return InputSelect();
            }
        }

        // Dies lässt den User eine Aktion auswählen und führt diese danach aus.
        private static void ExecuteAction()
        {
            while (true)
            {
                ShowMenu();
                int input = InputSelect();

                switch (input)
                {
                    case 0:
                        ShowToDos();
                        break;
                    case 1:
                        CreateToDo();
                        break;
                    case 2:
                        DeleteToDo();
                        break;
                    case 3:
                        EditToDo();
                        break;
                    case 4:
                        EndApplication();
                        break;

                    default:
                        // Diese Linie sollte nie erreicht werden, da wir bereits früher prüfen, ob die Zahl valid ist.
                        Console.WriteLine("Unbekannte Zahl");
                        continue;
                }
                break;
            }
        }

        private static int SelectToDo()
        {
            PrintAllToDos();

            try
            {
                var nummer = Convert.ToInt32(Console.ReadLine());

                if (!(nummer >= 0 && nummer < ToDos.Count))
                {
                    throw new Exception("Validation Error");
                }

                return nummer;
            }
            catch (Exception)
            {
                Console.WriteLine("Ein Fehler ist aufgetreten. Existiert das ToDo das du ausgewählt hast? Bitte versuche es erneut.\r\n");
                return SelectToDo();
            }
        }

        private static void ShowToDos()
        {
            Console.Clear();
            Console.WriteLine("Zeige To-dos an. Anzahl: " + ToDos.Count);

            PrintAllToDos();

            Console.WriteLine("\r\nZurück mit [Enter]");

            // Der User muss Enter klicken, um zurück zum Menu zu kommen.
            Console.ReadLine();
        }

        private static void PrintAllToDos()
        {
            // Für jedes To-do in der Liste eine Zeile ausgeben
            foreach (var todo in ToDos)
            {
                Console.WriteLine("Erstellt am: " + todo.CreatedAt.ToShortDateString() + " " + todo.CreatedAt.ToShortTimeString() + ": " + todo.Description);
            }
        }

        private static void CreateToDo()
        {
            Console.Clear();
            Console.WriteLine("Erstelle To-do.");

            Console.Write("Bitte gebe eine Beschreibung ein\r\n> ");
            var description = Console.ReadLine();

            // Hier initialisieren wir ein neues To-do-Objekt
            var todo = new ToDo(description);

            // Nun fügen wir es zur Liste hinzu
            ToDos.Add(todo);

            Console.WriteLine("Das To-do wurde erstellt.\r\nZurück mit [Enter]");
            Console.ReadLine();
        }

        private static void DeleteToDo()
        {
            Console.Clear();
            Console.WriteLine("Lösche To-do.");

            if (ToDos.Count == 0)
            {
                Console.WriteLine("Es existieren keine ToDos und demnach können keine gelöscht werden.\r\nZurück mit [Enter]");
                Console.ReadLine();
                return;
            }

            int todo = SelectToDo();

            // Hier könnte man den User noch fragen ob er sicher ist, dass er das To-do löschen möchte.


            // To do aus der liste entfernen
            ToDos.RemoveAt(todo);

            Console.WriteLine("Das ToDo wurde erfolgreich gelöscht.\r\nZurück mit [Enter]");
            Console.ReadLine();
        }

        private static void EditToDo()
        {
            Console.Clear();
            Console.WriteLine("Bearbeite To-do.");

            if (ToDos.Count == 0)
            {
                Console.WriteLine("Es existieren keine ToDos und demnach können keine bearbeitet werden.\r\nZurück mit [Enter]");
                Console.ReadLine();
                return;
            }

            var todo = SelectToDo();

            // Das To-do aus der Liste laden.
            var todoObj = ToDos[todo];

            Console.WriteLine("Alte Beschreibung: " + todoObj.Description);
            Console.Write("Neue Beschreibung: ");
            var description = Console.ReadLine();

            todoObj.Description = description;

            Console.WriteLine("Das ToDo wurde erfolgreich bearbeitet.\r\nZurück mit [Enter]");
            Console.ReadLine();
        }

        private static void EndApplication()
        {
            // Wenn du es in einer Datei speichern möchtest kannst du dies hier tun.
            

            // Applikation beenden. 0 ist der Exitcode und heisst soviel wie "Erfolgreich ausgeführt"
            Environment.Exit(0);
        }
    }
}