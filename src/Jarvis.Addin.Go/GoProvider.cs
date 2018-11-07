using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Jarvis.Core;
using Jarvis.Core.Diagnostics;
using JetBrains.Annotations;

namespace Jarvis.Addin.Go
{
    [UsedImplicitly]
    internal sealed class GoProvider : QueryProvider<GoResult>
    {
        private readonly IJarvisLog _log;

        private readonly HttpClient _client;

        private readonly ImageSource _linkIcon;

        public override string Command => "go";

        public GoProvider(IJarvisLog log)
        {
            _log = log;
            _client = new HttpClient();
            _linkIcon = new BitmapImage(new Uri("pack://application:,,,/Jarvis.Addin.Go;component/Resources/Link.png"));
        }

        protected override Task<ImageSource> GetIconAsync(GoResult result)
        {
            return Task.FromResult(_linkIcon);
        }

        public override async Task<IEnumerable<IQueryResult>> QueryAsync(Query query)
        {
            return await Task.Run<IEnumerable<IQueryResult>>(() =>
            {
                Ensure.NotNull(query, nameof(query));

                if (string.IsNullOrWhiteSpace(query.Argument))
                {
                    var uri = new Uri("https://paramg.com");

                    return new[] { (IQueryResult)new GoResult(uri, "Open in browser", uri.AbsoluteUri, 0, 0) };
                }

                return new[] { GoResult.Create(query, $"Go for {query.Argument}") };
            });
        }

        protected override Task ExecuteAsync(GoResult result)
        {
            Process.Start(result.Uri.AbsoluteUri);

            return Task.CompletedTask;
        }
    }
}