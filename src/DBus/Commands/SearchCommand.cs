using System.Threading.Tasks;
using Mercurius.Modrinth;
using Mercurius.Modrinth.Models;
using Tmds.DBus;

namespace Mercurius.DBus.Commands.Desprecated {
    public class SearchCommand : BaseCommand {
        public override string Name { get => "Search"; }
        public override string Description { get => "Gets top 10 results for query from Labrynth."; }
        public override string Format { get => "query<string>"; }
        public override bool TakesArgs { get => true; }
        public override ObjectPath ObjectPath { get => _objectPath; }
        private ObjectPath _objectPath = new ObjectPath("/org/mercurius/command/search");
        public override async Task ExecuteAsync(string[] args) {
            APIClient client = new APIClient();
            SearchModel searchResults;

            searchResults = await client.SearchAsync(string.Join(" ", args));

            Console.WriteLine($"Found {searchResults.total_hits} results, displaying 10:\n");
            Console.WriteLine("{0, -30} {1, -20} {2, 15}", "Project Title", "Latest Minecraft Version", "Downloads");
            foreach (Hit result in searchResults.hits) {
                Console.WriteLine("{0, -30} {1, -20} {2, 15}", result.title, result.latest_version, result.downloads);
            }
        }
    }
}