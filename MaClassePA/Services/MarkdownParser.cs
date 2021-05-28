namespace MaClassePA.Services
{

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
