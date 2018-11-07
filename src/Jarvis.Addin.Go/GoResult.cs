using System;
using System.Diagnostics;
using System.Net;
using Jarvis.Core;
using JetBrains.Annotations;

namespace Jarvis.Addin.Go
{
    [UsedImplicitly]
    [DebuggerDisplay("{" + nameof(Title) + ",nq}")]
    internal struct GoResult : IQueryResult, IEquatable<GoResult>
    {
        public Uri Uri { get; }

        public string Title { get; }

        public string Description { get; }

        public float Distance { get; }

        public float Score { get; }

        public QueryResultType Type => QueryResultType.Other;

        public GoResult(Uri uri, string title, string description, float distance, float score)
        {
            Uri = uri;
            Title = title;
            Description = description;
            Distance = distance;
            Score = score;
        }

        public static IQueryResult Create(Query query, string description = null)
        {
            Ensure.NotNull(query, nameof(query));

            var term = query.Argument.Replace(' ', '-');

            description = description ?? term;

            var uri = new Uri($"https://go.paramg.com/{WebUtility.UrlEncode(term)}");

            return new GoResult(uri, description, uri.AbsoluteUri, 100, 100);
        }

        public bool Equals(IQueryResult obj)
        {
            return obj != null && obj.GetType() == GetType()
                && Equals((GoResult)obj);
        }

        public bool Equals(GoResult other)
        {
            return other.Uri?.Equals(Uri) ?? false;
        }

        public override bool Equals(object obj)
        {
            return obj != null && (obj.GetType() == GetType()
                && Equals((GoResult)obj));
        }

        public override int GetHashCode()
        {
            return Uri.GetHashCode();
        }
    }
}