namespace ScreenDrafts.Api.Application.Common.Extensions;
public static class PickExtensions
{
    public static IEnumerable<List<Pick>> BatchPicksByDraftPosition(this IEnumerable<Pick> source)
    {
        var list = source.ToList();
        var result = list
            .GroupBy(x => x.DraftPosition, (_, g) => g.OrderBy(e => e.DraftPosition)
            .First()).ToList();

        if (result.Count  != list.Count)
        {
            while (list.Count > 0)
            {
                list.RemoveAll(p => result.Contains(p));
                yield return result;
                result = list.GroupBy(p => p.DraftPosition, (_, g) => g.OrderBy(e => e.DraftPosition)
                .First()).ToList();
            }
        }
        else
        {
            yield return list;
        }
    }
}
