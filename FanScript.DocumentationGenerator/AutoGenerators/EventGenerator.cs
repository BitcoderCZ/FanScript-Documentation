using FanScript.Compiler;

namespace FanScript.DocumentationGenerator.AutoGenerators
{
    public static class EventGenerator
    {
        public static void Generate(string docSrcPath, bool showSkipped)
        {
            docSrcPath = Path.Combine(docSrcPath, "Events");
            Directory.CreateDirectory(docSrcPath);

            foreach (EventType eventType in Enum.GetValues<EventType>())
            {
                string path = Path.Combine(docSrcPath, eventType.ToString() + ".docsrc");

                if (File.Exists(path))
                {
                    if (showSkipped)
                        Console.WriteLine($"Skipped '{path}', because it already exists.");
                    continue;
                }

                EventTypeInfo info = eventType.GetInfo();

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(stream))
                    generateEvent(info, writer);

                Console.WriteLine($"Generated '{path}'.");
            }
        }

        private static void generateEvent(EventTypeInfo info, TextWriter writer)
        {
            writer.WriteLine($"<arg name=\"name\">{Enum.GetName(info.Type)}</>");
            writer.WriteLine("<template>event</>");
        }
    }
}
