using ElectionCalculator;
using Spectre.Console;
using System.Reflection.Metadata.Ecma335;

internal class Program
{
    internal static void Main(string[] args)
    {
        Console.WriteLine("Welkom bij de verkiezings caluclator");
        bool isRunning = true;
        bool isEnteringParties = false;

        while (isRunning)
        {
            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Maak een keuze:[/]")
                .PageSize(10)
                .AddChoices(new[] {
                    "1. D'hondt",
                    "2. Sainte-Laguë",
                    "3. Kiesdeler",
                    "4. Sluiten"
                }));

            if (choice.Contains("1"))
            {
                List<Party> partyList = partyListMaker();
                List<Party> calculatedListMethod = DHondt(partyList);
                writeResult(calculatedListMethod);
            }
            else if (choice.Contains("2"))
            {
                List<Party> partyList = partyListMaker();
                List<Party> calculatedListMethod = SainteLeague(partyList);
                writeResult(calculatedListMethod);
            }
            else if(choice.Contains("3"))
            {
                List<Party> partyList = partyListMaker();
                List<Party> calculatedListMethod = KiesdelerMethod(partyList);
                writeResult(calculatedListMethod);

            }
            else if (choice.Contains("4"))
            {
                isRunning = false;
                AnsiConsole.MarkupLine("[yellow]Bedankt voor het gebruiken van de calculator![/]");
            }
        }


    }
    public static List<Party> partyListMaker()
    {

        bool isWorking = true;
        List<Party> partyList = new List<Party>();

        while (isWorking)
        {
            Console.Clear();
            Console.WriteLine("Voer afkorting partij in");
            string nameParty = Console.ReadLine().ToUpper();
            Console.WriteLine("Voer stemmen partij in");
            int.TryParse(Console.ReadLine(), out int ammountOfVotes);



            partyList.Add(new Party(nameParty, ammountOfVotes));

            Console.WriteLine("Druk op ENTER om door te gaan X om te stoppen");

            if (((Console.ReadLine()).ToUpper()).Contains("X"))
            {
                break;
            }

        }

        return partyList;
    }

    public static List<Party> DHondt(List<Party> calculationList)
    {
        Console.Clear();
        Console.WriteLine("Voer het aantal zetels in waaruit het parliment bestaat: ");
        int.TryParse(Console.ReadLine(), out int seatsOfParliment);

        for (int i = 0; i < seatsOfParliment; i++)
        {
            Party partyThatWon = calculationList.MaxBy(p => p.Quotient);
            partyThatWon.Seats++;
        }

        Console.Clear();
        return calculationList; 
    }

    public static List<Party> SainteLeague(List<Party> calculationList)
    {
        Console.Clear();
        Console.WriteLine("Voer het aantal zetels in waaruit het parliment bestaat: ");
        int.TryParse(Console.ReadLine(), out int seatsOfParliment);

        for (int i = 0; i < seatsOfParliment; i++)
        {
            Party partyThatWon = calculationList.MaxBy(p => p.SaintLeagueQuotient);
            partyThatWon.Seats++; 
        }

        Console.Clear();
        return calculationList; 

    }

    public static List<Party> KiesdelerMethod(List<Party> calculationList)
    {
        Console.Clear();
        Console.WriteLine("Voer het aantal zetels in waaruit het parliment bestaat: ");
        int.TryParse(Console.ReadLine(), out int seatsOfParliment);

        double kiesDeler = (calculationList.Sum(p => p.Votes) / seatsOfParliment); 

        foreach (Party party in calculationList)
        {
            party.Seats = (int)(party.Votes / kiesDeler);
        }

        for (int i = calculationList.Sum(p => p.Seats); i < seatsOfParliment; ++i)
        {
            Party partyThatWon = calculationList.MaxBy(p => p.KiesdelerQuotient);
            partyThatWon.Seats++;
        }


        Console.Clear();
        return calculationList;
    }

    public static void writeResult(List<Party> calculationList)
    {
        var table = new Table();

        table.AddColumn("Partij");
        table.AddColumn("Zetels");

        foreach (Party party in calculationList)
        {
            table.AddRow($"{party.PartyName}", $"{party.Seats}");
        }

        AnsiConsole.Write(table);
    }
}
