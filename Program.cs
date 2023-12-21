using System;
using System.Collections.Generic;

public class FootballMatch
{
    public string? HomeTeam { get; set; }
    public string? AwayTeam { get; set; }
    public DateTime? Date { get; set; }
    public string? Location { get; set; }
    public string? Result { get; set; }
}

public class FootballMatchCache
{
    private readonly Dictionary<string, FootballMatch> _matchCache;

    public FootballMatchCache()
    {
        _matchCache = new Dictionary<string, FootballMatch>();
    }

    public void AddMatch(string homeTeam, string awayTeam, DateTime date, string location, string result)
    {
        string key = GenerateKey(homeTeam, awayTeam, date);
        if (!_matchCache.ContainsKey(key))
        {
            var match = new FootballMatch
            {
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                Date = date,
                Location = location,
                Result = result
            };

            _matchCache[key] = match;
            Console.WriteLine($"Jogo adicionado ao cache: {homeTeam} vs {awayTeam} - Data: {date}, Local: {location}, Resultado: {result}");
        }
    }

    public FootballMatch GetMatch(string homeTeam, string awayTeam, DateTime date)
    {
        string key = GenerateKey(homeTeam, awayTeam, date);
        if (_matchCache.TryGetValue(key, out FootballMatch match))
        {
            Console.WriteLine($"Jogo recuperado do cache: {homeTeam} vs {awayTeam} - Data: {date}, Local: {match.Location}, Resultado: {match.Result}");
            return match;
        }

        Console.WriteLine($"Jogo não encontrado no cache para: {homeTeam} vs {awayTeam} - Data: {date}");
        return null;
    }

    private string GenerateKey(string homeTeam, string awayTeam, DateTime date)
    {
        return $"{homeTeam}_{awayTeam}_{date:yyyyMMdd}";
    }
}

class Program
{
    static void Main()
    {
        FootballMatchCache matchCache = new FootballMatchCache();

        // Adicionando jogos ao cache
        matchCache.AddMatch("EquipeA", "EquipeB", new DateTime(2023, 12, 20), "Estádio X", "3-2");
        matchCache.AddMatch("EquipeC", "EquipeD", new DateTime(2023, 12, 21), "Estádio Y", "1-1");

        // Recuperando jogos do cache
        FootballMatch match1 = matchCache.GetMatch("EquipeA", "EquipeB", new DateTime(2023, 12, 20));
        FootballMatch match2 = matchCache.GetMatch("EquipeC", "EquipeD", new DateTime(2023, 12, 21));

        // Recuperando jogos que não estão no cache
        FootballMatch match3 = matchCache.GetMatch("EquipeA", "EquipeC", new DateTime(2023, 12, 20));
    }
}
