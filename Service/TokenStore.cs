using System.Collections.Concurrent;

namespace EF_example.Service;

public static class TokenStore
{
    private static readonly ConcurrentDictionary<string, int> _tokens = new();

    public static void Add(string token, int userId) => _tokens[token] = userId;

    public static bool TryGetUserId(string token, out int userId) => _tokens.TryGetValue(token, out userId);

    public static void Remove(string token) => _tokens.TryRemove(token, out _);
}
