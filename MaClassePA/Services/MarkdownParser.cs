namespace MaClassePA.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Markdig;

    public class MarkdownParser
    {
        private readonly MarkdownPipeline pipeline;

        public MarkdownParser()
        {
            pipeline = new MarkdownPipelineBuilder()
                .UseSoftlineBreakAsHardlineBreak()
                .DisableHtml()
                .Build();
        }

        public string Render(string markdown)
        {
            return Markdown.ToHtml(markdown, this.pipeline);
        }
    }
}
